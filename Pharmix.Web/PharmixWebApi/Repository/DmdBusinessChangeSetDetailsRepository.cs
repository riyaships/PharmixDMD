using PharmixWebApi.Context;
using PharmixWebApi.Model;
using PharmixWebApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Repository
{
    public class DmdBusinessChangeSetDetailsRepository : IDmdBusinessChangeSetDetailsRepository<Dmd_BusinessChangeSetDetails, string>
    {
        ApplicationContext _context;
        public DmdBusinessChangeSetDetailsRepository(ApplicationContext Context)
        {
            _context = Context;
        }

        public Dmd_BusinessChangeSetDetails Get(string  userName)
        {
            var dmdBusinessChangeSetDetails = _context.Dmd_BusinessChangeSetDetails.Where(x=>x.BusinessEmail == userName).OrderByDescending(x=>x.DmdBusinessChangeSetDetailID).FirstOrDefault();
            return dmdBusinessChangeSetDetails;
        }

        public IEnumerable<Dmd_BusinessChangeSetDetails> GetAll()
        {
            var dmdBusinessChangeSetDetails = _context.Dmd_BusinessChangeSetDetails.ToList();
            return dmdBusinessChangeSetDetails;
        }

        public int Add(Dmd_BusinessChangeSetDetails dmdBusinessChangeSetDetails)
        {
            _context.Dmd_BusinessChangeSetDetails.Add(dmdBusinessChangeSetDetails);
            int studentID = _context.SaveChanges();
            return studentID;
        }
    }
}
