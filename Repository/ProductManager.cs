using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LinqKit;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repository
{
    public class ProductManager : SuperManager<Product>
    {
        public ProductManager(Models.MyDbContext dbContext):base(dbContext) { }
        public IQueryable<Product> Get()
        {
            return GetAll().Include(i => i.ProductAttachments).Include(i => i.Category);
        }
        public Product GetOneById(int id)
        {
            return Get().Where(i => i.ID == id).FirstOrDefault();
        }
        public void Edit(AddProductViewModel addProductViewModel)
        {
            Product oldProduct = GetAll().Where(i => i.ID == addProductViewModel.Id).FirstOrDefault();
            oldProduct.Name = addProductViewModel.Name;
            oldProduct.Price = addProductViewModel.Price;
            oldProduct.Description = addProductViewModel.Description;
            oldProduct.CategoryID = addProductViewModel.CategoryId;
            oldProduct.ProductAttachments = new List<ProductAttachments>();
            foreach (var item in addProductViewModel.ImagesURL)
            {
                oldProduct.ProductAttachments.Add(new ProductAttachments()
                {
                    Image = item
                });
            }
            Update(oldProduct);
        }
        public AddProductViewModel GetEditableByID(int id)
        {
            return GetAll().Where(i => i.ID == id).FirstOrDefault().ToAddViewModel();
        }
        public void Add(AddProductViewModel viewModel)
        {
            Product product = viewModel.ToModel();
            Add(product);
        }
        public void Delete (int id)
        {
            Delete(GetOneById(id));
        }
        public PaginationViewModel<List<ProductViewModel>> Search(
           string? Name = null,
           string? CategoryName = null,
           int CategoryID = 0,
           int ProductID = 0,
           double Price = 0,
           string OrderBy = "ID",
           bool IsAscending = false,
           int PageSize = 6,
           int PageIndex = 1
           )
        {
            var filter = PredicateBuilder.New<Product>();
            var oldFilter = filter;
            if (!string.IsNullOrEmpty(Name))
            {
                filter = filter.Or(i => i.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(CategoryName))
            {
                filter = filter.Or(i => i.Category.Name.ToLower().Contains(CategoryName.ToLower()));
            }
            if (CategoryID != 0)
            {
                filter = filter.Or(i => i.CategoryID == CategoryID);
            }
            if (ProductID != 0)
            {
                filter = filter.Or(i => i.ID == ProductID);
            }
            if (Price != 0)
            {
                filter = filter.And(i => i.Price <= Price);
            }
            if (oldFilter == filter)
            {
                filter = null;
            }
            var count = (filter != null) ? GetAll().Where(filter).Count() : base.GetAll().Count();
            var result = Get(filter, OrderBy, IsAscending, PageSize, PageIndex);
            return new PaginationViewModel<List<ProductViewModel>>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                Count = count,
                Data = result.Select(i => i.ToVeiwModel()).ToList()
            };
        }



    }
}
