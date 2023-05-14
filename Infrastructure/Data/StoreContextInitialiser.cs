using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextInitialiser
    {
        private readonly ILogger<StoreContextInitialiser> _logger;
        private readonly StoreContext _dbCon;

        public StoreContextInitialiser(ILogger<StoreContextInitialiser> logger, StoreContext dbCon)
        {
            _dbCon = dbCon;
            _logger = logger;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                _logger.LogInformation("Database Generated.");
                await _dbCon.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }
        public async Task TrySeedAsync()
        {
            if (!_dbCon.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                foreach(var item in brands){
                    _dbCon.ProductBrands.Add(item);
                }
                await _dbCon.SaveChangesAsync();
            }

           if (!_dbCon.ProductTypes.Any())
            {
                var typessData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typessData);
                foreach(var item in types){
                    _dbCon.ProductTypes.Add(item);
                }
                await _dbCon.SaveChangesAsync();
            }

            if (!_dbCon.Products.Any())
            {
                var productssData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productssData);
                foreach(var item in products){
                    _dbCon.Products.Add(item);
                }
                await _dbCon.SaveChangesAsync();
            }
        }
    }
}