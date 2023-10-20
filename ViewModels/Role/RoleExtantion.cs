using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class RoleExtantion
    {
        public static IdentityRole ToModel(this AddRoleViewModel veiwModel)
        {
            return new IdentityRole
            {
                Name = veiwModel.Name,
            };
        }
    }
}
