using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Repository.Interface
{
    public interface IDmdBusinessChangeSetDetailsRepository<TEntity, U> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(U id);
        int Add(TEntity b);
    }
}
