namespace Infrastructure.Data.Interfaces;

public interface IRepositoryManager
{
    IProductRepository Product { get; }

    void Save();
}