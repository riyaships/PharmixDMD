using Pharmix.Web.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Pharmix.Web.Entities;
using Pharmix.Data.Entities.Context;
using Microsoft.AspNetCore.Identity;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Data.Entities.ViewModels;
using AutoMapper;
using System.Threading.Tasks;

namespace Pharmix.Web.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IRepository _repository;
        private UserManager<ApplicationUser> _userManager;
        private readonly PharmixEntityContext _context;
        private readonly IUserService _userService;
        private readonly ITrustService _trustService;
        public string _trustAdminRole = "TrustAdmin";

        public ModuleService(IRepository repository, UserManager<ApplicationUser> userManager,
            IUserService userService, ITrustService trustService)
        {
            this._repository = repository;
            this._userManager = userManager;
            this._context = repository.GetContext();
            this._userService = userService;
            this._trustService = trustService;
        }

        public IEnumerable<Module> GetAllModules()
        {
            return _repository.GetAll<Module>().ToList();
        }




        #region Menu binding

        public List<PermissionViewModel> GetAvailableMenuByUserName(string userName)
        {
            if (_userService.IsPharmixAdmin(userName))
            {
                var availAccessPermissions = (from p in _context.Permissions
                                              where p.IsShowMenu ?? false
                                              select p)
                                              .OrderBy(x => x.ParentPermissionId).ThenBy(x => x.Sort).ToList().ToList();
                var availPermissionViewModelList = Mapper.Map<List<Permission>, List<PermissionViewModel>>(availAccessPermissions);
                return availPermissionViewModelList;
            }
            else
            {
                List<Permission> permissionList = new List<Permission>();
                var userRoles = _userService.GetRolesByUserName(userName);

                if (userRoles.Contains(_trustAdminRole))
                {   //Super Admin
                    permissionList = (from p in _context.Permissions
                                  join tm in _context.TrustModules on p.ModuleId equals tm.ModuleId
                                  join ur in _context.UserTrusts on tm.TrustId equals ur.TrustId
                                  join usr in _context.Users on ur.UserId equals usr.Id
                                  //join um in _context.UserModules on p.ModuleId equals um.ModuleId
                                  where (p.IsShowMenu ?? false) && usr.UserName == userName
                                  group p by p.Id into grp
                                  select grp.First())
                                                .OrderBy(x => x.ParentPermissionId).ThenBy(x => x.Sort).ToList();
                }
                else
                {

                    var roles = _context.Roles.Where(x => userRoles.Contains(x.Name));
                    List<string> roleIds = roles.Select(x => x.Id).ToList();

                    var permissions = _context.RolePermissions.Where(x => roleIds.Contains(x.RoleId) && x.PermissionId != null && x.PermissionId > 0).Select(x => x.Permission);
                    var permissionsInGroup = _context.RolePermissions.Where(x => roleIds.Contains(x.RoleId) && x.GroupId != null && x.GroupId > 0).SelectMany(x => x.Group.PermissionGroups.Select(pg=>pg.Permission));

                    if (permissions != null)
                        permissionList.AddRange(permissions);
                    if (permissionsInGroup != null)
                        permissionList.AddRange(permissionsInGroup);

                    permissionList.AddRange(_context.Permissions.Where(x=>permissionList.Select(p=>p.ParentPermissionId).Contains(x.Id)));
                    
                    //permission = (from p in _context.Permissions
                    //              join rp in _context.RolePermissions on p.Id equals rp.PermissionId
                    //              join r in _context.Roles on rp.RoleId equals r.Id

                    //              where userRoles.Contains(r.Name) && (p.IsShowMenu ?? false)

                    //              group p by p.Id into grp
                    //              select grp.First())
                    //                              .OrderBy(x => x.ParentPermissionId).ThenBy(x => x.Sort).ToList();
                }
                var availPermissionViewModelList = Mapper.Map<List<Permission>, List<PermissionViewModel>>(permissionList.OrderBy(x => x.ParentPermissionId).ThenBy(x => x.Sort).ToList());
                return availPermissionViewModelList;
            }
        }

        public List<string> GetAvailablePermissionsByUserName(string userName)
        {
            //join rp in _context.RolePermissions on new { pid = (int?)p.Id, rid = roleId } equals new { pid = rp.PermissionId, rid = rp.RoleId } into rpdef

            List<string> availableKeys = new List<string>();

            if (_userService.IsPharmixAdmin(userName))
            {
                availableKeys = (from p in _context.Permissions
                                 select p)
                                 .Select(x => x.Key)
                                 .ToList().ToList();
            }
            else
            {
                List<Permission> permission = new List<Permission>();
                var userRoles = _userService.GetRolesByUserName(userName);

                if (userRoles.Contains(_trustAdminRole))
                {   //Super Admin
                    availableKeys = (from p in _context.Permissions
                                     join tm in _context.TrustModules on p.ModuleId equals tm.ModuleId
                                     join ut in _context.UserTrusts on tm.TrustId equals ut.TrustId
                                     join usr in _context.Users on ut.UserId equals usr.Id
                                     //join um in _context.UserModules on p.ModuleId equals um.ModuleId
                                     where usr.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)
                                     group p by p.Id into grp
                                     select grp.First())
                                  .Select(x => x.Key).ToList();
                }
                else
                {

                    var roles = _context.Roles.Where(x => userRoles.Contains(x.Name));
                    List<string> roleIds = roles.Select(x => x.Id).ToList();
                    if (roleIds.Count > 0)
                    {
                        var permissions = _context.RolePermissions.Where(x => roleIds.Contains(x.RoleId) && x.PermissionId != null && x.PermissionId > 0).Select(x => x.Permission);
                        var permissionsInGroup = _context.RolePermissions.Where(x => roleIds.Contains(x.RoleId) && x.GroupId != null && x.GroupId > 0).Select(x => x.Permission);



                        if (permissions != null)
                            availableKeys.AddRange(permissions.Select(x => x.Key));
                        if (permissionsInGroup != null)
                            availableKeys.AddRange(permissionsInGroup.Select(x => x.Key));


                        var parentPermissionKeys = (from chld in _context.Permissions
                                                    join prt in _context.Permissions on chld.ParentPermissionId equals prt.Id
                                                    where availableKeys.Contains(chld.Key)
                                                    group prt by prt.Key into prtgrp
                                                    select prtgrp.First().Key
                                                  ).ToList();

                        if (parentPermissionKeys != null)
                            availableKeys.AddRange(parentPermissionKeys);


                        //availableKeys = (from p in _context.Permissions
                        //                 join rp in _context.RolePermissions on p.Id equals rp.PermissionId
                        //                 join r in _context.Roles on rp.RoleId equals r.Id
                        //                 where userRoles.Contains(r.Name)
                        //                 group p by p.Id into grp
                        //                 select grp.First())
                        //             .Select(x => x.Key).ToList();

                    }
                }
                //var availPermissionViewModelList = Mapper.Map<List<Permission>, List<PermissionViewModel>>(permission);
            }
            return availableKeys;
        }

        #endregion

        #region Manage Module

        public TrustViewModel GetTrustModules(int trustId)
        {
            TrustViewModel trustViewModel = new TrustViewModel();
            if (trustId > 0)
            {
                var trust = _trustService.GetTrustById(trustId);

                trustViewModel.Id = trustId;
                trustViewModel.Name = trust.Name;

                var trustModuleViewModelList = (from m in _context.Modules
                                                join tm in _context.TrustModules on new { mid = m.Id, tid = trustId } equals new { mid = tm.ModuleId, tid = tm.TrustId } into tmdef
                                                from tm in tmdef.DefaultIfEmpty()
                                                select new TrustModuleViewModel
                                                {
                                                    Id = tm != null ? tm.Id : 0,
                                                    ModuleId = m.Id,
                                                    ModuleName = m.Name,
                                                    TrustId = tm != null ? tm.TrustId : 0,
                                                    IsHaveAccess = tm != null
                                                }).ToList();

                trustViewModel.TrustModuleViewModelList = trustModuleViewModelList;
            }
            return trustViewModel;
        }

        public async Task UpdateTrustModule(TrustViewModel trustViewModel)
        {
            if (trustViewModel != null && trustViewModel.TrustModuleViewModelList != null && trustViewModel.TrustModuleViewModelList.Count() > 0)
            {
                var moduleIds = trustViewModel.TrustModuleViewModelList.Select(x => x.ModuleId).ToList();

                _context.TrustModules.RemoveRange(_context.TrustModules.Where(x => x.TrustId == trustViewModel.Id && moduleIds.Contains(x.ModuleId)));
                _context.RolePermissions.RemoveRange(_context.RolePermissions.Where(x => x.Permission != null && moduleIds.Contains(x.Permission.ModuleId ?? 0)));

                foreach (var trustModule in trustViewModel.TrustModuleViewModelList.Where(x => x.IsHaveAccess))
                {
                    _context.TrustModules.Add(new TrustModule() { TrustId = trustViewModel.Id, ModuleId = trustModule.ModuleId });
                }
                await _context.SaveChangesAsync();
            }
        }

        #endregion


        #region Module By user Old

        //public List<UserModuleViewModel> GetModulesByUser(string userId)
        //{
        //    //var roleModuleViewModelList = (from m in GetAllModules()
        //    //                               //join rm in _context.UserModules on m.Id equals rm.ModuleId into rmdef
        //    //                               join um in _context.UserModules on new { mid = m.Id, uid = userId } equals new { mid = um.ModuleId, uid = um.UserId }  into umdef
        //    //                               from um in umdef.DefaultIfEmpty()
        //    //                               //where (um == null || um.UserId == userId)
        //    //                               select new UserModuleViewModel
        //    //                               {
        //    //                                   Id = um?.Id ?? 0,
        //    //                                   IsHaveAccess = um != null,
        //    //                                   ModuleId = m.Id,
        //    //                                   ModuleName = m.Name,
        //    //                                   UserId = um?.UserId
        //    //                                   //UserName=rm?.user

        //    //                               }).ToList();

        //    //return roleModuleViewModelList;

        //    return new List<UserModuleViewModel>();
        //}

        //public void UpdateUserModule(UserViewModel userViewModel)
        //{
        //    //if (userViewModel != null)
        //    //{
        //    //    if (userViewModel.UserModuleViewModelList != null && userViewModel.UserModuleViewModelList.Count() > 0)
        //    //    {
        //    //        //var noAccessModules = userViewModel.UserModuleViewModelList.Where(x => !x.IsHaveAccess).Select(x => x.ModuleId).ToList();
        //    //        var modules = userViewModel.UserModuleViewModelList.Select(x => x.ModuleId).ToList();

        //    //        var permissionsToRemove = (from p in _context.Permissions
        //    //                                   join m in _context.Modules on p.ModuleId equals m.Id
        //    //                                   select p.RolePermissions).SelectMany(x=>x).ToList();
        //    //        if (permissionsToRemove != null && permissionsToRemove.Count() > 0)
        //    //            _context.RolePermissions.RemoveRange(permissionsToRemove);

        //    //        _context.UserModules.RemoveRange(_context.UserModules.Where(x => modules.Contains(x.ModuleId) && x.UserId == userViewModel.Id));

        //    //        List<UserModule> userModuleList = new List<UserModule>();
        //    //        foreach (var userModuleViewModel in userViewModel.UserModuleViewModelList.Where(x => x.IsHaveAccess))
        //    //        {
        //    //            _context.UserModules.Add(new UserModule() { UserId = userViewModel.Id, ModuleId = userModuleViewModel.ModuleId });
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        _context.UserModules.RemoveRange(_context.UserModules.Where(x => x.UserId.Equals(userViewModel.Id)));
        //    //    }
        //    //    _context.SaveChanges();
        //    //}
        //}
        #endregion

    }
}
