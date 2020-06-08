using AutoMapper;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Web.Entities;
using Pharmix.Web.Models;
using Pharmix.Web.Models.ManageViewModels;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Pharmix.Web.Services
{
    public class PermissionGroupService : IPermissionGroupService
    {
        private readonly IRepository repository;
        private readonly PharmixEntityContext _context;


        public PermissionGroupService(IRepository repository)
        {
            this.repository = repository;
            this._context = repository.GetContext();

        }

        public IEnumerable<Group> GetAllGroups()
        {
            return repository.GetAll<Group>();//.Where(e => !e.IsArchived);
        }

        public List<PermissionViewModel> GetPermissionsByGroup(int groupId)
        {
            var permissionViewModelList = (from p in _context.PermissionGroups
                               where p.GroupId == groupId
                                           select new PermissionViewModel()
                               {
                                   Id = p.PermissionId,
                                   Name = p.Permission != null ? p.Permission.Name : string.Empty,
                               }).ToList();

            return permissionViewModelList;
        }

        /// <summary>
        /// Get permissions which are not assigned to any group
        /// </summary>
        /// <returns></returns>
        public List<PermissionViewModel> GetNotAssignedChildPermissions(int trustId)
        {
            var assignedPermissionIdList = (from p in _context.PermissionGroups
                                            join g in _context.Groups on p.GroupId equals g.Id
                                            where g.TrustId==trustId
                                            select p.PermissionId).ToList();

            var availablePermissionViewModelList = (from p in _context.Permissions
                                                    join tm in _context.TrustModules on p.ModuleId equals tm.ModuleId
                                                    where !assignedPermissionIdList.Contains(p.Id)
                                                    && tm.TrustId== trustId
                                                    && p.ParentPermissionId!=0
                                                    select new PermissionViewModel()
                                                    {
                                                        Id = p.Id,
                                                        Name = p.Name
                                                    }).ToList();
            return availablePermissionViewModelList;
        }

        public Group GetGroupById(int id)
        {
            return repository.GetById<Group>(id);
        }

        public async Task UpdatePermissionGroup(GroupViewModel groupViewModel)
        {
            if (groupViewModel != null && groupViewModel.PermissionViewModelList != null && groupViewModel.PermissionViewModelList.Count() > 0)
            {
                var permissionIds = groupViewModel.PermissionViewModelList.Select(x => x.Id).ToList();

                _context.PermissionGroups.RemoveRange(_context.PermissionGroups.Where(x => x.GroupId == groupViewModel.Id));
                foreach (var permissionViewModel in groupViewModel.PermissionViewModelList.Where(x => x.IsSelected))
                {
                    _context.PermissionGroups.Add(new PermissionGroup() { GroupId = groupViewModel.Id, PermissionId = permissionViewModel.Id });
                }
             await  _context.SaveChangesAsync();

            }
        }

        public int SaveGroup(Group group, string user)
        {
            if (group.Id > 0)
            {
                repository.SaveExisting(group);
                return group.Id;
            }

            return repository.SaveNew(group).Id;
        }

        public GridViewModel GetSearchResult(SearchRequest request)
        {
            var model = GroupMapper.CreateGridViewModel();

            var pageResult = QueryListHelper.SortResults(GetAllGroups(), request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.Name.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(GroupMapper.BindGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public GroupViewModel CreateViewModel(int id)
        {
            var group = GetGroupById(id);
            var model = group == null ? new GroupViewModel() : Mapper.Map<Group, GroupViewModel>(group);

            return model;
        }

        public int MapViewModelToGroup(GroupViewModel model, string user, bool performSave)
        {
            var group = GetGroupById(model.Id);

            group = group == null ? Mapper.Map<GroupViewModel, Group>(model) :
                Mapper.Map(model, group);

            if (!performSave) return group.Id;

            if (group.Id > 0)
            {
                repository.SaveExisting(group);
            }
            else
            {

                repository.SaveNew(group);
            }

            return group.Id;
        }
        public bool ArchiveGroup(int id, string user)
        {
            var group = repository.GetById<Group>(id);
            repository.Delete<Group>(group);
            repository.SaveChanges();
            return true;
        }
    }
}
