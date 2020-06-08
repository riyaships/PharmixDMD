using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pharmix.Data.Entities.Context;

namespace Pharmix.Web.Entities
{
    [Table("IsolatorShift", Schema = "iso")]
    public class IsolatorShift : BaseEntity
    {
        [Key]
        public int ShiftId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ShiftTitle { get; set; }
        public double TotalShiftDurationInMins { get; set; }
    }

    [Table("IsolatorProcedure", Schema = "iso")]
    public class IsolatorProcedure : BaseEntity
    {
        [Key]
        public int IsolatorProcedureId { get; set; }
        public string Description { get; set; }
        public int ProcedureTypeId { get; set; }
    }
}