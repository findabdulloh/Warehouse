using System.Linq.Expressions;
using Warehouse.Domain.Commons;

namespace Warehouse.Data.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    Task<TEntity> InsertAsync(TEntity entity);

    TEntity Update(TEntity entity);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null);
    Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);

    Task SaveAsync();
}