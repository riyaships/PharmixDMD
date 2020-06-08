using System.ComponentModel.DataAnnotations.Schema;
using Pharmix.Data.Entities.Context;

namespace Pharmix.Web.Entities
{
    [Table("TrustContacts", Schema = "trust")]
    public class TrustContact
    {
        public long Id { get; set; }
        public int  TrustId { get; set; }
        [ForeignKey("TrustId")]
        public virtual Trust Trust { get; set; }
        public long ContactId { get; set; }
        [ForeignKey("ContactId ")]
        public virtual Contact Contact { get; set; }
    }
}
