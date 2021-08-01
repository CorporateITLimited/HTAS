using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.DBO
{
    public class clsFinancialYear
    {
        [Display(Name = "ফিনান্সিয়াল ইয়ার আইডি")]
        public int FinancialYearId { get; set; }
        [Display(Name = "ফিনান্সিয়াল ইয়ার")]
        public string FinancialYear { get; set; }
        [Display(Name = "শুরুর তারিখ")]
        public DateTime? StartingDate { get; set; }
        [Display(Name = "শেষ তারিখ")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "তারিখ তৈরি করুন")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "তৈরিকারী")]
        public int? CreatedBy { get; set; }
        [Display(Name = "হালনাগাদকরণ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "হালনাগাদকরণ করেছেন")]
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }


        // for reporting only
        public int? AreaId { get; set; }
    }
}