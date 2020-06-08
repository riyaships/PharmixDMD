using System;
using System.Linq;
using AutoMapper;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels.Customer;
using Pharmix.Services;
using Pharmix.Web.Extensions;
using Pharmix.Web.Services.Repositories;

namespace Pharmix.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository _repository;

        #region Constructor
        public CustomerService(IRepository repository)
        {
            this._repository = repository;
        }
        #endregion

        /// <summary>
        /// Map Customer View model to Customer object
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <param name="performSave"></param>
        /// <returns></returns>
        public Patient MapViewModelToCustomer(CustomerViewModel model, string user, bool performSave)
        {
            var customer = GetCustomerById(model.CustomerId);
            customer = customer == null ? Mapper.Map<CustomerViewModel, Patient>(model) : Mapper.Map(model, customer);

            if (!performSave) return customer;

            CreateCustomer(customer, user);

            return customer;
        }

        /// <summary>
        /// Get Customer by ID
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Patient GetCustomerById(int customerId)
        {
            return _repository.GetContext().Patients.Find(customerId);
        }

        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int CreateCustomer(Patient customer, string user)
        {
            var existingCustomer = _repository.GetContext().Patients.FirstOrDefault(m => m.EmailAddress.Equals(customer.EmailAddress, StringComparison.CurrentCultureIgnoreCase));
            if (existingCustomer != null)
            {
                return existingCustomer.Id;
            }

            customer.RegisteredDate = DateTime.Now;
            customer.SetCreateDetails(user);
            var newCustomer = _repository.SaveNew(customer);

            return newCustomer.Id;
        }

        /// <summary>
        /// Validate super admin password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateSuperAdminPassword(string password)
        {
            //string dynPassword = "Admin_" + DateTime.Now.ToString("ddMMyyyy");
            string dynPassword = PharmixStaticHelper.SuperAdminPassword;
            return password.Equals(dynPassword);
        }

    }
}
