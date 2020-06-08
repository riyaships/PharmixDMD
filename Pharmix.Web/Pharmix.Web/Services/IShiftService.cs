using System.Collections.Generic;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Services
{
    public interface IShiftService
    {
        IEnumerable<IsolatorShift> GetShifts();
        int CreateIsolatorStaffShift(IsolatorStaffAllocation isolatorStaffAllocation);
        IEnumerable<IsolatorStaffAllocation> GetIsolatorStaffShifts();
        string UpdateIsolatorStaffShift(IsolatorStaffAllocation isolatorStaffAllocation);
    }
}
