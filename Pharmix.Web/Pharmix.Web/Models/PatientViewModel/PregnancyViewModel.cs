using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Models.PatientViewModel
{
    public class PregnancyViewModel
    {
        public int Id { get; set; }

        [Required]
        public string NHSNumber { get; set; }

        [Required]
        public string MaternityUnit { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? EDD { get; set; }

        public int PatientId { get; set; }

        public bool IsAdmin { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        public CommunicationNeedViewModel CommunicationNeedViewModel { get; set; }
    }
}
