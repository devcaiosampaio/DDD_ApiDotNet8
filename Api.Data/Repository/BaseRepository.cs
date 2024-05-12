using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;
public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly MyContext _dbContext;
    private DbSet<T> _dataSet;

    public BaseRepository(MyContext dbContext)
    {
        _dbContext = dbContext;
        _dataSet = _dbContext.Set<T>(); 
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var result = await _dataSet.SingleOrDefaultAsync(e => e.Id.Equals(id));
            if (result == null)
                return false;


            _dataSet.Remove(result);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dataSet.AnyAsync(e => e.Id.Equals(id));
    }
    public async Task<IEnumerable<T>?> GetAllAsync()
    {
        try
        {
            return await _dataSet.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _dataSet.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }
        catch (Exception)
        {
            throw;
        }
          
    }

    public async Task<T> InsertAsync(T entity)
    {
        try
        {
            if(entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            entity.CreatAt = DateTime.UtcNow;
            _dataSet.Add(entity);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
        return entity;
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        try
        {
            var result = await _dataSet.SingleOrDefaultAsync(e => e.Id.Equals(entity.Id));
            if (result == null)
                return null;

            entity.CreatAt = result.CreatAt;
            entity.UpdateAt = DateTime.UtcNow;

            _dbContext.Entry(result).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
        return entity;
    }
}
