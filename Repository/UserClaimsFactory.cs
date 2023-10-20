using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserClaimsFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        UserManager<User> userManager;
        public UserClaimsFactory(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager, IOptions<IdentityOptions> _options)
            : base(_userManager, _roleManager, _options)
        {
            userManager = _userManager;
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var claims = await base.GenerateClaimsAsync(user);
            if (user != null)
            {
                var roles = await userManager.GetRolesAsync(user);
                foreach (var item in roles)
                {
                    claims.AddClaim(new Claim(item, item));
                }
            }
            return claims;
        }
    }
}
