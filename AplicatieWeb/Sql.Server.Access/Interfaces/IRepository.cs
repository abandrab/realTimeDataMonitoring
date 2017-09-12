using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sql.Server.Access.Interfaces
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(object id);
        void Add(TEntity entity);
        void AddMany(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Save();
    }
}
