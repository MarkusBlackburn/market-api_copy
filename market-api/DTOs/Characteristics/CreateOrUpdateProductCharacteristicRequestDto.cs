using System.ComponentModel.DataAnnotations;

namespace market_api.DTOs.Characteristics
{
    public class CreateOrUpdateProductCharacteristicRequestDto
    {
        [Required]
        public string ProductCharacteristicName { get; set; } = string.Empty;
        [Required]
        public string ProductCharacteristicDescription { get; set; } = string.Empty;
        public int ProductId { get; set; }
    }
}
