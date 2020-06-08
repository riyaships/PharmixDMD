using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models.ManageViewModels;

namespace Pharmix.Data.Entities.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string PasswordResetToken { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
        public int TempId { get; set; }
        public string   Name { get; set; }
        public string   FirstName { get; set; }
        public string SurName { get; set; }
        public string Surname { get; set; }
        public string MobileNumber { get; set; }

        public List<int> TrustIds { get; set; }  
        public int TrustId { get; set; }    //Temp. Currently one to one mapping. Later to list
        public List<TrustViewModel> TrustViewModelList { get; set; }

        public List<UserModuleViewModel> UserModuleViewModelList { get; set; }

        public List<UserRoleViewModel> UserRoleViewModelList { get; set; }

    }

    public class RoleViewModel
    {
        public string Id { get; set; }
        public  string Name { get; set; }
        public string NormalizedName { get; set; }
        public int TempId { get; set; }

        public List<GroupViewModel> GroupViewModelList { get; set; }

        public List<RolePermissionViewModel> RolePermissionViewModelList { get; set; }
    }

    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool  IsSelected { get; set; }
    }

    public class PermissionViewModel
    {
        public int Id { get; set; }
        public string   Name{ get; set; }
        public string Key{ get; set; }
        public int ModuleId{ get; set; }
        public int ParentPermissionId{ get; set; }
        public string Url{ get; set; }
        public int Sort { get; set; }
        public bool IsShowMenu { get; set; }
        public bool IsSelected { get; set; }
        public int GroupId { get; set; }
        public string CssClass { get; set; }
    }

    public class RolePermissionViewModel
    {
        public int? Id { get; set; }
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsHaveAccess { get; set; }
    }
}
