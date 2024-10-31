using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Kgm.ExampleNetEight.FunctionApp.Functions
{
    public class ChangeDetected
    {
        private readonly ILogger<ChangeDetected> logger;

        public ChangeDetected(ILogger<ChangeDetected> logger)
        {
            this.logger = logger;
        }

        [Function(nameof(ChangeDetected))]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req, [FromQuery] string validationToken)
        {
            this.logger.LogInformation("C# HTTP trigger function processed a request.");
            if (!string.IsNullOrEmpty(validationToken))
            {
                return this.ProcessValidationTokenEchoAsync(validationToken);
            }
            throw new ArgumentNullException(validationToken);
        }

        internal virtual IActionResult ProcessValidationTokenEchoAsync(string validationToken)
        {
            return new OkObjectResult(validationToken);
        }
    }
}
