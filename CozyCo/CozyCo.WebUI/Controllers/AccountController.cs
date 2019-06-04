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
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            RedirectUserWhenAlreadyLoggedIn();
            var roles = _roleManager.Roles.ToList();

            var vm = new RegisterViewModel
            {
                Roles = roles
            };

            return View(vm);
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

                if (result.Succeeded)//new user got created
                {
                    //assign the selected role to the newly created user
                    result = await _userManager.AddToRoleAsync(newUser, vm.Password);

                    if (result.Succeeded)
                    {
                        //we can login the user
                        await _signInManager.SignInAsync(newUser, false);

                        //redirect
                        if(vm.Role=="Landlord")
                        {
                            return RedirectToAction("Index", "Landlord");
                        }

                        return RedirectToAction("Index", "Tenant");
                    }
                    
                 
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

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, false);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password incorrect");
                }
            }
            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
           await  _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectUserWhenAlreadyLoggedIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return null;
        }




    }


}