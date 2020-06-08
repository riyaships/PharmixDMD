using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Extensions;
using Pharmix.Web.Models;
using Pharmix.Web.Services;
using PharmixJob.WebApiClient;

namespace Pharmix.Web.Controllers
{
    [Authorize]
    public class BusinessController : BaseController
    {
        PharmixWebApiClient pharmixWebApiClient = new PharmixWebApiClient();
        private UserManager<ApplicationUser> _userManager;
        private readonly IBusinessService businessService;
        private string _userRole = "User";
        string _apiBaseURI = string.Empty;
        private IConfiguration _configuration;


        #region Constructor

        public BusinessController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager
            , IUserService userService, IModuleService moduleService, IBusinessService _businessService, IConfiguration configuration)
            : base(userManager, _businessService)
        {
            _userManager = userManager;
            businessService = _businessService;
            _configuration = configuration;
            _apiBaseURI = _configuration["ServiceApiURL"].ToString();
        }

        #endregion


        public IActionResult Index()
        {
            var model = new SearchViewModel() { };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Save(BusinessViewModel model)
        {
            var response = businessService.MapViewModelToSite(model, CurrentUserName, true);
            HttpClient client = pharmixWebApiClient.InitializeClient(_apiBaseURI);
            HttpResponseMessage Apiresponse = client.GetAsync("api/PharmixApi/SaveBusinessDetails?CurrentUserName=" + model.ContactEmail + "&BusinessName=" + model.BusinessName).Result;
            return RedirectToAction("Index");
            //else return View("Create", model);
        }

        public async Task<RedirectToActionResult> Delete(int id)
        {
            businessService.DeleteBusiness(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(SearchRequest request)
        {
            var model = businessService.GetSearchResult(request);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }
        public ActionResult Get(int id)
        {
            var model = businessService.CreateViewModel(id);
            return PartialView("_form", model);
        }

        public ActionResult ManageUser(int id)
        {
            BusinessViewModel businessViewModel = businessService.CreateViewModel(id);
            BusinessUserViewModel model = new BusinessUserViewModel() { Id = businessViewModel.Id, BusinessName = businessViewModel.BusinessName, ContactEmail = businessViewModel.ContactEmail };
            return PartialView("_businessusers", model);
        }

        [HttpPost]
        public async Task<ActionResult> BusinessUserSave(BusinessUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.ContactEmail, Email = model.ContactEmail };
                IdentityResult result = null;
                if (string.IsNullOrEmpty(model.IdentityUserId) || model.Id.Equals("0"))
                {
                    user = _userManager.FindByEmailAsync(model.ContactEmail).Result;
                    if (user == null)
                    {
                        user = new ApplicationUser { UserName = model.ContactEmail, Email = model.ContactEmail };
                        result = await _userManager.CreateAsync(user, model.Password);
                    }

                    if (result.Succeeded)
                    {
                        string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        result = await _userManager.ResetPasswordAsync(user, code, model.Password);

                        if (result.Succeeded)
                        {
                            user = _userManager.FindByEmailAsync(model.ContactEmail).Result;
                            if (user != null)
                            {
                                var businessViewModel = businessService.CreateViewModel(model.Id);
                                businessViewModel.IdentityUserId = user.Id;
                                var response = businessService.MapViewModelToSite(businessViewModel, CurrentUserName, true);
                                var client = pharmixWebApiClient.InitializeClient(_apiBaseURI);
                                var apiResponse = await client.GetAsync("api/PharmixApi/SaveBusinessDetails?CurrentUserName=" + model.ContactEmail + "&BusinessName=" + model.BusinessName);
                            }
                        }
                        else
                        {

                            foreach (var item in result.Errors)
                            {
                                model.ErrorMessage = model.ErrorMessage + item.Description;
                            }
                            if (!string.IsNullOrEmpty(model.ErrorMessage))
                            {
                                ViewBag.IsSuccess = model.ErrorMessage;
                                return PartialView("_businessusers", model);

                            }
                        }
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            model.ErrorMessage = model.ErrorMessage + item.Description;
                        }

                        if (string.IsNullOrEmpty(model.ErrorMessage))
                        {

                        }
                        else
                        {
                            ViewBag.IsSuccess = model.ErrorMessage;
                            return PartialView("_businessusers", model);
                        }
                    }
                }


                ViewBag.IsSuccess = result.Succeeded;

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.IsSuccess = "Something Went wrong!..";
                
                return PartialView("_businessusers", model);
            }
        }
    }
}