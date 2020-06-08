using PharmixWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.CustomModel
{
    public class CustomReportModel
    {
        public List<Dmd_Amp_History> DmdAmpHistory { get; set; }
        public List<Dmd_Vtm_History> DmdVtmHistory { get; set; }
        public List<Dmd_Vmpp_History> DmdVmppHistory { get; set; }
        public List<Dmd_Vmp_History> DmdVmpHistory { get; set; }
        public List<Dmd_Ampp_History> DmdAmppHistory { get; set; }
        public List<Dmd_ROUTE_History> DmdROUTEHistory { get; set; }
        public List<Dmd_Form_History> DmdFormHistory { get; set; }
        public List<Dmd_SUPPLIER_History> DmdSUPPLIERHistory { get; set; }
        public List<Dmd_Gtin_History> DmdGtinHistory { get; set; }
        public List<DmdReferenceCombinedDataset> DmdReferenceCombinedDataset { get; set; }

        public List<Dmd_ChangeSetDetails> DmdChangeSetDetails { get; set; }
        public List<UploadedFiles> UploadedFiles { get; set; }
        public int? totalCount { get; set; }
        public List<Dmd_BusinessChangeSetDetails> DmdBusinessChangeSetDetails { get; set; }
        public List<string> AllFilePathForMychangesetDetails { get; set; }
        public int MyChangesetId { get; set; }
        public string MyChangeset { get; set; }
        public ChangesetBarViewModel ChangesetBarViewModel { get; set; }
        public List<Dmd_FTPFileDownloadDetails> DmdFTPFileDownloadDetails { get; set; }
        public string PageId { get; set; }
        public string SearchValue { get; set; }
        public List<UniversalSearchResults> UniversalSearchResultDetails { get; set; }
    }
    public class ChangesetBarViewModel
    {
        public string ChangesetTitle { get; set; }
        public string ChangesetFromString { get; set; }
        public string ChangesetToString { get; set; }
        public string ChangesetSetDateString { get; set; }
        public int ChangesetFromId { get; set; }
        public int ChangesetToId { get; set; }
    }
    public class UniversalSearchResults11
    {
       
    }
}
