using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Models;
using Pharmix.Web.Models.PatientViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Services
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetPatients();
        GridViewModel GetSearchResult(SearchRequest request);
        Task<bool> CreateUser(PatientViewModel model);
        PregnancyViewModel GetDetail(string UserId, int PatientId);
        bool SaveDetail(PregnancyViewModel model, string UserId);
    }
}
