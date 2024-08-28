using System.ComponentModel.DataAnnotations;

namespace Core.Models.Domain
{
    public class ShoppingCart
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = [];
        public int? DeliveryMethodId { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
