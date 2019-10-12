
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.WebUI.Entity
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Decimal Cost { get; set; }
        public string gecici { get; set; }

        //forigin Key
        public Company Company { get; set; }
       public int CompanyId { get; set; }
    }

    public enum ExpenseCatagory
    {
        Bakim,

    }
}
