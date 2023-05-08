using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _dbCon;
    public ProductRepository(StoreContext dbCon)
    {
        _dbCon = dbCon;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
       return await _dbCon.Products.FindAsync(id);
       
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
          return await _dbCon.Products.ToListAsync();
    }
}
