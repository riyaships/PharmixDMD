using System;
using System.ComponentModel.DataAnnotations;

namespace Pharmix.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "* Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "* Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "* Gender")]
        public int GenderId { get; set; }
        
        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Display(Name = "* First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "* Surname")]
        public string Surname { get; set; }

        [Display(Name = "Mobile No.")]
        public string MobileNumber { get; set; }

        [Display(Name = "Tel. No.")]
        public string AlternativeTel { get; set; }

        [Display(Name = "Address line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address line 2")]
        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }
    }
}
