using System.ComponentModel.DataAnnotations;

namespace market_api.DTOs.Products
{
    public class UpdateProductRequestDto
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Code must be from 0 to n")]
        public int ProductCode { get; set; }
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal ProductPrice { get; set; }
        [Range(0, 99, ErrorMessage = "Discount must be from 0% to 99%")]
        public int ProductDiscount { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Q-ty must be from 0 to n")]
        public int QuantityInStock { get; set; }
        public string ProductUrl { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public List<int> Categories { get; set; } = [];
        public List<int> Images { get; set; } = [];
        public List<int> Characteristics { get; set; } = [];
    }
}
