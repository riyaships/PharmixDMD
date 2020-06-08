using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class DmdReferenceCombinedDataset
    {
        [Key]      
        public string ProductPackId { get; set; }
        public string ProductPackName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string VtmId { get; set; }
        public string VtmName { get; set; }
        public string VmpId { get; set; }
        public string VmpName { get; set; }
        public string VmppId { get; set; }
        public string VmppName { get; set; }
    }
}
