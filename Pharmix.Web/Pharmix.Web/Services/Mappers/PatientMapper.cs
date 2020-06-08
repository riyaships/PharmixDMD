using Pharmix.Data.Entities.Context;
using Pharmix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Services.Mappers
{
    public class PatientMapper
    {
        public static GridViewModel CreateGridViewModel()
        {
            var gridModel = new GridViewModel
            {
                GridName = "divPatientGrid",
                PagingRoute = "Patient",
                PagingAction = "search"
            };
            gridModel.AddColumn("First Name", true, "FirstName");
            gridModel.AddColumn("Surname", true, "Surname");
            gridModel.AddColumn("Email", true, "Email");
            gridModel.AddColumn("Phone", true, "Phone");
            gridModel.AddColumn("Day of Birth", true, "DOB");
            gridModel.AddColumn("Actions");
            return gridModel;
        }

        public static GridRow BindGridData(Patient source)
        {
            var row = new GridRow { IdentityValue = source.Id };
            row.AddCell(source.FirstName);
            row.AddCell(source.Surname);
            row.AddCell(source.EmailAddress);
            row.AddCell(source.MobileNumber);
            row.AddCell(source.DateOfBirth == null ? "" : ((DateTime)source.DateOfBirth).ToString("dd/MM/yyyy"));

            row.AddActionIcon("fa fa-file-text text-success", "Click to view/edit");

            return row;
        }
    }
}
