using Domain.Model.Models;
using Sql.Server.Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Sql.Server.Access.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>, IDisposable
       where TEntity : Base
    {
        private MainContext context;

        protected BaseRepository()
        {
            context = new MainContext();
        }
        protected MainContext Context => context;

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity GetById(object id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddMany(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = context.Entry(entity);
            entry.State = EntityState.Modified;
        }
        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    
                    Debug.Write("Entity of type" + eve.Entry.Entity.GetType().Name + " in state"+ eve.Entry.State + " has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.Write("- Property: "+ ve.PropertyName+", Error: " +
                             ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}
