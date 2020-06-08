using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using Pharmix.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Controllers
{
    public class SiteController : BaseController
    {
        private readonly ISiteService siteService;
        private readonly IBusinessService businessService;
        #region Constructor
        public SiteController(UserManager<ApplicationUser> userManager, ISiteService siteService, IBusinessService _businessService) : base(userManager, _businessService)
        {
            this.siteService = siteService;
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
            var model = siteService.GetSearchResult(request);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }

        // GET: Sites/Details/5
        public ActionResult Get(int id)
        {
            var model = siteService.CreateViewModel(id);
            return PartialView("_SiteForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SiteViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var response = siteService.MapViewModelToSite(model, CurrentUserName, true);

                return Json(response > 0);
            }
            else
                return Json(false);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Json(siteService.ArchiveSite(id, CurrentUserName));
        }
    }
}
