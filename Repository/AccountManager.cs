using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using ViewModels;

namespace Repository
{
    public class AccountManager : SuperManager<User>
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        public AccountManager(MyDbContext dbContext, UserManager<User> _userManager, SignInManager<User> _signInManager) : base(dbContext)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public async Task<IdentityResult> SignUp(UserSignUpViewModel model)
        {
            User user = model.ToModel();
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                result = await userManager.AddToRoleAsync(user, model.Role);
            }
            return result;
        }
        public async Task<SignInResult> SignIn(UserSignInViewModel model)
        {
            return await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
        }
        public async void SignOut()
        {
            await signInManager.SignOutAsync();
        }
        //public async Task<IdentityResult> ChangePassword(UserChangePasswordViewModel model)
        //{
        //    var user = await userManager.FindByIdAsync(model.Id);
        //    if (user != null)
        //    {
        //        return await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //    }
        //    return IdentityResult.Failed(new IdentityError()
        //    {
        //        Description = "User Not Found"
        //    });
        //}
        public async Task<string> GetForgetPasswordCode(UserChangePasswordViewModel userChangePasswordViewModel)
        {
            var user = await userManager.FindByEmailAsync(userChangePasswordViewModel.Email);
            if (user != null)
            {
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                return code;
            }
            return string.Empty;
        }
        public async Task<IdentityResult> ForgetPassword(UserForgetPasswordViewModel userForgetPasswordViewModel)
        {
            var user = await userManager.FindByEmailAsync(userForgetPasswordViewModel.Email);
            if (user != null)
            {
                return await userManager.ResetPasswordAsync(user, userForgetPasswordViewModel.Code, userForgetPasswordViewModel.NewPassword);
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "User Not Found"
            });
        }
        public async Task<string> GetChangeEmailCode(UserRequestChangeEmailViewModel userRequestChangeEmail)
        {
            var user = await userManager.FindByEmailAsync(userRequestChangeEmail.CurrentEmail);
            if (user != null)
            {
                var code = await userManager.GenerateChangeEmailTokenAsync(user,userRequestChangeEmail.NewEmail);
                return code;
            }
            return string.Empty;
        }
        public async Task<IdentityResult> ChangeEmail(UserChangeEmailViewModel userChangeEmailViewModel)
        {
            var user = await userManager.FindByEmailAsync(userChangeEmailViewModel.CurrentEmail);
            if (user != null)
            {
                return await userManager.ChangeEmailAsync(user, userChangeEmailViewModel.NewEmail, userChangeEmailViewModel.Code);
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "User Not Found"
            });
        }
        public async Task<IdentityResult> AssignRoleToUser(string userId,List<string> roles)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await userManager.AddToRolesAsync(user, roles);
            }
            return new IdentityResult();
        }
    }
}
