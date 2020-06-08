using Pharmix.Web.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Services
{
    public interface IRolePermissionService
    {
        List<ModuleViewModel> GetModules();
        List<RoleModuleViewModel> GetModulesByCriteria(List<string> roleIds = null, List<string> roleNames = null, int moduleId = 0, string moduleName = "");
        List<RoleModuleViewModel> GetAvailableModulesByRoles(List<string> roleIds);
        List<ModuleViewModel> GetDistinctModules();
        void UpdatePermission(AccessPermissionViewModel accessPermissionViewModel);
    }
}
