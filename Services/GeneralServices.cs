using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.Abstract;
using MyApp.Data.Concrete.EfCore;
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class GeneralServices
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ICompanyRepository companyRepository;
        private Controller controller;
        public GeneralServices(Controller _controller)
        {
           controller=_controller;
          //  companyRepository = new EfCompanyRepository()
        }

        public GeneralServices(Controller _controller,
            UserManager<ApplicationUser> _userManager,
            ICompanyRepository _companyRepository)
        {
            controller = _controller;
            userManager = _userManager;
            companyRepository = _companyRepository;
        }

        public void ShowMessage(string subject, string containt)
        {
            controller.TempData["message"] = subject + " " + containt + " ";
        }


        /*----------------------------LoggedIn User--------------------------*/
        public string GetCurrentUserId(Controller controller)
        {
            string userId = controller.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }

        public async Task<ApplicationUser> GetCurrentUser(Controller controller)
        {
            string userId = GetCurrentUserId(controller);
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            return user;
        }
                     
        public string GetCurrentUserId( )
        {
            string userId = controller.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }

        public async Task<ApplicationUser> GetCurrentUser( )
        {
            string userId = GetCurrentUserId();
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            return user;
        }


        /*-------------------------------Currernt Company----------------*/
        public async Task<int> GetCurrentCompanyId(Controller controller)
        {
            ApplicationUser user = await GetCurrentUser(controller);
            return user.CompanyId;
        }
        public async Task<Company> GetCurrentCompany(Controller controller)
        {
            int companyId = await GetCurrentCompanyId(controller);
            return companyRepository.GetById(companyId);
        }
        
        public async Task<int> GetCurrentCompanyId( )
        {
            ApplicationUser user = await GetCurrentUser();
            return user.CompanyId;
        }
        public async Task<Company> GetCurrentCompany( )
        {
            int companyId = await GetCurrentCompanyId();
            return companyRepository.GetById(companyId);
        }

    }
}
