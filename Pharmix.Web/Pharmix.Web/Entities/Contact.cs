namespace Pharmix.Data.Entities.Context
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Contacts")]
    public class Contact : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        [StringLength(100)]
        public string ContactType { get; set; }
        [StringLength(50)]
        public string Phone1 { get; set; }
        [StringLength(50)]
        public string Phone2 { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public bool IsPrimary { get; set; }
    }
}
