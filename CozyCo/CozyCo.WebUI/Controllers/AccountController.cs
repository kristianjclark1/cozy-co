using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyCo.Domain.Models;
using CozyCo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CozyCo.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel vm)
        {
            // register the user
            if(ModelState.IsValid)
            {
                var newUser = new AppUser
                {
                    UserName = vm.Email,
                    NormalizedUserName = vm.Email.ToUpper(),
                    Email = vm.Email,
                    NormalizedEmail = vm.Email.ToUpper()
                };

                var result = await _userManager.CreateAsync(newUser, vm.Password);

                if (result.Succeeded)
                {
                    //new user got created
                    //we can login the user
                    await _signInManager.SignInAsync(newUser, false);
                    //send the user to the right page (redirect)
                    return RedirectToAction("Index", "Home"); // /home/index

                }
                else
                {
                    // new user was not added
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }   
                }

            }
            // sending back the error(s) to the view (register form)
            return View(vm);
        }
    }
}