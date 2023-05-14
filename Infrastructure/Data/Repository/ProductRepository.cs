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

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _dbCon.ProductBrands.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _dbCon.Products
        .Include(b => b.ProductBrand)
        .Include(b => b.ProductType)
        .FirstOrDefaultAsync(b=>b.Id==id);

    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _dbCon.Products
        .Include(b => b.ProductBrand)
        .Include(b => b.ProductType)
        .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _dbCon.ProductTypes.ToListAsync();
    }
}
