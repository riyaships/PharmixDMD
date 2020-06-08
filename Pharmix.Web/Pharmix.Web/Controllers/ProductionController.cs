using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Data.Enums;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels.Production;
using Pharmix.Web.Models;
using Pharmix.Web.Models.IsolatorVIewModels;
using Pharmix.Web.Services;

namespace Pharmix.Web.Controllers
{
    public class ProductionController : BaseController
    {
        private IIsolatorService _isolatorService;
        private IProductionService _productionService;
        private readonly ILookupService lookupService;
        private readonly IBusinessService businessService;
        public ProductionController(UserManager<ApplicationUser> userManager,IIsolatorService isolatorService, IProductionService productionService, ILookupService lookupService, IBusinessService _businessService) : base(userManager, _businessService)
        {
            _isolatorService = isolatorService;
            _productionService = productionService;
            this.lookupService = lookupService;
            businessService = _businessService;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.RequestTypes = _productionService.GetSupervisorRequetTypes().Select(p => new { Id = p.Id, Name = p.Type });
            var data = _productionService.GetProductionIsolators(GetCurrentUserId());
            return View(data.ToList());
        }

        [Authorize]
        public IActionResult SupervisorRequestTracking()
        {
            var model = new SearchViewModel() { };
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(SearchRequest request)
        {
            var model = _productionService.GetSearchResult(request, User.IsInRole("IsoSupervisor"), CurrentUserName);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }

        public ActionResult GetRequestForm(int isoId, int requestId, bool isModelEditable = false)
        {
            var model = _productionService.CreateRequestViewModel(requestId);
            model.IsolatorId = model.IsolatorId> 0?model.IsolatorId: isoId;
            model.IsModelEditable = isModelEditable;
            return PartialView("_RequestForm", model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult CreateRequest(SupervisorRequestViewModel param)
        {
            var result = _productionService.CreateRequest(param, CurrentUserName);
            return Json(result);
        }
       
        [HttpPost]
        [Authorize]
        public JsonResult ApproveRequest(int requestId)
        {
            var result = _productionService.ChangeRequestStatus(requestId, true, CurrentUserName);
            
            return Json(result);
        }

        [HttpPost]
        [Authorize]
        public JsonResult DeclineRequest(int requestId)
        {
            var result = _productionService.ChangeRequestStatus(requestId, false, CurrentUserName);

            return Json(result);
        }

        //private int GetCurrentShift()
        //{
        //    var dateNow = DateTime.Now;
        //    var x = dateNow.TimeOfDay;
        //    var shifts = _shiftService.GetShifts();
        //    //.Where(p => TimeSpan.Compare(dateNow.TimeOfDay, TimeSpan.ParseExact(p.StartTime, "g", CultureInfo.CurrentCulture)) == 1 && TimeSpan.Compare(dateNow.TimeOfDay,TimeSpan.ParseExact(p.EndTime, "g", CultureInfo.CurrentCulture)) == -1);

        //    var result = new Shift();
        //    foreach(var item in shifts)
        //    {
        //        var startTime = TimeSpan.ParseExact(item.StartTime, "g", CultureInfo.CurrentCulture);
        //        var endTime = TimeSpan.ParseExact(item.EndTime, "g", CultureInfo.CurrentCulture);

        //        if (TimeSpan.Compare(dateNow.TimeOfDay, startTime) == 1 && TimeSpan.Compare(dateNow.TimeOfDay, endTime) == -1)
        //        {
        //            result = item;
        //            break;
        //        }
        //    }
        //    return result.IsolatorId;
        //}

        //[HttpPost]
        //[Authorize]
        //public JsonResult StartUsingIsolator(int isolatorId)
        //{
        //    var result = _productionService.StartUsingIsolator(isolatorId, CurrentUserName);

        //    return Json(result);
        //}

        //[HttpPost]
        //[Authorize]
        //public JsonResult StopUsingIsolator(int isolatorId, int isolatorStaffShiftId)
        //{
        //    var result = _productionService.StopUsingIsolator(isolatorId, isolatorStaffShiftId, CurrentUserName);

        //    return Json(result);
        //}

        public IActionResult IsolatorProcedures(int isoId, DateTime dt, int procTypeId)
        {
            var model = _productionService.CreateIsolatorProcedureViewModel(isoId, dt, procTypeId);
            return PartialView("_IsolatorProcedures", model);
        }

        public IActionResult ProductionOrders(int isoId, DateTime dt)
        {
            var model = _productionService.CreateProductionOrderListViewModel(isoId, dt, GetCurrentUserId());
            return View(model);
        }

        public IActionResult GetPoductionOrderTimeline(int isoId, DateTime dt)
        {
            var model = _productionService.CreateProductionOrderListViewModel(isoId, dt, GetCurrentUserId());
            return PartialView("_ProductionOrderTimeline", model);
        }

        public IActionResult SetPoductionOrderStatus(int prepOrderId, int statusId)
        {
            var response = _productionService.SetProductionOrderStatus(prepOrderId, statusId, CurrentUserName);
            return Json(((OrderPreperationStatusEnum)response).ToString());
        }
        public IActionResult StopIsolator(int isoId, DateTime dt)
        {
            var response = _productionService.StopIsolator(isoId, dt, GetCurrentUserId());
            if (response)
                return RedirectToAction("Index");

            return Json(false);
        }
    }
}