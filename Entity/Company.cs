using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.WebUI.Entity
{
public    class Company
    {
        public int CompanyId { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string WebSite { get; set; }
        public string Code { get; set; }
        public IQueryable<User>  Users { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
        public string gecici { get; set; }

    }
}
