using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Tax
{
    public class RentedFlatTax
    {
        [Display(Name = "ভাড়া করা ফ্ল্যাট ট্যাক্স আইডি")]
        public int RentedFlatTaxId { get; set; }
        [Display(Name = "হোল্ডিং ট্যাক্স আইডি")]
        public int HoldingTaxId { get; set; }
        [Display(Name = "বিল্ডিং টাইপ আইডি")]
        public int BuildingTypeId { get; set; }
        [Display(Name = "ফ্লোর নং")]
        public int FloorNo { get; set; }
        [Display(Name = "ফ্ল্যাট নং")]
        public string FlatNo { get; set; }
        [Display(Name = "অঞ্চল")]
        public decimal? Area { get; set; }
        [Display(Name = "প্রতি এসএফ ভাড়া")]
        public decimal? PerSFRent { get; set; }
        [Display(Name = "মাসিক ভাড়া")]
        public decimal? MonthlyRent { get; set; }
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