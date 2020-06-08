using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Repository.Interface
{
    public interface IDmd_FTPFileDownloadDetailsRepository<TEntity, U> where TEntity : class
    {

        int Add(TEntity b);
    }
}
