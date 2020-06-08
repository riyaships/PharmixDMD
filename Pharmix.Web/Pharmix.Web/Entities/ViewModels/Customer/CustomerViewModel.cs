using Pharmix.Web.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmix.Data.Entities.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public int GenderId { get; set; }
        public List<KeyValuePair<string,string>> Genders { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string NhsNumber { get; set; }
        public string PasNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string MobileNumber { get; set; }

        public string AlternativeTel { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string County { get; set; }
        public string Postcode { get; set; }
         
        public string FullAddress { get; set; }
        public string GenderText { get; set; }

        public List<ModuleViewModel> ModuleViewModelList { get; set; }
         
    }
}
