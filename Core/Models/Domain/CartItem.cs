using System.ComponentModel.DataAnnotations;

namespace Core.Models.Domain
{
    public class CartItem
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; } = string.Empty;
    }
}
