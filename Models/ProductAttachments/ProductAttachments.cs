using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductAttachments
    {
        public int ID { get; set; }
        public string Image {  get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}
