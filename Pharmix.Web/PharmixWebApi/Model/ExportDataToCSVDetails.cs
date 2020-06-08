using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class ExportDataToCSVDetails
    {
        [Key]
        public int ExportDataToCSVDetailId { get; set; }
        public string ReportType { get; set; }
        public string FileName { get; set; }
        public string RootFolder { get; set; }
        public int? ChangetSetFrom { get; set; }
        public int? ChangetSetTo { get; set; }
        public string TabID { get; set; }
        public string ChangetSetFromId { get; set; }
        public string ChangetSetToId { get; set; }
        public string BusinessUser { get; set; }
        public bool? IsDownloadsqlQuery { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
