using System.Collections.Generic;
using System.Threading.Tasks;
using Advania.ProductApi.Models;

namespace Advania.ProductApi.Functions
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(Product product);
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
    }
}
