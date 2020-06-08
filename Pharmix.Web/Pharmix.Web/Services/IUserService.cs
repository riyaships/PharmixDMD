using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Models;

namespace Pharmix.Web.Services
{
    public interface IUserService
    {
        IQueryable<ApplicationUser> GetUsers();
        ApplicationUser GetUser(string userId);
        ApplicationUser ChangePassword(string userId, string password, string passwordToken);
        UserViewModel GetUserPasswordResetToken(string userId);
        SelectList GetRoleSelectList(string selectedValue = "");
        Task<List<ApplicationUser>> GetUsersByRoles(List<string> roles);
        GridViewModel GetSearchResult(SearchRequest request, string userName, bool isPharmixAdmin);
        UserViewModel CreateViewModel(string id);
        bool IsPharmixAdmin(string userName);
        string GetUserIdByTempId(int id);
        List<UserRoleViewModel> GetRolesByUser(string userId, bool isPharmixAdmin);
        Task UpdateUserRole(UserViewModel userViewModel);
        GridViewModel GetRoleSearchResult(SearchRequest request, bool isPharmixAdmin);
        string GetRoleIdByTempId(int tempId);

        RoleViewModel GetRolePermissionByRole(string roleId, string currUserId, int trustId=0);
        int GetTempIdByRoleId(string roleId);
        Task UpdateRolePermission(RoleViewModel roleViewModel);
        ApplicationUser GetUserByName(string userName);
        List<string> GetRolesByUserName(string userName);
        Task UpdateUserDetails(UserViewModel userViewModel);
        //bool IsTrustAdmin(string userName);
    }
}
