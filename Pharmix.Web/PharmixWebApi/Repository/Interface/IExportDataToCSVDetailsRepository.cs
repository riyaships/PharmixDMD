using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Repository.Interface
{
    public interface IExportDataToCSVDetailsRepository<TEntity, U> where TEntity : class
    {
        int Add(TEntity b);
        int Update(TEntity b);
    }
}
