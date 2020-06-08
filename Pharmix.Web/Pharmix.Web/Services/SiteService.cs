using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Services.Mappers;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.Context;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using X.PagedList;

namespace Pharmix.Web.Services
{
    public class SiteService : ISiteService
    {
        private readonly IRepository repository;

        public SiteService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Site> GetAllSites()
        {
            return repository.GetAll<Site>().Where(e => !e.IsArchived);
        }

        public Site GetSiteById(int id)
        {
            return repository.GetById<Site>(id);
        }

        public bool ArchiveSite(int id, string user)
        {
            var Site = repository.GetById<Site>(id);

            Site.SetArchiveDetails(user);
            repository.SaveExisting(Site);

            return true;
        }

    
        public int SaveSite(Site Site, string user)
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
            var model = SiteMapper.CreateGridViewModel();

            var pageResult = QueryListHelper.SortResults(GetAllSites(), request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.Name.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(SiteMapper.BindGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public SiteViewModel CreateViewModel(int id)
        {
            var Site = GetSiteById(id);
            var model = Site == null ? new SiteViewModel() : Mapper.Map<Site, SiteViewModel>(Site);

            return model;
        }

        public int MapViewModelToSite(SiteViewModel model, string user, bool performSave)
        {
            var Site = GetSiteById(model.Id);

            Site = Site == null ? Mapper.Map<SiteViewModel, Site>(model) :
                Mapper.Map(model, Site);

            if (!performSave) return Site.Id;

            if (Site.Id > 0)
            {
                Site.SetUpdateDetails(user);
                repository.SaveExisting(Site);
            }
            else
            {
                Site.SetCreateDetails(user);
                repository.SaveNew(Site);
            }

            return Site.Id;
        }
    }
}
