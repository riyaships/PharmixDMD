using Pharmix.Data.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Models.ManageViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int TrustId { get; set; }
        public bool IsSelected { get; set; }
        public List<PermissionViewModel> PermissionViewModelList { get; set; }
    }
}
