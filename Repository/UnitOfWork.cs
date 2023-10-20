using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork
    {
        private Models.MyDbContext _db;
        public UnitOfWork( Models.MyDbContext db)
        {
            _db = db;
        }
        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}
