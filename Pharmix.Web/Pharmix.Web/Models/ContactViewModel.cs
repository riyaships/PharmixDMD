using System.ComponentModel.DataAnnotations;

namespace Pharmix.Web.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "You must provide your name.")]
        [Display(Name = "Name*")]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide your email."), EmailAddress]
        [Display(Name = "Email*")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You must enter a subject.")]
        [Display(Name = "Subject*")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "You must include a message.")]
        [Display(Name = "Message*")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public bool SendCopy { get; set; }
    }
}
