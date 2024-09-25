using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Advania.ProductApi.Models;

namespace Advania.ProductApi.Functions
{
    public class GetProductsFunction
    {
        private readonly ProductService _productService;

        public GetProductsFunction(ProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("GetProducts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Processing GetProducts request.");

            var products = await _productService.GetProductsAsync();

            return new OkObjectResult(products);
        }
    }
}
