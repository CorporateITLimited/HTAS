using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Tax
{
    public class OwnFlatTax
    {
        [Display(Name = "নিজস্ব ফ্ল্যাট ট্যাক্স আইডি")]
        public int OwnFlatTaxId { get; set; }
        [Display(Name = "হোল্ডিং ট্যাক্স আইডি")]
        public int HoldingTaxId { get; set; }
        [Display(Name = "বাড়ি_ফ্ল্যাট নং")]
        public string House_FlatNo { get; set; }
        [Display(Name = "ফ্ল্যাট নং")]
        public string FlatNo { get; set; }
        [Display(Name = "অঞ্চল ")]
        public decimal? Area { get; set; }
        [Display(Name = "প্রতি এসএফ কর")]
        public decimal? PerSFTax { get; set; }
        [Display(Name = "তারিখ তৈরি করুন")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "তৈরিকারী")]
        public int? CreatedBy { get; set; }
        [Display(Name = "সর্বশেষ সংষ্করণ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "সর্বশেষ সংষ্করণ করেছেন")]
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }

    }
}