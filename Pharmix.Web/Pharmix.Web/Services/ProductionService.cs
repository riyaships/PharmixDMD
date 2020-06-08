using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Data.Enums;
using Pharmix.Services.Mappers;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Entities.ViewModels.Production;
using Pharmix.Web.Extensions;
using Pharmix.Web.Models;
using Pharmix.Web.Models.IsolatorVIewModels;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using X.PagedList;

namespace Pharmix.Web.Services
{
    public class ProductionService: IProductionService
    {
        private readonly IRepository _repository;
        private readonly IIsolatorService _isolatorService;
        private readonly IShiftService _shiftService;

        public ProductionService(IRepository repository, IIsolatorService isolatorService, IShiftService shiftService)
        {
            _repository = repository;
            _isolatorService = isolatorService;
            _shiftService = shiftService;
        }
        
        public IEnumerable<SupervisorRequest> GetSupervisorRequests()
        {
            return _repository.Get<SupervisorRequest>().Where(e => !e.IsArchived);
        }

        public SupervisorRequest FindById(int id)
        {
            return _repository.GetById<SupervisorRequest>(id);
        }


        public List<ProductionIsolatorViewModel> GetProductionIsolators(string userId)
        {
            var isos = _repository.GetContext().Isolators
                .Include(p => p.StaffShiftAllocations)
                .ThenInclude(p => p.Staff)
                .Where(p => p.StaffShiftAllocations.Any(m => m.StaffId == userId && m.AllocatedDate.Date == DateTime.Today.Date) && !p.IsArchived);

            var model = new List<ProductionIsolatorViewModel>();

            foreach (var iso in isos)
            {
                var item = new ProductionIsolatorViewModel
                {
                    IsolatorId = iso.IsolatorId,
                    IsolatorName = iso.Abbriviation,
                    TotalNumberOfDosesPerSession = iso.TotalNumberOfDosesPerSession,
                };

                var staffAlloc = iso.StaffShiftAllocations.FirstOrDefault(p => p.IsUsingIsolatorNow);
                if (staffAlloc != null)
                {
                    item.IsBeingUsed = true;
                    item.UserName = staffAlloc.Staff.DisplayName();
                }

                model.Add(item);
            }
            return model;
            //var data = from iso in _repository.GetAll<Isolator>().Where(p => !p.IsArchived).AsEnumerable()
            //           join isoshift in _repository.GetAll<IsolatorStaffAllocation>().Where(p => !p.IsArchived).AsEnumerable()
            //           on iso.IsolatorId equals isoshift.IsolatorId into ishoShiftGroup
            //           from g1 in ishoShiftGroup.DefaultIfEmpty(new IsolatorStaffAllocation { IsolatorStaffAllocationId = 0 })
            //           join user in _repository.Get<ApplicationUser>().AsEnumerable()
            //           on g1.CreatedUser equals user.UserName into userGroup
            //           from g2 in userGroup.DefaultIfEmpty(new ApplicationUser { UserName = string.Empty })
            //           select new ProductionIsolatorViewModel
            //           {
            //               Id = iso.IsolatorId,
            //               Abbriviation = iso.Abbriviation,
            //               TotalNumberOfDosesPerSession = iso.TotalNumberOfDosesPerSession,
            //               IsolatorStaffShiftId = g1.IsolatorStaffAllocationId == 0 ? (Int32?)null : g1.IsolatorStaffAllocationId,
            //               CreatedUser = g1.CreatedUser,
            //               StartTime = DateTime.Compare(g1.CreatedDate, DateTime.MinValue) == 0 ? (DateTime?)null : g1.CreatedDate,
            //               UserName = g2.UserName
            //           };
            //return data.ToList();
        }

        public SupervisorRequestViewModel CreateRequestViewModel(int requestId)
        {
            var req = _repository.GetById<SupervisorRequest>(requestId);
            
            var result = req == null? new SupervisorRequestViewModel() : Mapper.Map<SupervisorRequest, SupervisorRequestViewModel>(req);

            result.IntegrationOrders = _repository.GetAll<IntegrationOrder>().Where(p => !p.IsArchived);
            result.SupervisorRequetTypes = _repository.GetAll<SupervisorRequestType>().Where(p => !p.IsArchived);
            //result.Isolators = _repository.GetAll<Isolator>().Where(p => !p.IsArchived);
            return result;
        }

        //public BaseResultViewModel<int> StartUsingIsolator(int isolatorId, string user)
        //{
        //    BaseResultViewModel<int> result = new BaseResultViewModel<int>
        //    {
        //        IsSuccess = false,
        //        Message = "",
        //        Extra = 0
        //    };

