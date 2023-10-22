using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Text;
using ViewModels;

namespace API.Controllers
{
    public class ProductController : ControllerBase
    {
        ProductManager productManager;
        CategoryManager categoryManeger;
        UnitOfWork unitOfWork;
        public ProductController(ProductManager _productManager, CategoryManager _categoryManeger, UnitOfWork _unitOfWork)
        {
            this.productManager = _productManager;
            this.categoryManeger = _categoryManeger;
            this.unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            var list = productManager.Get();
            return new ObjectResult(list);
        }
        [HttpPost]
        public IActionResult Add( AddProductViewModel addProduct)
        {
            if (ModelState.IsValid)
            {
                foreach (IFormFile file in addProduct.Images)
                {
                    FileStream fileStream = new FileStream(
                        Path.Combine(
                            Directory.GetCurrentDirectory(), "Content", "Images", file.FileName),
                        FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Position = 0;
                    addProduct.ImagesURL.Add(file.FileName);
                }
                productManager.Add(addProduct);
                unitOfWork.Commit();
                return Ok();

            }
            else
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }
                return new ObjectResult(str);
            }
        }
    }
}