using MyApp.WebUI.Data.Abstract;
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.WebUI.Data.Concrete.EfCore
{
    public class EfInvoiceRepository : IInvoiceRepository
    {
        ApplicationDbContext context;
        public EfInvoiceRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public IQueryable<InvoiceHeader> GetAll()
        {
            return context.InvoiceHeaders;
        }
        public IQueryable<InvoiceHeader> GetAll(int companyId)
        {
            
            return context.InvoiceHeaders.Where(i => i.CompanyId == companyId);
        }

        public InvoiceHeader GetById(int id)
        {
            var header = context.InvoiceHeaders.FirstOrDefault(b => b.InvoiceHeaderId == id);
            IQueryable<InvoiceDetail> details = context.InvoiceDetails.Where(b => b.InvoiceHeaderId == id);
            foreach (var item in details)
            {
                if (!header.InvoiceDetails.Contains(item))
                    header.InvoiceDetails.Add(item);
            }
            return header;
        }

        public void Save(InvoiceHeader invoiceHeader, List<InvoiceDetail> invoiceDetails)
        {

            context.InvoiceHeaders.Add(invoiceHeader);
            /*      foreach (var item in invoiceDetails)
                  {  //Hiç gerek yok
                      item.InvoiceHeaderId = invoiceHeader.InvoiceHeaderId;
                      item.InvoiceHeaderId = 4;

                      context.InvoiceDetails.Add(item);
                  }*/
            context.SaveChanges();

        }
    }
}
