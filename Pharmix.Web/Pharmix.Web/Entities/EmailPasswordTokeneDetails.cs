using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Entities
{
    public class EmailPasswordTokeneDetails
    {
        [Key]
        public int Id { get; set; }
        public string ResetDetail { get; set; }
        public string PassworkToken { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
