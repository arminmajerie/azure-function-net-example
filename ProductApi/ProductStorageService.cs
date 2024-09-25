using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Azure.Data.Tables;
using Azure;

using Advania.ProductApi.Models;


namespace Advania.ProductApi.Functions
{
    public class ProductService
    {
        private readonly TableClient tableClient;

        public ProductService()
        {
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            tableClient = new TableClient(connectionString, "Products");
            tableClient.CreateIfNotExists();
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            if (string.IsNullOrEmpty(product.Id)){product.Id = Guid.NewGuid().ToString();}
            var entity = new TableEntity("Product", product.Id)
            {
                { "Id", product.Id},
                { "Name", product.Name },
                { "Description", product.Description },
                { "Price", product.Price }
            };

            await tableClient.AddEntityAsync(entity);
            return product;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = new List<Product>();

            await foreach (var entity in tableClient.QueryAsync<TableEntity>())
            {
                var product = new Product
                {
                    Id = entity.RowKey,
                    Name = entity.GetString("Name"),
                    Description = entity.GetString("Description"),
                    Price = (decimal)entity.GetDouble("Price").GetValueOrDefault(),

                };
                products.Add(product);
            }

            return products;
        }
    
        public async Task<Product> GetProductByIdAsync(string id)
        {
            try
            {
                var response = await tableClient.GetEntityAsync<TableEntity>("Product", id);
                var entity = response.Value;

                var product = new Product
                {
                    Id = entity.RowKey,
                    Name = entity.GetString("Name"),
                    Description = entity.GetString("Description"),
                    Price = (decimal)entity.GetDouble("Price").GetValueOrDefault(),
                };
                return product;
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                // Entity not found
                return null;
            }
            catch (Exception)
            {
                // Re-throw other exceptions
                throw;
            }
        }

    
    }
}
