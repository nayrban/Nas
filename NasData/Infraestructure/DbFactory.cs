using System;
using System.Data.Entity;

namespace NasData.Infraestructure
{
    public class DbFactory<T> : Disposable, IDbFactory<T> where T : DbContext
    {
        T dbContext;
        public T Init()
        {
            return dbContext ?? (dbContext = (T)Activator.CreateInstance(typeof(T)));

        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
