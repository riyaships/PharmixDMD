using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Data.Entities.ViewModels.Customer;
using Pharmix.Data.Enums;
using Pharmix.Services;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using Pharmix.Web.Services;

namespace Pharmix.Web.Controllers
{
    public class CustomerController : BaseController
    {

        private readonly ICustomerService _customerService;
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IModuleService _moduleService;
        private readonly IUserService _userService;
        private readonly IBusinessService businessService;
        private string superAdminRole = "SuperAdmin";

        #region Constructor

        public CustomerController(ICustomerService customerService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager,
            IModuleService moduleService, IUserService userService, IBusinessService _businessService)
            :base(userManager, _businessService)
        {
            _customerService = customerService;
            _userManager = userManager;
            _signManager = signManager;
            _moduleService = moduleService;
            _userService = userService;
            businessService = _businessService;
        }

        #endregion


        public IActionResult Index()
        {
            var model = new SearchViewModel() { };
            return View(model);
        }
        //public ActionResult Search(SearchRequest request)
        //{
        //    var model = _userService.GetSearchResult(request, superAdminRole, User.Identity.Name);
        //    return PartialView("DisplayTemplates/GridViewModel", model);
        //}


        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
            //var model = _customerService.CreateCustomerViewModel(0);
            //return View("RegistrationForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(CustomerViewModel model)
        {
            //_customerService.MapViewModelToCustomer(model, CurrentUserName, true);

            var user = new ApplicationUser { UserName = model.EmailAddress, Email = model.EmailAddress };
            var result = await _userManager.CreateAsync(user, model.Password);

            //if (result.Succeeded)
            //   await _userManager.AddToRoleAsync(user, "Customer");

            ViewBag.IsSuccess = result.Succeeded;

            return View("RegistrationSuccess");
        }

        /// <summary>
        /// pharmix.admin@pharmixtech.com
        /// Admin@123
        /// </summary>
        /// <returns></returns>
        //public async Task<ViewResult> ManageCustomer()
        //{
        //    var users= await _userService.GetUsersByRole(superAdminRole);
        //    return View();
        //}

        public async Task<ViewResult> ManageModule(int customerID)
        {
            var modules= _moduleService.GetAllModules();

            var moduleModelList = Mapper.Map<List<Module>, List<ModuleViewModel>>(modules.ToList());

            CustomerViewModel customerViewModel = new CustomerViewModel() { ModuleViewModelList=moduleModelList };

            return View(moduleModelList);
        }
        [HttpPost]
        public async Task<ActionResult> ManageModuel(CustomerViewModel model)
        {
            return View();
        }
    }
}