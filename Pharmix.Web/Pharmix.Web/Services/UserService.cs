using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Services.Mappers;
using Pharmix.Web.Entities;
using Pharmix.Web.Extensions;
using Pharmix.Web.Models;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using X.PagedList;
using Pharmix.Web.Models.ManageViewModels;

namespace Pharmix.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICacheService _cacheService;
        private readonly PharmixEntityContext _context;

        private string _userTempCache = "UserTemp";
        private string _trustAdminRole = "TrustAdmin";
         
        private List<string> _excludeManageRoles = new List<string>() { "TrustAdmin", "SuperAdmin" };

        #region Constructor

        /// pharmix.admin@pharmixtech.com
        /// Admin@123
        public UserService(IRepository repository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            ICacheService cacheService)
        {
            this._repository = repository;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._cacheService = cacheService;
            _context = repository.GetContext();

        }

        #endregion
        public IQueryable<ApplicationUser> GetUsers()
        {
            UpdateUserTempIdCache();
            return _repository.GetContext().Users;
        }

        public ApplicationUser GetUser(string userId)
        {
            UpdateUserTempIdCache();
            return _userManager.FindByIdAsync(userId).Result;
        }

        public ApplicationUser GetUserByName(string userName)
        {
            return  (_userManager.FindByNameAsync(userName)).Result;
        }

        public UserViewModel GetUserPasswordResetToken(string userId)
        {
            var user = GetUser(userId);
            string passwordResetToken = string.Empty;
            if (user != null)
                passwordResetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;

            return new UserViewModel() { Id = user.Id, Email = user.UserName, PasswordResetToken = passwordResetToken };
        }

        public ApplicationUser ChangePassword(string userId, string password, string passwordToken)
        {
            var user = GetUser(userId); //_userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                var result = _userManager.ResetPasswordAsync(user, passwordToken, password).Result;
            }
            return user;
        }

        public List<RoleViewModel> GetAllRoles()
        {
            var roleViewModelList = _roleManager.Roles.Select(x => new RoleViewModel { Id = x.Id, Name = x.Name, NormalizedName = x.NormalizedName }).ToList();
            return roleViewModelList;
        }

        public SelectList GetRoleSelectList(string selectedValue = "")
        {
            SelectList roles = null;
            var roleViewModelList = GetAllRoles();
            if (roleViewModelList != null)
            {
                if (!string.IsNullOrEmpty(selectedValue))
                    roles = new SelectList(roleViewModelList, "Name", "Name", selectedValue);
                else
                    roles = new SelectList(roleViewModelList, "Name", "Name");
            }
            else
                roles = new SelectList(Enumerable.Empty<SelectListItem>());
            return roles;
        }

        public async Task<List<ApplicationUser>> GetUsersByRoles(List<string> roles)
        {
            UpdateUserTempIdCache();
            List<ApplicationUser> users = null;

            if (roles != null && roles.Count > 0)
                foreach (var role in roles)
                {
                    var roleUsers = (await _userManager.GetUsersInRoleAsync(role)).ToList();
                    if (roleUsers != null && roleUsers.Count > 0)
                    {
                        if (users == null)
                            users = new List<ApplicationUser>();
                        users.AddRange(roleUsers);
                    }

                }
            return users;
            //return null;
        }

        public GridViewModel GetSearchResult(SearchRequest request, string userName, bool isPharmixAdmin)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            var isTrustAdmin = user != null && _userManager.IsInRoleAsync(user, _trustAdminRole).Result;
            UpdateUserTempIdCache();
            var model = UserMapper.CreateGridViewModel();

            List<UserViewModel> users = new List<UserViewModel>();

            //bool isPharmixAdmin = IsPharmixAdmin(userName);
            if (isPharmixAdmin)
            {
                List<string> trustAdminRoles = new List<string>() { _trustAdminRole };
                users = GetUsersByRoles(trustAdminRoles).Result.MapToViewModelList();
            }
            else
            {               //Trust Admin

                List<string> rolesToExclude = new List<string>();

                rolesToExclude.Add("SuperAdmin");
                rolesToExclude.Add("Admin");
                rolesToExclude = _userManager.GetRolesAsync(user).Result.ToList();

                users = (from u in _context.Users
                         join ur in _context.UserRoles on u.Id equals ur.UserId into urdef
                         from ur in urdef.DefaultIfEmpty()
                         join r in _context.Roles on ur.RoleId equals r.Id into rdef
                         from r in rdef.DefaultIfEmpty()
                         where r == null || !rolesToExclude.Contains(r.Name)      //Donot allow change same level of users
                         select new UserViewModel
                         {
                             Id = u.Id,
                             Name = u.UserName,
                             Email = u.Email,
                             FirstName=u.FirstName,
                             SurName=u.Surname,
                             MobileNumber=u.MobileNumber
                         }).ToList();
            }
            users = UpdateTempID(users.ToList());

            var pageResult = QueryListHelper.SortViewModelResults(users, request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.Email.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(x => UserMapper.BindGridData(x, isTrustAdmin));
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public UserViewModel CreateViewModel(string id)
        {
            var user = GetUser(id ?? string.Empty);
            var model = user == null ? new UserViewModel() : Mapper.Map<ApplicationUser, UserViewModel>(user);
            model = UpdateTempID(model);
            return model;
        }

        public bool IsPharmixAdmin(string userName)
        {
            return userName!=null && userName.Equals(PharmixStaticHelper.SuperAdminUsername, StringComparison.InvariantCultureIgnoreCase);
        }
        //public bool IsTrustAdmin(string userName)
        //{
        //    var user = _userManager.FindByNameAsync(userName).Result;
        //    return user != null && _userManager.IsInRoleAsync(user, _trustAdminRole).Result;
        //}

        public List<UserRoleViewModel> GetRolesByUser(string userId, bool isPharmixAdmin)
        {
            var roles = _roleManager.Roles.ToList();
            var user = GetUser(userId); // _userManager.FindByIdAsync(userId).Result; //.GetUserAsync(userPrincipal).Result;
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var userRoleViewModelList = (from r in roles
                                         join ur in userRoles on r.Name equals ur into urdef
                                         from ur in urdef.DefaultIfEmpty()
                                         select new UserRoleViewModel
                                         {
                                             RoleId = r.Id,
                                             RoleName = r.Name,
                                             IsSelected = ur != null
                                         }).ToList();
            if (!isPharmixAdmin)
                userRoleViewModelList = userRoleViewModelList.Where(x => !_excludeManageRoles.Contains(x.RoleName)).ToList();
            return userRoleViewModelList;

        }

        public List<string> GetRolesByUserName(string userName)
        {
            if (PharmixStaticHelper.SuperAdminUsername.Equals(userName, StringComparison.CurrentCultureIgnoreCase))
            {
                return new List<string>() { userName };
            }

            var user = GetUserByName(userName);
            return _userManager.GetRolesAsync(user).Result.ToList();
        }

        public async Task UpdateUserRole(UserViewModel userViewModel)
        {
            if (userViewModel != null)
            {
                if (userViewModel.UserRoleViewModelList != null && userViewModel.UserRoleViewModelList.Count() > 0)
                {
                    var user = GetUser(userViewModel.Id); // await _userManager.FindByIdAsync(userViewModel.Id);
                    var existingRoles = await _userManager.GetRolesAsync(user);
                    var selectedRoles = userViewModel.UserRoleViewModelList.Where(x => x.IsSelected).Select(x => x.RoleName).Except(existingRoles).ToList();
                    var unSelectedRoles = userViewModel.UserRoleViewModelList.Where(x => !x.IsSelected).Select(x => x.RoleName).ToList();
                    var rolesToRemove = existingRoles.Intersect(unSelectedRoles).ToList();



                    //var roleIdsToRemove = userViewModel.UserRoleViewModelList.Select(x => x.RoleId).ToList();

                    //_context.UserRoles.RemoveRange(_context.UserRoles.Where(x => x.UserId == userViewModel.Id && roleIdsToRemove.Contains(x.RoleId)));

                    //foreach (var role in userViewModel.UserRoleViewModelList.Where(x => x.IsSelected))
                    //{
                    //    _context.UserRoles.Add(new IdentityUserRole<string>() { RoleId = role.RoleId, UserId = userViewModel.Id });
                    //}
                    //_context.SaveChanges();


                    foreach (var role in rolesToRemove)
                    {
                        var identityRole = await _roleManager.FindByNameAsync(role);
                        await _userManager.RemoveFromRoleAsync(user, role);   //cannot access a disposed object AspNetUserManager
                        _context.RolePermissions.RemoveRange(_context.RolePermissions.Where(x => x.RoleId == identityRole.Id));
                    }
                    foreach (var role in selectedRoles)
                    {
                        try
                        {
                            await _userManager.AddToRoleAsync(user, role);  //cannot access a disposed object AspNetUserManager
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                }
            }
        }

        public GridViewModel GetRoleSearchResult(SearchRequest request, bool isPharmixAdmin)
        {
            var model = UserMapper.CreateRoleGridViewModel();
            //var roles = _repository.GetAll<IdentityRole>().ToList(); //_context.Roles.ToList();
            var roles = _context.Roles.ToList();
            
            var roleViewModelList = Mapper.Map<List<IdentityRole>, List<RoleViewModel>>(roles);
            roleViewModelList = roleViewModelList.Select((x, i) => { x.TempId = i + 1; return x; }).ToList();

            if (!isPharmixAdmin)
                roleViewModelList = roleViewModelList.Where(x => !_excludeManageRoles.Contains(x.Name)).ToList();


            var pageResult = QueryListHelper.SortViewModelResults(roleViewModelList, request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.Name.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(x => UserMapper.BindRoleGridData(x));
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public string GetRoleIdByTempId(int tempId)
        {
            var roles = _context.Roles.OrderBy(x => x.Name).ToList();
            var roleViewModelList = Mapper.Map<List<IdentityRole>, List<RoleViewModel>>(roles);
            return roleViewModelList.OrderBy(x => x.Name).Skip(tempId - 1).Take(1).FirstOrDefault().Id;
        }

        public int GetTempIdByRoleId(string roleId)
        {
            var roles = _context.Roles.OrderBy(x => x.Name).ToList();
            var roleViewModelList = Mapper.Map<List<IdentityRole>, List<RoleViewModel>>(roles);
            roleViewModelList = roleViewModelList.Select((x, i) => { x.TempId = i + 1; return x; }).ToList();

            return roleViewModelList.FirstOrDefault(x => x.Id == roleId).TempId;
        }

        #region TempId
        #region User
        private void UpdateUserTempIdCache()
        {
            _cacheService.Remove(_userTempCache);
            Dictionary<int, string> userTempId = new Dictionary<int, string>();
            var users = _repository.GetContext().Users.ToList();
            int i = 1;
            if (users != null)

                foreach (var user in users)
                {
                    userTempId.Add(i++, user.Id);
                }
            _cacheService.Set(_userTempCache, userTempId);
        }
        public string GetUserIdByTempId(int id)
        {
            var userTempDic = _cacheService.Get<Dictionary<int, string>>(_userTempCache);
            if (userTempDic != null)
                return userTempDic.FirstOrDefault(x => x.Key == id).Value;
            else return string.Empty;
        }
        public List<UserViewModel> UpdateTempID(List<UserViewModel> userViewModelList)
        {
            var tempIdCache = _cacheService.Get<Dictionary<int, string>>(_userTempCache);
            return userViewModelList.Select((x, i) => { x.TempId = tempIdCache.FirstOrDefault(t => t.Value.Equals(x.Id)).Key; return x; }).ToList();   //Temp ID. Gridview does not allowing string Id
        }
        public UserViewModel UpdateTempID(UserViewModel userViewModel)
        {
            var tempIdCache = _cacheService.Get<Dictionary<int, string>>(_userTempCache);
            userViewModel.TempId = tempIdCache.FirstOrDefault(t => t.Value.Equals(userViewModel.Id)).Key; //Temp ID. Gridview does not allowing string Id
            return userViewModel;
        }

        #endregion

        #endregion

        #region RolePermission

        //public RoleViewModel GetRolePermissionByRole(string roleId, string currUserId)
        //{
        //    var role = _roleManager.Roles.FirstOrDefault(x => x.Id == roleId);

        //    var rolePermissionViewModelList = (from p in _context.Permissions

        //                                       join tm in _context.TrustModules on p.ModuleId equals tm.ModuleId
        //                                       join ut in _context.UserTrusts on tm.TrustId equals ut.TrustId

        //                                           //join um in _context.UserModules on p.ModuleId equals um.ModuleId
        //                                       join rp in _context.RolePermissions on new { pid = (int?)p.Id, rid = roleId } equals new { pid = rp.PermissionId, rid = rp.RoleId } into rpdef
        //                                       from rp in rpdef.DefaultIfEmpty()

        //                                       join r in _context.Roles on rp.RoleId equals r.Id into rdef
        //                                       from r in rdef.DefaultIfEmpty()

        //                                           //where um.UserId.Equals(currUserId) && (rp == null || rp.RoleId.Equals(roleId))
        //                                       where ut.UserId== currUserId && p.Key != null && p.Key != "" && p.ModuleId != null && p.ModuleId != 0 && (rp == null || rp.RoleId.Equals(roleId))
        //                                       select new RolePermissionViewModel
        //                                       {
        //                                           //Id=rp?.Id,
        //                                           PermissionId = p.Id,
        //                                           PermissionName = p.Name,
        //                                           IsHaveAccess = rp != null
        //                                       }).ToList();

        //    RoleViewModel roleViewModel = new RoleViewModel() { Id = roleId, Name = role.Name, RolePermissionViewModelList = rolePermissionViewModelList };
        //    return roleViewModel;
        //}

        //public async Task UpdateRolePermission(RoleViewModel roleViewModel)
        //{
        //    if (roleViewModel != null && !string.IsNullOrEmpty(roleViewModel.Id) && roleViewModel.RolePermissionViewModelList != null && roleViewModel.RolePermissionViewModelList.Count() > 0)
        //    {
        //        var rolePermissionToRemove = _context.RolePermissions.Where(x => x.RoleId == roleViewModel.Id);
        //        _context.RolePermissions.RemoveRange(rolePermissionToRemove);

        //        foreach (var rolePermission in roleViewModel.RolePermissionViewModelList.Where(x => x.IsHaveAccess))
        //        {
        //            _context.RolePermissions.Add(new RolePermission() { PermissionId = rolePermission.PermissionId, RoleId = roleViewModel.Id });
        //        }

        //        await _context.SaveChangesAsync();

        //    }
        //}


        public RoleViewModel GetRolePermissionByRole(string roleId, string currUserId, int trustId=0)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == roleId);

            var groupViewModelList = (from g in _context.Groups
                                      join rp in _context.RolePermissions on new { gId =(int?) g.Id, rId = roleId } equals new { gId = rp.GroupId, rId = rp.RoleId }  into rpdef
                                      from rp in rpdef.DefaultIfEmpty()
                                      where (g.TrustId== trustId || trustId==0) && g.PermissionGroups!=null && g.PermissionGroups.Count()>0
                                      select new GroupViewModel()
                                      {
                                          Id = g.Id,
                                          IsSelected = rp != null,
                                          Name = g.Name
                                      }).ToList();
            //_context.RolePermissions.Where(x => x.GroupId != null).Select(x=>x.Group);

            var rolePermissionViewModelList = (from p in _context.Permissions

                                               join pg in _context.PermissionGroups on p.Id equals pg.PermissionId
                                               join g in groupViewModelList on pg.GroupId equals g.Id

                                               join tm in _context.TrustModules on p.ModuleId equals tm.ModuleId
                                               join ut in _context.UserTrusts on tm.TrustId equals ut.TrustId

                                               //join um in _context.UserModules on p.ModuleId equals um.ModuleId
                                               join rp in _context.RolePermissions on new { pid = (int?)p.Id, rid = roleId } equals new { pid = rp.PermissionId, rid = rp.RoleId } into rpdef
                                               from rp in rpdef.DefaultIfEmpty()

                                               join r in _context.Roles on rp.RoleId equals r.Id into rdef
                                               from r in rdef.DefaultIfEmpty()

                                                   //where um.UserId.Equals(currUserId) && (rp == null || rp.RoleId.Equals(roleId))
                                               where ut.UserId == currUserId && p.Key != null && p.Key != "" && p.ModuleId != null && p.ModuleId != 0 && (rp == null || rp.RoleId.Equals(roleId))
                                               select new PermissionViewModel
                                               {
                                                   //Id=rp?.Id,
                                                   Id = p.Id,
                                                   Name = p.Name,
                                                   IsSelected = g.IsSelected || rp != null,
                                                   GroupId = g.Id
                                               }).ToList();

            //List<GroupViewModel> groupViewModelList = new List<GroupViewModel>();
            if (groupViewModelList != null)
                for (int i = 0; i < groupViewModelList.Count; i++)
                {
                    int groupId = groupViewModelList[i].Id;
                    groupViewModelList[i].PermissionViewModelList = rolePermissionViewModelList.Where(x => x.GroupId == groupId).ToList();
                }


            RoleViewModel roleViewModel = new RoleViewModel() { Id = roleId, Name = role.Name, GroupViewModelList = groupViewModelList };
            return roleViewModel;
        }


        public async Task UpdateRolePermission(RoleViewModel roleViewModel)
        {
            if (roleViewModel != null && !string.IsNullOrEmpty(roleViewModel.Id) && roleViewModel.GroupViewModelList != null && roleViewModel.GroupViewModelList.Count() > 0)
            {

                var rolePermissionToRemove = _context.RolePermissions.Where(x => x.RoleId == roleViewModel.Id);
                _context.RolePermissions.RemoveRange(rolePermissionToRemove);


                foreach (var groupViewModel in roleViewModel.GroupViewModelList.Where(x => x.IsSelected))
                {
                    _context.RolePermissions.Add(new RolePermission() { GroupId = groupViewModel.Id, RoleId = roleViewModel.Id });
                }

                var groupViewModelList = roleViewModel.GroupViewModelList.Where(x => !x.IsSelected).ToList();
                if (groupViewModelList != null && groupViewModelList.Count() > 0)
                {
                    foreach (var permissionViewModel in groupViewModelList.SelectMany(x=>x.PermissionViewModelList).Where(x=>x.IsSelected))
                    {
                        _context.RolePermissions.Add(new RolePermission() { PermissionId = permissionViewModel.Id, RoleId = roleViewModel.Id });
                    }
                }

                await _context.SaveChangesAsync();
            }
        }


        #endregion

        #region Trusts

        public async Task UpdateUserDetails(UserViewModel userViewModel)
        {
            if (userViewModel != null && userViewModel.TrustIds != null && userViewModel.TrustIds.Count() > 0)
            {
                var trustIds = userViewModel.TrustIds;
                _context.UserTrusts.RemoveRange(_context.UserTrusts.Where(x => trustIds.Contains(x.TrustId) && x.UserId == userViewModel.Id));

                foreach (var trustId in userViewModel.TrustIds)
                {
                    _context.UserTrusts.Add(new UserTrust() { UserId = userViewModel.Id, TrustId = trustId });
                }

                await _context.SaveChangesAsync();
            }
        }
        #endregion



    }

    public static partial class CustomMapper
    {
        public static List<UserViewModel> MapToViewModelList(this List<ApplicationUser> users)
        {
            users = users == null ? new List<ApplicationUser>() : users;
            var userViewModelList = new List<UserViewModel>();
            return Mapper.Map(users, userViewModelList);
        }

    }
}
