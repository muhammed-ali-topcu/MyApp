using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebUI.Models;
using MyApp.WebUI.Entity;
using MyApp.Data.Abstract;

namespace MyApp.Controllers
{
   // [Authorize]//all methodes need login
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ICompanyRepository companyRepository;

        public AccountController(UserManager<ApplicationUser> _userManager,
            ICompanyRepository _companyRepository,
            SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            companyRepository = _companyRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]//not need login
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync(); //log out
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                        return Redirect("/");
                    else ModelState.AddModelError("Password", "Incorrect Password");
                }
                else
                    ModelState.AddModelError("emil", "invalid email");
            }

            //ViewBag.ReturnUrl = ReturnUrl;  DELETE iT FROM HERE     DO NOOOOOT USE İT HERE

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {

            return View();
        }

        //register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            /*
             * Company repository
             * get info 
             */
            if (ModelState.IsValid)
            {
                companyRepository.Creat(model.Company);

                // create user
                ApplicationUser user = new ApplicationUser();
                user.Company = model.Company;
                user.Email = model.Email;
                user.UserName = model.UserName;
                //model.ApplicationUser.Company = model.Company;
                var result = await userManager.CreateAsync(user, model.Passowrd);
                var result2 = await userManager.AddToRoleAsync(user, "Admin");
                var result3 = await userManager.AddToRoleAsync(user, "NormalRole");
              //  var resultTemp = await userManager.AddToRoleAsync(user, "Developer"); //hemen sil
                
                if (result.Succeeded)
                {
                    TempData["message"] = $"Welcome\n {model.Company.Name}  saved!";
                    await signInManager.SignOutAsync(); //log out
                     user = await userManager.FindByEmailAsync(model.Email);
                    var result4 = await signInManager.PasswordSignInAsync(user, model.Passowrd, false, false);

                    return Redirect("/Home/Index");
                }
                else
                {
                    companyRepository.Delete(model.Company);
                    foreach (var item in result.Errors)
                        ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        //public async Task<bool> CreateUser(ApplicationUser user, string password)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await userManager.CreateAsync(user, password);
        //        if (result.Succeeded)
        //            return true;
        //        else
        //            foreach (var item in result.Errors)
        //            {
        //                ModelState.AddModelError("", item.Description);
        //            }
        //    }
        //    return false;
        //}
    }
}