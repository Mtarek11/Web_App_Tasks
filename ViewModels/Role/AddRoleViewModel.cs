using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AddRoleViewModel
    {
        public string ID { get; set; } = "";
        [Required]
        public string Name { get; set; }
    }
}
