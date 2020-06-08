using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Data.Entities.ViewModels.Customer;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels.IntegrationOrder;
using Pharmix.Web.Entities.Context;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Models.IsolatorVIewModels;
using Pharmix.Web.Entities.ViewModels.Production;
using Pharmix.Web.Entities.ViewModels.UsedProduct;
using Pharmix.Web.Models.AccountViewModels;
using IsolatorShift = Pharmix.Web.Entities.IsolatorShift;
using Pharmix.Web.Models.ManageViewModels;
using Pharmix.Web.Models.PatientViewModel;

namespace Pharmix.Web.Services.Mappers
{
    public class AutoMapperBootStrapper
    {

        public static void RegisterMappings()
        {
            Mapper.Initialize(config =>
            {

                config.CreateMap<Patient, CustomerViewModel>().ReverseMap();
                config.CreateMap<IdentityUser, UserViewModel>().ReverseMap();
                config.CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
                config.CreateMap<IsolatorStaffAllocation, IsolatorStaffAllocationViewModel>().ReverseMap();
                config.CreateMap<IsolatorStaffAllocation, IsolatorStaffAllocation>().ForMember(m => m.IsolatorStaffAllocationId, opt=> opt.Ignore());
                config.CreateMap<Isolator, IsolatorViewModel>().ReverseMap();
                config.CreateMap<IntegrationOrder, IntegrationOrderViewModel>()
                .ForMember(d => d.IntegratedSystem, opt => opt.Ignore()).ReverseMap();
                config.CreateMap<IsolatorShift, ShiftViewModel>().ReverseMap();
                config.CreateMap<IsolatorProcedure, ProcedureViewModel>().ReverseMap();
                config.CreateMap<UsedProduct, UsedProductViewModel>().ReverseMap();
                config.CreateMap<UsedProductStock, UsedProductStockViewModel>().ReverseMap();
                config.CreateMap<Vtm, VtmViewModel>().ReverseMap();

                //config.CreateMap<IntegrationOrderViewModel, IntegrationOrder>().ReverseMap();
                config.CreateMap<SupervisorRequest, SupervisorRequestViewModel>().ReverseMap();
                config.CreateMap<Module, ModuleViewModel>().ReverseMap();
                config.CreateMap<Site, SiteViewModel>().ReverseMap();
                config.CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
                config.CreateMap<Permission, PermissionViewModel>().ReverseMap();
                config.CreateMap<Trust, TrustViewModel>().ReverseMap();
                config.CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
                config.CreateMap<Address, RegisterViewModel>().ReverseMap();
                config.CreateMap<Group, GroupViewModel>().ReverseMap();
                config.CreateMap<CommunicationNeed, CommunicationNeedViewModel>().ReverseMap();
                //config.CreateMap<IntegrationOrderCommentViewModel, CommunicationNeed>().ReverseMap();
            });
        }
    }
}
