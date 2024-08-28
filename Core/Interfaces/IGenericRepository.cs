using Core.Models.Domain;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseSimpleEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T?> GetEntityWithSpecs(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsyncWithSpecs(ISpecification<T> spec);

        Task<TResult?> GetEntityWithSpecs<TResult>(ISpecification<T, TResult> spec);
        Task<IReadOnlyList<TResult>> ListAsyncWithSpecs<TResult>(ISpecification<T, TResult> spec);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> SaveAllChangesAsync();
        bool Exists(int id);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
