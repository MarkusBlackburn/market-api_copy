using Infrastructure.Data.App;
using Infrastructure.Data.Interfaces;
using Infrastrucure.Data.Implementations;
using market_api.Data.Implementations;
using market_api.Models.Domain;

namespace Infrastructure.Data.Implementations;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationContext _repositoryContext;
    private readonly Lazy<IProductRepository> _productRepository;

    public RepositoryManager(ApplicationContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _productRepository = new Lazy<IProductRepository>(() => new
            ProductRepository(repositoryContext));
    }

    public IProductRepository Product => _productRepository.Value;

    public void Save() => _repositoryContext.SaveChanges();
}
