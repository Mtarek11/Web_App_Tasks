using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repository
{
    public class RoleManager : SuperManager<IdentityRole>
    {
        Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;
        public RoleManager(
            MyDbContext dbContext,
            Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager
            ) : base(dbContext)
        {
            roleManager = _roleManager;
        }
        public async Task<IdentityResult> Add(AddRoleViewModel veiwModel)
        {
            return await roleManager.CreateAsync(veiwModel.ToModel());
        }
    }
}
