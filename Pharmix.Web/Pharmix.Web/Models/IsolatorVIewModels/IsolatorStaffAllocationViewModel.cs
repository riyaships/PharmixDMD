using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pharmix.Web.Models.IsolatorVIewModels
{
    public static class PharmixConstents
    {
        public const string LineColorYellow = "#e6ff00";
    }
    public class IsolatorStaffAllocationIndexViewModel
    {
        public int IsolatorId { get; set; }
        public string IsolatorName { get; set; }
        public SelectList IsolatorList { get; set; }
    }
}
