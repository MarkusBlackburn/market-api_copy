using System.ComponentModel.DataAnnotations;

namespace Core.Models.Domain;

public class Product : BaseSimpleEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Range(0, int.MaxValue, ErrorMessage = "Code must be from 0 to n")]
    public int InternalCode { get; set; }
    [DataType(DataType.Currency)]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
    [Range(0, 99, ErrorMessage = "Discount must be from 0% to 99%")]
    public int Discount { get; set; }
    public decimal PriceWithDiscount { get; set; }
    [Required]
    public bool IsAvailable { get; set; } = true;
    [Range(0, int.MaxValue, ErrorMessage = "Q-ty must be from 0 to n")]
    public int QuantityInStock { get; set; }
    public string Description { get; set; } = string.Empty;
    public string UrlHandle { get; set; } = string.Empty;
    public ICollection<Category>? Categories { get; set; }
    public ICollection<ProductImage>? Images { get; set; }
    public ICollection<ProductCharacteristic>? Characteristics { get; set; }
}
