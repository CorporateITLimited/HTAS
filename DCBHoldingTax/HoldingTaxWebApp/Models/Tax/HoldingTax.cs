using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Tax
{
    public class HoldingTax
    {
        [Display(Name = "হোল্ডিং ট্যাক্স আইডি")]
        public int HoldingTaxId { get; set; }
        [Display(Name = "হোল্ডার আইডি")]
        public int HolderId { get; set; }
        [Display(Name = "আর্থিক বছরের আইডি")]
        public int FinancialYearId { get; set; }
        [Display(Name = "আর্থিক বছর")]
        public string FinancialYear { get; set; }

        [Display(Name = "মোট ভাড়া")]
        public decimal? TotalRent { get; set; }
        [Display(Name = "ভাড়া থেকে ট্যাক্স")]
        public decimal? TaxFromRent { get; set; }
        [Display(Name = "নিজস্ব সম্পত্তি থেকে ট্যাক্স")]
        public decimal? TaxFromOwnProperty { get; set; }
        [Display(Name = "মোট হোল্ডিং ট্যাক্স")]
        public decimal? TotalHoldingTax { get; set; }
        [Display(Name = "সারচার্জ")]
        public decimal? Surcharge { get; set; }
        [Display(Name = "ছাড়")]
        public decimal? Rebate { get; set; }
        [Display(Name = "ভুল তথ্য চার্জ")]
        public decimal? WrongInfoCharge { get; set; }
        [Display(Name = "তারিখ")]
        public DateTime? Date { get; set; }
        [Display(Name = "শেষ তারিখ")]
        public DateTime? EndDate { get; set; }
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
        [Display(Name = "চূড়ান্ত")]
        public bool? isFinalized { get; set; }
        [Display(Name = "পরিশোধিত টাকার পরিমান")]
        public decimal? PaidAmount { get; set; }
        [Display(Name = "নেট কর প্রদেয় পরিমাণ")]
        public decimal? NetTaxPayableAmount { get; set; }

        [Display(Name = "হোল্ডারের নাম")]
        public string HolderName { get; set; }

        [Display(Name = "প্লট নম্বর")]
        public string PlotIdNumber { get; set; }

        [Display(Name = "প্লট নম্বর")]
        public string PlotNo { get; set; }
        public int? AreaId { get; set; }

        [Display(Name = "এলাকার নাম ")]
        public string AreaName { get; set; }

        [Display(Name = "হোল্ডারের প্লট/ফ্ল্যাট/বাড়ী এর তথ্য")]
        public string AreaPlotFlatData { get; set; }


    }
}