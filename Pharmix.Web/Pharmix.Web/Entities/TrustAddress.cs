using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmix.Web.Entities
{
    [Table("TrustAddresses", Schema = "trust")]
    public class TrustAddress
    {
        public long Id { get; set; }
        public int  TrustId { get; set; }
        [ForeignKey("TrustId")]
        public virtual Trust Trust { get; set; }
        public long AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
    }
}
