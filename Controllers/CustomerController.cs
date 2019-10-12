using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.Abstract;
using MyApp.Services;
using MyApp.WebUI.Data.Abstract;
using MyApp.WebUI.Entity;

namespace MyApp.WebUI.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        ICutomerRepository repository;
        GeneralServices services;
        ICompanyRepository companyRepository;
        private UserManager<ApplicationUser> userManager;

        public CustomerController(
                    ICutomerRepository _repository,
                    ICompanyRepository _companyRepository,
                    UserManager<ApplicationUser> _userManager)
        {
            services = new GeneralServices(this, _userManager, _companyRepository);
            repository = _repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task< IActionResult> List()
        {
            int companyId =await services.GetCurrentCompanyId();
            return View(repository.GetAll(companyId));
        }

        public IActionResult AddOrUpdate(int? id)
        {
            if (id == 0 || id == null)//add
                return View(new Customer());
            else//Details
                return View(repository.GetById((int)id));
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Customer entity)
        {
            if (ModelState.IsValid)
            {
                //fill data save give message return list
                entity.CompanyId = 3;
                entity.CompanyId = await services.GetCurrentCompanyId();
                repository.Save(entity);
                TempData["message"] = $"{entity.Name} Saved! ";
                return RedirectToAction("List");
            }
            //else stay 
            return View(entity);

        }

        public IActionResult Delete(int id)
        {
            //confirm 
            return View(repository.GetById(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //delete message return list
            repository.Delete(id);
            TempData["message"] = $"{id} Deleted! ";
            return RedirectToAction("List");
        }
    }
}