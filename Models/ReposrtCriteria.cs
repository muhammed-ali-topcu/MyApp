using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class ReportCriteriaVM
    {
        // rapor kriterleri
        public DateTime FromDate { get; set; }//ikl tarih
        public DateTime ToDate { get; set; } //son tarih

        public long  FromSerialNo { get; set; }//ilk sıra no
        public long  ToSerialNo { get; set; }//son sıra no

        public string UserId { get; set; }
        public ApplicationUser  User { get; set; }

        public DateTime ReportDate { get; set; }


    }
}
