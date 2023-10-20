using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserRequestChangeEmailViewModel
    {
        public string Id { get; set; } = "";
        [Required, StringLength(50, MinimumLength = 8), Display(Name = "Current Email"), DataType(DataType.EmailAddress)]
        public string CurrentEmail { get; set; }
        [Required, StringLength(50, MinimumLength = 8), Display(Name = "New Email"), DataType(DataType.EmailAddress)]
        public string NewEmail { get; set; }
    }
}
