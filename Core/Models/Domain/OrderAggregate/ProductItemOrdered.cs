using System.ComponentModel.DataAnnotations;

namespace Core.Models.Domain.OrderAggregate
{
    public class ProductItemOrdered
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string PictureUrl { get; set; } = string.Empty;
    }
}