        //    var IsolatorIsUsed = _isolatorService.IsolatorUsed(isolatorId);
        //    if (IsolatorIsUsed)
        //    {
        //        result.Message = "Failed, Isolator has been used by other users";
        //        return result;
        //    }
        //    var getUsedIsolatorId = _shiftService.GetIsolatorStaffShifts().Where(p => !p.IsArchived && p.CreatedUser.ToString().Equals(user, StringComparison.CurrentCultureIgnoreCase));
        //    if (getUsedIsolatorId.Count() > 0)
        //    {
        //        result.Message = "Failed, Please logout of any other isolators.";
        //        return result;
        //    }

        //    try
        //    {
        //        //TODO: Riyaz Initialising shift if not exists
        //        var firstAvailableShift = _shiftService.GetShifts().FirstOrDefault() ?? new IsolatorShift() { StartTime = "08:00", EndTime = "13:00" };

        //        var isolatorStaffShift = new IsolatorStaffAllocation()
        //        {
        //            IsolatorId = isolatorId,
        //            CreatedUser = user,
        //            CreatedDate = DateTime.Now,
        //            IsArchived = false,
        //            IsolatorShift = firstAvailableShift
        //        };

        //        result.Extra = _shiftService.CreateIsolatorStaffShift(isolatorStaffShift);
        //        result.IsSuccess = result.Extra > 0;
        //        if(result.IsSuccess)
        //            result.Message = "Success, Start using Isolator";
        //        else
        //            result.Message = "Failed using Isolator";
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }

        //    return result;
        //}

        //public BaseResultViewModel<string> StopUsingIsolator(int isolatorId, int isolatorStaffShiftId, string user)
        //{
        //    BaseResultViewModel<string> result = new BaseResultViewModel<string>
        //    {
        //        IsSuccess = false,
        //        Message = ""
        //    };

        //    var getIsolaorShift = _shiftService.GetIsolatorStaffShifts().Where(p => p.IsolatorId == isolatorId && p.IsolatorStaffAllocationId == isolatorStaffShiftId && p.CreatedUser.Equals(user));
        //    if (getIsolaorShift.Count() == 0)
        //    {
        //        result.Message = "Failed, You didn't use any Isolator";
        //        return result;
        //    }

