using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasData.Infraestructure
{
    public interface IDbFactory<T>  : IDisposable where T : class
    {
        T Init();
    }
}
