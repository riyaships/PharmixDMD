using Pharmix.Data.Entities.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Entities
{
    [Table("Pregnancy", Schema = "PREG")]
    public class Pregnancy : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(25)]
        public string NHSNumber { get; set; }

        [MaxLength(25)]
        public string MaternityUnit { get; set; }

        public DateTime? EDD { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

    }

    [Table("CommunicationNeed", Schema = "PREG")]
    public class CommunicationNeed : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool AssistanceRequired { get; set; }

        public string AssistanceRequiredDetail { get; set; }

        public string PreferredAssistance { get; set; }

        public bool SpeakEnglish { get; set; }

        public string FirstLanguage { get; set; }

        public string PreferedLanguage { get; set; }

        public string InterpreterPhone { get; set; }

        public int PregnancyId { get; set; }
        [ForeignKey("PregnancyId")]
        public virtual Pregnancy Pregnancy { get; set; }
    }

    [Table("PlanOfCare", Schema = "PREG")]
    public class PlanOfCare : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string PlannedPlaceOfBirth { get; set; }

        public string LeadProfessional { get; set; }

        public string JobTitle { get; set; }

        public string ReasonIfChanged { get; set; }

        public int PregnancyId { get; set; }
        [ForeignKey("PregnancyId")]
        public virtual Pregnancy Pregnancy { get; set; }
    }

    [Table("MaternityContact", Schema = "PREG")]
    public class MaternityContact : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Midwife { get; set; }

        public string MidwifePhone { get; set; }

        public string MaternityUnit { get; set; }

        public string MaternityUnitPhone { get; set; }

        public string AntenatalClinicPhone { get; set; }

        public string CommunityOfficePhone { get; set; }

        public string DeliverySuitePhone { get; set; }

        public string AmbulancePhone { get; set; }

        public int PregnancyId { get; set; }
        [ForeignKey("PregnancyId")]
        public virtual Pregnancy Pregnancy { get; set; }
    }

    [Table("PrimaryCareContact", Schema = "PREG")]
    public class PrimaryCareContact : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Centre { get; set; }

        public string GPInitial { get; set; }

        public string GPSurename { get; set; }

        public string GPPostcode { get; set; }

        public string PhoneNumber1 { get; set; }

        public string PhoneNumber2 { get; set; }

        public string PhoneNumber3 { get; set; }

        public string PhoneNumber4 { get; set; }

        public int PregnancyId { get; set; }
        [ForeignKey("PregnancyId")]
        public virtual Pregnancy Pregnancy { get; set; }
    }

    [Table("NextOfKin", Schema = "PREG")]
    public class NextOfKin : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Relaton { get; set; }

        public int PregnancyId { get; set; }
        [ForeignKey("PregnancyId")]
        public virtual Pregnancy Pregnancy { get; set; }
    }

    [Table("EmergencyContact", Schema = "PREG")]
    public class EmergencyContact : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber1 { get; set; }

        public string PhoneNumber2 { get; set; }

        public int PregnancyId { get; set; }
        [ForeignKey("PregnancyId")]
        public virtual Pregnancy Pregnancy { get; set; }
    }
}
