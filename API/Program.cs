using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddScoped<IProductRepository,ProductRepository>();
      builder.Services.AddControllers();
      IConfiguration Configuration = builder.Configuration;
      builder.Services.AddDbContext<StoreContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var app = builder.Build();
      using (var scope = app.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
          var context = services.GetRequiredService<StoreContext>();
          await context.Database.MigrateAsync();
          await StoreContextSeed.SeedAsync(context, loggerFactory);
        }
        catch (Exception ex)
        {
          var logger = loggerFactory.CreateLogger<Program>();
          logger.LogError(ex, "An error occurred seeding the DB.");
        }

      }

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }


  }
}
