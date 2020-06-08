using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pharmix.Data.Entities.Context;

namespace Pharmix.Web.Entities
{
    [Table("Module", Schema = "trust")]
    public class Module : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public virtual ICollection<TrustModule> TrustModules { get; set; } = new HashSet<TrustModule>();
    }
    [Table("TrustModule", Schema = "trust")]
    public class TrustModule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ModuleId { get; set; }
        [ForeignKey("ModuleId")]
        public virtual Module Module { get; set; }
        public int TrustId { get; set; }
        [ForeignKey("TrustId")]
        public virtual Trust Trust { get; set; }
    }

    //[Table("UserModule")]
    //public class UserModule
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    public int ModuleId { get; set; }
    //    [ForeignKey("ModuleId")]
    //    public virtual Module Module { get; set; }
    //    public string  UserId { get; set; }
    //    public virtual ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();

    //}

    [Table("Permission", Schema = "trust")]
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public int? ModuleId { get; set; }
        [ForeignKey("ModuleId")]
        public virtual Module Module { get; set; }
        [Required]
        [StringLength(200)]
        public string Key { get; set; }
        public int ParentPermissionId { get; set; }
        [Required]
        [StringLength(200)]
        public string Url { get; set; }
        public bool? IsShowMenu { get; set; }
        public int? Sort { get; set; }
        public string DisplayName { get; set; }
        public virtual ICollection<PermissionGroup> PermissionGroups { get; set; } = new HashSet<PermissionGroup>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
        public string CssClass { get; set; }

    }
    [Table("PermissionGroup", Schema = "trust")]
    public class PermissionGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //public string Name { get; set; }
        public int PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        //public virtual ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
    }

    [Table("RolePermission", Schema = "trust")]
    public class RolePermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string RoleId { get; set; }
        public int? PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }

        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
    }

    [Table("Group", Schema = "trust")]
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int TrustId { get; set; }
        [ForeignKey("TrustId")]
        public virtual Trust Trust { get; set; }
        public virtual ICollection<PermissionGroup> PermissionGroups { get; set; } = new HashSet<PermissionGroup>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();

    }
}
