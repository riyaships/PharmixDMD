using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Models;
using Pharmix.Web.Services;

namespace Pharmix.Web.Controllers
{
    [Authorize]
    public class SubscriptionController : BaseController
    {
        private readonly IBusiness_SubscriptionService subscriptionService;
        private readonly IBusinessService businessService;

        #region Constructor
        public SubscriptionController(UserManager<ApplicationUser> userManager, IBusiness_SubscriptionService subscriptionService, IBusinessService _businessService) : base(userManager, _businessService)
        {
            this.subscriptionService = subscriptionService;
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
            var model = subscriptionService.GetSearchResult(request, CurrentUserName);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }
    }
}