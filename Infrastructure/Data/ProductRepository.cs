using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class ProductRepository : IProductRepository
  {
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context)
    {
      _context = context;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
      if (_context.ProductBrands == null) throw new ArgumentNullException(nameof(_context.ProductBrands));
      return await _context.ProductBrands.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int? id)
    {
      if (_context.Products == null) throw new ArgumentNullException(nameof(_context.Products));
      return await _context.Products
      .Include(p => p.ProductType)
      .Include(p => p.ProductBrand)
      .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
      if (_context.Products == null) throw new ArgumentNullException(nameof(_context.Products));
      return await _context.Products
      .Include(p => p.ProductType)
      .Include(p => p.ProductBrand)
      .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
      if (_context.ProductTypes == null) throw new ArgumentNullException(nameof(_context.ProductTypes));
      return await _context.ProductTypes.ToListAsync();
    }
  }
}