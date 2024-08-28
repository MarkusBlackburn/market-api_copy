using Core.Models.Domain;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces
{
    public interface IImageRepository
    {
        Task<ProductImage> Upload(IFormFile file, ProductImage image);
        Task<IEnumerable<ProductImage>> GetAllImages();
    }
}
