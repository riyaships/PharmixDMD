using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Data.Enums;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using X.PagedList;

namespace Pharmix.Web.Services
{
    public class LookupService : ILookupService
    {
        private readonly IRepository repository;

        public LookupService(IRepository repository)
        {
            this.repository = repository;
        }

        #region Shift
        public IEnumerable<IsolatorShift> GetAllStandaredShifts()
        {
            return repository.GetContext().IsolatorShifts.Where(p => !p.IsArchived);
        }

        public GridViewModel GetShiftSearchResult(SearchRequest request)
        {
            var model = IsolatorMapper.CreateShiftGridViewModel();

            var pageResult = QueryListHelper.SortResults(GetAllStandaredShifts(), request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.ShiftTitle.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(IsolatorMapper.BindShiftGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public ShiftViewModel CreateShiftViewModel(int id)
        {
            var shift = repository.GetById<IsolatorShift>(id);
            var model = shift == null ? new ShiftViewModel() : Mapper.Map<IsolatorShift, ShiftViewModel>(shift);

            return model;
        }

        public int MapViewModelToShift(ShiftViewModel model, string user, bool performSave)
        {
            var shift = repository.GetById<IsolatorShift>(model.ShiftId);

            shift = shift == null ? Mapper.Map<ShiftViewModel, IsolatorShift>(model) : Mapper.Map(model, shift);

            var st = Convert.ToDateTime(model.StartTime);
            var end = Convert.ToDateTime(model.EndTime);

            if (st.Hour > end.Hour)
                end = end.AddDays(1);

            shift.TotalShiftDurationInMins = end.Subtract(st).Duration().TotalMinutes;

            if (!performSave) return shift.ShiftId;

            if (shift.ShiftId > 0)
            {
                shift.SetUpdateDetails(user);
                repository.SaveExisting(shift);
            }
            else
            {
                shift.SetCreateDetails(user);
                repository.SaveNew(shift);
            }

            return shift.ShiftId;
        }

        public BaseResultViewModel<string> ArchiveShift(int id, string user)
        {
            var activeShifts = repository.GetContext().Isolators
                .Where(p => p.StaffShiftAllocations.Any(i => i.IsolatorShiftId == id && !i.IsArchived) && !p.IsArchived);

            if (activeShifts.Any())
                return new BaseResultViewModel<string>
                {
                    IsSuccess = false,
                    Message = "Shift has been allocated with one or more isolators, please unallocate this shift from those isolator(s), in order to delete this shift."
                };

            var shift = repository.GetById<IsolatorShift>(id);

            shift.SetArchiveDetails(user);
            repository.SaveExisting(shift);

            return new BaseResultViewModel<string>
            {
                IsSuccess = true,
                Message = "Shift has been deleted successfully."
            }; ;
        }
        #endregion

        #region Procedure

        public IEnumerable<IsolatorProcedure> GetAllProcedures(int typeId = 0)
        {
            return repository.GetContext().IsolatorProcedures
                .Where(p => !p.IsArchived)
                .Where(p => typeId == 0 || p.ProcedureTypeId == typeId);
        }

        public GridViewModel GetProcedureSearchResult(SearchRequest request)
        {
            var model = IsolatorMapper.CreateProcedureGridViewModel();

            var pageResult = QueryListHelper.SortResults(GetAllProcedures(), request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.Description.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(IsolatorMapper.BindProcedureGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public ProcedureViewModel CreateProcedureViewModel(int id)
        {
            var proc = repository.GetById<IsolatorProcedure>(id);
            var model = proc == null ? new ProcedureViewModel() : Mapper.Map<IsolatorProcedure, ProcedureViewModel>(proc);

            var types = Enum.GetValues(typeof(IsolatorProcedureTypeEnum)).Cast<IsolatorProcedureTypeEnum>().Select(i => new { Value = (int)i, Text = i.ToString() });
            model.ProcedureTypeList = new SelectList(types, "Value", "Text", model.ProcedureTypeId);

            return model;
        }

        public int MapViewModelToProcedure(ProcedureViewModel model, string user, bool performSave)
        {
            var proc = repository.GetById<IsolatorProcedure>(model.IsolatorProcedureId);

            proc = proc == null ? Mapper.Map<ProcedureViewModel, IsolatorProcedure>(model) : Mapper.Map(model, proc);

            if (!performSave) return proc.IsolatorProcedureId;

            if (proc.IsolatorProcedureId > 0)
            {
                proc.SetUpdateDetails(user);
                repository.SaveExisting(proc);
            }
            else
            {
                proc.SetCreateDetails(user);
                repository.SaveNew(proc);
            }

            return proc.IsolatorProcedureId;
        }

        public BaseResultViewModel<string> ArchiveProcedure(int id, string user)
        {
            var activeProc = repository.GetContext().Isolators
                .Where(p => p.Procedures.Any(i => i.ProcedureId == id) && !p.IsArchived);

            if (activeProc.Any())
                return new BaseResultViewModel<string>
                {
                    IsSuccess = false,
                    Message = "This isolator procedure being used with one or more isolators, please remove this procedure from those isolator(s), in order to delete this shift."
                };

            var proc = repository.GetById<IsolatorProcedure>(id);

            proc.SetArchiveDetails(user);
            repository.SaveExisting(proc);

            return new BaseResultViewModel<string>
            {
                IsSuccess = true,
                Message = "Isolator procedure has been deleted successfully."
            }; ;
        }
        #endregion
    }
}