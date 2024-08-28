namespace market_api.DTOs
{
    public class ProductImageDto
    {
        public string Filename { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
    }
}
