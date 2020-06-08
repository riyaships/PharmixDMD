using PharmixWebApi.Context;
using PharmixWebApi.Model;
using PharmixWebApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Repository
{
    public class Dmd_FTPFileDownloadDetailsRepository : IDmd_FTPFileDownloadDetailsRepository<Dmd_FTPFileDownloadDetails, int>
    {
        ApplicationContext _context;

        public Dmd_FTPFileDownloadDetailsRepository(ApplicationContext Context)
        {
            _context = Context;
        }
       
        public int Add(Dmd_FTPFileDownloadDetails dmdFTPFileDownloadDetails)
        {
            _context.Dmd_FTPFileDownloadDetails.Add(dmdFTPFileDownloadDetails);
            int id = _context.SaveChanges();
            return id;
        }
    }
}
