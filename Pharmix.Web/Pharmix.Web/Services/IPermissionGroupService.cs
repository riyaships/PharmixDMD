using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Models;
using Pharmix.Web.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Services
{
    public interface IPermissionGroupService
    {
        IEnumerable<Group> GetAllGroups();
        Group GetGroupById(int id);
        int SaveGroup(Group group, string user);
        GridViewModel GetSearchResult(SearchRequest request);
        GroupViewModel CreateViewModel(int id);
        int MapViewModelToGroup(GroupViewModel model, string user, bool performSave);
        bool ArchiveGroup(int id, string user);
        List<PermissionViewModel> GetPermissionsByGroup(int groupId);
        List<PermissionViewModel> GetNotAssignedChildPermissions(int trustId);
        Task UpdatePermissionGroup(GroupViewModel groupViewModel);

    }
}
