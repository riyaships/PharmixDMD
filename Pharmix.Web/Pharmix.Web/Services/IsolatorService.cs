using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Data.Enums;
using Pharmix.Web.Entities;
using Pharmix.Web.Models;
using Pharmix.Web.Models.IsolatorVIewModels;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using X.PagedList;

namespace Pharmix.Web.Services
{
    public class IsolatorService : IIsolatorService
    {
        private readonly IRepository repository;

        public IsolatorService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Isolator> GetAllIsolators()
        {
            return repository.GetAll<Isolator>().Where(e => !e.IsArchived);
        }

        public Isolator GetIsolatorById(int id)
        {
            return repository.GetById<Isolator>(id);
        }

        public bool ArchiveIsolator(int id, string user)
        {
            var isolator = repository.GetById<Isolator>(id);

            isolator.SetArchiveDetails(user);
            repository.SaveExisting(isolator);

            return true;
        }
        
        public bool IsolatorUsed(int IsolatorId)
        {
            var iso = repository.GetContext().IsolatorStaffAllocations.FirstOrDefault(p => p.IsolatorId == IsolatorId && !p.IsArchived);
            return iso != null;
        }

        public int SaveIsolatorOfflineDetail(IsolatorOfflineViewModel model, string user)
        {
            var iso = repository.GetContext().Isolators
                .Include(p => p.StaffShiftAllocations)
                .Include(p => p.PreperationOrders).ThenInclude(p => p.IntegrationOrder)
                .FirstOrDefault(p => p.IsolatorId == model.IsolatorId);

            if (iso == null || !model.StartDate.HasValue || !model.EndDate.HasValue) return 0;
            
            iso.OfflineStartDate = model.StartDate;
            iso.OfflineEndDate = model.EndDate;
            iso.OfflineShifts = model.AllShifts? null : string.Join("|", model.SelectedShifts);

            iso.SetUpdateDetails(user);
            //repository.SaveExisting(iso);

            //unmapping staff allocation
            foreach (var staffAllocation in iso.StaffShiftAllocations.Where(s =>
                s.AllocatedDate.Date <= model.StartDate.Value.Date && s.AllocatedDate.Date <= model.EndDate.Value.Date && 
                (!model.SelectedShifts.Any() || model.SelectedShifts.Contains(s.IsolatorShiftId))))
            {
                staffAllocation.SetArchiveDetails(user);
            }

            //unmapping order scheduling
            foreach (var prepOrder in iso.PreperationOrders.Where(s =>
                s.PreperationDateTime.Date <= model.StartDate.Value.Date && s.PreperationDateTime.Date <= model.EndDate.Value.Date &&
                    (!model.SelectedShifts.Any() || model.SelectedShifts.Contains(s.IsolatorStaffAllocation.IsolatorShiftId))))
            {
                prepOrder.SetArchiveDetails(user);
                prepOrder.IntegrationOrder.OrderLastProgressId = (int)OrderProgressEnum.Scheduled;
            }

            repository.SaveChanges();

            return iso.IsolatorId;
        }

        //public IEnumerable<Shift> GetAllStandaredShifts()
        //{
        //    return repository.GetContext().Shifts;
        //}

        //public IEnumerable<IsolatorStaffAllocation> GetIsolatorStaffAllocations(int isolatorId, DateTime requstedDate)
        //{
        //    return repository.GetAll<IsolatorStaffAllocation>().Where(e =>
        //        e.IsolatorId == isolatorId && e.AllocatedDate.Date == requstedDate.Date && !e.IsArchived);
        //}

        public IsolatorStaffAllocationViewModel CreateAllocationViewModel(int allocationId, int isolatorId)
        {
            var allocation = repository.GetContext().IsolatorStaffAllocations.Find(allocationId);
            var model = new IsolatorStaffAllocationViewModel() { IsolatorId = isolatorId };

            if (allocation != null)
            {
                model = Mapper.Map<IsolatorStaffAllocation, IsolatorStaffAllocationViewModel>(allocation);

                if (allocation.IsRecurring)
                {
                    model.RecurringType = (RecurringTypeEnum)allocation.RecurringTypeId;
                    model.DailyRecurringType = (DailyRecurringTypeEnum)allocation.DailyRecurringTypeId;
                    model.SelectedDays = string.IsNullOrEmpty(allocation.WeeklyRecurringWeekdays)? new int[0] : allocation.WeeklyRecurringWeekdays.Split("|").Select(p => Convert.ToInt32(p)).ToArray();
                    model.RecurrenceEndDate = allocation.RecurringEndDate;
                }
            }
            
            var staffs = repository.GetContext().Users.ToList().Select(i => new { Value = i.Id, Text = i.DisplayName() });
            model.IsolatorStaffList = new SelectList(staffs, "Value", "Text", model.StaffId);
            
            var days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(i => new { Value = (int)i, Text = i.ToString() });
            model.DayList = new MultiSelectList(days, "Value", "Text", model.SelectedDays);
            
            return model;
        }

