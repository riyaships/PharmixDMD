using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Services
{
    public class TrustService: ITrustService
    {
        private readonly IRepository repository;
        private readonly PharmixEntityContext _context;


        public TrustService(IRepository repository)
        {
            this.repository = repository;
            _context = repository.GetContext();
        }

        public List<TrustViewModel> GetAllTrusts()
        {
            var trusts= repository.GetAll<Trust>().Where(e => !e.IsArchived).ToList();
            return AutoMapper.Mapper.Map<List<Trust>, List<TrustViewModel>>(trusts);
        }

        public Trust GetTrustById(int id)
        {
            return repository.GetById<Trust>(id);
        }

        public SelectList GetTrustSelectList(string selectedValue = "")
        {
            SelectList roles = null;
            var roleViewModelList = GetAllTrusts();
            if (roleViewModelList != null)
            {
                if (!string.IsNullOrEmpty(selectedValue))
                    roles = new SelectList(roleViewModelList, "Id", "Name", selectedValue);
                else
                    roles = new SelectList(roleViewModelList, "Id", "Name");
            }
            else
                roles = new SelectList(Enumerable.Empty<SelectListItem>());
            return roles;
        }

        public int GetTrustIdByUser(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                var trustId=(from ut in _context.UserTrusts
                 join usr in _context.Users on ut.UserId equals usr.Id
                 where usr.UserName.Equals(userName)
                 select ut.TrustId).FirstOrDefault();
                return trustId;
            }
            else return 0;
        }

    }
}
