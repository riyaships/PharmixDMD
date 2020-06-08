using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class Dmd_Gtin_History
    {
        [Key]
        public int GtinHistoryId { get; set; }
        public string ActionType { get; set; }
        public string AMPPIDLive { get; set; }
        public string GTINDATAGTINLive { get; set; }
        public string GTINDATASTARTDTLive { get; set; }
        public string AMPPIDChangeset { get; set; }
        public string GTINDATAGTINChangeset { get; set; }
        public string GTINDATASTARTDTChangeset { get; set; }        
        public int DmdChangeSetDetailID { get; set; }
    }
}
