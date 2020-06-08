using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Services.Mappers
{
    public static class BusinessMapper
    {
        public static GridViewModel CreateGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "GridServiceResult",
                PagingRoute = "User",
                PagingAction = "search"
            };
           
            gridModel.AddColumn("Business Name", true, "Abbriviation");
            gridModel.AddColumn("Contact Person", true, "Abbriviation");
            gridModel.AddColumn("Contact Email", true, "Abbriviation");
            gridModel.AddColumn("Contact Phone", true, "Abbriviation");
            gridModel.AddColumn("City", true, "Abbriviation");
            gridModel.AddColumn("Postcode", true, "Abbriviation");
            gridModel.AddColumn("Actions");
            return gridModel;
        }

        public static GridRow BindGridData(BusinessDetails source)
        {
            var row = new GridRow { IdentityValue = source.Id };           
            row.AddCell(source.BusinessName);
            row.AddCell(source.ContactPerson);
            row.AddCell(source.ContactEmail);
            row.AddCell(source.ContactPhone);
            row.AddCell(source.City);
            row.AddCell(source.Postcode);
            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-trash text-danger", "Click to delete");
            row.AddActionIcon("fa fa-cog text-info", "Click to add/update business user");
            return row;
        }

    }
}