        public IsolatorStaffAllocation MapViewModelToStaffAllocation(IsolatorStaffAllocationViewModel model, string user,
            bool performSave)
         {
            var allocation = repository.GetContext().IsolatorStaffAllocations.Find(model.IsolatorStaffAllocationId);

            allocation = allocation == null ? Mapper.Map<IsolatorStaffAllocationViewModel, IsolatorStaffAllocation>(model) :
                Mapper.Map(model, allocation);

            if (model.IsRecurring)
            {
                allocation.IsRecurring = true;
                allocation.RecurringTypeId = (int)model.RecurringType;
                allocation.RecurringEndDate = model.RecurrenceEndDate;
                switch (model.RecurringType)
                {
                    case RecurringTypeEnum.Daily:
                        allocation.DailyRecurringTypeId = (int)model.DailyRecurringType;
                        break;
                    case RecurringTypeEnum.Weekly:
                        allocation.WeeklyRecurringWeekdays = string.Join("|", model.SelectedDays);
                        break;
                }
            }
            
            if (!performSave) return allocation;

            if (allocation.IsolatorStaffAllocationId > 0)
            {
                allocation.SetUpdateDetails(user);
                repository.SaveExisting(allocation);
            }
            else
            {
                allocation.SetCreateDetails(user);
                repository.SaveNew(allocation);
                CreateRecurringAllocations(allocation);
            }

            return allocation;
        }

        public void CreateRecurringAllocations(IsolatorStaffAllocation parent)
        {
            if(!parent.RecurringEndDate.HasValue) return;

            var recurringDate = parent.AllocatedDate;

            do
            {
                recurringDate = recurringDate.AddDays(1);
                var weeklyRecuringDays = !string.IsNullOrEmpty(parent.WeeklyRecurringWeekdays)
                    ? parent.WeeklyRecurringWeekdays.Split('|') : new string[0];

                switch (parent.RecurringTypeId)
                {
                    case (int)RecurringTypeEnum.Daily:
                        switch (parent.DailyRecurringTypeId)
                        {
                            case (int) DailyRecurringTypeEnum.Everyday:
                                CreateStaffAllocationRecuring(parent, recurringDate);
                                break;
                            case (int) DailyRecurringTypeEnum.EveryWeekday:
                                if (recurringDate.DayOfWeek == DayOfWeek.Saturday || recurringDate.DayOfWeek == DayOfWeek.Sunday) continue;
                                CreateStaffAllocationRecuring(parent, recurringDate);
                                break;
                        }
                        break;
                    case (int)RecurringTypeEnum.Weekly:
                        if (weeklyRecuringDays.Any(p => (int) recurringDate.DayOfWeek == Convert.ToInt32(p)))
                            CreateStaffAllocationRecuring(parent, recurringDate);
                        break;
                }
            } while (parent.RecurringEndDate.Value.Date > recurringDate.Date);
        }

        private void CreateStaffAllocationRecuring(IsolatorStaffAllocation parent, DateTime recurringDate)
        {
            var child = Mapper.Map<IsolatorStaffAllocation, IsolatorStaffAllocation>(parent);
            child.ParentAllocationId = parent.IsolatorStaffAllocationId;
            child.AllocatedDate = recurringDate;

            repository.SaveNew(child);
        }

