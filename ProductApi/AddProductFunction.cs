using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Advania.ProductApi.Models;
using Advania.ProductApi.Functions;

namespace Advania.ProductApi.Functions
{
    public class AddProductFunction
    {
        private readonly IProductService _productService;

        // Use IProductService interface in the constructor
        public AddProductFunction(IProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("AddProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Processing AddProduct request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Product product;

            try
            {
                product = JsonConvert.DeserializeObject<Product>(requestBody);
            }
            catch (JsonException ex)
            {
                log.LogError(ex, "Invalid JSON in request body.");
                return new BadRequestObjectResult(new { error = "Invalid JSON in request body." });
            }

            if (product == null)
            {
                return new BadRequestObjectResult(new { error = "Product data is required." });
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return new BadRequestObjectResult(new { error = "Product name is required." });
            }

            if (product.Price <= 0)
            {
                return new BadRequestObjectResult(new { error = "Product price must be greater than zero." });
            }

            // Generate a new Id if not provided
            if (string.IsNullOrEmpty(product.Id))
            {
                product.Id = Guid.NewGuid().ToString();
            }

            try
            {
                // Check if product already exists
                var existingProduct = await _productService.GetProductByIdAsync(product.Id);
                if (existingProduct != null)
                {
                    return new ConflictObjectResult(new { error = "A product with the same ID already exists." });
                }

                // Add the product to storage
                await _productService.AddProductAsync(product);
            }
            catch (Azure.RequestFailedException ex)
            {
                log.LogError(ex, "Error occurred while adding the product.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            // Create the response
            string uri = $"{req.Scheme}://{req.Host}{req.Path}/{product.Id}";
            log.LogInformation("Inserted productId: " + product.Id);

            return new CreatedResult(uri, product);
        }
    }
}
