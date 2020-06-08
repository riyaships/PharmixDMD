using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Pharmix.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pharmix.Web.Extensions
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute() : base(typeof(RolePermissionFilter))
        {
            //Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }
    public class RolePermissionFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        //private readonly IRolePermissionService _moduleService;
        private readonly IModuleService _moduleService;
        private readonly IUserService _userService;

        private Dictionary<string, List<string>> _skipPermissions;

        public RolePermissionFilter(IModuleService moduleService, IUserService userService)
        {
            //_claim = claim;
            _moduleService = moduleService;
            _userService = userService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdentity = (ClaimsIdentity)context.HttpContext.User.Identity;

            if (!userIdentity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            //List<string> skipPermissions = new List<string>() { "Sear"};

            //var rolePermissions = _moduleService.GetModulesByCriteria(new List<string>(), roles.Select(x => x.Value).ToList());

            if (!_userService.IsPharmixAdmin(context.HttpContext.User.Identity.Name))
            {
                var permissionKeys = _moduleService.GetAvailablePermissionsByUserName(context.HttpContext.User.Identity.Name);
                string pageKey = ConstructPageKey(context);

                GetRelaventPermissions(pageKey, ref permissionKeys);

                //if (!permissionKeys.Contains(pageKey))
                //{
                //    context.Result = new ForbidResult();
                //}
                //if (!(rolePermissions.Where(x => x.Key.Equals(pageKey) && x.IsHaveAccess).Count() > 0) && !(rolePermissions.Where(x => x.Key.Equals(pageKey)).Count() <= 0))
                //{
                //    //Shos access denied 
                //    context.Result = new ForbidResult();

                //    //Redirect to login page
                //    //context.Result = new RedirectToRouteResult(new
                //    //   RouteValueDictionary(new { controller = "Account", action = "Login"}));
                //}
            }
        }

        private void SetSkipPermissions()
        {
            Dictionary<string, List<string>> skipPermissions = new Dictionary<string, List<string>>();

            //User Index
            skipPermissions.Add("User_Index", new List<string>() { "User_Search", "User_SetUserPassword" });

            //User Create
            skipPermissions.Add("User_Create", new List<string>() { "User_Get", "User_Save" });

            //User Edit
            skipPermissions.Add("User_Edit", new List<string>() { "User_Get", "User_Save" });

            //User Roles
            skipPermissions.Add("User_Roles", new List<string>() { "User_SearchRole" });

            skipPermissions.Add("Patient_Index", new List<string>() { "Patient_Search", "Patient_Create", "Patient_Detail" });
            skipPermissions.Add("Isolator_Index", new List<string>() { "Isolator_Search", "Patient_Create", "Patient_Detail" });

            _skipPermissions = skipPermissions;
        }

        private void GetRelaventPermissions(string key, ref List<string> availablePermissions)
        {
            SetSkipPermissions();
            var permissionDic = _skipPermissions.Where(x => x.Value.Contains(key)).ToList();

            if (permissionDic != null)
            {
                if (availablePermissions.Intersect(permissionDic.Select(x => x.Key)).Count() > 0)
                    availablePermissions.Add(key);
            }
            //List<string> skipPermissions = new List<string>();
            //_skipPermissions.TryGetValue(key, out skipPermissions);
            //return skipPermissions!=null? skipPermissions:new List<string>();       //Returning new list, there is checking for null in Getting this method
        }

        private string ConstructPageKey(AuthorizationFilterContext context)
        {
            string controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            string actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            return controllerName + "_" + actionName;
        }
    }
    public class RolePermissionAttribute
    {
    }
}
