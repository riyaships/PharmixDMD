using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.Context;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using PharmixWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Services
{
    public interface IBusinessService
    {
        int SaveSite(BusinessDetails Site, string user);
        GridViewModel GetSearchResult(SearchRequest request);
        BusinessViewModel CreateViewModel(int id);
        int MapViewModelToSite(BusinessViewModel model, string user, bool performSave);
        void DeleteBusiness(int id);
        Dmd_BusinessChangeSetDetails ToGetLatestChangeSetDetails(string userName);
        void SaveEmailPasswordTokeneDetails(EmailPasswordTokeneDetails model);
        EmailPasswordTokeneDetails GetEmailPasswordTokeneDetailsById(string guid);
    }
}
