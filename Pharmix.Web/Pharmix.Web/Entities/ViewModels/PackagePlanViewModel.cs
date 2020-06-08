using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pharmix.Web.Entities.ViewModels
{
    public class PackagePlanViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} is required.")]
        [DisplayName("Package Name")]
        public string PackageName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [DisplayName("Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [DisplayName("Subscription Frequency")] 
        public string Duration { get; set; }

        [DisplayName("Details")]
        public string Details { get; set; } 
    }
}
