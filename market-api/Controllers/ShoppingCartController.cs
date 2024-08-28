using Core.Interfaces;
using Core.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController(ICartService cartService) : BaseAPIController
    {
        private readonly ICartService _cartService = cartService;

        [HttpGet]
        public async Task<ActionResult<BaseServiceResponse<ShoppingCart>>> GetShoppingCartById(string id)
        {
            var response = new BaseServiceResponse<ShoppingCart>();
            var cart = await _cartService.GetShoppingCartAsync(id);

            if (cart is null)
            {
                response.Data = new ShoppingCart { Id = id };
                response.Message = "New shopping cart has been created";

                return Ok(response);
            }

            response.Data = cart;

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseServiceResponse<ShoppingCart>>> UpdateShoppingCartById(ShoppingCart cart)
        {
            var response = new BaseServiceResponse<ShoppingCart>();
            var updatedCart = await _cartService.SetShoppingCartAsync(cart);

            if (updatedCart is null)
            {
                response.IsSuccess = false;
                response.Message = "Shopping cart doesn't exist!";

                return BadRequest(response);
            }

            response.Data = updatedCart;

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<BaseServiceResponse<ShoppingCart>>> DeleteShoppingCartById(string id)
        {
            var response = new BaseServiceResponse<ShoppingCart>();

            try
            {
                await _cartService.DeleteShoppingCartAsync(id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Shopping cart with ID {id} not found or can't be deleted" + ex.Message;

                return BadRequest(response);
            }

            response.Message = $"Shopping cart with ID {id} has been removed";

            return Ok(response);
        }
    }
}
