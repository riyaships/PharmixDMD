using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Services.Mappers;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using X.PagedList;

namespace Pharmix.Web.Services
{
    public class PackagePlanService : IPackagePlanService
    {
        private readonly IRepository repository;

        public PackagePlanService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<PackagePlan> GetAllPackagePlans()
        {
            return repository.GetAll<PackagePlan>().Where(e => !e.IsArchived);
        }

        public PackagePlan GetPackagePlanById(int id)
        {
            return repository.GetById<PackagePlan>(id);
        }

        public bool ArchivePackagePlan(int id, string user)
        {
            var PackagePlan = repository.GetById<PackagePlan>(id);

            PackagePlan.SetArchiveDetails(user);
            repository.SaveExisting(PackagePlan);

            return true;
        }

        public int SavePackagePlan(PackagePlan PackagePlan, string user)
        {
            if (PackagePlan.Id > 0)
            {
                PackagePlan.SetUpdateDetails(user);
                repository.SaveExisting(PackagePlan);
                return PackagePlan.Id;
            }

            PackagePlan.SetCreateDetails(user);
            return repository.SaveNew(PackagePlan).Id;
        }

        public SelectList GetDurationSelectList(string selectedValue = "")
        {
            List<SelectListItem> duration = new List<SelectListItem>{
                                                                        new SelectListItem { Text = "One Time", Value = "One Time"},
                                                                        new SelectListItem { Text = "Monthly", Value = "Monthly"},
                                                                        new SelectListItem { Text = "Annual", Value = "Annual"}
                                                                    };

            SelectList roles = null;
            if (!string.IsNullOrEmpty(selectedValue))
                roles = new SelectList(duration, "Value", "Text", selectedValue);
            else
                roles = new SelectList(duration, "Value", "Text");
            return roles;
        }

        public GridViewModel GetSearchResult(SearchRequest request)
        {
            var model = PackagePlanMapper.CreateGridViewModel();

            var pageResult = QueryListHelper.SortResults(GetAllPackagePlans(), request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.PackageName.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(PackagePlanMapper.BindGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public PackagePlanViewModel CreateViewModel(int id)
        {
            var model = GetPackagePlanById(id);
            PackagePlanViewModel plan = new PackagePlanViewModel();
            if (model != null)
            {
                plan.Id = model.Id;
                plan.PackageName = model.PackageName;
                plan.Price = model.Price;
                plan.Duration = model.Duration;
                plan.Details = model.Details;
            }

            return plan;
        }

        public int MapViewModelToPackagePlan(PackagePlanViewModel model, string user, bool performSave)
        {
            var plan = GetPackagePlanById(model.Id);

            if (plan == null)
            {
                plan = new PackagePlan();
            }
            plan.Id = model.Id;
            plan.PackageName = model.PackageName;
            plan.Price = model.Price;
            plan.Duration = model.Duration;
            plan.Details = model.Details;

            if (!performSave) return plan.Id;

            if (plan.Id > 0)
            {
                plan.SetUpdateDetails(user);
                repository.SaveExisting(plan);
            }
            else
            {
                plan.SetCreateDetails(user);
                repository.SaveNew(plan);
            }

            return plan.Id;
        }

    }
}
