using Infrastructure.Data.Interfaces;
using Infrastrucure.Data.Implementations;
using market_api.Data.Implementations;

namespace Infrastructure.Data.Implementations;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IProductService> _productService;
    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _productService = new Lazy<IProductService>(() => new
            ProductService(repositoryManager));
    }

    public IProductService ProductService => _productService.Value;
}