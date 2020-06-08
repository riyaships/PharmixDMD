using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Entities.ViewModels.IntegrationOrder;
using Pharmix.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Services
{
    public interface IIntegrationOrderService
    {
        IEnumerable<IntegrationOrder> GetIntegrationOrders();
        IEnumerable<IntegrationOrderClassification> GetOrderClassifications();
        IEnumerable<IntegrationOrderProgress> GetOrderProgresses();

        IntegrationOrder GetIntegrationOrder(int id);
        BaseResultViewModel<string> SaveUpdateIntegrationOrder(IntegrationOrderViewModel integrationOrderViewModel, string user);
        GridViewModel GetSearchResult(SearchRequest request);
        IntegrationOrderViewModel CreateViewModel(int id);
        CallSupervisorViewModel CreateCallSupervisorViewModel(int OrderId);
        BaseResultViewModel<string> CallSupervisor(CallSupervisorViewModel param, string user, int location);
        BaseResultViewModel<string> SaveActionDelineClassify(IntegrationOrderCommentViewModel param, string user, int location);
        BaseResultViewModel<string> ApproveOrder(int OrderId, string user, int location);
    }
}
