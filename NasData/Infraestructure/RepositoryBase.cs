using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace NasData.Infraestructure
{
    public abstract class RepositoryBase<T, C> where T : DbContext where  C : class
    {
        #region Properties
        private T dataContext;
        private readonly IDbSet<C> dbSet;

        protected IDbFactory<T> DbFactory
        {
            get;
            private set;
        }

        protected T DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory<T> dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<C>();
        }

        #region Implementation
        public virtual void Add(C entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(C entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(C entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<C, bool>> where)
        {
            IEnumerable<C> objects = dbSet.Where(where).AsEnumerable();
            foreach (C obj in objects)
                dbSet.Remove(obj);
        }

        public virtual C GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual C GetById(string id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<C> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<C> GetMany(Expression<Func<C, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public C Get(Expression<Func<C, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault();
        }

        #endregion

    }
}
