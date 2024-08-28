using System.ComponentModel.DataAnnotations;

namespace Core.Models.Domain
{
    public class ProductCharacteristic : BaseSimpleEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public Product? Product { get; set; }
    }
}
