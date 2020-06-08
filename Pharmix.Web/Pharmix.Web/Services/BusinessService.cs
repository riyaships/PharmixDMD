using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Services.Mappers;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using PharmixJob.WebApiClient;
using PharmixWebApi.CustomModel;
using PharmixWebApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using X.PagedList;

namespace Pharmix.Web.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IRepository repository;
        PharmixWebApiClient pharmixWebApiClient = new PharmixWebApiClient();
        private IConfiguration _configuration;
        string _apiBaseURI = string.Empty;
        public BusinessService(IRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            _configuration = configuration;
            _apiBaseURI = _configuration["ServiceApiURL"].ToString();
        }
        public IEnumerable<BusinessDetails> GetAllSites()
        {
            return repository.GetAll<BusinessDetails>().Where(e => !e.IsArchived);
        }

        public int SaveSite(BusinessDetails Site, string user)
        {
            if (Site.Id > 0)
            {
                Site.SetUpdateDetails(user);
                repository.SaveExisting(Site);
                return Site.Id;
            }

            Site.SetCreateDetails(user);
            return repository.SaveNew(Site).Id;
        }



        public GridViewModel GetSearchResult(SearchRequest request)
        {
            var model = BusinessMapper.CreateGridViewModel();

            var pageResult = QueryListHelper.SortResults(GetAllSites(), request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.BusinessName.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase)
                || p.ContactPerson.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase)
                || p.ContactEmail.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase)
                || p.ContactPhone.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase)
                || p.City.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase)
                || p.Postcode.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(BusinessMapper.BindGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public BusinessViewModel CreateViewModel(int id)
        {
            var businessDetails = GetSiteById(id);
            BusinessViewModel businessViewModel = new BusinessViewModel();
            if (businessDetails != null)
            {
                businessViewModel.Id = businessDetails.Id;
                businessViewModel.BusinessName = businessDetails.BusinessName;
                businessViewModel.LastSubscribedDate = businessDetails.LastSubscribedDate;
                businessViewModel.LastExpirationDate = businessDetails.LastExpirationDate;
                businessViewModel.AddressLine1 = businessDetails.AddressLine1;
                businessViewModel.AddressLine2 = businessDetails.AddressLine2;
                businessViewModel.AddressLine3 = businessDetails.AddressLine3;
                businessViewModel.City = businessDetails.City;
                businessViewModel.Postcode = businessDetails.Postcode;
                businessViewModel.ContactPerson = businessDetails.ContactPerson;
                businessViewModel.ContactEmail = businessDetails.ContactEmail;
                businessViewModel.ContactPhone = businessDetails.ContactPhone;
                businessViewModel.IdentityUserId = businessDetails.IdentityUserId;
                businessViewModel.NotifyWeekly = businessDetails.NotifyWeekly;

                businessViewModel.DmdAmp = businessDetails.DmdAmp;
                businessViewModel.DmdAmpp = businessDetails.DmdAmpp;
                businessViewModel.DmdVmp = businessDetails.DmdVmp;
                businessViewModel.DmdVmpp = businessDetails.DmdVmpp;
                businessViewModel.DmdVtm = businessDetails.DmdVtm;
                businessViewModel.DmdForm = businessDetails.DmdForm;
                businessViewModel.DmdGtin = businessDetails.DmdGtin;
                businessViewModel.DmdRoute = businessDetails.DmdRoute;
                businessViewModel.DmdSupplier = businessDetails.DmdSupplier;

            }

            return businessViewModel;
        }

        public BusinessDetails GetSiteById(int id)
        {
            return repository.GetById<BusinessDetails>(id);
        }

        public int MapViewModelToSite(BusinessViewModel model, string user, bool performSave)
        {
            BusinessDetails businessDetails = new BusinessDetails();
            businessDetails = GetSiteById(model.Id);

            //businessDetails = businessDetails == null ? Mapper.Map<BusinessViewModel, BusinessDetails>(model) :
            //    Mapper.Map(model, businessDetails);

            // businessDetails = businessDetails == null ? new BusinessViewModel() : Mapper.Map<BusinessDetails, BusinessDetails>(model);
            if (businessDetails == null)
            {
                businessDetails = new BusinessDetails();
                businessDetails.Id = model.Id;
                businessDetails.BusinessName = model.BusinessName;
                //businessDetails.LastSubscribedDate = model.LastSubscribedDate;
                //businessDetails.LastExpirationDate = model.LastExpirationDate;
                businessDetails.AddressLine1 = model.AddressLine1;
                businessDetails.AddressLine2 = model.AddressLine2;
                businessDetails.AddressLine3 = model.AddressLine3;
                businessDetails.City = model.City;
                businessDetails.Postcode = model.Postcode;
                businessDetails.ContactPerson = model.ContactPerson;
                businessDetails.ContactEmail = model.ContactEmail;
                businessDetails.ContactPhone = model.ContactPhone;
                businessDetails.IdentityUserId = "0";
                businessDetails.NotifyWeekly = model.NotifyWeekly == true;
                businessDetails.DmdAmp = model.DmdAmp;
                businessDetails.DmdAmpp = model.DmdAmpp;
                businessDetails.DmdVmp = model.DmdVmp;
                businessDetails.DmdVmpp = model.DmdVmpp;
                businessDetails.DmdVtm = model.DmdVtm;
                businessDetails.DmdForm = model.DmdForm;
                businessDetails.DmdGtin = model.DmdGtin;
                businessDetails.DmdRoute = model.DmdRoute;
                businessDetails.DmdSupplier = model.DmdSupplier;
            }
            else
            {
                businessDetails.Id = model.Id;
                businessDetails.BusinessName = model.BusinessName;
                businessDetails.LastSubscribedDate = model.LastSubscribedDate;
                businessDetails.LastExpirationDate = model.LastExpirationDate;
                businessDetails.AddressLine1 = model.AddressLine1;
                businessDetails.AddressLine2 = model.AddressLine2;
                businessDetails.AddressLine3 = model.AddressLine3;
                businessDetails.City = model.City;
                businessDetails.Postcode = model.Postcode;
                businessDetails.ContactPerson = model.ContactPerson;
                businessDetails.ContactEmail = model.ContactEmail;
                businessDetails.ContactPhone = model.ContactPhone;
                businessDetails.NotifyWeekly = model.NotifyWeekly == true;
                businessDetails.IdentityUserId = model.IdentityUserId;

                businessDetails.DmdAmp = model.DmdAmp;
                businessDetails.DmdAmpp = model.DmdAmpp;
                businessDetails.DmdVmp = model.DmdVmp;
                businessDetails.DmdVmpp = model.DmdVmpp;
                businessDetails.DmdVtm = model.DmdVtm;
                businessDetails.DmdForm = model.DmdForm;
                businessDetails.DmdGtin = model.DmdGtin;
                businessDetails.DmdRoute = model.DmdRoute;
                businessDetails.DmdSupplier = model.DmdSupplier;
            }

            if (!performSave) return businessDetails.Id;

            if (businessDetails.Id > 0)
            {
                businessDetails.SetUpdateDetails(user);
                repository.SaveExisting(businessDetails);
            }
            else
            {
                businessDetails.SetCreateDetails(user);
                repository.AddNew(businessDetails);
            }

            return businessDetails.Id;
        }
        public void DeleteBusiness(int id)
        {
            BusinessDetails businessDetails = new BusinessDetails();
            businessDetails = GetSiteById(id);
            repository.Delete(businessDetails);
        }

        public Dmd_BusinessChangeSetDetails ToGetLatestChangeSetDetails(string userName)
        {
            Dmd_BusinessChangeSetDetails dmBusinessChangeSetDetails = new Dmd_BusinessChangeSetDetails();
            try
            {
                //TODO:Riyaz
                HttpClient client = pharmixWebApiClient.InitializeClient(_apiBaseURI);

                HttpResponseMessage response = client.GetAsync("api/PharmixApi/ToGetLatestChangeSetDetails?userName=" + userName).Result;

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var result = response.Content.ReadAsStringAsync().Result;
                    dmBusinessChangeSetDetails = JsonConvert.DeserializeObject<Dmd_BusinessChangeSetDetails>(result);
                }
            }
            catch { }
            return dmBusinessChangeSetDetails;

        }

        public void SaveEmailPasswordTokeneDetails(EmailPasswordTokeneDetails model)
        {
            repository.AddNew(model);
        }

        public EmailPasswordTokeneDetails GetEmailPasswordTokeneDetailsById(string guid)
        {
            return repository.GetAll<EmailPasswordTokeneDetails>().Where(e => e.PassworkToken == guid && e.IsActive ==true).FirstOrDefault();
        }
    }
}
