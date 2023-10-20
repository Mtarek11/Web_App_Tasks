using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Models;
using Repository;
using ViewModels;

namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        ProductManager productManager;
        CategoryManager categoryManager;
        UnitOfWork unitOfWork;
        public ProductController(ProductManager productManager, CategoryManager categoryManager, UnitOfWork unitOfWork)
        {
            this.productManager = productManager;
            this.categoryManager = categoryManager;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewData["Categories"] = categoryManager.GetAll().Select(i => i.Name).ToList();
            List<Product> products = productManager.GetAll().ToList();
            return View(products);
        }
        public IActionResult GetOne(int id)
        {
            Product product = productManager.GetOneById(id);
            return View(product);
        }
        public List<SelectListItem> GetCategories()
        {
            return categoryManager.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.ID.ToString(),
            }).ToList();
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Add()
        {
            ViewData["Categories"] = GetCategories();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Add(AddProductViewModel addProductViewModel)
        {
            if (ModelState.IsValid)
            {
                foreach(IFormFile file in addProductViewModel.Images)
                {
                    FileStream fileStream = new FileStream(
                        Path.Combine(
                        Directory.GetCurrentDirectory(), "Content", "Images", file.FileName),
                        FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Position = 0;
                    addProductViewModel.ImagesURL.Add(file.FileName);    
                }
                productManager.Add(addProductViewModel);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["Categories"] = GetCategories();
                return View();
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Delete(Product product)
        {
            Product product1 = productManager.GetOneById(product.ID);
            string fileName = product1.ProductAttachments.ToArray()[0].Image.ToString();
            System.IO.File.Delete($"Content/Images/{fileName}");
            productManager.Delete(product.ID);
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Edit(int id)
        {
            ViewData["Categories"] = GetCategories();
            AddProductViewModel product = productManager.GetEditableByID(id);
            ViewBag.Title = "Edit Product " + product.Name;
            return View(product);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Edit(AddProductViewModel addProduct)
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
                productManager.Edit(addProduct);
                unitOfWork.Commit();
                return RedirectToAction("Index");

            }
            else
            {
                ViewData["Categories"] = GetCategories();
                ViewBag.Title = "Edit Product " + addProduct.Name;
                return View(addProduct);
            }

        }
        public IActionResult Search(
           int ID = 0,
           string? Name = null,
           string? CategoryName = null,
           int CategoryID = 0,
           double Price = 0,
           string OrderBy = "Price",
           bool IsAscending = false,
           int PageSize = 6,
           int PageIndex = 1
           )
        {
            ViewData["Categories"] = GetCategories();
            var data = productManager.Search(Name, CategoryName, CategoryID, ID, Price, OrderBy, IsAscending, PageSize, PageIndex);
            return View(data);
        }
    }
}
