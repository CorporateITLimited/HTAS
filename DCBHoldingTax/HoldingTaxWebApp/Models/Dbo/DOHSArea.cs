using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.DBO
{
    public class DOHSArea
    {
        [Display(Name = "এলাকার আইডি")]
        public int AreaId { get; set; }
        [Display(Name = "এলাকার নাম")]
        public string AreaName { get; set; }
        [Display(Name = "মোট এলাকা")]
        public decimal? TotalArea { get; set; }
        [Display(Name = "বর্তমান প্লট সংখ্যা")]
        public int CurrentPlotNumber { get; set; }
        [Display(Name = "বর্তমান ফ্ল্যাট সংখ্যা ")]
        public int CurrentFlatNumber { get; set; }

        [Display(Name = "তারিখ তৈরি করুন")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "তারিখ তৈরি করুন")]
        public string StrCreateDate { get; set; }
        [Display(Name = "তৈরিকারী")]
        public int? CreatedBy { get; set; }
        [Display(Name = "হালনাগাদকরণ")]
        public DateTime? LastUpdated { get; set; }

        [Display(Name = "হালনাগাদকরণ")]
        public string StrLastUpdated { get; set; }

        [Display(Name = "হালনাগাদকরণ করেছেন")]
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "তৈরিকারী")]
        public string CreatedByUsername { get; set; }

        [Display(Name = "হালনাগাদকরণ করেছেন")]
        public string UpdatedByUsername { get; set; }







    }
}