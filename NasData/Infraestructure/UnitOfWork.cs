using System.Data.Entity;
using System.Threading.Tasks;

namespace NasData.Infraestructure
{
    public class UnitOfWork<T>  : IUnitOfWork<T> where T : DbContext
    {
        private readonly  IDbFactory<T> dbFactory;
        private T dbContext;

        public UnitOfWork(IDbFactory<T> dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public T DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public async Task<bool> CommitAsync()
        {
            return await DbContext.SaveChangesAsync() > 0;
        }
    }
}
