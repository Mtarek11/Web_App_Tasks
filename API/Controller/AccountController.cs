using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Text;
using ViewModels;

namespace API.Controllers
{
    public class AccountController : ControllerBase
    {
        AccountManager accManger;
        public AccountController(AccountManager _accManger)
        {
            this.accManger = _accManger;
        }

        public async Task<IActionResult> SignIn(UserSignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accManger.SignIn(viewModel);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return new ObjectResult("Invalid User Name or Password");
                }
            }
            var erroes = new StringBuilder();
            foreach (var item in ModelState.Values)
            {
                foreach (var item1 in item.Errors)
                {
                    erroes.Append(item1.ErrorMessage);
                }
            }

            return new ObjectResult(erroes);
        }


        public async Task<IActionResult> SignUp( UserSignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await accManger.SignUp(viewModel);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    var errors2 = new StringBuilder();
                    foreach (var item in result.Errors)
                    {
                        errors2.Append(item.Description);
                    }
                    return new ObjectResult(errors2);
                }
            }
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