using System;
using System.Collections.Generic;

namespace Pharmix.Web.Entities.ViewModels
{
    public class Business_SubscriptionViewModel
    {
        public int Id { get; set; }
        public string IdentityUserId { get; set; }
        public int PackagePlanId { get; set; }

        public string Package { get; set; }
        public string Duration { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PaidAmount { get; set; }
        public string Currency { get; set; }
        public string PaymentReceipt { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public bool PaymentReceived { get; set; }
        public string PaymentNotes { get; set; }
        public string PaymentVia { get; set; }
         
    }
}
