using System.ComponentModel.DataAnnotations;

namespace market_api.DTOs.Categories
{
    public class CreateOrUpdateCategoryRequestDto
    {
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public string Url { get; set; } = string.Empty;
        [Required]
        public bool IsSubCategory { get; set; } = false;
    }
}
