using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels.UsedProduct;
using Pharmix.Web.Models;
using Pharmix.Web.Services;

namespace Pharmix.Web.Controllers
{
    public class UsedProductController : BaseController
    {
        private readonly IUsedProdService usedProdService;
        private readonly IBusinessService businessService;
        public UsedProductController(UserManager<ApplicationUser> userManager, IUsedProdService usedProdService, IBusinessService _businessService) : base(userManager, _businessService)
        {
            this.usedProdService = usedProdService;
            businessService = _businessService;
        }
        // GET: UsedProduct
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllUsedProductsForOrder(SearchRequest request, int orderId)
        {
            var model = usedProdService.GetUsedProdSearchResult(request, orderId);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }

        public ActionResult GetUsedProductList(int orderId, int isoId)
        {
            var model = usedProdService.CreateUsedProdListViewModel(orderId, isoId);
            return PartialView("_UsedProductList", model);
        }

        public ActionResult GetUsedProductForm(int id, int orderId, int isoId)
        {
            var model = usedProdService.CreateViewModel(id, orderId, isoId);
            return PartialView("_UsedProductForm", model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUsedProduct(UsedProductViewModel model)
        {
            var response = usedProdService.MapViewModelToUsedProduct(model, GetCurrentUserId(), true);

            return Json(response.UsedProductId > 0);
        }
        
        public ActionResult RemoveUsedProduct(int id)
        {
            var response = usedProdService.RemoveUsedProduct(id, GetCurrentUserId());
            return Json(response);
        }

        public ActionResult VtmIndex()
        {
            return View("VtmIndex", new SearchViewModel());
        }

        public ActionResult SearchVtm(SearchRequest request)
        {
            var model = usedProdService.GetVtmSearchResult(request);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }

        public ActionResult GetVtmForm(int id)
        {
            var model = usedProdService.CreateVtmViewModel(id);
            return PartialView("_VtmForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveVtm(VtmViewModel model)
        {
            var response = usedProdService.SaveVtm(model, GetCurrentUserId());
            return Json(response);
        }
        public ActionResult DeleteVtm(int id)
        {
            var response = usedProdService.DeleteVtm(id, GetCurrentUserId());
            return Json(response);
        }
    }
}