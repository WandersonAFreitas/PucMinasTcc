using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApplicationCore.Services
{
    public class BaseService<TPrimaryKey, TEntity> : IService<TPrimaryKey, TEntity>
        where TPrimaryKey : IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class, IBaseEntity<TPrimaryKey>
    {
        protected readonly IAsyncRepository<TPrimaryKey, TEntity> _repository;
        protected readonly IAppLogger<TEntity> _logger;

        public BaseService(IAsyncRepository<TPrimaryKey, TEntity> repository, IAppLogger<TEntity> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            return await _repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }
        public async Task DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.ListAllAsync();
        }

        public async Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            return await _repository.ListQueryableAsync();
        }
    }
}
