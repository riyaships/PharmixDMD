using System;
using System.Collections.Generic;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Models;
using Pharmix.Web.Models.IsolatorVIewModels;

namespace Pharmix.Web.Services
{
    public interface IIsolatorService
    {
        IEnumerable<Isolator> GetAllIsolators();
        Isolator GetIsolatorById(int id);
        int SaveIsolator(Isolator isolator, string user);
        bool ArchiveIsolator(int id, string user);
        bool IsolatorUsed(int isolatorId);
        int SaveIsolatorOfflineDetail(IsolatorOfflineViewModel model, string user);

        IEnumerable<IsolatorStaffAllocation> GetIsolatorStaffAllocations(int isolatorId, DateTime requstedDate);
        IsolatorStaffAllocationViewModel CreateAllocationViewModel(int allocationId, int isolatorId);
        IsolatorStaffAllocation MapViewModelToStaffAllocation(IsolatorStaffAllocationViewModel model, string user, bool performSave);
        bool RemoveStaffAllocation(int allocationId, string user);

        GridBoxViewModel GetSearchResult(SearchRequest request);
        IsolatorViewModel CreateViewModel(int id);
        int MapViewModelToIsolator(IsolatorViewModel model, string user, bool performSave);

        IsolatorMappedProcedureViewModel CreateIsolatorProcedureViewModel(int isoId);
        int SaveIsolatorProcedures(IsolatorMappedProcedureViewModel model, string user);

        #region Order preperation
        IEnumerable<IntegrationOrderPreperation> GetPreperationOrdersByDate(DateTime preperationDate);
        IEnumerable<IntegrationOrderPreperation> GetPreperationOrdersByIsolator(int isoId, DateTime preperationDate);
        IEnumerable<Isolator> GetAvailableIsolatorsByDate(DateTime date);
        void CreateOrderIsolatorMapping(int orderId, int isoId, DateTime scheduledDate, int staffAllocationId);
        bool RemoveOrderIsolatorMapping(int orderPreperationId);
        GridBoxViewModel GetOrderSearchResult(SearchRequest request);
        #endregion
    }
}
