using Pharmix.Web.Entities;
using Pharmix.Web.Models;

namespace Pharmix.Services.Mappers
{
    public static class PackagePlanMapper
    {
        public static GridViewModel CreateGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "GridServiceResult",
                PagingRoute = "PackagePlan",
                PagingAction = "search"
            };
            gridModel.AddColumn("Package Name", true, "PackageName");
            gridModel.AddColumn("Subscription Frequency", true, "Duration");
            gridModel.AddColumn("Price", true, "Price"); 
            gridModel.AddColumn("Actions");

            return gridModel;
        }

        public static GridRow BindGridData(PackagePlan source)
        {
            var row = new GridRow { IdentityValue = source.Id };
            row.AddCell(source.PackageName);
            row.AddCell(source.Duration);
            row.AddCell(source.Price.ToString("#,##0.00")); 
            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-trash text-danger", "Click to delete");
            return row;
        }
    }
}