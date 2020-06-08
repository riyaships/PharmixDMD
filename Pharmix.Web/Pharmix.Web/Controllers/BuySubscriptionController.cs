using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;
using Pharmix.Web.Services;

namespace Pharmix.Web.Controllers
{
    [Authorize]
    public class BuySubscriptionController : BaseController
    {
        private readonly IPackagePlanService packagePlanService;
        private readonly IBusinessService businessService;

        #region Constructor
        public BuySubscriptionController(UserManager<ApplicationUser> userManager, 
            IPackagePlanService packagePlanService,  
            IBusinessService _businessService) : base(userManager, _businessService)
        {
            this.packagePlanService = packagePlanService; 
            this.businessService = _businessService;
        }
        #endregion

        public IActionResult Index()
        {
            var model = packagePlanService.GetAllPackagePlans();
            return View(model);
        }  
    }
}