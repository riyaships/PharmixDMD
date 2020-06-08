using System;
using System.Collections.Generic;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Entities.ViewModels.Production;
using Pharmix.Web.Models;
using Pharmix.Web.Models.IsolatorVIewModels;

namespace Pharmix.Web.Services
{
    public interface IProductionService
    {
        IEnumerable<SupervisorRequest> GetSupervisorRequests();
        SupervisorRequest FindById(int id);
        List<ProductionIsolatorViewModel> GetProductionIsolators(string userId);
        SupervisorRequestViewModel CreateRequestViewModel(int RequestId);
        //BaseResultViewModel<int> StartUsingIsolator(int isolatorId, string user);
        //BaseResultViewModel<string> StopUsingIsolator(int isolatorId, int isolatorStaffShiftId, String user);
        BaseResultViewModel<string> CreateRequest(SupervisorRequestViewModel supervisorRequest, String user);
        IEnumerable<SupervisorRequestType> GetSupervisorRequetTypes();
        BaseResultViewModel<string> ChangeRequestStatus(int RequestId, bool IsApproved, string user);
        GridViewModel GetSearchResult(SearchRequest request, bool isSupervisorRequest, string currentUserId);
        ProductionOrderListViewModel CreateProductionOrderListViewModel(int isoId, DateTime dt, string staffId);
        int SetProductionOrderStatus(int prepOrderId, int statusId, string user);
        bool StopIsolator(int isoId, DateTime dt, string staffId);
        IsolatorProcedureViewModel CreateIsolatorProcedureViewModel(int isoId, DateTime dt, int procTypeId);
    }
}
