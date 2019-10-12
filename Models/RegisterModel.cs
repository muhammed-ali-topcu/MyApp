
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.WebUI.Models
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Passowrd { get; set; }

        public Company  Company { get; set; }

        public ApplicationUser ApplicationUser { get; set; }


    }
}
