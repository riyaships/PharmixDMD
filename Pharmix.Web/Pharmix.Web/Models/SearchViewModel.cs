using Pharmix.Data.Entities.ViewModels;

namespace Pharmix.Web.Models
{
    public class SearchViewModel
    {   public string SearchText { get; set; }
        public GridViewModel Grid { get; set; }
        public bool IsModelEditable { get; set; }
    }
}
