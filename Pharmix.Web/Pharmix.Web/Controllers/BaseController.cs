using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.Context;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Pharmix.Web.Entities;
using Pharmix.Web.Extensions;
using Pharmix.Web.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using PharmixWebApi.Model;

namespace Pharmix.Web.Controllers
{
    [CustomAuthorize]
    public class BaseController : Controller
    {
        protected int _defaultPageSize = 10;
        private readonly UserManager<ApplicationUser> _userManager;
        protected string _trustAdminRole = "TrustAdmin";
        protected string _userRole = "User";
        protected string _customerRole = "Customer";
       
        protected string _pharmixAdminID = "5E16A271-BDAA-480D-A219-E16907D2AAE1";

        private readonly IBusinessService businessService;

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            //Get changeset model here
            Dmd_BusinessChangeSetDetails dmdBusinessChangeSetDetails = businessService.ToGetLatestChangeSetDetails(CurrentUserName);
            ViewBag.ChangesetBarModel = dmdBusinessChangeSetDetails;
        }
        public BaseController(UserManager<ApplicationUser> userManager, IBusinessService _businessService)
        {
            _userManager = userManager;
            businessService = _businessService;

        }
        public Guid CurrentUserId
        {
            get
            {
                if (!User.Identity.IsAuthenticated) return Guid.Parse(GetCurrentUserId());

                return Guid.Parse(GetCurrentUserId());
            }
        }
        public string CurrentUserName
        {
            get { return User.Identity.Name; }
        }
        public string GetCurrentUserId()
        {
            if (User.Identity.Name.Equals(PharmixStaticHelper.SuperAdminUsername))
                return _pharmixAdminID;
            ApplicationUser user = GetCurrentUser().GetAwaiter().GetResult();
            return user?.Id;
        }

        private async Task<ApplicationUser> GetCurrentUser() { return await _userManager.GetUserAsync((HttpContext.User)); }

        protected bool IsPharmixAdmin { get { return User.Identity.Name.Equals(PharmixStaticHelper.SuperAdminUsername, StringComparison.CurrentCultureIgnoreCase); } }
        protected bool IsTrustAdmin
        {
            get
            {
                var userIdentity = (ClaimsIdentity)User.Identity;
                var claims = userIdentity.Claims;
                var roleClaimType = userIdentity.RoleClaimType;
                var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                return roles.Select(x=>x.Value).Contains(_trustAdminRole);
            }
        }

    }
}