using System.ComponentModel.DataAnnotations;

namespace Core.Models.Domain.OrderAggregate
{
    public class ShippingAddress
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Line1 { get; set; } = string.Empty;
        public string? Line2 { get; set; }
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Disctrict { get; set; } = string.Empty;
        [Required]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
    }
}
