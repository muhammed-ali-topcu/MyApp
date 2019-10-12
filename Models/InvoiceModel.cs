using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.WebUI.Models
{
    public static class InvoiceModel//change to InvoiceVM
    {
        public static InvoiceHeader head { get; set; }
        public static List<InvoiceDetail> invoiceDetails { get; set; }

        static InvoiceModel()
        {
            invoiceDetails = new List<InvoiceDetail>();
            invoiceDetails.Add(new InvoiceDetail());

        }
    }
}
