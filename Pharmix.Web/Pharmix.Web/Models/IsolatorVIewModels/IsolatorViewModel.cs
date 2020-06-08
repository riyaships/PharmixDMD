using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Enums;

namespace Pharmix.Web.Models.IsolatorVIewModels
{
    public class IsolatorViewModel
    {
        public int IsolatorId { get; set; }
        public string Abbriviation { get; set; }
        public int TotalNumberOfDosesPerSession { get; set; }
        public decimal? MinIsolatorPressure { get; set; }
        public decimal? MaxIsolatorPressure { get; set; }
        public int? IsolatorPressureUnit { get; set; }
        public int? OperationType { get; set; }
        public decimal? RadiiOfIsolatorChamber { get; set; }
        public decimal? RequiredChamberGradient { get; set; }
        public bool TightnessTestRequired { get; set; }
        public bool GloveTestRequired { get; set; }
        public DateTime? LastHEPAFilterVerifiedDate { get; set; }
        public decimal? LatestPressure { get; set; }
        public int? LatestPressureUnit { get; set; }
        public decimal? LatestTemperature { get; set; }
        public int? LatestTemperatureUnit { get; set; }
        public decimal? LatestHumidity { get; set; }
        public int? LatestHumidityUnit { get; set; }
        public DateTime? LastVHPSterilisationDate { get; set; }
        public SelectList OperationTypeList { get; set; }
        public SelectList PressureUnitList { get; set; }
        public SelectList LatestPressureUnitList { get; set; }
        public SelectList TemperatureUnitList { get; set; }
        public SelectList HumidityUnitList { get; set; }
    }

    public class IsolatorOfflineViewModel
    {
        public int IsolatorId { get; set; }
        public string IsolatorName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate {get; set; }
        public int[] SelectedShifts { get; set; }
        public MultiSelectList ShiftList { get; set; }
        public bool AllShifts { get; set; }
    }

    public class IsolatorProcedureViewModel
    {
        public int IsolatorId { get; set; }
        public string IsolatorName { get; set; }
        public string ProductionDate { get; set; }
        public IsolatorProcedureTypeEnum ProcedureType { get; set; }
        public int[] SelectedProcedures { get; set; }
        public MultiSelectList ProcedureList { get; set; }
    }

    public class IsolatorMappedProcedureViewModel
    {
        public int IsolatorId { get; set; }
        public string IsolatorName { get; set; }
        public int[] StartProcedures { get; set; }
        public MultiSelectList StartProcedureList { get; set; }
        public int[] StopProcedures { get; set; }
        public MultiSelectList StopProcedureList { get; set; }
    }
}