        //    try
        //    {
        //        IsolatorStaffAllocation isolatorStaffShift = getIsolaorShift.FirstOrDefault();
        //        isolatorStaffShift.IsArchived = true;
        //        isolatorStaffShift.ArchivedUser = user;
        //        isolatorStaffShift.ArchivedDate = DateTime.Now;
        //        var data = _shiftService.UpdateIsolatorStaffShift(isolatorStaffShift);
        //        result.IsSuccess = string.IsNullOrEmpty(data);
        //        if(result.IsSuccess)
        //            result.Message = "Success, Stop using Isolator";
        //        else
        //            result.Message = "Failed, Stop using Isolator";

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }

        //    return result;
        //}

        public BaseResultViewModel<string> CreateRequest(SupervisorRequestViewModel supervisorRequest, string user)
        {
            BaseResultViewModel<string> result = new BaseResultViewModel<string>
            {
                IsSuccess = false,
                Message = ""
            };

            try
            {
                var model = Mapper.Map<SupervisorRequestViewModel, SupervisorRequest>(supervisorRequest);
                model.RequestedBy = user;
                model.LatestRequestStatus = RequestStatusEnum.Awaiting;
                
                if (model.Id > 0)
                {
                    model.SetUpdateDetails(user);
                    _repository.SaveExisting(model);
                }
                else
                {
                    model.SetCreateDetails(user);
                    _repository.SaveNew(model);
                }
                
                result.IsSuccess = true;
                result.Message = "Success: Request has sent";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public BaseResultViewModel<string> ChangeRequestStatus(int RequestId, bool IsApproved, string user)
        {
            BaseResultViewModel<string> result = new BaseResultViewModel<string>
            {
                IsSuccess = false,
                Message = "",
                Extra = user
            };

            var data = this.UpdateRequestStatus(RequestId, IsApproved, user);

            if (IsApproved && data)
            {
                result.Message = "Approved notification has sent";
                result.IsSuccess = true;
            }
            else if (!IsApproved && data)
            {
                result.Message = "Decline notification has sent";
                result.IsSuccess = true;
            }
            else
            {
                result.Message = "Failed: an error occured";
            }

            return result;
        }
        
        public bool UpdateRequestStatus(int RequestId, bool IsApproved, string UserName)
        {
            bool result = false;
            try
            {
                var findRequest = _repository.GetById<SupervisorRequest>(RequestId);
                if(findRequest.LatestRequestStatus.Equals(RequestStatusEnum.Declined) || findRequest.Equals(RequestStatusEnum.Approved))
                {
                    return false;
                }

                var tracking = new SupervisorRequestTracking()
                {
                    RequestId = RequestId,
                    RequestStatus = RequestStatusEnum.Approved,
                    CreatedDate = DateTime.Now,
                    CreatedUser = UserName,
                    LastModified = DateTime.Now,
                    LastModifiedUser = UserName
                };

                if (IsApproved)
                {
                    findRequest.LatestRequestStatus = RequestStatusEnum.Approved;
                    findRequest.SetUpdateDetails(UserName);
                    findRequest.SetArchiveDetails(UserName);
                }
                else
                {
                    tracking.RequestStatus = RequestStatusEnum.Declined;

                    findRequest.LatestRequestStatus = RequestStatusEnum.Declined;
                    findRequest.SetUpdateDetails(UserName);
                }

                var createTracking = _repository.SaveNew<SupervisorRequestTracking>(tracking);
                _repository.SaveExisting<SupervisorRequest>(findRequest);
                result = true;
            }
            catch(Exception ex)
            {
                var error = ex.Message;
            }

            return result;
        }
        public IEnumerable<SupervisorRequestType> GetSupervisorRequetTypes()
        {
            return _repository.GetAll<SupervisorRequestType>().Where(p => !p.IsArchived);
        }


        public GridViewModel GetSearchResult(SearchRequest request, bool isSupervisorRequest, string currentUser)
        {
            var model = SupervisorRequestMapper.CreateGridViewModel();
            var getAllSupervisorRequests = _repository.GetContext().SupervisorRequest.Include(p => p.Isolator).Where(p => !p.IsArchived &&(isSupervisorRequest || p.RequestedBy == currentUser));

            var pageResult = QueryListHelper.SortResults(getAllSupervisorRequests, request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) ||
                    p.Title.Contains(request.SearchText, StringComparison.OrdinalIgnoreCase) ||
                    Enum.GetName(typeof(RequestStatusEnum), p.LatestRequestStatus).Contains(request.SearchText, StringComparison.OrdinalIgnoreCase) ||
                    Enum.GetName(typeof(RequsetPriorityEnum), p.Priority).Contains(request.SearchText, StringComparison.OrdinalIgnoreCase) ||
                    p.Isolator.Abbriviation.Contains(request.SearchText, StringComparison.OrdinalIgnoreCase)
                    //p.RequestType.Type.Contains(request.SearchText, StringComparison.OrdinalIgnoreCase)
                )
                .Select(item => SupervisorRequestMapper.BindGridData(item, isSupervisorRequest));
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }
        private bool IsCurrentShift(IsolatorShift p)
        {
            var st = Convert.ToDateTime(p.StartTime);
            var et = Convert.ToDateTime(p.EndTime);

            if (et < st)
                st = st.AddDays(-1);

            var stat1 = st <= DateTime.Now;
            var stat2 = et >= DateTime.Now;
            return stat1 && stat2;
        }

        public ProductionOrderListViewModel CreateProductionOrderListViewModel(int isoId, DateTime dt, string staffId)
        {
            var iso = _repository.GetById<Isolator>(isoId);
            var prodOrders = _repository.GetContext().IntegrationOrderPreperations
                .Include(p => p.IntegrationOrder)
                .Where(p => p.PreperationDateTime.Date == dt.Date && !p.IsArchived)
                .Where(p => p.IsolatorId == isoId)
                .Where(p => p.IsolatorStaffAllocation.StaffId == staffId);

            var model = new ProductionOrderListViewModel
            {
                IsolatorId = isoId,
                IsolatorName = iso.Abbriviation,
                ProductionDate = dt
            };
            
            var currentShift = _repository.GetContext().IsolatorShifts.FirstOrDefault(p => IsCurrentShift(p));

            if (currentShift == null) return model;

            var startTime = Convert.ToDateTime(currentShift.StartTime);

            var onProcessOrder = prodOrders.FirstOrDefault(p =>
                p.PreperationStatusId == (int) OrderPreperationStatusEnum.Started);
            //var pausedOrders = prodOrders.Where(p =>
            //    p.PreperationStatusId == (int)OrderPreperationStatusEnum.Paused);

            foreach (var order in prodOrders)
            {
                var endTime = startTime.AddMinutes(order.IntegrationOrder.RequiredPreperationTimeInMins);
                model.Orders.Add(new ProductionOrderDetail
                {
                    OrderPreperationId = order.IntegrationOrderPreperationId,
                    OrderId=order.IntegrationOrderId,
                    OrderName = order.IntegrationOrder.Name,
                    RequiredPreperationTimeInMins = order.IntegrationOrder.RequiredPreperationTimeInMins,
                    PreperationStatus = (OrderPreperationStatusEnum)order.PreperationStatusId,
                    ProductionDisplayTime = string.Format("{0} to {1}", startTime.ToString("HH:mm"), endTime.ToString("HH:mm")),
                    EnableActions = onProcessOrder == null || onProcessOrder.IntegrationOrderId == order.IntegrationOrder.Id
                });
                startTime = endTime;
            }

            model.ProductionShift = string.Format("{2} ({0} - {1})", currentShift.StartTime, currentShift.EndTime,
                currentShift.ShiftTitle);

            //set isolator using flag
            var staffAlloc = _repository.GetContext().IsolatorStaffAllocations
                .Where(p => p.AllocatedDate.Date == dt.Date && !p.IsArchived)
                .Where(p => p.IsolatorId == isoId)
                .FirstOrDefault(p => p.StaffId == staffId);

            if (staffAlloc == null) return model;

            staffAlloc.IsUsingIsolatorNow = true;
            _repository.SaveExisting(staffAlloc);

            return model;
        }

        public int SetProductionOrderStatus(int prepOrderId, int statusId, string user)
        {
            var prepOrder = _repository.GetContext().IntegrationOrderPreperations
                .Include(p => p.IntegrationOrder)
                .FirstOrDefault(p => p.IntegrationOrderPreperationId == prepOrderId);

            if (prepOrder == null) return 0;
            
            switch ((OrderPreperationStatusEnum)statusId)
            {
                case OrderPreperationStatusEnum.Started:
                    prepOrder.IntegrationOrder.OrderLastProgressId = (int)OrderProgressEnum.Compounding;
                    break;
                case OrderPreperationStatusEnum.Paused:
                    prepOrder.IntegrationOrder.OrderLastProgressId = (int)OrderProgressEnum.Compounding;
                    break;
                case OrderPreperationStatusEnum.Completed:
                    prepOrder.IntegrationOrder.OrderLastProgressId = (int)OrderProgressEnum.Completed;
                    break;
                case OrderPreperationStatusEnum.YetToStart:
                    prepOrder.IntegrationOrder.OrderLastProgressId = (int)OrderProgressEnum.PreperationScheduled;
                    break;
                default:
                    prepOrder.IntegrationOrder.OrderLastProgressId = (int)OrderProgressEnum.PreperationScheduled;
                    break;
            }
            
            prepOrder.IntegrationOrder.SetUpdateDetails(user);
            _repository.SaveExisting(prepOrder.IntegrationOrder);

            prepOrder.PreperationStatusId = statusId;
            prepOrder.SetUpdateDetails(user);
            _repository.SaveExisting(prepOrder);

            return statusId;
        }

        public bool StopIsolator(int isoId, DateTime dt, string staffId)
        {
            //set isolator using flag
            var staffAlloc = _repository.GetContext().IsolatorStaffAllocations
                .Where(p => p.AllocatedDate.Date == dt.Date && !p.IsArchived)
                .Where(p => p.IsolatorId == isoId)
                .FirstOrDefault(p => p.StaffId == staffId);

            if (staffAlloc == null) return false;

            staffAlloc.IsUsingIsolatorNow = false;
            _repository.SaveExisting(staffAlloc);

            return true;
        }

        public IsolatorProcedureViewModel CreateIsolatorProcedureViewModel(int isoId, DateTime dt, int procTypeId)
        {
            var iso = _repository.GetContext().Isolators
                .Include(p => p.Procedures)
                .ThenInclude(p => p.Procedure)
                .FirstOrDefault(p => p.IsolatorId == isoId);

            if(iso == null) return new IsolatorProcedureViewModel();

            var procs = iso.Procedures.Where(p => p.Procedure.ProcedureTypeId == procTypeId)
                .Select(p => new {Value = p.ProcedureId, Text = p.Procedure.Description});
            var model = new IsolatorProcedureViewModel
            {
                IsolatorId = isoId,
                IsolatorName = iso.Abbriviation,
                ProductionDate = dt.ToString("yyyy-MM-dd"),
                ProcedureType = (IsolatorProcedureTypeEnum)procTypeId,
                ProcedureList = new MultiSelectList(procs, "Value", "Text")
            };

            return model;
        }
    }
    
}
