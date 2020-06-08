using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class UniversalSearchResults
    {
        [Key]
        public int UniversalSearchResultId  {get;set;}
        public string TempName { get; set; }
        public string ReportName { get; set; }
        public string ReportID { get; set; }
        public string ReportDesc { get; set; }
        public string UniversalSearch { get; set; }
    }
}
