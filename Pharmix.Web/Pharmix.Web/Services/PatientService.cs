using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Models;
using Pharmix.Web.Models.PatientViewModel;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Pharmix.Web.Services
{
    public class PatientService: IPatientService
    {
        private readonly IRepository _repository;
        private readonly IUserService _userService;
        private UserManager<ApplicationUser> _userManager;

        public PatientService(UserManager<ApplicationUser> userManager, IRepository repository, IUserService userService)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public IEnumerable<Patient> GetPatients()
        {
            return _repository.GetAll<Patient>().Where(p => !p.IsArchived);
        }

        public GridViewModel GetSearchResult(SearchRequest request)
        {
            var model = PatientMapper.CreateGridViewModel();
            var getAllPatient = _repository.GetContext().Patients
                .Where(p => !p.IsArchived);

            var pageResult = QueryListHelper.SortResults(getAllPatient, request);
            var serviceRows = pageResult
                .Select(PatientMapper.BindGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public async Task<bool> CreateUser(PatientViewModel patientModel)
        {
            var model = new UserViewModel
            {
                FirstName = patientModel.FirstName,
                Surname = patientModel.FirstName,
                MobileNumber = patientModel.Phone,
                Password = "Pass123!",
                Email = patientModel.Email
            };

            var user = Mapper.Map<UserViewModel, ApplicationUser>(model);
            user.UserName = model.Email;
            user.IsApproved = true;
            user.FirstName = model.FirstName;
            user.Surname = model.Surname;
            user.MobileNumber = model.MobileNumber;
            
            IdentityResult result = null;

            result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                //get patient admint Trust
                //var trustIds = _repository.GetContext().UserTrusts.Where(p => p.UserId.Equals(patientModel.PatientAdmin)).Select(p => p.TrustId).ToList();
                //model.Id = user.Id;
                //model.TrustIds = trustIds;
                //await _userService.UpdateUserDetails(model);
                var addRole = await _userManager.AddToRoleAsync(user, "Patient");

                //Add to table Patient
                var patient = new Patient
                {
                    FirstName = patientModel.FirstName,
                    Surname = patientModel.Surname,
                    EmailAddress = patientModel.Email,
                    MobileNumber = patientModel.Phone,
                    IdNumber = patientModel.IdNumber,
                    DateOfBirth = patientModel.DOB,
                    UserId = user.Id,
                    RegisteredDate = DateTime.Now
                };
                patient.SetCreateDetails(patientModel.PatientAdmin);

                var savedPatient = _repository.SaveNew(patient,patientModel.PatientAdmin);

                var pregnancy = new Pregnancy
                {
                    PatientId = savedPatient.Id
                };
                pregnancy.SetCreateDetails(patientModel.PatientAdmin);

                var savedPregnancy = _repository.SaveNew(pregnancy, patientModel.PatientAdmin);

                return true;
            }

            return false;
        }

        public PregnancyViewModel GetDetail(string UserId, int PatientId)
        {
            var result = new PregnancyViewModel();

            var adminRole = _repository.GetContext().Roles.Where(p => p.Name == "PatientAdmin").Select(p => p.Id).FirstOrDefault();
            var applicationUser = _repository.GetContext().UserRoles.Where(p => p.UserId == UserId && p.RoleId == adminRole).ToList();
            if (applicationUser.Count > 0)
            {
                result.IsAdmin = true;
            }
            else
            {
                result.IsAdmin = false;
                PatientId = _repository.GetContext().Patients.Where(p => p.UserId == UserId).Select(p => p.Id).FirstOrDefault();
            }

            //var patient = _repository.GetContext().Patients.Where(p => p.Id == PatientId).FirstOrDefault();
            var pregnancy = _repository.GetContext().Pregnancy.Include(p => p.Patient).Where(p => p.PatientId == PatientId).FirstOrDefault();
            result.Id = pregnancy.Id;
            result.FirstName = pregnancy.Patient.FirstName;
            result.Surname = pregnancy.Patient.Surname;
            result.DateOfBirth = pregnancy.Patient.DateOfBirth;
            result.EDD = pregnancy.EDD;
            result.NHSNumber = pregnancy.NHSNumber;
            result.MaternityUnit = pregnancy.MaternityUnit;

            var communicationNeed = _repository.GetContext().CommunicationNeed.Include(p => p.Pregnancy).Where(p => p.PregnancyId == pregnancy.Id).FirstOrDefault();
            result.CommunicationNeedViewModel = Mapper.Map<CommunicationNeed, CommunicationNeedViewModel>(communicationNeed);

            return result;
        }

        public bool SaveDetail(PregnancyViewModel model, string UserId)
        {
            var result = false;
            try
            {
                var patient = _repository.GetContext().Patients.Where(p => p.Id == model.PatientId).FirstOrDefault();
                patient.FirstName = model.FirstName;
                patient.Surname = model.Surname;
                patient.DateOfBirth = model.DateOfBirth;
                patient.SetUpdateDetails(UserId);

                _repository.SaveExisting(patient);

                var pregnancy = _repository.GetContext().Pregnancy.Where(p => p.Id == model.Id).FirstOrDefault();
                pregnancy.EDD = model.EDD;
                pregnancy.NHSNumber = model.NHSNumber;
                pregnancy.MaternityUnit = model.MaternityUnit;

                _repository.SaveExisting(pregnancy);

                if(model.CommunicationNeedViewModel.Id > 0)
                {
                    //Save Existing
                    var communicationNeed = _repository.GetContext().CommunicationNeed.Where(p => p.Id == model.CommunicationNeedViewModel.Id).FirstOrDefault();
                    communicationNeed.AssistanceRequired = model.CommunicationNeedViewModel.AssistanceRequired;
                    communicationNeed.AssistanceRequiredDetail = model.CommunicationNeedViewModel.AssistanceRequiredDetail;
                    communicationNeed.PreferredAssistance = model.CommunicationNeedViewModel.PreferredAssistance;
                    communicationNeed.SpeakEnglish = model.CommunicationNeedViewModel.SpeakEnglish;
                    communicationNeed.FirstLanguage = model.CommunicationNeedViewModel.FirstLanguage;
                    communicationNeed.PreferedLanguage = model.CommunicationNeedViewModel.PreferedLanguage;
                    communicationNeed.InterpreterPhone = model.CommunicationNeedViewModel.InterpreterPhone;
                    communicationNeed.SetUpdateDetails(UserId);

                    _repository.SaveExisting(communicationNeed);
                }
                else
                {
                    //Add new Record
                    var communicationNeed = new CommunicationNeed();
                    communicationNeed.AssistanceRequired = model.CommunicationNeedViewModel.AssistanceRequired;
                    communicationNeed.AssistanceRequiredDetail = model.CommunicationNeedViewModel.AssistanceRequiredDetail;
                    communicationNeed.PreferredAssistance = model.CommunicationNeedViewModel.PreferredAssistance;
                    communicationNeed.SpeakEnglish = model.CommunicationNeedViewModel.SpeakEnglish;
                    communicationNeed.FirstLanguage = model.CommunicationNeedViewModel.FirstLanguage;
                    communicationNeed.PreferedLanguage = model.CommunicationNeedViewModel.PreferedLanguage;
                    communicationNeed.InterpreterPhone = model.CommunicationNeedViewModel.InterpreterPhone;
                    communicationNeed.PregnancyId = pregnancy.Id;
                    communicationNeed.SetUpdateDetails(UserId);

                    var saveNew = _repository.SaveNew(communicationNeed);
                }

                result = true;
            }catch(Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}
