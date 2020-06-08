using System;
using System.Collections.Generic;
using Pharmix.Web.Entities.ViewModels.IntegrationOrder;

namespace Pharmix.Web.Models.IsolatorVIewModels
{
    public class IsolatorShiftDetail
    {
        public int IsolatorStaffAllocationId { get; set; }
        public int ShiftId { get; set; }
        public string ShiftTitle { get; set; }
        public int IsolatorId { get; set; }
        public int StaffId { get; set; }
        public int AllocationId { get; set; }
        public bool IsShiftUnavailable { get; set; }
        public bool IsShiftReadonly { get; set; }
        public string StaffName { get; set; }
        public DateTime Date { get; set; }
        public double PercentFilled { get; set; } = 0;
        public double AvailableShiftTimeInMins { get; set; }
        public List<IntegrationOrderDetail> IntegrationOrders { get; set; }
    }
}