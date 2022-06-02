using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
  public class StoreContextSeed
  {
    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
      try
      {
        if (context.ProductBrands != null && !context.ProductBrands.Any())
        {
          var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
          var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

          for(int i = 0; i < brands?.Count; i++){
              context.ProductBrands.Add(brands[i]);
          }
          await context.SaveChangesAsync();
        }
        if (context.ProductTypes != null && !context.ProductTypes.Any())
        {
          var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
          var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

          for (int i = 0; i < types?.Count; i++)
          {
              context.ProductTypes.Add(types[i]);
          }
          await context.SaveChangesAsync();
        }
        if (context.Products != null && !context.Products.Any())
        {
          var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
          var products = JsonSerializer.Deserialize<List<Product>>(productsData);

          for (int i = 0; i < products?.Count; i++)
          {
              context.Products.Add(products[i]);
          }
          await context.SaveChangesAsync();
        }

      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<StoreContextSeed>();
        logger.LogError(ex.Message);
      }
    }
  }
}