using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace MyApp.WebUI.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public long UserNo { get; set; }
        public string Image { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
