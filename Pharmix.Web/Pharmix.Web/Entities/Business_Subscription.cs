using Pharmix.Data.Entities.Context;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pharmix.Web.Entities
{
    public class Business_Subscription : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string IdentityUserId { get; set; }
        public int PackagePlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PaidAmount { get; set; }
        public string Currency { get; set; }
        public string PaymentReceipt{ get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public bool PaymentReceived { get; set; }
        public string PaymentNotes { get; set; }
        public string PaymentVia { get; set; }
    }
}
