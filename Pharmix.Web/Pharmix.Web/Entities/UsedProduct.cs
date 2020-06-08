using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pharmix.Data.Entities.Context;

namespace Pharmix.Web.Entities
{
    [Table("UsedProduct", Schema = "prod")]
    public class UsedProduct : BaseEntity
    {
        [Key]
        public int UsedProductId { get; set; }
        public string CustomName { get; set; }
        public string Description { get; set; }
        public int VtmId { get; set; }
        [ForeignKey("VtmId")]
        public Vtm Vtm { get; set; }
        public decimal? DoseAmountSize { get; set; }
        public string DoseMeasurementUnit { get; set; }
        public decimal? ConcentrationDosePerMl { get; set; }
        public virtual ICollection<UsedProductStock> Stocks { get; set; } = new HashSet<UsedProductStock>();
    }

    [Table("UsedProductStock", Schema = "prod")]
    public class UsedProductStock : BaseEntity
    {
        [Key]
        public int UsedProductStockId { get; set;}
        public int IntegrationOrderId { get; set; }
        [ForeignKey("IntegrationOrderId")]
        public virtual IntegrationOrder IntegrationOrder { get; set; }
        public int IsolatorId { get; set; }
        [ForeignKey("IsolatorId")]
        public virtual Isolator Isolator { get; set; }
        public int UsedProductId { get; set; }
        [ForeignKey("UsedProductId")]
        public virtual UsedProduct UsedProduct { get; set; }
        public int StorageLocationId { get; set; }
        [ForeignKey("StorageLocationId")]
        public virtual StorageLocation StorageLocation { get; set; }
        public int StockStatusId { get; set; }
        [ForeignKey("StockStatusId")]
        public virtual StockStatus StockStatus { get; set; }
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

    [Table("StorageLocation", Schema = "prod")]
    public class StorageLocation : BaseEntity
    {
        [Key]
        public int StorageLocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationCode { get; set; }
        public string Description { get; set; }
        public decimal? LastTemperature { get; set; }
        public decimal? LastHumidity { get; set; }
        public string SensorIdentifier { get; set; }
        public DateTime? SensorInstalledDate { get; set; }
    }

    [Table("StockEvidence", Schema = "prod")]
    public class StockEvidence : BaseEntity
    {
        [Key]
        public int StockEvidenceId { get; set; }
        public int UsedProductStockId { get; set; }
        [ForeignKey("UsedProductStockId")]
        public UsedProductStock UsedProductStock { get; set; }
        public string FileTitle { get; set; }
        public string FileContent { get; set; }
        public string FileExtension { get; set; }
        public string FileAddedBy { get; set; }
        public DateTime? FileAddedDate { get; set; }
        public string Notes { get; set; }
    }
    
    [Table("VTM", Schema = "prod")]
    public class Vtm : BaseEntity
    {
        [Key]
        public int VtmId { get; set; }
        public string DrugName { get; set; }
        public long? DmdId { get; set; }
        public int? DmdSupplierId { get; set; }
        [ForeignKey("DmdSupplierId")]
        public DmdSupplier DmdSupplier { get; set; }
        public int? DmdRouteId { get; set; }
        [ForeignKey("DmdRouteId")]
        public DmdRoute DmdRoute { get; set; }
        public string BrandName { get; set; }
        //public long? AmppId { get; set; }
        //public string AmppName { get; set; }
        public bool IsLicensed { get; set; }
    }

    [Table("StockStatus", Schema = "prod")]
    public class StockStatus
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }

    [Table("DMDSupplier", Schema = "prod")]
    public class DmdSupplier
    {
        [Key]
        public int Id { get; set; }
        public string SupplierName { get; set; }
    }

    [Table("DMDRoute", Schema = "prod")]
    public class DmdRoute
    {
        [Key]
        public int Id { get; set; }
        public string Route { get; set; }
    }
}
