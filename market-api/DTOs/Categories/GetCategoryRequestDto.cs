using market_api.DTOs.Products;

namespace market_api.DTOs.Categories
{
    public class GetCategoryRequestDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsSubCategory { get; set; }
        public List<GetProductRequestDto>? Products { get; set; } = [];
    }
}
