using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Services
{
    
    public interface IModuleService
    {
        IEnumerable<Module> GetAllModules();
        //List<UserModuleViewModel> GetModulesByUser(string userId);
        //void UpdateUserModule(UserViewModel userViewModel);
        List<PermissionViewModel> GetAvailableMenuByUserName(string userName);
        List<string> GetAvailablePermissionsByUserName(string userName);
        TrustViewModel GetTrustModules(int trustId);
        Task UpdateTrustModule(TrustViewModel trustViewModel);
    }
}
