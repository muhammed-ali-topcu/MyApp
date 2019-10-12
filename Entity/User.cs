
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.WebUI.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string UserNo { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }

        //forigin Key
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        /*Role
        1_Admin
        2 normel user
        */
    }
}
