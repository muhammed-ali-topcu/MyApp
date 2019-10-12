using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.Abstract;
using MyApp.Models;
using MyApp.Services;
using MyApp.WebUI.Entity;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyApp.Controllers
{

    public class ReportController : Controller
    {
        ICompanyRepository companyRepository;
        GeneralServices services;
        private UserManager<ApplicationUser> userManager;
        private int companyId;

        public ReportController(UserManager<ApplicationUser> _userManager,
            ICompanyRepository _companyRepository)
        {
            companyRepository = _companyRepository;
            userManager = _userManager;
            services = new GeneralServices(this, userManager, companyRepository);

        }


        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Developer")]
        public IActionResult Designer()
        {
            return View();
        }

        //views on design report
        public IActionResult Viewer()
        {
            return View();
        }

        public IActionResult MainViewer()
        {

            return View(new XtraReport());
        }

        public IActionResult Invoices()
        {
            return View();
        }

        public async Task<IActionResult> Today()
        {
            //bugun satilan urunler count(procuctId) order by count desc
            companyId = await services.GetCurrentCompanyId();
            var assembly = typeof(MyApp.ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("MyApp.Reports.Invoices.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.RequestParameters = true;
            report.Parameters["prmCompanyId"].Value = companyId;
            report.Parameters["prmCompanyId"].Visible = false;
            report.Parameters["prmFromDate"].Value = DateTime.Today.AddDays(-1);
            report.Parameters["prmFromDate"].Visible = false;

            report.Parameters["prmToDate"].Value = DateTime.Today.AddDays(+1);
            report.Parameters["prmToDate"].Visible = false;
            report.CreateDocument();
            return View("MainViewer", report);
        }

        //fatura kriterleri

            
      
        [HttpPost]
        public  async Task<IActionResult> CreateInvoiceReport( ReportCriteria criteria)
        {
            companyId = await services.GetCurrentCompanyId();
            var assembly = typeof(MyApp.ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("MyApp.Reports.Invoices.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.RequestParameters = true;
            report.Parameters["prmCompanyId"].Value = companyId;
            report.Parameters["prmCompanyId"].Visible = false;
            report.Parameters["prmFromDate"].Value = criteria.FromDate;
            report.Parameters["prmToDate"].Value = criteria.ToDate.AddDays(1) ;
            report.CreateDocument();
            return View("MainViewer", report);

        }
                     
        public async Task<IActionResult> Products()
        {
            companyId = await services.GetCurrentCompanyId();

            var assembly = typeof(MyApp.ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("MyApp.Reports.Products.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.RequestParameters = true;
            report.Parameters["prmCompanyId"].Value = companyId;
            report.Parameters["prmCompanyId"].Visible = false;
            report.CreateDocument();
            return View("MainViewer", report);

        }

        public async Task<IActionResult> BestSales()
        {
            companyId = await services.GetCurrentCompanyId();

            var assembly = typeof(MyApp.ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("MyApp.Reports.BestSales.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.RequestParameters = true;
            report.Parameters["prmCompanyId"].Value = companyId;
            report.Parameters["prmCompanyId"].Visible = false;
            report.CreateDocument();
            return View("MainViewer", report);
        }
         //send parameters to report 
        public IActionResult test()
        {
            var assembly = typeof(MyApp.ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("MyApp.Reports.XtraReport1.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.RequestParameters = true;
            report.Parameters["parameter1"].Value = 654456;
            report.Parameters["parameter1"].Visible = false;
            report.CreateDocument();
            return View(report);
        }


    }
}