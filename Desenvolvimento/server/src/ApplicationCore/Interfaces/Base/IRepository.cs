using System;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces.Base
{
    public interface IRepository<TPrimaryKey, TEntity>
        where TPrimaryKey : IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class
        , IBaseEntity<TPrimaryKey>
    {
        TEntity GetById(TPrimaryKey id);
        TEntity GetSingleBySpec(ISpecification<TEntity> spec);
        IEnumerable<TEntity> ListAll();
        IEnumerable<TEntity> List(ISpecification<TEntity> spec);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
