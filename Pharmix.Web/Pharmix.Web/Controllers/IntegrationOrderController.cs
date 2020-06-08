using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels.IntegrationOrder;
using Pharmix.Web.Models;
using Pharmix.Web.Services;

namespace Pharmix.Web.Controllers
{
    public class IntegrationOrderController : BaseController
    {
        private IIntegrationOrderService _integrationOrderService;
        private readonly IBusinessService businessService;
        public IntegrationOrderController(UserManager<ApplicationUser> userManager, IIntegrationOrderService integrationOrderService, IBusinessService _businessService) : base(userManager, _businessService)
        {
            _integrationOrderService = integrationOrderService;
            businessService = _businessService;
        }

        public IActionResult Index()
        {
            var model = new SearchViewModel() { };
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(SearchRequest request)
        {
            var model = _integrationOrderService.GetSearchResult(request);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }
        
        public ActionResult Get(int id)
        {
            var model = _integrationOrderService.CreateViewModel(id);
            return PartialView("_Form", model);
        }

        public JsonResult SaveUpdateIntegrationOrder(IntegrationOrderViewModel param)
        {
            var result = _integrationOrderService.SaveUpdateIntegrationOrder(param, CurrentUserName);
            return Json(result);
        }
        
        public ActionResult GetComment(IntegrationOrderCommentViewModel param)
        {
            param.Classifications = _integrationOrderService.GetOrderClassifications();
            return PartialView("_FormComment", param);
        }

        public ActionResult GetCallSUpervisor(int OrderId)
        {
            var model = _integrationOrderService.CreateCallSupervisorViewModel(OrderId);
            return PartialView("_FormCallSupervisor", model);
        }

        public JsonResult ApproveOrder(int OrderId)
        {
            var location = 2;
            var result = _integrationOrderService.ApproveOrder(OrderId, CurrentUserName,location);
            return Json(result);
        }

        public JsonResult CallSupervisor(CallSupervisorViewModel param)
        {
            var location = 2;
            var result = _integrationOrderService.CallSupervisor(param, CurrentUserName, location);
            return Json(result);
        }

        public JsonResult SaveActionDelineClassify(IntegrationOrderCommentViewModel param)
        {
            var location = 2;
            var result = _integrationOrderService.SaveActionDelineClassify(param, CurrentUserName, location);
            return Json(result);
        }
    }
}