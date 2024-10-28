using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Advania.ProductApi.Functions;

[assembly: FunctionsStartup(typeof(Advania.ProductApi.Startup))]

namespace Advania.ProductApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IProductService, ProductService>();
        }
    }
}