using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class Dmd_Form_History
    {
        [Key]
        public int FormHistoryId { get; set; }
        public string ActionType { get; set; }
        public string CDLive { get; set; }
        public string CDDTLive { get; set; }
        public string CDPREVLive { get; set; }
        public string DESCLive { get; set; }
        public string CDChangeset { get; set; }
        public string CDDTChangeset { get; set; }
        public string CDPREVChangeset { get; set; }
        public string DESCChangeset { get; set; }
        public int DmdChangeSetDetailID { get; set; }

    }
}
