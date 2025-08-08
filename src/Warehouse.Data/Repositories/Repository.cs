using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using Warehouse.Data.DbContexts;
using Warehouse.Data.IRepositories;
using Warehouse.Domain.Commons;

namespace Warehouse.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    protected readonly AppDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await this.SelectAsync(expression);

        if (entity is not null)
        {
            _dbSet.Remove(entity);
            return true;
        }

        return false;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        EntityEntry<TEntity> entry = await _dbSet.AddAsync(entity);

        return entry.Entity;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null)
    {
        return expression is null ? _dbSet : _dbSet.Where(expression);
    }

    public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }

    public TEntity Update(TEntity entity)
    {
        EntityEntry<TEntity> entryEntity = _dbSet.Update(entity);

        return entryEntity.Entity;
    }
}