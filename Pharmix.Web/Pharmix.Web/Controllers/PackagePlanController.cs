using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using Pharmix.Web.Services;
using System;
using System.Drawing;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Controllers
{
    [Authorize]
    public class PackagePlanController : BaseController
    {
        private readonly IPackagePlanService packagePlanService;
        private readonly IBusinessService businessService;
        #region Constructor
        public PackagePlanController(UserManager<ApplicationUser> userManager, IPackagePlanService packagePlanService, IBusinessService _businessService) : base(userManager, _businessService)
        {
            this.packagePlanService = packagePlanService;
            businessService = _businessService;
        }
        #endregion

        public IActionResult Index()
        {
            var model = new SearchViewModel() { };
            return View(model);
        }

        public ActionResult Search(SearchRequest request)
        {
            var model = packagePlanService.GetSearchResult(request);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }

        // GET: Sites/Details/5
        public ActionResult Get(int id)
        {
            ViewBag.Duration = packagePlanService.GetDurationSelectList();
            var model = packagePlanService.CreateViewModel(id);
            return PartialView("_PlanForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(PackagePlanViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = packagePlanService.MapViewModelToPackagePlan(model, CurrentUserName, true);
                return Json(response > 0);
            }
            else
                return Json(false);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Json(packagePlanService.ArchivePackagePlan(id, CurrentUserName));
        }
         
    }
}