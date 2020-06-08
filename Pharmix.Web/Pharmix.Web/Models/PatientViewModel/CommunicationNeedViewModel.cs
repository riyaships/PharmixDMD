using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Models.PatientViewModel
{
    public class CommunicationNeedViewModel
    {
        public int Id { get; set; }

        [Required]
        public bool AssistanceRequired { get; set; }

        [Required]
        public string AssistanceRequiredDetail { get; set; }

        [Required]
        public string PreferredAssistance { get; set; }

        [Required]
        public bool SpeakEnglish { get; set; }

        [Required]
        public string FirstLanguage { get; set; }

        [Required]
        public string PreferedLanguage { get; set; }

        public string InterpreterPhone { get; set; }

        public int PregnancyId { get; set; }
    }
}
