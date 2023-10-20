using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserChangePasswordViewModel
    {
        public string Id { get; set; } = "";
        [Required, StringLength(50, MinimumLength = 8), Display(Name = "Current Password"), DataType(DataType.Password)]
       
        public string Email { get; set; }
    }
}
