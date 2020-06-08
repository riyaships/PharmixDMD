using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class Dmd_Vtm_History
    {
        [Key]
        public int  VtmHistoryId { get; set; }
        public string ActionType { get; set; }
        public string VTMIDLive { get; set; }
        public string INVALIDLive { get; set; }
        public string NMLive { get; set; }
        public string ABBREVNMLive { get; set; }
        public string VTMIDPREVLive { get; set; }
        public string VTMIDDTLive { get; set; }
        public string VTMIDChangeset { get; set; }
        public string INVALIDChangeset { get; set; }
        public string NMChangeset { get; set; }
        public string ABBREVNMChangeset { get; set; }
        public string VTMIDPREVChangeset { get; set; }
        public string VTMIDDTChangeset { get; set; }
        public int DmdChangeSetDetailID { get; set; }
    }
}
