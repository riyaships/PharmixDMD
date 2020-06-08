using Pharmix.Data.Entities.Context;
using Pharmix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Services.Mappers
{
    public static class GroupMapper
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

        public static GridRow BindGridData(Group source)
        {
            var row = new GridRow { IdentityValue = source.Id };
            row.AddCell(source.Name);
            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-trash text-danger", "Click to delete");
            row.AddActionIcon("fa fa-cog text-info managePermission", "Assign Permissions", "/PermissionGroup/ManagePermission/"+source.Id);
            return row;
        }
    }
}
