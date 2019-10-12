using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebUI.Models;
using MyApp.WebUI.Entity;
using Microsoft.AspNetCore.Authorization;
using MyApp.Services;
using MyApp.Data.Abstract;

namespace MyApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        //list, create, 
        UserManager<ApplicationUser> userManager;
        private IPasswordHasher<ApplicationUser> passwordHasher;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        GeneralServices services;
        ICompanyRepository companyRepository;

        public AdminController(UserManager<ApplicationUser> _userManager,
                   IPasswordValidator<ApplicationUser> _passwordValidator,
                   ICompanyRepository _companyRepository,
                   IPasswordHasher<ApplicationUser> _passwordHasher)
        {
            passwordValidator = _passwordValidator;
            passwordHasher = _passwordHasher;
            userManager = _userManager;
            services = new GeneralServices(this, _userManager, _companyRepository);


        }
        public async Task< IActionResult> Index()
        {
            int companyId = await services.GetCurrentCompanyId();
            return View(userManager.Users.Where(i=>i.CompanyId==companyId));


        }
        public IActionResult List()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.CompanyId = await services.GetCurrentCompanyId();
                var result = await userManager.CreateAsync(user, model.Passowrd);
                if (result.Succeeded)
                {
                    var result3 = await userManager.AddToRoleAsync(user, "NormalRole");

                    result = await userManager.AddToRoleAsync(user, "NormalRole");
                    return RedirectToAction("Index");
                }
                else
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            //find record, delete it, message
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
            }
            else
                ModelState.AddModelError("", "Record not found!");

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Update(string Id, string Password, string Email, ApplicationUser model)
        {
            /*
             * find user
             *       if exist :
             *          if password is validate ? updata pw and update user
             *          
             *          
             */


            var user = await userManager.FindByIdAsync(Id);

            if (user != null)//if user exist
            {
                user.Email = Email;
                user.UserName = model.UserName;

                IdentityResult validPass = new IdentityResult();

                if (!string.IsNullOrEmpty(Password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, Password);// check password validation

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, Password);//update pw
                    }
                    else //pw is not validated
                    {
                        foreach (var item in validPass.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }

                //you can chek email ....etc valdation

                if (validPass.Succeeded)//if all validation is Ok
                {
                    var result = await userManager.UpdateAsync(user); //update user info

                    if (result.Succeeded) //if saved 
                    {
                        TempData["message"] = $" {user.UserName} saved !";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return View(user);
        }
    }
}