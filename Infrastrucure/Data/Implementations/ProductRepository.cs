using market_api.Models.Domain;
using market_api.DTOs;
using Infrastructure.Data.Base;
using Infrastructure.Data.App;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Implementations;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(ApplicationContext repositoryContext) : base(repositoryContext)
    {

    }

    public async Task<IEnumerable<Product>> GetAllProducts(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();

    public void CreateProduct(ProductForCreationDto productForCreationDto)
    {
        var product = new Product
        {
            Description = productForCreationDto.Description,
            Name = productForCreationDto.Name,
            Price = productForCreationDto.Price,
            Quantity = productForCreationDto.Quantity,
            InternalCode = productForCreationDto.InternalCode,
            IsAvailable = productForCreationDto.IsAvailable,
            ImageUrl = productForCreationDto.ImageLink
        };

        Create(product);
    }

    public Product GetProduct(int productId, bool trackChanges)
    {
        return FindByCondition(p => p.Id.Equals(productId), trackChanges).SingleOrDefault();
    }
}