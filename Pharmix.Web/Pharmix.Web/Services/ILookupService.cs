using System.Collections.Generic;
using System.Threading.Tasks;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models;

namespace Pharmix.Web.Services
{
    public interface ILookupService
    {
        #region Shift
        IEnumerable<IsolatorShift> GetAllStandaredShifts();
        GridViewModel GetShiftSearchResult(SearchRequest request);
        ShiftViewModel CreateShiftViewModel(int id);
        int MapViewModelToShift(ShiftViewModel model, string user, bool performSave);
        BaseResultViewModel<string> ArchiveShift(int id, string user);
        #endregion

        #region Procedure
        IEnumerable<IsolatorProcedure> GetAllProcedures(int typeId = 0);
        GridViewModel GetProcedureSearchResult(SearchRequest request);
        ProcedureViewModel CreateProcedureViewModel(int id);
        int MapViewModelToProcedure(ProcedureViewModel model, string user, bool performSave);
        BaseResultViewModel<string> ArchiveProcedure(int id, string user);
        #endregion
    }
}
