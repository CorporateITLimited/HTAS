using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Tax
{
    public class OwnTaxRate
    {
        
        [Display(Name = "নিজস্ব করের হারের আইডি")]
        public int OwnTaxRateId { get; set; }
        [Display(Name ="সামরিক/বেসামরিক")]
        public int? Mill_Civil { get; set; }
    
        [Display(Name = "অঞ্চল এসএফ")]
        public decimal? AreaSF { get; set; }
        [Display(Name = "পরিমাণ")]
        public decimal? Amount { get; set; }
        [Display(Name = "মন্তব্য")]
        public string Remarks { get; set; }
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
    }
}