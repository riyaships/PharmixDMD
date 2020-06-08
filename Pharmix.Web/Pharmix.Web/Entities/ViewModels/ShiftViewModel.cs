using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pharmix.Web.Entities.ViewModels
{
    public class ShiftViewModel
    {
        public int ShiftId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ShiftTitle { get; set; }
    }

    public class ProcedureViewModel
    {
        public int IsolatorProcedureId { get; set; }
        public string Description { get; set; }
        public int ProcedureTypeId { get; set; }
        public SelectList ProcedureTypeList { get; set; }
    }
}
