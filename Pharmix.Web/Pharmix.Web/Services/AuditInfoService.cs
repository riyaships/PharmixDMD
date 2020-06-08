using Pharmix.Web.Entities;
using Pharmix.Web.Models;
using Pharmix.Web.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Services
{
    public class AuditInfoService : IAuditInfoService
    {
        private readonly IRepository repository;
        private readonly PharmixEntityContext _context;

        public AuditInfoService(IRepository repository)
        {
            this.repository = repository;
            this._context = repository.GetContext();
        }

        #region AuditInfo
        public IEnumerable<AuditInfoViewModel> GetAuditInfos()
        {
            var auditInfoModel = (from aI in _context.AuditInfos
                                  select new AuditInfoViewModel()
                                  {
                                      Id = aI.Id,
                                      Info = aI.Info,
                                      CreatedDate = aI.CreatedDate,
                                      CreatedUser = aI.CreatedUser
                                  }).ToList();
            return auditInfoModel;
        }

        public AuditInfo GetAuditInfoById(int id)
        {
            return _context.AuditInfos.Find(id);
        }

        public object GetAuditVersionInfoById(int id)
        {
            var auditInfo = _context.AuditInfos.Find(id);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<object>(auditInfo.Info);
        }

        public List<KeyValuePair<int, decimal>> GetAuditVersionsByName(string name, string id)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(id))
            {
                var versionInfos = new List<KeyValuePair<int, decimal>>();
                versionInfos = _context.AuditInfos.Where(x => x.Name.Equals(name) && x.KeyId == id)
                    .Select(x => new KeyValuePair<int, decimal>(x.Id, x.Version)).ToList();
                        
                //var updVersionInfos = versionInfos.Select((x, i) => new KeyValuePair<int, decimal>(x.Key, i+1)).ToList();     //Update the version id

                return versionInfos;
            }
            else return null;
        }

        #endregion
    }
}
