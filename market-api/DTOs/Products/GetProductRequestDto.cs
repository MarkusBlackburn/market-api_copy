using market_api.DTOs.Categories;
using market_api.DTOs.Characteristics;
using System.ComponentModel.DataAnnotations;

namespace market_api.DTOs.Products
{
    public class GetProductRequestDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int ProductCode { get; set; }
        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }
        public int ProductDiscount { get; set; }
        public decimal PriceWithDiscount { get; set; }
        public bool IsAvailable { get; set; }
        public int QuantityInStock { get; set; }
        public string ProductUrl { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public List<GetCategoryRequestDto>? Categories { get; set; } = [];
        public List<ProductImageDto>? Images { get; set; } = [];
        public List<GetProductCharacteristicRequestDto>? Characteristics { get; set; } = [];
    }
}
