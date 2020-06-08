using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Web.Models;
using Pharmix.Data.Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Pharmix.Web.Extensions;
using Pharmix.Web.Services;
using Microsoft.AspNetCore.Routing;
using Pharmix.Web.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Pharmix.Web.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Pharmix.Web.Controllers
{
    [CustomAuthorize]
    public class UserController : BaseController
    {
        private SignInManager<ApplicationUser> _signManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IModuleService _moduleService;
        private readonly ITrustService _trustService;
        private readonly PharmixEntityContext _context;
        private readonly IBusinessService businessService;

        #region Constructor

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager
            , IUserService userService, IModuleService moduleService, ITrustService trustService,
            IRepository repository, IBusinessService _businessService)
            : base(userManager, _businessService)
        {
            _userService = userService;
            _userManager = userManager;
            _signManager = signManager;
            _moduleService = moduleService;
            _trustService = trustService;
            businessService = _businessService;
            _context = repository.GetContext();
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            var model = new SearchViewModel() { };
            return View(model);
        }
        public ActionResult Search(SearchRequest request)
        {
            var isPharmixAdmin = IsPharmixAdmin; //_userService.IsPharmixAdmin(User.Identity.Name);

            ////Hardcoding Roles Temporary. Need to get from Db 
            List<string> rolesToFind = isPharmixAdmin ? new List<string>() { _trustAdminRole } : new List<string>() { _userRole, _customerRole };

            //var model = _userService.GetSearchResult(request, rolesToFind, User.Identity.Name, IsPharmixAdmin);
            var model = _userService.GetSearchResult(request, User.Identity.Name, IsPharmixAdmin);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }

        public ActionResult Get(int id)
        {
            string userId = _userService.GetUserIdByTempId(id);
            var model = _userService.CreateViewModel(userId);
            if (IsPharmixAdmin)
            {
                ViewBag.Trusts = _trustService.GetTrustSelectList();
                model.Role = _trustAdminRole;
            }
            else if (IsTrustAdmin)
            {
                model.TrustId = _trustService.GetTrustIdByUser(User.Identity.Name);
            }





            //if (string.IsNullOrEmpty(userId) || userId.Equals(new Guid().ToString()))
            //    model.Role = superAdminRole;

            return PartialView("_Form", model);
        }

        #endregion

        #region Save/Delete User

        [HttpPost]
        public async Task<ActionResult> Save(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<UserViewModel, ApplicationUser>(model);
                user.UserName = model.Email;
                user.IsApproved = true;
                user.FirstName = model.FirstName;
                user.Surname = model.Surname;
                user.MobileNumber = model.MobileNumber;

                //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                IdentityResult result = null;
                if (string.IsNullOrEmpty(model.Id) || model.Id.Equals("0"))
                {
                    result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (model.TrustId > 0)
                        {
                            model.Id = user.Id;
                            model.TrustIds = new List<int>() { model.TrustId };
                            await _userService.UpdateUserDetails(model);
                        }
                        if (!string.IsNullOrEmpty(model.Role) && IsPharmixAdmin)
                            await _userManager.AddToRoleAsync(user, model.Role);
                    }

                }
                else
                {
                    user = _userManager.FindByIdAsync(model.Id).Result;
                    if (user != null)
                    {
                        var existingRoles = await _userManager.GetRolesAsync(user);
                        foreach (var role in existingRoles)
                        {
                            await _userManager.RemoveFromRoleAsync(user, role);
                        }
                        if (!string.IsNullOrEmpty(model.Role) && IsPharmixAdmin)
                            await _userManager.AddToRoleAsync(user, model.Role);
                        result = _userManager.ResetPasswordAsync(user, model.PasswordResetToken, model.ConfirmPassword).Result;
                    }
                }

                //if (result.Succeeded)
                //    await _userManager.AddToRoleAsync(user, "Customer");

                ViewBag.IsSuccess = result.Succeeded;

                return RedirectToAction("Index");
            }
            else return View("Create", model);
        }

        public async Task<RedirectToActionResult> Delete(int id)
        {
            string userId = _userService.GetUserIdByTempId(id);
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }


        #endregion

        #region Manage User Module. For Pharmix admin

        //public ViewResult ManageModule(int id)
        //{
        //    string userId = _userService.GetUserIdByTempId(id);
        //    var user = _userService.GetUser(userId);
        //    List<UserModuleViewModel> userModuleViewModelList = _moduleService.GetModulesByUser(userId).ToList();
        //    UserViewModel userViewModel = new UserViewModel() { Name = user.UserName, Id = userId, UserModuleViewModelList = userModuleViewModelList };

        //    return View(userViewModel);
        //}
        //[HttpPost]
        //public ActionResult ManageModule(UserViewModel userViewModel)
        //{
        //    _moduleService.UpdateUserModule(userViewModel);
        //    return RedirectToAction("ManageModule");
        //}

        #endregion

        #region Manage User Role

        public ViewResult ManageUserRole(int id)
        {
            string userId = _userService.GetUserIdByTempId(id);
            var user = _userService.GetUser(userId);

            List<UserRoleViewModel> userRoleViewModelList = _userService.GetRolesByUser(userId, IsPharmixAdmin);
            UserViewModel userViewModel = new UserViewModel() { Name = user.UserName, Id = userId, UserRoleViewModelList = userRoleViewModelList };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ManageUserRole(UserViewModel userViewModel)
        {
            await _userService.UpdateUserRole(userViewModel);
            return RedirectToAction("ManageUserRole");
        }
        #endregion

        #region Manage Role Permission

        public async Task<ViewResult> Roles()
        {
            return View();
        }

        public ActionResult SearchRole(SearchRequest request)
        {
            var model = _userService.GetRoleSearchResult(request, IsPharmixAdmin);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }


        public async Task<ViewResult> ManagePermission(int id)
        {
            string roleId = _userService.GetRoleIdByTempId(id);
            Guid currUserId = CurrentUserId;
            int trustId = 0;
            if (IsTrustAdmin)
            {
                trustId = _trustService.GetTrustIdByUser(User.Identity.Name);
            }

            var roleViewModel = _userService.GetRolePermissionByRole(roleId, currUserId.ToString(), trustId);

            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ManagePermission(RoleViewModel roleViewModel)
        {
            int tempId = _userService.GetTempIdByRoleId(roleViewModel.Id);

            await _userService.UpdateRolePermission(roleViewModel);

            return RedirectToAction("ManagePermission", new RouteValueDictionary(new { id = tempId }));
        }

        #endregion

        #region Change Password by Admin

        [Authorize(Roles = "SuperAdmin,TrustAdmin")]
        public async Task<JsonResult> SetUserPassword(int id)
        {
            string newPwd = Guid.NewGuid().ToString().Substring(0, 8);
            try
            {
                string userId = _userService.GetUserIdByTempId(id);
                var applicationUser = _userService.GetUser(userId);

                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(_context);
                String hashedNewPassword = _userManager.PasswordHasher.HashPassword(applicationUser, newPwd);
                await store.SetPasswordHashAsync(applicationUser, hashedNewPassword);
                applicationUser.IsResetPasswordRequired = true;
                await store.UpdateAsync(applicationUser);
            }
            catch (Exception ex)
            {
                newPwd = string.Empty;
            }

            return Json(new { NewPassword = newPwd, StatusCode = !string.IsNullOrEmpty(newPwd) ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError });
        }

        #endregion

        #region Access Permission

        //public ViewResult ManagePermission()
        //{
        //    var roles = _userService.GetRoleSelectList();
        //    ViewBag.Roles = roles;
        //    ViewBag.Modules = new SelectList(_moduleService.GetDistinctModules(), "Name", "Name");
        //    return View();
        //}

        //public PartialViewResult GetPermissionView(string roleID, string moduleName)
        //{
        //    AccessPermissionViewModel accessPermissionViewModelList = new AccessPermissionViewModel();
        //    if (!string.IsNullOrEmpty(roleID) && roleID != "0")
        //        accessPermissionViewModelList.RoleModuleViewModelList = _moduleService.GetModulesByCriteria(new List<string>() { roleID }, null, 0, moduleName);

        //    return PartialView("_permissionGrid", accessPermissionViewModelList);
        //}

        //[HttpPost]
        //public ActionResult ManagePermission(AccessPermissionViewModel model)
        //{
        //    _moduleService.UpdatePermission(model);

        //    var roles = _userService.GetRoleSelectList(model.RoleId);
        //    ViewBag.Roles = roles;
        //    ViewBag.Modules = new SelectList(_moduleService.GetDistinctModules(), "Name", "Name", model.ModuleName);
        //    return View();
        //}

        #endregion




    }
}