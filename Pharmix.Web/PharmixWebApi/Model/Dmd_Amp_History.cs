using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class Dmd_Amp_History
    {
        [Key]
        public int AmpHistoryId { get; set; }        
        public string APIDLive { get; set; }
        public string ActionType { get; set; }        
        public string INVALIDLive { get; set; }
        public string VPIDLive { get; set; }
        public string NMLive { get; set; }
        public string ABBREVNMLive { get; set; }
        public string DESCLive { get; set; }
        public string NMDTLive { get; set; }
        public string NM_PREVLive { get; set; }
        public string SUPPCDLive { get; set; }
        public string LIC_AUTHCDLive { get; set; }
        public string LIC_AUTH_PREVCDLive { get; set; }
        public string LIC_AUTHCHANGECDLive { get; set; }
        public string LIC_AUTHCHANGEDTLive { get; set; }
        public string COMBPRODCDLive { get; set; }
        public string FLAVOURCDLive { get; set; }
        public string EMALive { get; set; }
        public string PARALLEL_IMPORTLive { get; set; }
        public string AVAIL_RESTRICTCDLive { get; set; }
        public string AMPS_IdLive { get; set; }
        public string APIDChangeset { get; set; }
        public string INVALIDChangeset { get; set; }
        public string VPIDChangeset { get; set; }
        public string NMChangeset { get; set; }
        public string ABBREVNMChangeset { get; set; }
        public string DESCChangeset { get; set; }
        public string NMDTChangeset { get; set; }
        public string NM_PREVChangeset { get; set; }
        public string SUPPCDChangeset { get; set; }
        public string LIC_AUTHCDChangeset { get; set; }
        public string LIC_AUTH_PREVCDChangeset { get; set; }
        public string LIC_AUTHCHANGECDChangeset { get; set; }
        public string LIC_AUTHCHANGEDTChangeset { get; set; }
        public string COMBPRODCDChangeset { get; set; }
        public string FLAVOURCDChangeset { get; set; }
        public string EMAChangeset { get; set; }
        public string PARALLEL_IMPORTChangeset { get; set; }
        public string AVAIL_RESTRICTCDChangeset { get; set; }
        public string AMPS_IdChangeset { get; set; }
        public int DmdChangeSetDetailID { get; set; }
    }
}
