using Microsoft.AspNetCore.Mvc;
using Repository;
using ViewModels;

namespace WebApplication.Controllers
{
    public class RoleController : Controller
    {
        RoleManager roleManager;
        public RoleController(RoleManager roleManager)
        {
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Success = 0;
            ViewData["Roles"] = roleManager.GetAll().Select(i => new AddRoleViewModel()
            {
                ID = i.Id,
                Name = i.Name
            }).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.Add(model);
                if (result.Succeeded)
                {
                    ViewBag.Success = 1;
                }
                else
                {
                    ViewBag.Success = 2;
                }
            }
            else
            {
                ViewBag.Success = 2;
            }
            ViewData["Roles"] = roleManager.GetAll().Select(i => new AddRoleViewModel()
            {
                ID = i.Id,
                Name = i.Name
            }).ToList();
            return View();
        }
    }
}
