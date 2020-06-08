using Pharmix.Data.Entities.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Enums;
using Pharmix.Web.Entities.ViewModels.Production;
using Pharmix.Web.Models;
using Pharmix.Web.Models.IsolatorVIewModels;

namespace Pharmix.Web.Entities.ViewModels.IntegrationOrder
{
    public class IntegrationOrderViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Order Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Allocated Isolator")]
        public int AlocatedIsolatorId { get; set; }

        public IEnumerable<Isolator> Isolators { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Administered Date")]
        public DateTime? AdministeredDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Booked In")]
        public DateTime? BookedInDate { get; set; }

        [Display(Name = "External Barcode")]
        public string ExternalBarcode { get; set; }

        [Display(Name = "External Order Id")]
        public string ExternalOrderId { get; set; }

        [Required]
        [Display(Name = "System Id")]
        public int IntegratedSystemId { get; set; }

        public IEnumerable<IntegratedSystem> IntegratedSystem { get; set; }

        [Display(Name = "Progress Status")]
        public int? OrderLastProgressId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Required Date")]
        public DateTime? RequiredDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Scheduled Date")]
        public DateTime? ScheduledDate { get; set; }

    }

    public class IntegrationOrderCommentViewModel
    {
        public int State { get; set; }

        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Action Comment")]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Order Classification")]
        public int Classification { get; set; }

        public IEnumerable<IntegrationOrderClassification> Classifications { get; set; }
    }

    public class CallSupervisorViewModel
    {
        public SupervisorRequestViewModel SupervisorRequest { get; set; }
        public IntegrationOrderCommentViewModel IntegrationOrderComment { get; set; }
    }

    public class IntegrationOrderDetail
    {
        public int OrderPreperationId { get; set; }
        public int OrderId { get; set; }
        public string RequiredDateTime { get; set; }
        public string ScheduledDateTime { get; set; }
        public string OrderName { get; set; }
        public string Status { get; set; }
        public int RequiredPreperationTimeInMins { get; set; }
        public string OrderPriority { get; set; }
        public string OrderTimeline { get; set; }
        public bool EnableUnschedule { get; set; }
    }

    public class ItegrationOrderPreperation
    {
        public int IsolatorId { get; set; }
        public string IsolatorName { get; set; }
        public string PreperationDate { get; set; }
        public SelectList IsolatorList { get; set; }
        public List<IsolatorShiftDetail> IsolatorShiftList { get; set; }
    }

    public class IntegrationOderScheduleViewModel
    {
        public int IsolatorId { get; set; }
        public string IsolatorName { get; set; }
        public DateTime PreperationDate { get; set; }
        public SelectList IsolatorList { get; set; }
        public SearchViewModel OrderSearch { get; set; } = new SearchViewModel();
    }
}
