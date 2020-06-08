using System;
using System.Collections.Generic;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.Context;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;

namespace Pharmix.Web.Services
{
    public interface ISiteService
    {
        IEnumerable<Site> GetAllSites();
        Site GetSiteById(int id);
        int SaveSite(Site Site, string user);
        bool ArchiveSite(int id, string user);

        GridViewModel GetSearchResult(SearchRequest request);
        SiteViewModel CreateViewModel(int id);
        int MapViewModelToSite(SiteViewModel model, string user, bool performSave);
    }
}
