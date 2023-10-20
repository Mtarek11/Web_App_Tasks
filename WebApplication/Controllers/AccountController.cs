using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using ViewModels;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        AccountManager accManger;
        RoleManager roleManager;
        public AccountController(AccountManager _accManger, RoleManager _roleManager)
        {
            accManger = _accManger;
            roleManager = _roleManager;
        }
        private List<SelectListItem> RoleList()
        {
            return roleManager.GetAll().Select(r => new SelectListItem()
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            ViewData["list"] = RoleList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpViewModel userSignUpViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await accManger.SignUp(userSignUpViewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    ViewData["list"] = RoleList();
                    return View();
                }
            }
            ViewData["list"] = RoleList();
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        public async Task<IActionResult> SignIn(UserSignInViewModel userSignInViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accManger.SignIn(userSignInViewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Invaild User Name Or Password");
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult SignOut()
        {
            accManger.SignOut();
            return RedirectToAction("SignIn");
        }
        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {

            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accManger.GetForgetPasswordCode(viewModel);
                if (result != string.Empty)
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("mohamedtarek70m@gmail.com", "chsb cjdv wnik uxav"),
                        EnableSsl = true,
                    };
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("mohamedtarek70m@gmail.com"),
                        Subject = "Hello, Gmail!",
                        Body = $"code is :{result}",
                    };
                    mailMessage.To.Add(viewModel.Email);
                    await smtpClient.SendMailAsync(mailMessage);
                    ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(UserForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accManger.ForgetPassword(viewModel);
                if (!result.Succeeded)
                {
                    ViewBag.Success = false;
                }
                else
                {
                    ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }
        public IActionResult RequestChangeEmail()
        {

            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RequestChangeEmail(UserRequestChangeEmailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accManger.GetChangeEmailCode(viewModel);
                if (result != string.Empty)
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("mohamedtarek70m@gmail.com", "chsb cjdv wnik uxav"),
                        EnableSsl = true,
                    };
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("mohamedtarek70m@gmail.com"),
                        Subject = "Hello, Gmail!",
                        Body = $"code is :{result}",
                    };
                    mailMessage.To.Add(viewModel.CurrentEmail);
                    await smtpClient.SendMailAsync(mailMessage);
                    ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }
        [HttpGet]
        public IActionResult ChangeEmail()
        {
            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangeEmail(UserChangeEmailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accManger.ChangeEmail(viewModel);
                if (!result.Succeeded)
                {
                    ViewBag.Success = false;
                }
                else
                {
                    ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }
    }
}

