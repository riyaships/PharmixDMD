namespace Pharmix.Data.Entities.Context
{
    using Pharmix.Web.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Patient")]
    public class Patient : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int? GenderId { get; set; }
        
        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool? RequiresPasswordReset { get; set; }
      
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        public string NhsNumber { get; set; }

        public string PasNumber { get; set; }

        [StringLength(200)]
        public string EmailAddress { get; set; }

        [StringLength(25)]
        public string MobileNumber { get; set; }

        [StringLength(25)]
        public string AlternativeTel { get; set; }

        [StringLength(500)]
        public string AddressLine1 { get; set; }

        [StringLength(500)]
        public string AddressLine2 { get; set; }

        [StringLength(500)]
        public string AddressLine3 { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [StringLength(200)]
        public string County { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }

        public DateTime RegisteredDate { get; set; }

        public DateTime? LastVisitedDate { get; set; }

        public String IdNumber { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string GetDisplayName()
        {
            return string.Format("{0}, {1}", Surname, FirstName);
        }

        private string GetCommaSeperatedValue(string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : string.Format(", {0}", input);
        }

        public string GetFullAddress()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}", AddressLine1, GetCommaSeperatedValue(AddressLine2), GetCommaSeperatedValue(AddressLine3),
                GetCommaSeperatedValue(City), GetCommaSeperatedValue(County), GetCommaSeperatedValue(Postcode));
        }
    }
}


