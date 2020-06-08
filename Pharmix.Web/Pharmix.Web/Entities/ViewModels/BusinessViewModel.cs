using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pharmix.Web.Entities.ViewModels
{
    public class BusinessViewModel
    {
        public int Id { get; set; }
        [DisplayName("Business Name")]
        public string BusinessName { get; set; }
        [DisplayName("Last Subscribed Date")]
        [DataType(DataType.DateTime)]
        public DateTime? LastSubscribedDate { get; set; }
        [DisplayName("Last Expiration Date")]
        [DataType(DataType.DateTime)]
        public DateTime? LastExpirationDate { get; set; }
        [DisplayName("Address Line1")]
        public string AddressLine1 { get; set; }
        [DisplayName("Address Line2")]
        public string AddressLine2 { get; set; }
        [DisplayName("Address Line3")]
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        [DisplayName("Post Code")]
        public string Postcode { get; set; }
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }
        [DisplayName("Contact Phone")]
        public string ContactPhone { get; set; }
        [DisplayName("IdentityUserId")]
        public string IdentityUserId { get; set; }       
        [DisplayName("Dmd Amp")]
        public string DmdAmp { get; set; }      
        [DisplayName("Dmd Ampp")]
        public string DmdAmpp { get; set; }       
        [DisplayName("Dmd Vmp")]
        public string DmdVmp { get; set; }       
        [DisplayName("Dmd Vmpp")]
        public string DmdVmpp { get; set; }      
        [DisplayName("Dmd Vtm")]
        public string DmdVtm { get; set; }       
        [DisplayName("Dmd Form")]
        public string DmdForm { get; set; }       
        [DisplayName("Dmd Gtin")]
        public string DmdGtin { get; set; }       
        [DisplayName("Dmd Route")]
        public string DmdRoute { get; set; }       
        [DisplayName("Dmd Supplier")]
        public string DmdSupplier { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public int PackageID { get; set; }

        public bool? NotifyWeekly { get; set; }

    }
}
