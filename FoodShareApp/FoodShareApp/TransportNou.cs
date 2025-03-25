using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System;

namespace FoodShareApp
{
    public class TransportNou
    {
        private const string CosmosConnection = "AccountEndpoint=https://muradbgroup.documents.azure.com:443/;AccountKey=NeQMRHfcyMsPdZupqf5ROXocGeP5oGniQcoVpv8YCEjqDomI8qz9rjhPhpN8Krgk3A7N1bur3hQwACDbCEpYzw==;";
        private const string dbName = "DHL";
        private const string transportContainer = "Transporturi";
        private readonly ILogger _logger;

        public TransportNou(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TransportNou>();
        }

        [Function("TransportNou")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            //try
            //{
            //    _logger.LogInformation("C# HTTP trigger function processed a request.");
            //    CosmosClient client = new CosmosClient(CosmosConnection);
            //    var DHLDb = client.GetDatabase(dbName);
            //    var toateTransporturile = DHLDb.GetContainer(transportContainer);
            //    TransportModel transport = new TransportModel(true);
            //    toateTransporturile.CreateItemAsync(transport);

            //    var response = req.CreateResponse(HttpStatusCode.OK);
            //    response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            //    response.WriteString("Welcome to Azure Functions!");

            //    return response;
            //}
            //catch (Exception ex)
            //{
            //    return new BadRequestObjectResult(ex.Message);
            //};
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                CosmosClient client = new CosmosClient(CosmosConnection);
                var sameDayDb = client.GetDatabase(dbName);
                var toateTransporturile = sameDayDb.GetContainer(transportContainer);

                string requestBody = new StreamReader(req.Body).ReadToEnd();

                TransportModel transport = new TransportModel(true);
                toateTransporturile.CreateItemAsync(transport);
                return new OkObjectResult("Welcome to Azure Functions!");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
