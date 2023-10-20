using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryManager:SuperManager<Category>
    {
        public CategoryManager(Models.MyDbContext dbContext):base(dbContext) { }
    }
}
