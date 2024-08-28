using market_api.DTOs;
using market_api.Models.Domain;

namespace Infrastructure.Data.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts(bool trackChanges);
    void CreateProduct(ProductForCreationDto product);
    Product GetProduct(int productId, bool trackChanges);
}