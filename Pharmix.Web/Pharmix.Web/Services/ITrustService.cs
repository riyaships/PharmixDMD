using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Web.Entities.ViewModels;
using System.Collections.Generic;
using Pharmix.Web.Entities;

namespace Pharmix.Web.Services
{
    public interface ITrustService
    {
        List<TrustViewModel> GetAllTrusts();
        Trust GetTrustById(int id);
        SelectList GetTrustSelectList(string selectedValue = "");
        int GetTrustIdByUser(string userName);
    }
}
