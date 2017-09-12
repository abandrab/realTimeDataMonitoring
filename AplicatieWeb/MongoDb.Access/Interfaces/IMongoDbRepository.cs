using Domain.Model.Models.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoDb.Access.Interfaces
{
    public interface IMongoDbRepository<TEntity> where TEntity : Entity
    {
        ICollection<TEntity> GetByPatientId(object id);
        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        void AddMany(ICollection<TEntity> entities);
        ICollection<TEntity> GetAll();
    }
}
