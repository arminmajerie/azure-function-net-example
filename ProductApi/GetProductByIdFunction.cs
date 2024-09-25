using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure;
using Advania.ProductApi.Models;

namespace Advania.ProductApi.Functions
{
    public class GetProductByIdFunction
    {
        private readonly ProductService _productService;

        public GetProductByIdFunction(ProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("GetProductById")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products/{id}")] HttpRequest req,
            string id,
            ILogger log)
        {
            log.LogInformation($"Processing GetProductById request for Id: {id}");

            if (string.IsNullOrWhiteSpace(id))
            {
                return new BadRequestObjectResult(new { error = "Product ID is required." });
            }

            // Validate ID format (assuming GUIDs)
            if (!Guid.TryParse(id, out _))
            {
                return new BadRequestObjectResult(new { error = "Invalid product ID format." });
            }

            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                if (product == null)
                {
                    return new NotFoundObjectResult(new { error = "Product not found." });
                }

                return new OkObjectResult(product);
            }
            catch (RequestFailedException ex)
            {
                log.LogError(ex, "Error occurred while retrieving the product.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
