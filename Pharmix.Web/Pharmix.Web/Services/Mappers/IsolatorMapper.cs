using Pharmix.Data.Enums;
using Pharmix.Web.Entities;
using Pharmix.Web.Models;

namespace Pharmix.Web.Services.Mappers
{
    public static class IsolatorMapper
    {
        public static GridBoxViewModel CreateGridViewModel()
        {
            var gridModel = new GridBoxViewModel()
            {
                GridName = "GridBoxIsoResult",
                PagingRoute = "isolator",
                PagingAction = "search",
                BackgroundIconCss = "fa fa-laptop"
            };
         
            return gridModel;
        }

        public static GridBoxRow BindGridData(Isolator source)
        {
            var row = new GridBoxRow
            {
                IdentityValue = source.IsolatorId,
                BoxHeading = source.Abbriviation
            };
            
            row.BoxBodyDictionary.Add("Total no. of doses per session", source.TotalNumberOfDosesPerSession.ToString());

            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-trash text-danger", "Click to delete");
            row.AddActionIcon("fa fa-calendar-times-o text-warning", "Click to manage isolator offline dates");
            row.AddActionIcon("fa fa-list text-info", "Click to manage isolator start/stop procedures");
            row.AddActionIcon("fa fa-users text-primary", "Click to manage staff allocation", "/isolator/isolatorstaffallocationindex?isoid=" + source.IsolatorId + "&nm=" + source.Abbriviation + "");

            return row;
        }

        public static GridBoxViewModel CreateOrderGridViewModel()
        {
            var gridModel = new GridBoxViewModel()
            {
                GridName = "GridBoxOrderResult",
                PagingRoute = "isolator",
                PagingAction = "ordersearch",
                BackgroundIconCss = "fa fa-medkit",
                IsBoxDraggable = true,
                ShowBodyContentOnHover = true,
            };

            return gridModel;
        }

        public static GridBoxRow BindOrderGridData(IntegrationOrder source)
        {
            var row = new GridBoxRow
            {
                IdentityValue = source.Id,
                BoxHeading = source.Name
            };

            row.DataAttriDictionary.Add("reqtime", source.RequiredPreperationTimeInMins.ToString());

            row.BoxBodyDictionary.Add("Req. prepration time", source.RequiredPreperationTimeInMins + " mins");
            row.BoxBodyDictionary.Add("Req. date", source.RequiredDate.HasValue? source.RequiredDate.Value.ToString("dd/MM/yyyy") : "");
            row.BoxBodyDictionary.Add("Scheduled date", source.ScheduledDate.HasValue? source.ScheduledDate.Value.ToString("dd/MM/yyyy") : "");

            return row;
        }

        public static GridViewModel CreateShiftGridViewModel()
        {
            var gridModel = new GridViewModel()
            {
                GridName = "GridSearchResult",
                PagingRoute = "lookup",
                PagingAction = "searchshift",
            };

            gridModel.AddColumn("Shift Title", true, "ShiftTitle");
            gridModel.AddColumn("Shift Start Time", true, "StartTime");
            gridModel.AddColumn("Shift End Time", true, "EndTime");
            gridModel.AddColumn("Actions");

            return gridModel;
        }

        public static GridRow BindShiftGridData(IsolatorShift source)
        {
            var row = new GridRow
            {
                IdentityValue = source.ShiftId,
            };

            row.AddCell(source.ShiftTitle);
            row.AddCell(source.StartTime);
            row.AddCell(source.EndTime);

            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-trash text-danger", "Click to delete");

            return row;
        }

        public static GridViewModel CreateProcedureGridViewModel()
        {
            var gridModel = new GridViewModel()
            {
                GridName = "GridSearchResult",
                PagingRoute = "lookup",
                PagingAction = "searchprocedure",
            };

            gridModel.AddColumn("Procedure", true, "Description");
            gridModel.AddColumn("Type", true, "ProcedureTypeId");
            gridModel.AddColumn("Actions");

            return gridModel;
        }

        public static GridRow BindProcedureGridData(IsolatorProcedure source)
        {
            var row = new GridRow
            {
                IdentityValue = source.IsolatorProcedureId,
            };

            row.AddCell(source.Description);
            row.AddCell(((IsolatorProcedureTypeEnum)source.ProcedureTypeId).ToString());
            
            row.AddActionIcon("fa fa-edit text-success", "Click to view/edit");
            row.AddActionIcon("fa fa-trash text-danger", "Click to delete");

            return row;
        }
    }
}
