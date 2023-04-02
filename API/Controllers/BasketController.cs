using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRespository;

        public BasketController(IBasketRepository basketRespository)
        {
            this.basketRespository = basketRespository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById (string id)
        {
            
            var basket = await basketRespository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket (CustomerBasket basket)
        {
            var UpdateBasket = await basketRespository.UpdateBasketAsync(basket);
            return Ok(UpdateBasket); 
        }

        [HttpDelete]
        public async Task  DeleteBasketAsync(string id)
        {
            await  basketRespository.DeleteBasketAsync(id);
        }
    }
}