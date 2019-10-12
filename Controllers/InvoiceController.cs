using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Data.Abstract;
using MyApp.Services;
using MyApp.WebUI.Data.Abstract;
using MyApp.WebUI.Entity;
using MyApp.WebUI.Models;

namespace MyApp.WebUI.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        IInvoiceRepository repository;
        ICutomerRepository customerRepository;
        IProductRepository productRepository;
        InvoiceHeader head = new InvoiceHeader();
        GeneralServices services;
        ICompanyRepository companyRepository;
        private UserManager<ApplicationUser> userManager;
        int companyId;

        public InvoiceController(IInvoiceRepository _repository,
            ICutomerRepository _cutomerRepository,
            ICompanyRepository _companyRepository,
            UserManager<ApplicationUser> _userManager,
            IProductRepository _productRepository)
        {
            repository = _repository;
            productRepository = _productRepository;
            customerRepository = _cutomerRepository;
            companyRepository = _companyRepository;
            userManager = _userManager;
            services = new GeneralServices(this, userManager, companyRepository);
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(int? id)
        {
            if (id == null || id < 0)
            {
                head = new InvoiceHeader();
                head.InvoiceDetails = new List<InvoiceDetail>();
                head.InvoiceDetails.AddRange(new InvoiceDetail[3]);
            }
            else head = repository.GetById((int)id);
            companyId = await services.GetCurrentCompanyId();

            ViewBag.Customers = new SelectList(customerRepository.GetAll(companyId), "CustomerId", "Name");
            ViewBag.Products = new SelectList(productRepository.GetAll(companyId), "ProductId", "Name");

            return View(head);
        }
        [HttpPost]
        public async Task<IActionResult> Create(InvoiceHeader head)
        {
          //  if (ModelState.IsValid )

            {/* *fil data header and details
                *save
                *displaay msg
                 */
                head.InvoiceHeaderId = 0;
                head.Date = DateTime.Now;
                companyId = await services.GetCurrentCompanyId();
                head.CompanyId = companyId;
                head.ApplicationUserId = services.GetCurrentUserId();
                
                //remove details with 0 quntity
                for (int i = 0; i < head.InvoiceDetails.Count; i++)
                {
                    var item = head.InvoiceDetails[i];
                    if (item.Quantity == 0 || item.ProductId == 0 || item.ProductId == null)
                        head.InvoiceDetails.Remove(item);
                }
           
                //tutarHisapla
                head.Total = CalculateGereralAmount(head);
                repository.Save(head, head.InvoiceDetails);
                TempData["message"] = $"Saved!";
                return RedirectToAction("List");
            }
            companyId = await services.GetCurrentCompanyId();
            ViewBag.Customers = new SelectList(customerRepository.GetAll(companyId), "CustomerId", "Name");
            ViewBag.Products = new SelectList(productRepository.GetAll(companyId), "ProductId", "Name");
            return View(head);
        }

          public async Task<IActionResult> List()
        {
            int companyId = await services.GetCurrentCompanyId();
            var list = repository.GetAll(companyId).Include(i => i.Customer)
                .Include(i => i.ApplicationUser).ToList();
            return View(list.OrderByDescending(i=>i.Date));
        }
        public IActionResult Details(int id)
        {
            /*  var inv = repository.GetAll()
                  .Where(i => i.InvoiceHeaderId == id)
                  .Include(i => i.InvoiceDetails)
                  .Select(i => new InvoiceModel
                  {
                      Header = i,
                      Details = i.InvoiceDetails
                  }).FirstOrDefault();*/
            ViewBag.Customers = new SelectList(customerRepository.GetAll(), "CustomerId", "Name");
            ViewBag.Products = new SelectList(productRepository.GetAll(), "ProductId", "Name");
            return View(repository.GetById(id));
        }

        //toplam turatı hisapla
        decimal CalculateGereralAmount(InvoiceHeader header)
        {
            decimal amount = new decimal(0);
            decimal temp = new decimal(0);
            foreach (var item in header.InvoiceDetails)
            {
                temp = 0;
                temp = (productRepository.GetById(item.ProductId)).MonopolyPrice;
                amount += temp * ((decimal)item.Quantity);
            }
            return amount;
        }


        public JsonResult getProductInfo(int id)
        {
            Product product = productRepository.GetById(id);
            return new JsonResult(product);
        }
    }
   
}
