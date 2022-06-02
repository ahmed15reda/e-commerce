using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
      _repository = repository;
    }


    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
      var products = await _repository.GetProductsAsync();
      return Ok(products);
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Product?>> GetProduct(int id)
    {
      return await _repository.GetProductByIdAsync(id);
    }
    [HttpGet]
    [Route("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
    {
      return Ok(await _repository.GetProductBrandsAsync());
    }
    [HttpGet]
    [Route("types")]
    public async Task<ActionResult<List<ProductType>>> GetProductTypes()
    {
      return Ok(await _repository.GetProductTypesAsync());
    }
  }
}