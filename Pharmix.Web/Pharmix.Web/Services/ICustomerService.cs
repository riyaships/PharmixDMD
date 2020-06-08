using System;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels.Customer;

namespace Pharmix.Web.Services
{
    public interface ICustomerService
    {
        Patient MapViewModelToCustomer(CustomerViewModel model, string user, bool performSave);
        Patient GetCustomerById(int customerId);
        int CreateCustomer(Patient customer, string user);
        bool ValidateSuperAdminPassword(string password);
    }
}
