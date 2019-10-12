using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Data.Abstract;
using MyApp.Services;
using MyApp.WebUI.Data.Abstract;
using MyApp.WebUI.Entity;
using MyApp.WebUI.Models;

namespace MyApp.Controllers
{
    public class TestController : Controller
    {
        IProductRepository productRepository;
        ICompanyRepository companyRepository;
        GeneralServices services;
        private UserManager<ApplicationUser> userManager;
        InvoiceHeader invoiceHeader = new InvoiceHeader()
        { InvoiceDetails = new List<InvoiceDetail>()  };
        

        public TestController(IProductRepository _productRepository,
                    ICompanyRepository _companyRepository,
                    UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
            productRepository = _productRepository;
            companyRepository = _companyRepository;
            //services = new GeneralServices(this);
            services = new GeneralServices(this, _userManager, _companyRepository);

        }

        public  IActionResult Index()
        {//Ef core Incluce, thenInclude
            //http://www.entityframeworktutorial.net/efcore/querying-in-ef-core.aspx
            var resultList = productRepository.GetAll()
                .Where(i => i.CompanyId ==5006)
                .Include(i => i.Company)
                .ThenInclude(g=>g.ApplicationUsers)
                .FirstOrDefault();

            Product product =(Product) resultList;
            return View("Index",resultList);
        }
        public IActionResult SelectList()
        {
            ViewBag.Products = new SelectList(productRepository.GetAll(), "ProductId", "Name");

            return View();
        }
        public IActionResult RemoveLine()
        {
            ViewBag.Products = new SelectList(productRepository.GetAll(), "ProductId", "Name");
            return View();
        }

        public IActionResult SelectedItemeChange()
        {
            ViewBag.Products = new SelectList(productRepository.GetAll(), "ProductId", "Name");
            return View();
        }

        public JsonResult getProductINfo(int id)
        {
            Product product = productRepository.GetById(id);
            return new JsonResult(product);
        }

        public IActionResult AddLine()
        {
            ViewBag.Products = new SelectList(productRepository.GetAll(), "ProductId", "Name");
            invoiceHeader.InvoiceDetails.Add(new InvoiceDetail());
            return View(invoiceHeader);
        }

        public IActionResult RemoveLine2()//works well
        {

            ViewBag.Products = new SelectList(productRepository.GetAll(), "ProductId", "Name");
            return View();
        }
       
        public IActionResult ChangeSelectedProduct()//works welll
        {
            
            ViewBag.Products = new SelectList(productRepository.GetAll(), "ProductId", "Name");
            return View();
        }
        public IActionResult CalculateTotal()
        {
            ViewBag.Products = new SelectList(productRepository.GetAll(), "ProductId", "Name");

            return View();
        }

        public IActionResult RemoveLine3()
        {
            ViewBag.Products = new SelectList(productRepository.GetAll(), "ProductId", "Name");

            return View();
        }
        public IActionResult SearchableDrooDownList()
        {

            return View();
        }

    }
}