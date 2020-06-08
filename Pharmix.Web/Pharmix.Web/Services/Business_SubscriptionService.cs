using System.Collections.Generic;
using System.Linq;
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
    public class Business_SubscriptionService : IBusiness_SubscriptionService
    {
        private readonly IRepository repository;

        public Business_SubscriptionService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Business_Subscription> GetAllBusiness_Subscription()
        {
            return repository.GetAll<Business_Subscription>();
        }

        public Business_Subscription GetBusiness_SubscriptionById(int id)
        {
            return repository.GetById<Business_Subscription>(id);
        }
         
        public GridViewModel GetSearchResult(SearchRequest request, string user)
        {
            var model = Business_SubscriptionMapper.CreateGridViewModel();

            var pageResult = QueryListHelper.SortResults(GetAllBusiness_Subscription(), request);
            var serviceRows = pageResult.Where(p => p.IdentityUserId == user).Select(Business_SubscriptionMapper.BindGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }
         
        public int MapViewModelToBusiness_Subscription(Business_Subscription model, string user, bool performSave)
        {
            model.SetCreateDetails(user);
            repository.SaveNew(model);
            return model.Id;
        }
    }
}