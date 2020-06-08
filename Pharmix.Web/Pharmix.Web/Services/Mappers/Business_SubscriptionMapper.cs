using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using System;

namespace Pharmix.Services.Mappers
{
    public static class Business_SubscriptionMapper
    {
        public static GridViewModel CreateGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "GridServiceResult",
                PagingRoute = "Subscription",
                PagingAction = "search"
            }; 
            gridModel.AddColumn("Start Date", true, "StartDate");
            gridModel.AddColumn("End Date", true, "EndDate");
            gridModel.AddColumn("Amount", true, "Amount");
            gridModel.AddColumn("Status", true, "Status");
            gridModel.AddColumn("Payment Receipt", true, "PaymentReceipt");
            gridModel.AddColumn("Payment Date", true, "PaymentDate");
            gridModel.AddColumn("Payment Via", true, "PaymentVia");

            return gridModel;
        }

        public static GridRow BindGridData(Business_Subscription source)
        {
            var row = new GridRow { IdentityValue = source.Id }; 
            row.AddCell(source.StartDate.ToString("dd/MM/yyyy"));
            row.AddCell(source.EndDate.ToString("dd/MM/yyyy"));
            row.AddCell(source.Currency + "" + source.PaidAmount.ToString("#,##0.00"));
            row.AddCell(source.PaymentReceived ? "Paid" : "Pendinng");
            row.AddCell(source.PaymentReceipt);
            row.AddCell(Convert.ToDateTime(source.PaymentReceivedDate).ToString("dd/MM/yyyy"));
            row.AddCell(source.PaymentVia);
            return row;
        }
    }
}