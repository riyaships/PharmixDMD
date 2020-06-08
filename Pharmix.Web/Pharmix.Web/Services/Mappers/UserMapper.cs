using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Services.Mappers
{
    public static class UserMapper
    {
        public static GridViewModel CreateGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "GridServiceResult",
                PagingRoute = "User",
                PagingAction = "search"
            };
            gridModel.AddColumn("User Name", true, "Abbriviation");
            gridModel.AddColumn("First Name", true, "Abbriviation");
            gridModel.AddColumn("Surname", true, "Abbriviation");
            gridModel.AddColumn("Mobile", true, "Abbriviation");
            gridModel.AddColumn("Actions");

            return gridModel;
        }

        public static GridRow BindGridData(UserViewModel source, bool isTrustAdmin)
        {
            var row = new GridRow { IdentityValue = source.TempId };
            row.AddCell(source.Email);
            row.AddCell(source.FirstName);
            row.AddCell(source.Surname);
            row.AddCell(source.MobileNumber);
            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-trash text-danger", "Click to delete");
            //if (isPharmixAdmin)
            //    row.AddActionIcon("fa fa-cog text-info manageModules", "Manage Modules", "/User/ManageModule/"+source.TempId);
            //else
            if (isTrustAdmin)
            {
                row.AddActionIcon("fa fa-user-secret changePassword", "Click to change password");
                row.AddActionIcon("fa fa-cog text-info manageRoles", "Manage Roles", "/User/ManageUserRole/" + source.TempId);
            }
            return row;
        }

        public static GridViewModel CreateRoleGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "GridServiceResult",
                PagingRoute = "User",
                PagingAction = "SearchRole"
            };
            gridModel.AddColumn("Name", true);
            gridModel.AddColumn("Actions");
            return gridModel;
        }


        public static GridRow BindRoleGridData(RoleViewModel source)
        {
            var row = new GridRow { IdentityValue = source.TempId };
            row.AddCell(source.Name);
            row.AddActionIcon("fa fa-cog text-info managePermission", "Manage Permission", "/User/ManagePermission/" + source.TempId);
            return row;
        }
    }
}
