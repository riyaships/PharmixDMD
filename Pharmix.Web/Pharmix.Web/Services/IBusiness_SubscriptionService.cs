using System.Collections.Generic;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;

namespace Pharmix.Web.Services
{
    public interface IBusiness_SubscriptionService
    {
        IEnumerable<Business_Subscription> GetAllBusiness_Subscription();
        Business_Subscription GetBusiness_SubscriptionById(int id);

        GridViewModel GetSearchResult(SearchRequest request, string user);
        int MapViewModelToBusiness_Subscription(Business_Subscription model, string user, bool performSave);
    }
}
