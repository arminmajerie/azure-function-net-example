using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Advania.ProductApi.Functions
{
    public class GetProductsFunction
    {
        private readonly IProductService _productService;

        public GetProductsFunction(IProductService productService)
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
