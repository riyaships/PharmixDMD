using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.ViewComponents
{
    [ViewComponent(Name = "Menu")]
    public class MenuViewComponent : ViewComponent
    {
        private readonly IModuleService _moduleService;
        #region Constructor

        public MenuViewComponent(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                var permissionViewModelList = _moduleService.GetAvailableMenuByUserName(User.Identity.Name);
                return View(permissionViewModelList);
            }
            else
                return View(new List<PermissionViewModel>() );
        }

    }
}
