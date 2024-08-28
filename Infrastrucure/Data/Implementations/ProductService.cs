using Infrastructure.Data.Interfaces;
using market_api.DTOs;
using market_api.Models.Domain;

namespace Infrastructure.Data.Implementations;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repository;

    public ProductService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllProducts(bool trackChanges)
    {
        try
        {
            var products =
                _repository.Product.GetAllProducts(trackChanges);
            return await products;
        }
        catch (Exception ex)
        {
            // HERE GOES LOGGER 
            // _logger.LogError($"Something went wrong in the
            // {nameof(GetAllCompanies)} service method {ex}");
            // throw;
            throw;
        }
    }

    public void CreateProduct(ProductForCreationDto productForCreationDto)
    {
        try
        {
            _repository.Product.CreateProduct(productForCreationDto);
            _repository.Save();
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public Product GetProduct(int productId, bool trackChanges)
    {
        try
        {
            return _repository.Product.GetProduct(productId, trackChanges);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}