using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAllWithSpecAsync(ISpecification<T> spec);
    Task<int> CountAllWithSpecAsync(ISpecification<T> spec);

    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}