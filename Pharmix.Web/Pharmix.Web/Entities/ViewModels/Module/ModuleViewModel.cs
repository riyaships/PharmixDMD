using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Entities.ViewModels
{
    public class ModuleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public string Area { get; set; }
        //public string Controller { get; set; }
        //public string Action { get; set; }
        //public string Key { get; set; }
        //public bool IsHaveAccess { get; set; }


    }

    public class TrustModuleViewModel
    {
        public int Id { get; set; }
        public int TrustId { get; set; }
        public string TrustName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool IsHaveAccess { get; set; }
    }

    public class UserModuleViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool IsHaveAccess { get; set; }
    }

    public class RoleModuleViewModel
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string ModuleName { get; set; }
        public int ModuleId { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string Action { get; set; }
        public string Key { get; set; }
        public bool IsHaveAccess { get; set; }
    }
    public class AccessPermissionViewModel
    {
        public string RoleId { get; set; }
        public string ModuleName { get; set; }
        public List<RoleModuleViewModel> RoleModuleViewModelList { get; set; }
    }
}
