
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repo;

        //private readonly StoreContext context;

        public ProductController(IProductRepository repo)
        {
            this.repo = repo;
            //this.context = context;

        }
        [HttpGet]
        public async Task <ActionResult<List<Product>>> GetProduct()
        { //tách functon này ra thành 1 task riêng, như vậy khi thực hiện query nó sẽ thực thi như là 1 task khác nên không phải đợi khi nó đang thực hiện
            var products = await repo.GetProductAsync();
            return Ok(products);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await repo.GetProductByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await repo.GetProductBrandAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await repo.GetProductTypeAsync());
        }
    }
}