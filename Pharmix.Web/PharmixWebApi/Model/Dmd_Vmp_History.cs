using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class Dmd_Vmp_History
    {
        [Key]
        public int VmpHistoryId { get; set; }        
        public string ActionType { get; set; }
        public string VPIDLive { get; set; }
        public string VPIDDTLive { get; set; }
        public string VPIDPREVLive { get; set; }
        public string VTMIDLive { get; set; }
        public string INVALIDLive { get; set; }
        public string NMLive { get; set; }
        public string ABBREVNMLive { get; set; }
        public string BASISCDLive { get; set; }
        public string NMDTLive { get; set; }
        public string NMPREVLive { get; set; }
        public string BASIS_PREVCDLive { get; set; }
        public string NMCHANGECDLive { get; set; }
        public string COMBPRODCDLive { get; set; }
        public string PRES_STATCDLive { get; set; }
        public string SUG_FLive { get; set; }
        public string GLU_FLive { get; set; }
        public string PRES_FLive { get; set; }
        public string CFC_FLive { get; set; }
        public string NON_AVAILCDLive { get; set; }
        public string NON_AVAILDTLive { get; set; }
        public string DF_INDCDLive { get; set; }
        public string UDFSLive { get; set; }
        public string UDFS_UOMCDLive { get; set; }
        public string UNIT_DOSE_UOMCDLive { get; set; }
        public string VMPS_IdLive { get; set; }
        public string VPIDChangeset { get; set; }
        public string VPIDDTChangeset { get; set; }
        public string VPIDPREVChangeset { get; set; }
        public string VTMIDChangeset { get; set; }
        public string INVALIDChangeset { get; set; }
        public string NMChangeset { get; set; }
        public string ABBREVNMChangeset { get; set; }
        public string BASISCDChangeset { get; set; }
        public string NMDTChangeset { get; set; }
        public string NMPREVChangeset { get; set; }
        public string BASISChangeset_PREVCD { get; set; }
        public string NMCHANGECDChangeset { get; set; }
        public string COMBPRODCDChangeset { get; set; }
        public string PRES_STATCDChangeset { get; set; }
        public string SUG_FChangeset { get; set; }
        public string GLU_FChangeset { get; set; }
        public string PRES_FChangeset { get; set; }
        public string CFC_FChangeset { get; set; }
        public string NON_AVAILCDChangeset { get; set; }
        public string NON_AVAILDTChangeset { get; set; }
        public string DF_INDCDChangeset { get; set; }
        public string UDFSChangeset { get; set; }
        public string UDFS_UOMCDChangeset { get; set; }
        public string UNIT_DOSE_UOMCDChangeset { get; set; }
        public string VMPS_IdChangeset { get; set; }
        public int DmdChangeSetDetailID { get; set; }
    }
}
