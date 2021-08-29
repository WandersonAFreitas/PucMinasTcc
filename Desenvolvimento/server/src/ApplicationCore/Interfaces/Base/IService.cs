using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Base
{
    public interface IService<TPrimaryKey, TEntity>
        where TPrimaryKey : IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class, IBaseEntity<TPrimaryKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetQueryableAsync();
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
