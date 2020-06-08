using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Enums;

namespace Pharmix.Web.Entities.ViewModels.Production
{
    public class SupervisorRequestViewModel
    {
        public int Id { get; set; }

        public RequsetPriorityEnum Priority { get; set; }
        
        [Required]
        [Display(Name = "Request Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Request Type")]
        public int TypeId { get; set; }

        public IEnumerable<SupervisorRequestType> SupervisorRequetTypes { get; set; }

        public RequestStatusEnum LatestRequestStatus { get; set; }

        public int IsolatorId { get; set; }
        public IEnumerable<Pharmix.Web.Entities.Isolator> Isolators { get; set; }
        
        public int? CurrentOrderId { get; set; }

        public IEnumerable<Entities.IntegrationOrder> IntegrationOrders { get; set; }

        public bool IsModelEditable { get; set; }

    }
}
