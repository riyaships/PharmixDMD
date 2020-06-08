using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pharmix.Data.Entities.Context;

namespace Pharmix.Web.Entities
{
    [Table("IntegrationOrder", Schema = "order")]
    public class IntegrationOrder : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntegrationOrder()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string ExternalOrderId { get; set; }
        public string ExternalBarcode { get; set; }

        public DateTime? BookedInDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? AdministeredDate { get; set; }
        public int RequiredPreperationTimeInMins { get; set; }
        public int PriorityId { get; set; }
        public int OrderLastProgressId { get; set; }

        public int? OrderlastClassificationId { get; set; }
        [ForeignKey("OrderlastClassificationId")]
        public virtual IntegrationOrderClassification IntegrationOrderClassification { get; set; }

        public int AllocatedIsolatorId { get; set; }

        public int IntegratedSystemId { get; set; }
        [ForeignKey("IntegratedSystemId")]
        public virtual IntegratedSystem IntegratedSystem { get; set; }
    }

    [Table("IntegrationOrderTracking", Schema = "order")]
    public class IntegrationOrderTracking : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntegrationOrderTracking()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IntegrationOrderId { get; set; }
        [ForeignKey("IntegrationOrderId")]
        public virtual IntegrationOrder IntegrationOrder { get; set; }

        public int? OrderLastProgressId { get; set; }
        [ForeignKey("OrderLastProgressId")]
        public virtual IntegrationOrderProgress IntegrationOrderProgress { get; set; }

        public int? OrderCurrentLocationId { get; set; }
        [ForeignKey("OrderCurrentLocationId")]
        public virtual IntegrationOrderLocation IntegrationOrderLocation { get; set; }

        public int? OrderLastClassificationId { get; set; }
        [ForeignKey("OrderLastClassificationId")]
        public virtual IntegrationOrderClassification IntegrationOrderClassification { get; set; }

        public string Comment { get; set; }
    }

    [Table("IntegrationOrderLocation", Schema = "order")]
    public class IntegrationOrderLocation : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntegrationOrderLocation()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string LocationCode { get; set; }
    }

    [Table("OrderTrackingDevice", Schema = "order")]
    public class OrderTrackingDevice : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderTrackingDevice()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Serial { get; set; }
        public string Description { get; set; }
        public DateTime? LastHeartBeat { get; set; }

        public int? LastLocationId { get; set; }
        [ForeignKey("LastLocationId")]
        public virtual IntegrationOrderLocation LastLocation { get; set; }

        public decimal? BatteryPercent { get; set; }
        public DateTime? LastCharged { get; set; }
    }

    [Table("IntegrationOrderProgress", Schema = "order")]
    public class IntegrationOrderProgress : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntegrationOrderProgress()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }


    [Table("IntegratedSystem", Schema = "order")]
    public class IntegratedSystem : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntegratedSystem()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Type2: Systems that requires execution of a stored procedure against another database.
        /// </summary>
        public string Type1ConnectionString { get; set; }
        public string Type1StoredProcedureName { get; set; }
        public string Type1ParameterString { get; set; }

        /// <summary>
        /// Type2: Systems that requires API consumption for downloading Orders.
        /// Systems might require API call for progress updates
        /// </summary>
        public string Type2ServiceUrl { get; set; }
        public int Type2QueryString { get; set; }
        public string Type2ResultTemplate { get; set; }
        /// <summary>
        /// Name of the parameter that identifies their order back field name
        /// </summary>
        public string OrderNumberFieldName { get; set; }
        /// <summary>
        /// Current Order progress as a string field name
        /// </summary>
        public string OrderStatusFieldName { get; set; }
    }

    [Table("IntegrationOrderClassification", Schema = "order")]
    public class IntegrationOrderClassification : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntegrationOrderClassification()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}