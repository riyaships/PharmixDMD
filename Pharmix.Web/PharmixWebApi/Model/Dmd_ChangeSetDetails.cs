using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class Dmd_ChangeSetDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? DmdChangeSetDetailID { get; set; }
        public string Description { get; set; }
        public string WeekNo { get; set; }
        public string YearNo { get; set; }
        public decimal ChangesetFileSize { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Isprocessed { get; set; }
    }
}
