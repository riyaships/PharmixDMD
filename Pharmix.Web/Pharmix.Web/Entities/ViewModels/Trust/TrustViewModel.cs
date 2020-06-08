using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Entities.ViewModels
{
    public class TrustViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] LogoImage { get; set; }
        public string LogoImageName { get; set; }
        public string MondayOpenTiming { get; set; }
        public string TuesdayOpenTiming { get; set; }
        public string WednesdayOpenTiming { get; set; }
        public string ThursdayOpenTiming { get; set; }
        public string FridayOpenTiming { get; set; }
        public string SaturdayOpenTiming { get; set; }
        public string SundayOpenTiming { get; set; }
        public List<TrustModuleViewModel> TrustModuleViewModelList { get; set; }
    }
}
