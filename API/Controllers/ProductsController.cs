using Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    //we need to inject our store context into products controller
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //private readonly IProductRepository _repo;
        //version 1 implementation   
        // public ProductsController(IProductRepository repo)
        // {
        //     _repo = repo;
        // }

        //version 2 implementation using generic Repository
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _prodocutTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> prodocutTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _prodocutTypeRepo = prodocutTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productRepo = productRepo;

        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            //var products = await _productRepo.ListAllAsync();

            //version with spec
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);
            
                        return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            //return await _productRepo.GetByIdAsync(id);

            //version 2 with spec
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product,ProductToReturnDto>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
            //used Ok to return because IreadOnlyList is not acceptable while converting the datatype to List
            //its just a dotnet quirk to get around the problem 
        }
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
        {
            return Ok(await _prodocutTypeRepo.ListAllAsync());
            //used Ok to return because IreadOnlyList is not acceptable while converting the datatype to List
            //its just a dotnet quirk to get around the problem 
        }
    }
}