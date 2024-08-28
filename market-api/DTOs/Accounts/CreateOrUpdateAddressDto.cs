using System.ComponentModel.DataAnnotations;

namespace market_api.DTOs.Accounts
{
    public class CreateOrUpdateAddressDto
    {
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
