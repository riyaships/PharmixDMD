using System;
using System.Collections.Generic;
using Pharmix.Web.Entities;
using Pharmix.Web.Services.Repositories;

namespace Pharmix.Web.Services
{
    public class ShiftService: IShiftService
    {
        private readonly IRepository _repository;
        public ShiftService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<IsolatorShift> GetShifts()
        {
            return _repository.Get<IsolatorShift>();
        }

        public IEnumerable<IsolatorStaffAllocation> GetIsolatorStaffShifts()
        {
            return _repository.Get<IsolatorStaffAllocation>();
        }

        public int CreateIsolatorStaffShift(IsolatorStaffAllocation isolatorStaffAllocation)
        {
            return _repository.SaveNew<IsolatorStaffAllocation>(isolatorStaffAllocation).IsolatorShiftId;
        }

        public string UpdateIsolatorStaffShift(IsolatorStaffAllocation isolatorStaffAllocation)
        {
            string result = null;
            try
            {
                _repository.SaveExisting<IsolatorStaffAllocation>(isolatorStaffAllocation);
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
