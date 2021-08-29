using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Base
{
    public interface IAsyncRepository<TPrimaryKey, TEntity>
        where TPrimaryKey : IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class, IBaseEntity<TPrimaryKey>
    {
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
        Task<List<TEntity>> ListAllAsync();
        Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IQueryable<TEntity>> ListQueryableAsync();
        Task<IQueryable<TEntity>> ListQueryableAsync(params Expression<Func<TEntity, object>>[] includes);
    }
}
