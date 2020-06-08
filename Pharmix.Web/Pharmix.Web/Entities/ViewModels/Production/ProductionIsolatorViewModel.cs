using System;
using System.Collections.Generic;
using Pharmix.Data.Enums;
using Pharmix.Web.Entities.ViewModels.IntegrationOrder;

namespace Pharmix.Web.Entities.ViewModels.Production
{
    public class ProductionIsolatorViewModel
    {
        public int IsolatorId { get; set; }
        public string IsolatorName { get; set; }
        public int TotalNumberOfDosesPerSession { get; set; }
        public DateTime ProductionDate { get; set; }
        public string UserName { get; set; }
        public bool IsBeingUsed { get; set; }
    }

    public class ProductionOrderDetail : IntegrationOrderDetail
    {
        public string ProductionDisplayTime { get; set; }
        public OrderPreperationStatusEnum PreperationStatus { get; set; }
        public bool EnableActions { get; set; }
    }

    public class ProductionOrderListViewModel
    {
        public int IsolatorId { get; set; }
        public string IsolatorName { get; set; }
        public DateTime ProductionDate { get; set; }
        public string ProductionShift { get; set; }
        public bool IsStartProcedureVerified { get; set; }
        public bool IsStopProcedureVerified { get; set; }
        public List<ProductionOrderDetail> Orders { get; set; } = new List<ProductionOrderDetail>();
    }
}
