
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.WebUI.Entity
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }

        //forigin Key
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public List<InvoiceHeader> InvoiceHeaders { get; set; }
    }
}
