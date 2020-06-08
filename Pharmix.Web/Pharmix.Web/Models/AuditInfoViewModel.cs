using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Models
{
    public class AuditInfoViewModel
    {
        public int Id { get; set; }
        public string Info { get; set; }
        public decimal Version { get; set; }
        public string KeyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public string Name { get; set; }
    }
}
