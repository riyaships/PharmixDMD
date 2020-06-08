using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pharmix.Data.Entities.Context;

namespace Pharmix.Web.Entities
{
    [Table("Isolator", Schema = "iso")]
    public class Isolator : BaseEntity
    {
        [Key]
        public int IsolatorId { get; set; }
        public string Abbriviation { get; set; }
        public int TotalNumberOfDosesPerSession { get; set; }
        //public decimal? MinIsolatorPressure { get; set; }
        //public decimal? MaxIsolatorPressure { get; set; }
        //public int? IsolatorPressureUnit { get; set; }
        //public int? OperationType { get; set; }
        //public decimal? RadiiOfIsolatorChamber { get; set; }
        //public decimal? RequiredChamberGradient { get; set; }
        //public bool TightnessTestRequired { get; set; }
        //public bool GloveTestRequired { get; set; }
        //public DateTime? LastHEPAFilterVerifiedDate { get; set; }
        //public decimal? LatestPressure { get; set; }
        //public int? LatestPressureUnit { get; set; }
        //public decimal? LatestTemperature { get; set; }
        //public int? LatestTemperatureUnit { get; set; }
        //public decimal? LatestHumidity { get; set; }
        //public int? LatestHumidityUnit { get; set; }
        //public DateTime? LastVHPSterilisationDate { get; set; }

        public DateTime? OfflineStartDate { get; set; }
        public DateTime? OfflineEndDate { get; set; }
        public string OfflineShifts { get; set; }

        public virtual ICollection<IsolatorStaffAllocation> StaffShiftAllocations { get; set; } = new HashSet<IsolatorStaffAllocation>();
        public virtual ICollection<IntegrationOrderPreperation> PreperationOrders { get; set; } = new HashSet<IntegrationOrderPreperation>();
        public virtual ICollection<IsolatorMappedProcedure> Procedures { get; set; } = new HashSet<IsolatorMappedProcedure>();
    }

    [Table("IsolatorStaffAllocation", Schema = "iso")]
    public class IsolatorStaffAllocation : BaseEntity
    {
        [Key]
        public int IsolatorStaffAllocationId { get; set; }
        public int IsolatorId { get; set; }
        [ForeignKey("IsolatorId")]
        public virtual Isolator Isolator { get; set; }
        public int IsolatorShiftId { get; set; }
        [ForeignKey("IsolatorShiftId")]
        public virtual IsolatorShift IsolatorShift { get; set; }
        public DateTime AllocatedDate { get; set; }
        public string StaffId { get; set; }
        [ForeignKey("StaffId")]
        public virtual ApplicationUser Staff { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime? RecurringEndDate { get; set; }
        public int RecurringTypeId { get; set; }
        public int DailyRecurringTypeId { get; set; }
        public string WeeklyRecurringWeekdays { get; set; }
        public int? ParentAllocationId { get; set; }
        [ForeignKey("ParentAllocationId")]
        public virtual IsolatorStaffAllocation ParentStaffAllocation { get; set; }
        public bool IsUsingIsolatorNow { get; set; }
    }
    
    [Table("IntegrationOrderPreperation", Schema = "iso")]
    public class IntegrationOrderPreperation : BaseEntity
    {
        [Key]
        public int IntegrationOrderPreperationId { get; set; }
        public int IntegrationOrderId { get; set; }
        [ForeignKey("IntegrationOrderId")]
        public virtual IntegrationOrder IntegrationOrder { get; set; }
        public int IsolatorId { get; set; }
        [ForeignKey("IsolatorId")]
        public virtual Isolator Isolator { get; set; }
        public int? IsolatorStaffAllocationId { get; set; }
        [ForeignKey("IsolatorStaffAllocationId")]
        public virtual IsolatorStaffAllocation IsolatorStaffAllocation { get; set; }
        public int PreperationStatusId { get; set; }
        public DateTime PreperationDateTime { get; set; }
    }

    [Table("IsolatorMappedProcedure", Schema = "iso")]
    public class IsolatorMappedProcedure
    {
        [Key]
        public int IsolatorMappedProcedureId { get; set; }
        public int ProcedureId { get; set; }
        [ForeignKey("ProcedureId")]
        public virtual IsolatorProcedure Procedure { get; set; }
        public int IsolatorId { get; set; }
        [ForeignKey("IsolatorId")]
        public virtual Isolator Isolator { get; set; }
    }
}
