using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pharmix.Data.Entities.Context;

namespace Pharmix.Web.Entities
{
    [Table("Addresses")]
    public class Address : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        [StringLength(250)]
        public string Address1 { get; set; }
        [StringLength(250)]
        public string Address2 { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(100)]
        public string Zip { get; set; }
        public bool IsPrimary { get; set; }

        public int AddressTypeId { get; set; }
        [ForeignKey("AddressTypeId")]
        public virtual AddressType AddressType { get; set; }
        
    }
    [Table("AddressType")]
    public class AddressType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
