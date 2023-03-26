
using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;
        private readonly IMapper mapper;


        //private readonly StoreContext context;

        public ProductController(IGenericRepository<Product> productRepo, 
        IGenericRepository<ProductBrand> productBrandRepo, 
        IGenericRepository<ProductType> productTypeRepo,
        IMapper mapper)
        {
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task <ActionResult<Pagination<ProductToReturnDto>>> GetProduct([FromQuery]ProductSpecParams productParams)
        { //tách functon này ra thành 1 task riêng, như vậy khi thực hiện query nó sẽ thực thi như là 1 task khác nên không phải đợi khi nó đang thực hiện
            
            var specification= new ProductsWithTypesAndBrandsSpecification(productParams);
            var products = await productRepo.ListAsync(specification);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await productRepo.CountAsync(countSpec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>> (products);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems,data));
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof (ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);
            var product= await productRepo.GetEntityWithSpec(specification);
            if(product == null) return NotFound(new ApiResponse(404));
            return mapper.Map<Product,ProductToReturnDto> (product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await productBrandRepo.ListAllSync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await productTypeRepo.ListAllSync());
        }
    }
}