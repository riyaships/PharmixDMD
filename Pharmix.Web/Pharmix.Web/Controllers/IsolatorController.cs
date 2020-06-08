using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Data.Enums;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels.IntegrationOrder;
using Pharmix.Web.Models;
using Pharmix.Web.Models.IsolatorVIewModels;
using Pharmix.Web.Services;

namespace Pharmix.Web.Controllers
{
    public class IsolatorController : BaseController
    {
        private readonly IIsolatorService isolatorService;
        private readonly ILookupService lookupService;
        private readonly IBusinessService businessService;
        public IsolatorController(UserManager<ApplicationUser> userManager, IIsolatorService isolatorService, ILookupService lookupService, IBusinessService _businessService) :base(userManager, _businessService)
        {
            this.isolatorService = isolatorService;
            this.lookupService = lookupService;
            businessService = _businessService;
        }

        // GET: Isolators
        public IActionResult Index()
        {
            return View(new SearchViewModel());
        }

        public ActionResult Search(SearchRequest request)
        {
            var model = isolatorService.GetSearchResult(request);
            return PartialView("DisplayTemplates/GridBoxViewModel", model);
        }

        // GET: Isolators/Details/5
        public ActionResult Get(int id)
        {
            var model = isolatorService.CreateViewModel(id);
            return PartialView("_IsolatorForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(IsolatorViewModel model)
        {
            var response = isolatorService.MapViewModelToIsolator(model, CurrentUserName, true);

            return Json(response > 0);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Json(isolatorService.ArchiveIsolator(id, CurrentUserName));
        }

        public ActionResult GetIsolatorOfflineDetail(int id)
        {
            var iso = isolatorService.GetIsolatorById(id);
            var shifts = lookupService.GetAllStandaredShifts();

            var shiftIds = new String[0];
            if (!string.IsNullOrEmpty(iso.OfflineShifts))
            {
                shiftIds = iso.OfflineShifts.Split('|');
            }

            var model = new IsolatorOfflineViewModel
            {
                IsolatorId = iso.IsolatorId,
                IsolatorName = iso.Abbriviation,
                StartDate = iso.OfflineStartDate,
                EndDate = iso.OfflineEndDate,
                AllShifts = shiftIds.Length == 0,
                ShiftList = new MultiSelectList(shifts.Select(p => new SelectListItem { Value = p.ShiftId.ToString(), Text = p.ShiftTitle }), "Value", "Text", shiftIds),
            };
            
            return PartialView("_IsolatorOffline", model);
        }

        public ActionResult GetIsolatorProcedures(int id)
        {
            var model = isolatorService.CreateIsolatorProcedureViewModel(id);
            return PartialView("_Procedures", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveIsolatorProcedures(IsolatorMappedProcedureViewModel model)
        {
            var response = isolatorService.SaveIsolatorProcedures(model, CurrentUserName);
            return Json(response>0);
        }
        
        public IActionResult SaveIsolatorOfflineDetail(IsolatorOfflineViewModel model)
        {
            if (!ModelState.IsValid) return Json(false);
            
            var response = isolatorService.SaveIsolatorOfflineDetail(model, CurrentUserName);
            return Json(response);
        }
        #region Staff allocation

        public ActionResult IsolatorStaffAllocationIndex(int isoId, string nm)
        {
            var model = new IsolatorStaffAllocationIndexViewModel()
            {
                IsolatorId = isoId,
                IsolatorName = nm,
                IsolatorList = new SelectList(isolatorService.GetAllIsolators().Select(p => new SelectListItem{ Value = p.IsolatorId.ToString(), Text = p.Abbriviation }), "Value", "Text", isoId)
            };
            return View(model);
        }

        public ActionResult GetIsolatorStaffAllocations(int entityId, DateTime requestDate)
        {
            var response = new List<IsolatorShiftDetail>();

            var iso = isolatorService.GetIsolatorById(entityId);
            var hasOffline = iso.OfflineStartDate.HasValue && iso.OfflineEndDate.HasValue &&
                             (iso.OfflineStartDate.Value.Date <= requestDate.Date &&
                              iso.OfflineEndDate.Value.Date >= requestDate.Date);

            if (hasOffline && string.IsNullOrEmpty(iso.OfflineShifts)) return Json(response);

            var offlineShifts = string.IsNullOrEmpty(iso.OfflineShifts) ? new string[0] : iso.OfflineShifts.Split('|');

            var shifts = lookupService.GetAllStandaredShifts();
            var allocations = isolatorService.GetIsolatorStaffAllocations(entityId, requestDate).ToList();

            foreach (var shift in shifts)
            {
                var isoShift = new IsolatorShiftDetail
                {
                    ShiftId = shift.ShiftId,
                    ShiftTitle = string.Format("{2} ({0} - {1})", shift.StartTime, shift.EndTime, shift.ShiftTitle),
                    Date = requestDate,
                    IsolatorId = entityId,
                    IsShiftUnavailable = hasOffline && offlineShifts.Any(p => Convert.ToInt32(p) == shift.ShiftId),
                    IsShiftReadonly = requestDate.Date < DateTime.Today.Date
                };

                var isoAl = allocations.FirstOrDefault(p => p.IsolatorShiftId == shift.ShiftId);

                if (isoAl != null)
                {
                    isoShift.AllocationId = isoAl.IsolatorStaffAllocationId;
                    isoShift.StaffName = isoAl.Staff.DisplayName();
                }

                response.Add(isoShift);
            }

            return Json(response);
        }
        
        public ActionResult ViewIsolatorStaffAllocation(int allocationId, int isolatorId, int shiftId, string shiftDescription,
            DateTime requestDate, bool isReadonly)
        {
            var model = isolatorService.CreateAllocationViewModel(allocationId, isolatorId);
            model.IsolatorShiftId = shiftId;
            model.ShiftDescription = shiftDescription;
            model.AllocatedDate = requestDate;
            model.IsModelReadonly = isReadonly;

            return PartialView("_IsolatorStaffAllocationForm", model);
        }

        public JsonResult CreateIsolatorStaffAllocation(IsolatorStaffAllocationViewModel model)
        {
            var allocation = isolatorService.MapViewModelToStaffAllocation(model, CurrentUserName, true);
            return Json(allocation != null);
        }

        public JsonResult RemoveIsolatorStaffAllocation(int allocationId)
        {
            var response = isolatorService.RemoveStaffAllocation(allocationId, CurrentUserName);
            return Json(response);
        }

        #endregion

        #region Order preperation
        public IActionResult OrderSchedulingIndex(string defaultDate)
        {
            ViewBag.DefaultDate = defaultDate;
            return View(new SearchViewModel());
        }

        public IActionResult OrderSearch(SearchRequest request)
        {
            var model = isolatorService.GetOrderSearchResult(request);
            return PartialView("DisplayTemplates/GridSmallBoxViewModel", model);
        }

        public IActionResult GetAvailableIsolatorsByDate(DateTime requestDate)
        {
            var isolators = isolatorService.GetAvailableIsolatorsByDate(requestDate);
            //var isoOrders = isolatorService.GetPreperationOrdersByDate(requestDate);

            var response = isolators.Select(iso => MapIsolatorRepsonse(iso, requestDate));

            return Json(response);
        }

        private object MapIsolatorRepsonse(Isolator iso, DateTime requestDate)
        {
            var isoTotalAvailabilityMins = iso.StaffShiftAllocations.Where(a => a.AllocatedDate.Date.Equals(requestDate.Date) && !a.IsArchived).Sum(s => s.IsolatorShift.TotalShiftDurationInMins);
            var prepOrders = iso.PreperationOrders.Where(a => a.PreperationDateTime.Date.Equals(requestDate.Date) && !a.IsArchived).ToList();

            var percent = isoTotalAvailabilityMins > 0 ? (prepOrders.Sum(p => p.IntegrationOrder.RequiredPreperationTimeInMins) * 100/isoTotalAvailabilityMins) : 0;

            return new
            {
                iso.IsolatorId,
                IsolatorName = iso.Abbriviation,
                SessionDose = iso.TotalNumberOfDosesPerSession,
                ScheduledOrders = prepOrders.Count(),
                ScheduledPercent = Math.Round(percent),
                IsolatorUnavailabe = isoTotalAvailabilityMins <= 0
            };
        }

        public IActionResult CreateOderIsolatorMapping(int orderId, int isoId, DateTime scheduledDate, int staffAllocationId)
        {
            isolatorService.CreateOrderIsolatorMapping(orderId, isoId, scheduledDate, staffAllocationId);
            return Json(true);
        }

        public IActionResult RemoveOderIsolatorMapping(int orderPreperationId)
        {
           var response = isolatorService.RemoveOrderIsolatorMapping(orderPreperationId);
            return Json(response);
        }

        public IActionResult OrderScheduling(int isoId, DateTime dt)
        {
            var model = new IntegrationOderScheduleViewModel
            {
                IsolatorId = isoId,
                PreperationDate = dt,
                IsolatorList = new SelectList(isolatorService.GetAllIsolators().Select(p =>
                        new SelectListItem {Value = p.IsolatorId.ToString(), Text = p.Abbriviation}), "Value", "Text", isoId)
            };
            return View(model);
        }

        public IActionResult PreperationOrderTimeline(int isoId, DateTime dt)
        {
            var isoShifts = isolatorService.GetIsolatorStaffAllocations(isoId, dt).ToList();
            var isoOrders = isolatorService.GetPreperationOrdersByIsolator(isoId, dt);

            var model = new ItegrationOrderPreperation
            {
                IsolatorId = isoId,
                PreperationDate = dt.ToString("yyyy-MM-dd"),
                IsolatorList = new SelectList(isolatorService.GetAllIsolators().Select(p => new SelectListItem {Value = p.IsolatorId.ToString(), Text = p.Abbriviation}), "Value", "Text", isoId),
                IsolatorShiftList = isoShifts.Select(shift => IsolatorShiftDetail(shift, isoOrders.Where(s => s.IsolatorStaffAllocation.IsolatorStaffAllocationId == shift.IsolatorStaffAllocationId).ToList())).ToList()
            };

            return PartialView("_PreperationOrderTimeline", model);
        }

        private IsolatorShiftDetail IsolatorShiftDetail(IsolatorStaffAllocation shift, IList<IntegrationOrderPreperation> shiftOrders)
        {
            var totalOrderReqTime = shiftOrders.Sum(p => p.IntegrationOrder.RequiredPreperationTimeInMins);
            var percent = shift.IsolatorShift.TotalShiftDurationInMins > 0 ? (shiftOrders.Sum(p => p.IntegrationOrder.RequiredPreperationTimeInMins) * 100 / shift.IsolatorShift.TotalShiftDurationInMins) : 0;

            return new IsolatorShiftDetail
            {
                IsolatorStaffAllocationId = shift.IsolatorStaffAllocationId,
                ShiftId = shift.IsolatorShift.ShiftId,
                ShiftTitle = string.Format("{2} ({0} - {1})", shift.IsolatorShift.StartTime, shift.IsolatorShift.EndTime, shift.IsolatorShift.ShiftTitle),
                StaffName = shift.Staff.DisplayName(),
                PercentFilled = Math.Round(percent),
                AvailableShiftTimeInMins = shift.IsolatorShift.TotalShiftDurationInMins - totalOrderReqTime,
                IntegrationOrders = shiftOrders.Select(io =>
                    new IntegrationOrderDetail()
                    {
                        OrderPreperationId = io.IntegrationOrderPreperationId,
                        OrderId = io.IntegrationOrderId,
                        OrderName = io.IntegrationOrder.Name,
                        RequiredDateTime = io.IntegrationOrder.RequiredDate.HasValue ? io.IntegrationOrder.RequiredDate.Value.ToShortDateString() : "",
                        Status = ((OrderProgressEnum)io.IntegrationOrder.OrderLastProgressId).ToString(),
                        RequiredPreperationTimeInMins = io.IntegrationOrder.RequiredPreperationTimeInMins,
                        OrderPriority = ((PriorityEnum)io.IntegrationOrder.PriorityId).ToString(),
                        OrderTimeline = string.Format("{0}", io.CreatedDate.ToString("dd/MM/yyyy HH:mm")),
                        EnableUnschedule = io.IntegrationOrder.OrderLastProgressId == (int)OrderProgressEnum.PreperationScheduled
                    }).ToList()
            };
        }
        #endregion
    }
}
