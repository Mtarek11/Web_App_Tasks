using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repository
{
    public class SuperManager<T> where T : class
    {
        private readonly Models.MyDbContext dbContext;
        public readonly DbSet<T> dbSet;
        public SuperManager(Models.MyDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>(); 
        }
        public IQueryable<T> GetAll()
        {
            return dbSet.AsQueryable();
        }
        public EntityEntry<T> Add(T entity)
        {
           return dbSet.Add(entity);
        }
        public EntityEntry<T> Update(T entity)
        {
            return dbSet.Update(entity);
        }
        public EntityEntry<T> Delete(T entity)
        {
            return dbSet.Remove(entity);
        }
        public IQueryable<T> Get(
          Expression<Func<T, bool>> Filter, string OrderBy, bool IsAscending, int PageSize, int PageIndex)
        {
            var Quary = dbSet.AsQueryable();
            if (Filter != null)
            {
                Quary = Quary.Where(Filter);
            }
            Quary = Quary.OrderBy(OrderBy, IsAscending);
            if (PageIndex < 1)
            {
                PageIndex = 1;
            }
            var temp = dbSet.Count() / Convert.ToDouble(PageSize);
            if (PageIndex > temp + 1)
            {
                PageIndex = 1;
            }
            int ToBeSkiped = (PageIndex - 1) * PageSize;
            return Quary.Skip(ToBeSkiped).Take(PageSize);
        }
    }
}
