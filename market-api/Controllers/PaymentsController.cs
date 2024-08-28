using Core.Interfaces;
using Core.Models.Domain;
using market_api.DTOs.Deliveries;
using Microsoft.AspNetCore.Mvc;

namespace market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IPaymentService paymentService) : ControllerBase
    {
        private readonly IPaymentService _paymentService = paymentService;

        [HttpPost("cartId")]
        public async Task<ActionResult<BaseServiceResponse<ShoppingCart>>> CreateOrUpdatePaymentIntent(string cartId)
        {
            var response = new BaseServiceResponse<ShoppingCart>();
            var cart = await _paymentService.CreateOrUpdatePaymentIntent(cartId);

            if (cart is null)
            {
                response.IsSuccess = false;
                response.Message = $"Cart with ID {cartId} not found or doesn't exist";

                return BadRequest(response);
            }

            response.Data = cart;

            return Ok(response);
        }
    }
}
