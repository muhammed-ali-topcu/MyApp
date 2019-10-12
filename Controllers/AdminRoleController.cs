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
using MyApp.Services;
using MyApp.Data.Abstract;

namespace MyApp.Controllers
{
    [Authorize(Roles ="Developer")]
    public class AdminRoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> userManager;
        GeneralServices services;
        ICompanyRepository companyRepository;


        public AdminRoleController(RoleManager<IdentityRole> _roleManager,
                   ICompanyRepository _companyRepository,
                    UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            services = new GeneralServices(this, _userManager, _companyRepository);

        }
        //[Authorize(Roles = "Admin")]

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
         public IActionResult List()
        {

            return View(roleManager.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (ModelState.IsValid)
            {

                var result = await roleManager.CreateAsync(new IdentityRole { Name = roleName });
                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(roleName);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityResult result = new IdentityResult();
            if (ModelState.IsValid)
            {
                IdentityRole role = await roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        TempData["message"] = $"{role.Name} deleted !";
                        return RedirectToAction("List");
                    }
                    else
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(item.Code, item.Description);
                        }
                }
            }
            return View(id);
        }
     //-----------------------Do nooooooooooooooooooooot change role name
        //public async Task<IActionResult> Update(string id)
        //{
        //    IdentityRole role = await roleManager.FindByIdAsync(id);
        //    if (role != null)
        //        return View(role);
        //    else
        //        return RedirectToAction("List");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(IdentityRole model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IdentityRole role = await roleManager.FindByIdAsync(model.Id);
        //        if (role != null)
        //        {
        //            role.Name = model.Name;
        //            IdentityResult result = await roleManager.UpdateAsync(role);
        //            if (result.Succeeded)
        //            {
        //                TempData["message"] = $"{role.Name} updated !";
        //                return RedirectToAction("List");
        //            }
        //            else
        //                foreach (var item in result.Errors)
        //                {
        //                    ModelState.AddModelError(item.Code, item.Description);
        //                }
        //        }

        //    }
        //    return View(model);
        //}

        public async Task<IActionResult> Edit(string id)
        {
            /*
             * find role 
             * find its members
             * find non members
             * fill data
             * return view
             */
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var members = new List<ApplicationUser>();
                var nonMembers = new List<ApplicationUser>();

                foreach (var user in userManager.Users)
                {
                    if (await userManager.IsInRoleAsync(user, role.Name))
                        members.Add(user);
                    else
                        nonMembers.Add(user);
                }

                var model = new RoleDetails()
                {
                    Role = role,
                    Members = members,
                    NonMembers = nonMembers
                };
                return View(model);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleEditModel model)
        {
            /*
             * find role
             * add members 
             * remove members
             */

            IdentityRole role = await roleManager.FindByIdAsync(model.RoleId);
            
            if(ModelState.IsValid && role!=null )
            {
                IdentityResult result = new IdentityResult();
                foreach (var userId in model.IdsToAdd??new string[] { })
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                        result = await userManager.AddToRoleAsync(user, role.Name);
                }

                foreach (var userId in model.IdsToRemove ?? new string[] { })
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                if(!result.Succeeded)
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
            }

            return RedirectToAction("List");
        }
    }
}