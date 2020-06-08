using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;

namespace Pharmix.Web.Services
{
    public interface IPackagePlanService
    {
        IEnumerable<PackagePlan> GetAllPackagePlans();
        PackagePlan GetPackagePlanById(int id); 
        bool ArchivePackagePlan(int id, string user);

        GridViewModel GetSearchResult(SearchRequest request);
        PackagePlanViewModel CreateViewModel(int id);
        int MapViewModelToPackagePlan(PackagePlanViewModel model, string user, bool performSave);

        SelectList GetDurationSelectList(string selectedValue = "");
    }
}
