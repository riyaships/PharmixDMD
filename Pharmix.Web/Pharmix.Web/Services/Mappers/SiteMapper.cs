using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.Context;
using Pharmix.Web.Models;

namespace Pharmix.Services.Mappers
{
    public static class SiteMapper
    {
        public static GridViewModel CreateGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "GridServiceResult",
                PagingRoute = "Sites",
                PagingAction = "search"
            };
            gridModel.AddColumn("Site Name", true, "Abbriviation");
            gridModel.AddColumn("Actions");

            return gridModel;
        }

        public static GridRow BindGridData(Site source)
        {
            var row = new GridRow { IdentityValue = source.Id };
            row.AddCell(source.Name);
            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-trash text-danger", "Click to delete");
            return row;
        }
    }
}
