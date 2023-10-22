using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserSignInViewModel
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required, StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
