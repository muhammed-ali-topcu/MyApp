
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace MyApp.WebUI.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int ShortCode { get; set; }
        public long Parcode { get; set; }
        public int Quntity { get; set; }
        public string Image { get; set; }
        public decimal PurchasePrice { get; set; }//Aliş Fiyati
        public decimal MonopolyPrice { get; set; }//Tekel Fiyati
        public decimal WholesalePrice { get; set; } //Toptan Fiyati
        public string Notes { get; set; }

        public bool IsDeleted { get; set; }


        [BindNever]
        public DateTime FirstSaveDate { get; set; }
        public DateTime LastSaveDate { get; set; }

        //forigin Key
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}
