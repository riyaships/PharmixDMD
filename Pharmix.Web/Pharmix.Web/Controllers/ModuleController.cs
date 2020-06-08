using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Services;

namespace Pharmix.Web.Controllers
{
    public class ModuleController : BaseController
    {
        private readonly IModuleService _moduleService;
        private UserManager<ApplicationUser> _userManager;
        private readonly ITrustService _trustService;
        private readonly IBusinessService businessService;
        public int _trustId = 1;
        public ModuleController(IModuleService moduleService, UserManager<ApplicationUser> userManager,
            ITrustService trustService, IBusinessService _businessService)
            :base(userManager, _businessService)
        {
            _moduleService = moduleService;
            _trustService = trustService;
            businessService = _businessService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Manage Trust Module

        public ViewResult ManageModule(int trustId=0)
        {

            if (trustId == 0)
            {
                var trusts=_trustService.GetAllTrusts();
                if (trusts != null && trusts.Count>0)
                    trustId = trusts[0].Id;
            }

            var trustViewModel = _moduleService.GetTrustModules(_trustId);
            return View(trustViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> ManageModule(TrustViewModel trustViewModel)
        {
             await _moduleService.UpdateTrustModule(trustViewModel);
            return RedirectToAction("ManageModule");
        }

        #endregion
    }
}