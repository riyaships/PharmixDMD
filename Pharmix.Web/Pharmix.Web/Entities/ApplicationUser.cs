using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Pharmix.Data.Entities.Context;

namespace Pharmix.Web.Entities
{
    [Table("ApplicationUser")]
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        private IUserClaimsPrincipalFactory<ApplicationUser> _claimsPrincipalFactory;
        #region Constructor

        public ApplicationUser()
        { }

        public ApplicationUser(IUserClaimsPrincipalFactory<ApplicationUser> claimsPrincipalFactory)
        {
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }

        #endregion
        public int? GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeTel { get; set; }
        public bool IsApproved { get; set; }
        public bool? IsResetPasswordRequired { get; set; }
        public virtual Address Address { get; set; }

        public string DisplayName() { return string.Format("{0}, {1}", Surname, FirstName); }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            ClaimsPrincipal claimsPrincipal = await _claimsPrincipalFactory.CreateAsync(this);
            ((ClaimsIdentity)claimsPrincipal.Identity).AddClaim(new Claim("user_id", this.UserName));

            return ((ClaimsIdentity)claimsPrincipal.Identity);
        }
    }


}
