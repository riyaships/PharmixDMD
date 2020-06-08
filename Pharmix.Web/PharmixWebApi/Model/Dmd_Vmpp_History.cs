using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class Dmd_Vmpp_History
    {
        [Key]
        public int VmppHistoryId { get; set; }        
        public string ActionType { get; set; }
        public string VPPIDLive { get; set; }
        public string INVALIDLive { get; set; }
        public string NMLive { get; set; }
        public string ABBREVNMLive { get; set; }
        public string VPIDLive { get; set; }
        public string QTYVALLive { get; set; }
        public string QTY_UOMCDLive { get; set; }
        public string COMBPACKCDLive { get; set; }
        public string VMPPS_IdLive { get; set; }
        public string VPPIDChangeset { get; set; }
        public string INVALIDChangeset { get; set; }
        public string NMChangeset { get; set; }
        public string ABBREVNMChangeset { get; set; }
        public string VPIDChangeset { get; set; }
        public string QTYVALChangeset { get; set; }
        public string QTY_UOMCDChangeset { get; set; }
        public string COMBPACKCDChangeset { get; set; }
        public string VMPPS_IdChangeset { get; set; }
        public int DmdChangeSetDetailID { get; set; }
    }
}
