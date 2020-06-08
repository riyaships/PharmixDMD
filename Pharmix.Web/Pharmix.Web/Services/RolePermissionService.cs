using AutoMapper;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pharmix.Web.Services
{
    public class RolePermissionService : IRolePermissionService
    {

        private readonly IRepository _repository;
        private readonly PharmixEntityContext _context;

        #region Constructor
        public RolePermissionService(IRepository repository)
        {
            this._repository = repository;
            _context = _repository.GetContext();
        }
        #endregion

        public List<ModuleViewModel> GetModules()
        {
            var modules = _repository.Get<Module>().OrderBy(x => x.Name).ToList();
            var moduleViewModelList = Mapper.Map<List<Module>, List<ModuleViewModel>>(modules);
            return moduleViewModelList;
        }

        public List<ModuleViewModel> GetDistinctModules()
        {
            var modules = _repository.Get<Module>().GroupBy(x => x.Name).Select(x => x.First()).OrderBy(x => x.Name).ToList();
            var moduleViewModelList = Mapper.Map<List<Module>, List<ModuleViewModel>>(modules);
            return moduleViewModelList;
        }

        public List<RoleModuleViewModel> GetModulesByCriteria(List<string> roleIds = null, List<string> roleNames = null, int moduleId = 0, string moduleName = "")
        {
            //var roleModuleViewModelList = (from m in _context.Modules
            //                               join rm in _context.RoleModules on m.Id equals rm.ModuleId into rmdef
            //                               from rm in rmdef.DefaultIfEmpty()
            //                               join r in _context.Roles on rm.RoleId equals r.Id into rdef
            //                               from r in rdef.DefaultIfEmpty()
            //                               where
            //                               (roleIds == null || roleIds.Contains(rm.RoleId) || rm.RoleId == null || roleIds.Count() == 0)
            //                               && (roleNames == null || roleNames.Contains(r.Name) || r.Name == null || roleNames.Count() == 0)
            //                               && (moduleId == 0 || m.Id == moduleId)
            //                               && (string.IsNullOrEmpty(moduleName) || m.Name == moduleName)
            //                               select new RoleModuleViewModel
            //                               {
            //                                   Id = rm != null ? rm.Id : 0,
            //                                   ModuleId = m.Id,
            //                                   IsHaveAccess = rm != null,
            //                                   ModuleName = m.Name,
            //                                   Action = m.Action,
            //                                   Area = m.Area,
            //                                   Controller = m.Controller,
            //                                   Key = m.Key
            //                               }).ToList();

            //roleModuleViewModelList = roleModuleViewModelList.GroupBy(x => x.Id).SelectMany(x => x).OrderBy(x => x.ModuleName).ToList();

            //return roleModuleViewModelList;

            return new List<RoleModuleViewModel>();

        }

        public List<RoleModuleViewModel> GetAvailableModulesByRoles(List<string> roleIds)
        {
            return GetModulesByCriteria(roleIds, new List<string>(), moduleId: 0, moduleName: string.Empty).Where(x => x.IsHaveAccess).ToList();
        }

        public void UpdatePermission(AccessPermissionViewModel accessPermissionViewModel)
        {
            //if (accessPermissionViewModel != null && accessPermissionViewModel.RoleId != null && accessPermissionViewModel.RoleModuleViewModelList != null && accessPermissionViewModel.RoleModuleViewModelList.Count() > 0)
            //{
            //    //var unAccessModules = accessPermissionViewModel.RoleModuleViewModelList.Where(x => !x.IsHaveAccess).ToList();
            //    var selectedModules = accessPermissionViewModel.RoleModuleViewModelList.ToList();
            //    _context.RoleModules.RemoveRange(_context.RoleModules.Where(x => x.RoleId == accessPermissionViewModel.RoleId && selectedModules.Select(m => m.ModuleId).Contains(x.ModuleId)));

            //    List<RoleModule> roleModuleList = new List<RoleModule>();
            //    foreach (var module in selectedModules.Where(x => x.IsHaveAccess))
            //    {
            //        roleModuleList.Add(new RoleModule() { ModuleId = module.ModuleId, RoleId = accessPermissionViewModel.RoleId });
            //    }
            //    _context.RoleModules.AddRange(roleModuleList);
            //    _context.SaveChanges();
            //}
        }

    }
}
