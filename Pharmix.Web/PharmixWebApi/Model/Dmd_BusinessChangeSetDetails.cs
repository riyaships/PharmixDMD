using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class Dmd_BusinessChangeSetDetails
    {
        [Key]
        public int DmdBusinessChangeSetDetailID { get; set; }
        public int FromDateChangesetId { get; set; }
        public int ToDateChangesetId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string BusinessEmail { get; set; }
        public string FromDateChangeset { get; set; }
        public string ToDateChangeset { get; set; }
        public bool IsActive { get; set; }
    }
}
