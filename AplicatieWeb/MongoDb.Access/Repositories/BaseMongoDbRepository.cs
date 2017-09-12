using Domain.Model.Models.MongoDB;
using MongoDb.Access.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoDb.Access.Repositories
{
    public abstract class BaseMongoDbRepository<TEntity>
        : IMongoDbRepository<TEntity> where TEntity : Entity
    { 
        protected abstract IMongoCollection<TEntity> Collection { get; }
      
        public virtual ICollection<TEntity> GetByPatientId(object id)
        {
            return Collection.Find(x => x.PatientId.Equals(id)).ToList();
        }
        public virtual void AddMany(ICollection<TEntity> entities)
        {
            Collection.InsertMany(entities);
        }

        public virtual ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Find(predicate).ToList();
        }
        public virtual ICollection<TEntity> GetAll()
        {
            return Collection.Find(new BsonDocument()).ToList();
        }
    }
}
