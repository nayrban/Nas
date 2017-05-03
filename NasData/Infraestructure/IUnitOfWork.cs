using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasData.Infraestructure
{
    public interface IUnitOfWork<T> where T : DbContext
    {
        void Commit();

        Task<bool> CommitAsync();
    }
}
