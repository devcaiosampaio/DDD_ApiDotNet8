using Api.Domain.Entities;

namespace Api.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> InsertAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>?> GetAllAsync();
    Task<bool> ExistsAsync(Guid id);
}
