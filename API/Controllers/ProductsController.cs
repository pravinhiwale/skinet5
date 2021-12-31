using Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    //we need to inject our store context into products controller
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _repo.GetProductyByIdAsync(id);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
        {
         return  Ok(await _repo.GetProductBrandsAsync()); 
         //used Ok to return because IreadOnlyList is not acceptable while converting the datatype to List
         //its just a dotnet quirk to get around the problem 
        }
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
        {
         return  Ok(await _repo.GetProductTypesAsync()); 
         //used Ok to return because IreadOnlyList is not acceptable while converting the datatype to List
         //its just a dotnet quirk to get around the problem 
        }
    }
}