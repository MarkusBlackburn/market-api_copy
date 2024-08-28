namespace Core.Models.Domain
{
    public class ProductImage : BaseSimpleEntity
    {
        public string Filename { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public Product? Product { get; set; }
    }
}
