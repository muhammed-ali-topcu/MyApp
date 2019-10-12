
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyApp.WebUI.Entity;

namespace MyApp.WebUI.Data.Abstract
{
    public interface IInvoiceRepository
    {
        IQueryable<InvoiceHeader> GetAll();
        IQueryable<InvoiceHeader> GetAll(int companyId);
        void Save(InvoiceHeader invoiceHeader, List<InvoiceDetail> invoiceDetails);
        InvoiceHeader GetById(int id);

        // invoice iptal
    }
}
