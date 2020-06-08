using PharmixWebApi.Context;
using PharmixWebApi.Model;
using PharmixWebApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Repository
{
    public class DmdChangeSetDetailsRepository : IDmdChangeSetDetailsRepository<Dmd_ChangeSetDetails, int>
    {
        ApplicationContext _context;
        public DmdChangeSetDetailsRepository(ApplicationContext Context)
        {
            _context = Context;
        }

        public Dmd_ChangeSetDetails Get(int id)
        {
            var dmdAmpHistory = _context.Dmd_ChangeSetDetails.Where(x=>x.DmdChangeSetDetailID == id).FirstOrDefault();
            return dmdAmpHistory;
        }


        public IEnumerable<Dmd_ChangeSetDetails> GetAll()
        {
            var dmdAmpHistory = _context.Dmd_ChangeSetDetails.ToList();
            return dmdAmpHistory;
        }
        public int Add(Dmd_ChangeSetDetails stundent)
        {
            _context.Dmd_ChangeSetDetails.Add(stundent);
            int studentID = _context.SaveChanges();
            return studentID;
        }
    }
}
