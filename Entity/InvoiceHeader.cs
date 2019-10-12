
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.WebUI.Entity
{
    public class InvoiceHeader
    {
        public int InvoiceHeaderId { get; set; }
        public decimal  Total { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal UnpaidAmount { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
        //invoice no uniq(No , CompanyId)
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public long SerialNo { get; set; }
        public bool IsCanceled { get; set; }

        [BindNever]
        public DateTime Date { get; set; }

        //forigin Key

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        //        public int BranchId { get; set; }
        //       public Branch Branch { get; set; }
        public int  CompanyId { get; set; }
        public Company Company { get; set; }


        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
