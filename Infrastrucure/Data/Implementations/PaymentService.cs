using Core.Interfaces;
using Core.Models.Domain;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Data.Implementations
{
    public class PaymentService(IConfiguration config, ICartService cartService, IGenericRepository<Core.Models.Domain.Product> products, IGenericRepository<DeliveryMethod> deliveries) : IPaymentService
    {
        private readonly IConfiguration _config = config;
        private readonly ICartService _cartService = cartService;
        private readonly IGenericRepository<Core.Models.Domain.Product> _products = products;
        private readonly IGenericRepository<DeliveryMethod> _deliveries = deliveries;

        public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
        {
            StripeConfiguration.ApiKey = _config["StripeSetting:SecretKey"];

            var cart = await _cartService.GetShoppingCartAsync(cartId);

            if (cart is null) return null;

            var shippingPrice = 0m;

            if (cart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _deliveries.GetByIdAsync((int)cart.DeliveryMethodId.Value);

                if (deliveryMethod is null) return null;

                shippingPrice = deliveryMethod.Price;
            }

            foreach (var item in cart.Items)
            {
                var productItem = await _products.GetByIdAsync(item.ProductId);

                if (productItem is null) return null;
                if (item.Price != productItem.Price) item.Price = productItem.Price;
            }

            var service = new PaymentIntentService();
            PaymentIntent? intent = null;

            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };

                intent = await service.CreateAsync(options);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }

            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100
                };

                intent = await service.UpdateAsync(cart.PaymentIntentId, options);
            }

            await _cartService.SetShoppingCartAsync(cart);

            return cart;
        }
    }
}
