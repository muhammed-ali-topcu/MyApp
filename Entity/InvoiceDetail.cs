
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.WebUI.Entity
{
    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int Quantity { get; set; }
        
        //forigin Key
        public int InvoiceHeaderId { get; set; }
        public InvoiceHeader InvoiceHeader { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
