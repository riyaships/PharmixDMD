using PharmixWebApi.Context;
using PharmixWebApi.Model;
using PharmixWebApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Repository
{
    public class ExportDataToCSVDetailsRepository : IExportDataToCSVDetailsRepository<ExportDataToCSVDetails, int>
    {
        ApplicationContext _context;

        public ExportDataToCSVDetailsRepository(ApplicationContext Context)
        {
            _context = Context;
        }

        public int Add(ExportDataToCSVDetails exportDataToCSVDetails)
        {
            _context.ExportDataToCSVDetails.Add(exportDataToCSVDetails);
            int id = _context.SaveChanges();
            return id;
        }
        public int Update(ExportDataToCSVDetails exportDataToCSVDetails)
        {
            _context.ExportDataToCSVDetails.Update(exportDataToCSVDetails);
            int id = _context.SaveChanges();
            return id;
        }
    }
}
