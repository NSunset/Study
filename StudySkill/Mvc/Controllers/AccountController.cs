using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4.Services;
using IdentityServer4.Events;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Mvc.Data;

namespace Mvc.Controllers
{
    public class AccountController : Controller
    {
        //private readonly TestUserStore _user;


        //public AccountController(TestUserStore user)
        //{
        //    _user = user;
        //}

        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signinManager;

        private IIdentityServerInteractionService _identityServerInteraction;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService identityServerInteraction
            )
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _identityServerInteraction = identityServerInteraction;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.Username);
                if (user == null)
                {
                    ModelState.AddModelError(nameof(loginViewModel.Username), "用户名不存在");
                }
                else
                {
                    if (await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
                    {
                        AuthenticationProperties proper = null;
                        if (loginViewModel.RememberMe)
                        {
                            proper = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                            {
                                ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30)),
                                IsPersistent = true
                            };
                        }

                        //var iuser = new IdentityServer4.IdentityServerUser(user.SubjectId)
                        //{
                        //    DisplayName = user.Username
                        //};


                        //await HttpContext.SignInAsync(iuser, proper);
                        await _signinManager.SignInAsync(user, proper);

                        if (_identityServerInteraction.IsValidReturnUrl(loginViewModel.ReturnUrl))
                        {
                            return Redirect(loginViewModel.ReturnUrl);
                        }
                    }
                    ModelState.AddModelError(nameof(loginViewModel.Password), "密码错误");
                }
            }
            return View();
        }

        public IActionResult Logout(string logoutId)
        {
            //HttpContext.SignOutAsync();
            _signinManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
