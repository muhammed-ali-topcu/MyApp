using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.Abstract;
using MyApp.Services;
using MyApp.WebUI.Data.Abstract;
using MyApp.WebUI.Entity;

namespace MyApp.WebUI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        IProductRepository repository;
        ICompanyRepository companyRepository;
        GeneralServices services;
        private UserManager<ApplicationUser> userManager;

        public ProductController(IProductRepository _productRepository,
                    ICompanyRepository _companyRepository,
                    UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
            repository = _productRepository;
            companyRepository = _companyRepository;
            //services = new GeneralServices(this);
            services = new GeneralServices(this, _userManager, _companyRepository);
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            int companyId = await services.GetCurrentCompanyId();
            return View(repository.GetAll(companyId));
        }
        [HttpGet]
        [Authorize]
        public IActionResult AddOrUpdate(int? id)
        {
            if (id == 0 || id == null)
                return View(new Product());
            else
                return View(repository.GetById((int)id));  //id might be nulll so (int)id
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Product entity, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    entity.Image = file.FileName;
                }
                //finding user and his company
                //var userid = services.GetCurrentUserId();
                //var user = services.GetCurrentUser();
                //var company = services.GetCurrentCompany();

                entity.CompanyId = 3;
                entity.CompanyId = await services.GetCurrentCompanyId(this);
                repository.Save(entity);
                //message 
                // TempData["message"] = $"{entity.Name} Saved!";
                services.ShowMessage(entity.Name, "Saved");
                return RedirectToAction("List");
            }
            return View(entity);
        }

        public IActionResult Delete(int id)
        {
            return View(repository.GetById(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            TempData["message"] = $"{id} nolu silindi!";
            return RedirectToAction("List");
        }


    }
}