        public bool RemoveStaffAllocation(int allocationId, string user)
        {
            var alloc = repository.GetById<IsolatorStaffAllocation>(allocationId);

            if (alloc == null) return false;
            
            var prepOrders = repository.GetContext().IntegrationOrderPreperations
                .Include(p => p.IntegrationOrder)
                .Where(p => p.IsolatorId == alloc.IsolatorId &&
                            p.PreperationDateTime.Date == alloc.AllocatedDate.Date &&
                            p.IsolatorStaffAllocationId == allocationId && !p.IsArchived).ToList();

            foreach (var prepOrder in prepOrders)
            {
                prepOrder.IntegrationOrder.OrderLastProgressId = (int)OrderProgressEnum.Scheduled;
                repository.GetContext().Entry(prepOrder.IntegrationOrder).State = EntityState.Modified;
                repository.GetContext().Entry(prepOrder).State = EntityState.Deleted;
                repository.SaveChanges();
            }

            alloc.SetArchiveDetails(user);
            repository.SaveExisting(alloc);

            return true;
        }

        public int SaveIsolator(Isolator isolator, string user)
        {
            if (isolator.IsolatorId > 0)
            {
                isolator.SetUpdateDetails(user);
                repository.SaveExisting(isolator);
                return isolator.IsolatorId;
            }

            isolator.SetCreateDetails(user);
            return repository.SaveNew(isolator).IsolatorId;
        }


