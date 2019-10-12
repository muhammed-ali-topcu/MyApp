using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.WebUI.Data.Concrete.EfCore
{
    public static class SeedData
    {
        private static ApplicationDbContext context;

        public static void EnsurPupulated(IApplicationBuilder app)
        {
            context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();
            /* if (!context.Companys.Any())
            {
                context.Companies.AddRange(
                    new Company { Name = "TestCompany1" },
                    new Company { Name = "TestCompany2" },
                    new Company { Name = "TestCompany3" }

                    );
                context.SaveChanges();
            }  */
            if (!context.pages.Any())
            {
                context.pages.AddRange(
                    new Page { Name = "Invoices", Controller = "Invoice", Action = "List", Link = "/Invoice/List", Code =1 },
                    new Page { Name="Products", Controller="Product",Action="List", Link="/Product/List",Code=2 },
                    new Page { Name="Customers", Controller= "Customer", Action="List", Link= "/Customer/List", Code=3 },
                    new Page { Name="Users", Controller="Admin",Action="List", Link="/Admin/List",Code=4 },
                    new Page { Name="Roles", Controller="AdminRole",Action="List", Link="/AdminRole/List",Code=5 }
                    );
                    context.SaveChanges();
            }
         

        }

    }
}
