using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace controlledFunctionapp
{
    public class AddSmileyFunction
    {
        private readonly ILogger<AddSmileyFunction> _logger;

        public AddSmileyFunction(ILogger<AddSmileyFunction> logger)
        {
            _logger = logger;
        }

        [Function("AddSmileyFunction")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {

            string name = req.Query["name"];

            if (string.IsNullOrEmpty(name))
            {
                using var reader = new StreamReader(req.Body);
                var requestBody = await reader.ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                name = data?.name;
            }

            var responseMessage = !string.IsNullOrEmpty(name)
    ? $"Hello, {name}. This HTTP triggered function executed successfully."
    : "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.";

            return new OkObjectResult(responseMessage);
        }
    }
}
