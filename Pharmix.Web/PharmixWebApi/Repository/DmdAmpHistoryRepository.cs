using PharmixWebApi.Context;
using PharmixWebApi.Model;
using PharmixWebApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Repository
{
    public class DmdAmpHistoryRepository : IDmdAmpHistoryRepository<Dmd_Amp_History, int>
    {
        ApplicationContext _context;
        public DmdAmpHistoryRepository(ApplicationContext Context)
        {
            _context = Context;
        }

        public Dmd_Amp_History Get(int id)
        {
            var dmdAmpHistory = _context.Dmd_Amp_History.FirstOrDefault();
            return dmdAmpHistory;
        }

        public IEnumerable<Dmd_Amp_History> GetAll()
        {
            var dmdAmpHistory = _context.Dmd_Amp_History.ToList();
            var x = _context.Dmd_BusinessChangeSetDetails.FirstOrDefault(m => m.DmdBusinessChangeSetDetailID == 1);
             
            return dmdAmpHistory;
        }
    }
}
