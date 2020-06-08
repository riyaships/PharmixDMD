using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Web.Models;

namespace Pharmix.Web.Entities.ViewModels.UsedProduct
{
    public class UsedProductListViewModel : SearchViewModel
    {
        public int IsolatorId { get; set; }
        public int IntegrationOrderId { get; set; }
        public string IntegrationOrderName { get; set; }

        //public UsedProductViewModel UsedProductModel = new UsedProductViewModel();
        public VtmViewModel VtmViewModel { get; set; } = new VtmViewModel();
    }

    public class UsedProductViewModel
    {
        public int UsedProductId { get; set; }
        public string CustomName { get; set; }
        public string Description { get; set; }
        public int VtmId { get; set; }
        public SelectList VtmList { get; set; }
        public decimal? DoseAmountSize { get; set; }
        public string DoseMeasurementUnit { get; set; }
        public decimal? ConcentrationDosePerMl { get; set; }
        public UsedProductStockViewModel Stock { get; set; } = new UsedProductStockViewModel();
    }

    public class UsedProductStockViewModel
    {
        public int UsedProductStockId { get; set; }
        public int UsedProductId { get; set; }
        public int IntegrationOrderId { get; set; }
        public string IntegrationOrderName { get; set; }
        public int IsolatorId { get; set; }
        public int StorageLocationId { get; set; }
        public SelectList StorageLocationList { get; set; }
        public decimal? DoseAmountSizeAvailable { get; set; }
        public DateTime? LastStoredDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string PackWeightWithLabel { get; set; }
        public string AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string Notes { get; set; }
    }

    public class VtmViewModel
    {
        public int VtmId { get; set; }
        public string DrugName { get; set; }
        public long? DmdId { get; set; }
        public int? DmdSupplierId { get; set; }
        public SelectList DmdSupplierList { get; set; }
        public int? DmdRouteId { get; set; }
        public SelectList DmdRouteList { get; set; }
        public string BrandName { get; set; }
        public bool IsLicensed { get; set; }
    }
}