        public GridBoxViewModel GetSearchResult(SearchRequest request)
        {
            var model = IsolatorMapper.CreateGridViewModel();

            var pageResult = QueryListHelper.SortResults(GetAllIsolators(), request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.Abbriviation.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(IsolatorMapper.BindGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public IsolatorViewModel CreateViewModel(int id)
        {
            var isolator = GetIsolatorById(id);
            var model = isolator == null ? new IsolatorViewModel() : Mapper.Map<Isolator, IsolatorViewModel>(isolator);

            //var opType = Enum.GetValues(typeof(IsolatorOperationTypeEnum)).Cast<IsolatorOperationTypeEnum>().Select(i => new { Value = (int)i, Text = i.ToString() }).ToList();
            //var pUnits = Enum.GetValues(typeof(PressureUnitEnum)).Cast<PressureUnitEnum>().Select(i => new { Value = (int)i, Text = i.ToString() }).ToList();
            //var tUnits = Enum.GetValues(typeof(TemperatureUnitEnum)).Cast<TemperatureUnitEnum>().Select(i => new { Value = (int)i, Text = i.ToString() }).ToList();

            //model.OperationTypeList = new SelectList(opType, "Value", "Text", model.OperationType);
            //model.PressureUnitList = new SelectList(pUnits, "Value", "Text", model.IsolatorPressureUnit);
            //model.LatestPressureUnitList = new SelectList(pUnits, "Value", "Text", model.LatestPressureUnit);
            //model.TemperatureUnitList = new SelectList(tUnits, "Value", "Text", model.LatestTemperatureUnit);
            //model.LatestPressureUnitList = new SelectList(tUnits, "Value", "Text", model.LatestHumidityUnit);

            return model;
        }

        public int MapViewModelToIsolator(IsolatorViewModel model, string user, bool performSave)
        {
            var isolator = GetIsolatorById(model.IsolatorId);

            isolator = isolator == null ? Mapper.Map<IsolatorViewModel, Isolator>(model) :
                Mapper.Map(model, isolator);

            if (!performSave) return isolator.IsolatorId;

            if (isolator.IsolatorId > 0)
            {
                isolator.SetUpdateDetails(user);
                repository.SaveExisting(isolator);
            }
            else
            {
                isolator.SetCreateDetails(user);
                repository.SaveNew(isolator);
            }

            return isolator.IsolatorId;
        }

        public IsolatorMappedProcedureViewModel CreateIsolatorProcedureViewModel(int isoId)
        {
            var procs = repository.GetContext().IsolatorProcedures.Where(p => !p.IsArchived);
            var iso = repository.GetContext().Isolators
                .Include(p => p.Procedures)
                .ThenInclude(p => p.Procedure)
                .FirstOrDefault(p => p.IsolatorId == isoId);

            var model = new IsolatorMappedProcedureViewModel();
            if (iso == null) return null;
            model.IsolatorId = iso.IsolatorId;
            model.IsolatorName = iso.Abbriviation;

            model.StartProcedures = iso.Procedures.Where(p => p.Procedure.ProcedureTypeId == (int)IsolatorProcedureTypeEnum.Start)
                    .Select(p => p.ProcedureId).ToArray();
            model.StopProcedures = iso.Procedures.Where(p => p.Procedure.ProcedureTypeId == (int)IsolatorProcedureTypeEnum.Stop)
                .Select(p => p.ProcedureId).ToArray();

            model.StartProcedureList = new MultiSelectList(procs.Where(p => p.ProcedureTypeId == (int)IsolatorProcedureTypeEnum.Start), "IsolatorProcedureId", "Description", model.StartProcedures);
            model.StopProcedureList = new MultiSelectList(procs.Where(p => p.ProcedureTypeId == (int)IsolatorProcedureTypeEnum.Stop), "IsolatorProcedureId", "Description", model.StopProcedures);

            return model;
        }

        public int SaveIsolatorProcedures(IsolatorMappedProcedureViewModel model, string user)
        {
            var iso = GetIsolatorById(model.IsolatorId);

            var startProcs = model.StartProcedures.Select(procId => new IsolatorMappedProcedure()
            { IsolatorId = iso.IsolatorId, ProcedureId = procId }).ToList();
            var stopProcs = model.StopProcedures.Select(procId => new IsolatorMappedProcedure()
            { IsolatorId = iso.IsolatorId, ProcedureId = procId });

            startProcs.AddRange(stopProcs);

            iso.Procedures = startProcs;
            iso.SetUpdateDetails(user);
            repository.SaveExisting(iso);

            return iso.IsolatorId;
        }

        public IEnumerable<IsolatorStaffAllocation> GetIsolatorStaffAllocations(int isolatorId, DateTime requstedDate)
        {
            return repository.GetContext().IsolatorStaffAllocations
                .Include(p=>p.IsolatorShift)
                .Include(p => p.Staff)
                .Where(e => e.IsolatorId == isolatorId && !e.IsArchived)
                .Where(e => e.AllocatedDate.Date == requstedDate.Date);
        }

        #region Order preperation

        public IEnumerable<IntegrationOrderPreperation> GetPreperationOrdersByDate(DateTime preperationDate)
        {
            return repository.GetContext().IntegrationOrderPreperations.Include(p => p.IntegrationOrder).Where(p => p.PreperationDateTime.Date == preperationDate);
        }

        public IEnumerable<IntegrationOrderPreperation> GetPreperationOrdersByIsolator(int isoId, DateTime preperationDate)
        {
            return repository.GetContext().IntegrationOrderPreperations
                .Include(p => p.IntegrationOrder)
                .Where(p => p.IsolatorId == isoId && p.PreperationDateTime.Date == preperationDate && !p.IsArchived);
        }

        public IEnumerable<Isolator> GetAvailableIsolatorsByDate(DateTime date)
        {
            return repository.GetContext().Isolators
                .Include(p => p.PreperationOrders)
                .ThenInclude(p => p.IntegrationOrder)
                .Include(p => p.StaffShiftAllocations)
                .ThenInclude(p => p.IsolatorShift);
        }

        public void CreateOrderIsolatorMapping(int orderId, int isoId, DateTime scheduledDate, int staffAllocationId)
        {
            var entity = new IntegrationOrderPreperation() { IntegrationOrderId = orderId, IsolatorId = isoId, PreperationDateTime = scheduledDate, IsolatorStaffAllocationId = staffAllocationId };
            entity.SetCreateDetails("admin");
            repository.SaveNew(entity);

            var order = repository.GetById<IntegrationOrder>(orderId);
            order.OrderLastProgressId = (int)OrderProgressEnum.PreperationScheduled;
            repository.SaveExisting(order);
        }

        public bool RemoveOrderIsolatorMapping(int orderPreperationId)
        {
            var entity = repository.GetContext().IntegrationOrderPreperations.Include(p => p.IntegrationOrder)
                .FirstOrDefault(p => p.IntegrationOrderPreperationId == orderPreperationId);
            if (entity == null) return false;

            entity.SetArchiveDetails("admin");
            repository.SaveExisting(entity);
            
            entity.IntegrationOrder.OrderLastProgressId = (int)OrderProgressEnum.Scheduled;
            repository.SaveExisting(entity.IntegrationOrder);

            return true;
        }

        public GridBoxViewModel GetOrderSearchResult(SearchRequest request)
        {
            var model = IsolatorMapper.CreateOrderGridViewModel();

            var orders = repository.GetAll<IntegrationOrder>().Where(p => p.OrderLastProgressId == (int)OrderProgressEnum.Scheduled && !p.IsArchived);

            var pageResult = QueryListHelper.SortResults(orders, request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.Name.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(IsolatorMapper.BindOrderGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        #endregion
    }
}
