using Pharmix.Data.Entities.Context;
using System.ComponentModel.DataAnnotations;

namespace Pharmix.Web.Entities
{
    public class PackagePlan : BaseEntity
    {
        [Key]
        public int Id { get; set; }       
        public string PackageName { get; set; }
        public decimal Price { get; set; } 
        public string Duration { get; set; }
        public string Details { get; set; } 
    }
}
