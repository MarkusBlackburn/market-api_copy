using System.ComponentModel.DataAnnotations;

namespace Core.Models.Domain
{
    public class Category : BaseSimpleEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string UrlHandle { get; set; } = string.Empty;
        [Required]
        public bool IsSubCategory { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
