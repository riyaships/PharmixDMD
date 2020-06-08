using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class Dmd_Ampp_History
    {
        [Key]
        public int AmppHistoryId { get; set; }        
        public string ActionType { get; set; }
        public string APPIDLive { get; set; }
        public string INVALIDLive { get; set; }
        public string NMLive { get; set; }
        public string ABBREVNMLive { get; set; }
        public string VPPIDLive { get; set; }
        public string APIDLive { get; set; }
        public string COMBPACKCDLive { get; set; }
        public string LEGAL_CATCDLive { get; set; }
        public string SUBPLive { get; set; }
        public string DISCCDLive { get; set; }
        public string DISCDTLive { get; set; }
        public string AMPPS_IdLive { get; set; }
        public string APPIDChangeset { get; set; }
        public string INVALIDChangeset { get; set; }
        public string NMChangeset { get; set; }
        public string ABBREVNMChangeset { get; set; }
        public string VPPIDChangeset { get; set; }
        public string APIDChangeset { get; set; }
        public string COMBPACKCDChangeset { get; set; }
        public string LEGAL_CATCDChangeset { get; set; }
        public string SUBPChangeset { get; set; }
        public string DISCCDChangeset { get; set; }
        public string DISCDTChangeset { get; set; }
        public string AMPPS_IdChangeset { get; set; }
        public int DmdChangeSetDetailID { get; set; }
    }
}
