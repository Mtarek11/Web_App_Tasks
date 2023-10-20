using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class ProductExtantions
    {
        public static Product ToModel(this ProductViewModel productViewModel)
        {
            var productAttachments = new List<ProductAttachments>();
            foreach (var item in productViewModel.Images)
            {
                productAttachments.Add(new ProductAttachments()
                {
                    Image = item
                });
            }
            return new Product
            {
                ID = productViewModel.Id,
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                Description = productViewModel.Description,
                CategoryID = productViewModel.CategoryId,
                ProductAttachments = productAttachments
            };
        }
        public static ProductViewModel ToViewModel(this Product product)
        {
            return new ProductViewModel
            {
                Id = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryID,
            };
        }
        public static AddProductViewModel ToAddViewModel(this Product product)
        {
            return new AddProductViewModel
            {
                Id = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryID,
            };
        }
        public static Product ToModel(this AddProductViewModel productViewModel)
        {
            var productAttachments = new List<ProductAttachments>();
            foreach (var item in productViewModel.ImagesURL)
            {
                productAttachments.Add(new ProductAttachments()
                {
                    Image = item
                });
            }
            return new Product
            {
                ID = productViewModel.Id,
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                Description = productViewModel.Description,
                CategoryID = productViewModel.CategoryId,
                ProductAttachments = productAttachments
            };

        }
        public static ProductViewModel ToVeiwModel(this Product product)
        {
            return new ProductViewModel
            {
                Id = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryID,
            };
        }
    }
}
