using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Models.PatientViewModel;
using Pharmix.Web.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pharmix.Web.Controllers
{
    public class PatientController : BaseController
    {
        private readonly IPatientService _patientService;
        private readonly IBusinessService businessService;
        public PatientController(UserManager<ApplicationUser> userManager, IPatientService patientService, IBusinessService _businessService) : base(userManager, _businessService)
        {
            this._patientService = patientService;
            businessService = _businessService;
        }

        // GET: /<controller>/
        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.PatientAdmin = CurrentUserId.ToString();
                var createUser = await _patientService.CreateUser(model);
                if (createUser)
                    return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Detail(int PatientId)
        {
            var model = _patientService.GetDetail(CurrentUserId.ToString(), PatientId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Detail(PregnancyViewModel model)
        {
            var SaveDetail = _patientService.SaveDetail(model, CurrentUserId.ToString());
            if (SaveDetail)
                return RedirectToAction("Index");

            model.IsAdmin = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(SearchRequest request)
        {
            var model = _patientService.GetSearchResult(request);
            return PartialView("DisplayTemplates/GridViewModel", model);
        }
    }
}
