using Pharmix.Web.Models;
using System;
using System.Linq;
using Pharmix.Data.Enums;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Services.Mappers
{
    public class IntegrationOrderMapper
    {
        public static GridViewModel CreateGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "GridServiceResult",
                PagingRoute = "IntegrationOrder",
                PagingAction = "search"
            };
            gridModel.AddColumn("Name", true, "Name");
            gridModel.AddColumn("AllocatedIsolator", true, "AllocatedIsolator");
            gridModel.AddColumn("BookedInDate", true, "BookedInDate");
            gridModel.AddColumn("IntegrationProgress", true, "IntegrationProgress");
            //gridModel.AddColumn("ExternalBarcode", true, "ExternalBarcode");
            gridModel.AddColumn("RequiredDate", true, "RequiredDate");
            gridModel.AddColumn("ScheduledDate", true, "ScheduledDate");
            gridModel.AddColumn("Actions");

            return gridModel;
        }

        public static GridRow BindGridData(IntegrationOrder source)
        {
            var row = new GridRow { IdentityValue = source.Id };
            row.AddCell(source.Name);
            row.AddCell(source.AllocatedIsolatorId.ToString());
            row.AddCell(source.BookedInDate == null ? "" : ((DateTime)source.BookedInDate).ToString("dd/MM/yyyy"));
            row.AddCell(((OrderProgressEnum)source.OrderLastProgressId).ToString());
            //row.AddCell(source.ExternalBarcode);
            //row.AddCell(source.ExternalOrderId);
            row.AddCell(source.RequiredDate == null ? "" : ((DateTime)source.RequiredDate).ToString("dd/MM/yyyy"));
            row.AddCell(source.ScheduledDate == null ? "" : ((DateTime)source.ScheduledDate).ToString("dd/MM/yyyy"));

            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-info-circle text-success compareVersion", "Compare version");

            //if(!source.IntegrationOrderProgress.Name.Contains("Decline", StringComparison.OrdinalIgnoreCase))
            if (source.OrderLastProgressId == null)
            {
                row.AddActionIcon("fa fa-check text-success", "Click to authorize request");
                row.AddActionIcon("fa fa-times text-danger", "Click to decline request");
                row.AddActionIcon("fa fa-commenting-o text-default", "Click to call for supervisor");
                row.AddActionIcon("fa fa-object-group text-default", "Click to classify order");
            }
            return row;
        }

        public static GridViewModel CreateUsedProdGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "UsedProdGridResult",
                PagingRoute = "GetAllUsedProductsForOrder",
                PagingAction = "usedproduct"
            };
            gridModel.AddColumn("Drug Name", true);
            gridModel.AddColumn("Total Dose Amount");
            gridModel.AddColumn("Avail. Dose Amount");
            gridModel.AddColumn("Storage Location");
            gridModel.AddColumn("Actions");

            return gridModel;
        }

        public static GridRow BindUsedProdGridData(UsedProduct source)
        {
            var row = new GridRow { IdentityValue = source.UsedProductId };
            row.AddCell(source.Vtm.DrugName);
            row.AddCell(string.Format("{0} {1}", source.DoseAmountSize, source.DoseMeasurementUnit));

            var stock = source.Stocks.FirstOrDefault();

            row.AddCell(string.Format("{0} {1}", stock == null? 0 : stock.DoseAmountSizeAvailable, source.DoseMeasurementUnit));
            row.AddCell(stock == null? "N/A" : stock.StorageLocation.LocationName);

            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-trash text-danger", "Click to remove this item");
            return row;
        }

        public static GridViewModel CreateVtmGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "VtmGridResult",
                PagingRoute = "searchvtm",
                PagingAction = "usedproduct"
            };
            gridModel.AddColumn("Drug Name", true, "DrugName");
            gridModel.AddColumn("Route");
            gridModel.AddColumn("Supplier");
            gridModel.AddColumn("Brand");
            gridModel.AddColumn("Licensed");
            gridModel.AddColumn("Actions");

            return gridModel;
        }

        public static GridRow BindVtmGridData(Vtm source)
        {
            var row = new GridRow { IdentityValue = source.VtmId };
            row.AddCell(source.DrugName);
            row.AddCell(source.DmdRoute.Route);
            row.AddCell(source.DmdSupplier.SupplierName);
            row.AddCell(source.BrandName);
            if (source.IsLicensed)
            {
                row.AddCell("Yes");
            }
            else
            {
                row.AddCell("No", cellCss:"text-danger");
            }

            if (source.IsLicensed)
            {
                row.AddActionIcon("fa fa-ban text-danger", "Licensed proudct can not be modified");
                return row;
            }

            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-trash text-danger", "Click to remove this item");
            return row;
        }
    }
}
