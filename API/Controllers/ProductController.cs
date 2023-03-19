
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext context;

        public ProductController(StoreContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task <ActionResult<List<Product>>> GetProduct()
        { //tách functon này ra thành 1 task riêng, như vậy khi thực hiện query nó sẽ thực thi như là 1 task khác nên không phải đợi khi nó đang thực hiện
            var products = await context.Products.ToListAsync();
            return products;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await context.Products.FindAsync(id);
        }
    }
}