using Pharmix.Web.Entities;
using Pharmix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Services
{
    public interface IAuditInfoService
    {
        IEnumerable<AuditInfoViewModel> GetAuditInfos();
        List<KeyValuePair<int, decimal>> GetAuditVersionsByName(string name, string id);
        AuditInfo GetAuditInfoById(int id);
        object GetAuditVersionInfoById(int id);
    }
}
