using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using Pharmix.Web.Services;

namespace Pharmix.Web.Controllers
{
    public class LookupController : BaseController
    {
        private readonly ILookupService _lookupService;
        private readonly IBusinessService businessService;
        public LookupController(UserManager<ApplicationUser> userManager, ILookupService lookupService, IBusinessService _businessService) : base(userManager, _businessService)
        {
            _lookupService = lookupService;
            businessService = _businessService;
        }

        #region Shift

        public IActionResult ShiftIndex()
        {
            var model = new SearchViewModel() { };
            return View(model);
        }

        public ActionResult SearchShift(SearchRequest request)
        {
            var model = _lookupService.GetShiftSearchResult(request);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }

        // GET: Isolators/Details/5
        public ActionResult GetShift(int id)
        {
            var model = _lookupService.CreateShiftViewModel(id);
            return PartialView("_ShiftForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveShift(ShiftViewModel model)
        {
            if (!ModelState.IsValid) return Json(false);
            var response = _lookupService.MapViewModelToShift(model, CurrentUserName, true);

            return Json(response > 0);
        }

        [HttpPost]
        public ActionResult DeleteShift(int id)
        {
            var response = _lookupService.ArchiveShift(id, CurrentUserName);
            return Json(response);
        }

        #endregion


        #region Procedure
        public IActionResult ProcedureIndex()
        {
            var model = new SearchViewModel() { };
            return View(model);
        }

        public ActionResult SearchProcedure(SearchRequest request)
        {
            var model = _lookupService.GetProcedureSearchResult(request);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }

        // GET: Isolators/Details/5
        public ActionResult GetProcedure(int id)
        {
            var model = _lookupService.CreateProcedureViewModel(id);
            return PartialView("_ProcedureForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProcedure(ProcedureViewModel model)
        {
            if (!ModelState.IsValid) return Json(false);
            var response = _lookupService.MapViewModelToProcedure(model, CurrentUserName, true);

            return Json(response > 0);
        }

        [HttpPost]
        public ActionResult DeleteProcedure(int id)
        {
            var response = _lookupService.ArchiveProcedure(id, CurrentUserName);
            return Json(response);
        }
        #endregion
    }
}