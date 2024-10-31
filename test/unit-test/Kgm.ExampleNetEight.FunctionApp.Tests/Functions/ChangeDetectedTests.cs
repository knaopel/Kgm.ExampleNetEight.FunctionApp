using AutoFixture.Xunit2;
using Kgm.ExampleNetEight.FunctionApp.Functions;
using Kgm.ExampleNetEight.FunctionApp.Tests.Fakes.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using NSubstitute;
using Shouldly;
using System.Reflection;

namespace Kgm.ExampleNetEight.FunctionApp.Tests.Functions
{
    public class ChangeDetectedTests
    {
        private readonly FakeLogger<ChangeDetected> logger = Substitute.For<FakeLogger<ChangeDetected>>();

        [Fact]
        public void Run_FunctionAttribute_DecoratesTheMethod()
        {
            // Arrange
            var sut = this.CreateSut();
            var methodInfo = sut.GetType().GetMethod(nameof(sut.Run))!;
            var expectedType = typeof(FunctionAttribute);

            // Act
            var actual = methodInfo.GetCustomAttributes(expectedType, false);

            // Assert
            actual.Length.ShouldBe(1);
        }

        [Fact]
        public void Run_FunctionAttribute_HasCorrectValue()
        {
            // Arrange
            var sut = this.CreateSut();
            var methodInfo = sut.GetType().GetMethod(nameof(sut.Run))!;
            var expectedType = typeof(FunctionAttribute);

            // Act
            var actual = (FunctionAttribute)methodInfo.GetCustomAttributes(expectedType, false)[0];

            // Assert
            actual.Name.ShouldBe("ChangeDetected");
        }

        [Theory]
        [AutoData]
        public void Run_HasValidationToken_ProcessesValidationTokenEcho(string validationToken)
        {
            // Arrange
            var sut = this.CreateSut();
            //var functionContext = Substitute.For<FunctionContext>;
            var httpRequest = Substitute.For<HttpRequest>();

            // Act
            sut.Run(httpRequest, validationToken);

            // Assert
            sut.ReceivedWithAnyArgs(1).ProcessValidationTokenEchoAsync(default!);
            sut.Received(1).ProcessValidationTokenEchoAsync(validationToken);
        }

        private ChangeDetected CreateSut() {
            var sut = Substitute.ForPartsOf<ChangeDetected>(this.logger);

            sut.WhenForAnyArgs(o => o.ProcessValidationTokenEchoAsync(default!)).DoNotCallBase();

            return sut;
        }
    }
}