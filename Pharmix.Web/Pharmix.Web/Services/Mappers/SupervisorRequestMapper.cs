using Pharmix.Data.Entities.Context;
using Pharmix.Data.Enums;
using Pharmix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Services.Mappers
{
    public class SupervisorRequestMapper
    {
        public static GridViewModel CreateGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "GridServiceResult",
                PagingRoute = "production",
                PagingAction = "search"
            };
            gridModel.AddColumn("Status", true, "LatestRequestStatus");
            gridModel.AddColumn("Title", true, "Title");
            gridModel.AddColumn("Priority", true, "Priority");
            gridModel.AddColumn("Isolator", true, "Isolator");
            //gridModel.AddColumn("Request Type", true, "RequestType");
            gridModel.AddColumn("Actions");

            return gridModel;
        }

        public static GridRow BindGridData(SupervisorRequest source, bool isSupervisorRequest)
        {
            var row = new GridRow { IdentityValue = source.Id };
            row.AddCell(Enum.GetName(typeof(RequestStatusEnum), source.LatestRequestStatus));
            row.AddCell(source.Title);
            row.AddCell(Enum.GetName(typeof(RequsetPriorityEnum), source.Priority));
            row.AddCell(source.Isolator.Abbriviation);
            //row.AddCell(source.RequestType.Type);
            if (isSupervisorRequest)
            {
                row.AddActionIcon("fa fa-search text-Primary", "Click to view detail");
                if (!source.LatestRequestStatus.Equals(RequestStatusEnum.Declined))
                {
                    row.AddActionIcon("fa fa-check text-success", "Click to approve request");
                    row.AddActionIcon("fa fa-times text-danger", "Click to decline request");
                }
            }
            else if(!source.LatestRequestStatus.Equals(RequestStatusEnum.Approved))
            {
                row.AddActionIcon("fa fa-edit text-Primary", "Click to edit request");
                row.AddActionIcon("fa fa-trash text-danger", "Click to delete request");
            }
           
            //row.AddActionIcon("fa fa-users text-primary", "Click to manage staff allocation", "/isolators/isolatorstaffallocationindex?isoid=" + source.IsolatorId + "&nm=" + source.Abbriviation + "");

            return row;
        }
    }
}
