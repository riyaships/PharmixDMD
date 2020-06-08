using Pharmix.Data.Entities.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Entities
{
    public class BusinessDetails : BaseEntity
    {
        [Key]
        public int Id { get; set; }       
        public string BusinessName { get; set; }
        public DateTime? LastSubscribedDate { get; set; }
        public DateTime? LastExpirationDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string IdentityUserId { get; set; }
        public bool? NotifyWeekly { get; set; }
        public string DmdAmp { get; set; }
        public string DmdAmpp { get; set; }
        public string DmdVmp { get; set; }
        public string DmdVmpp { get; set; }
        public string DmdVtm { get; set; }
        public string DmdForm { get; set; }
        public string DmdGtin { get; set; }
        public string DmdRoute { get; set; }
        public string DmdSupplier { get; set; }
    }
}
