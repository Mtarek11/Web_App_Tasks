using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserForgetPasswordViewModel
    {
        public string Code { get; set; } = "";
        [Required, StringLength(50, MinimumLength = 8), Display(Name = "Email"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 8), Display(Name = "New Password"), DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
