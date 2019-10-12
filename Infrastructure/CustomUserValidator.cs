using  MyApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.WebUI.Models;
using MyApp.WebUI.Entity;

namespace  MyApp.Infrastructure
{
    public class CustomUserValidator : IUserValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {

            if (user.Email.ToLower().EndsWith("@gmail.com") || user.Email.ToLower().EndsWith("@hotmail.com"))
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError()
                {
                    Code = "EmailDomainError",
                    Description="Sadece @gmail.com ve @hotmail.com a izin veriliyor."
                }));
            }

        }
    }
}
