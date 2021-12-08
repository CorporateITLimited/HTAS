using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Holding
{
    public class HolderFlat
    {
        [Display(Name = "হোল্ডার ফ্ল্যাট আইডি")]
        public int HolderFlatId { get; set; }

        [Display(Name = "হোল্ডার আইডি")]
        public int? HolderId { get; set; }

        public int? MainHolderId { get; set; }

        [Display(Name = "ফ্লোর নং")]
        public int? FlorNo { get; set; }

        [Display(Name = "ফ্ল্যাট নং")]
        public string FlatNo { get; set; }

        [Display(Name = "ফ্লাট অঞ্চল")]
        public decimal? FlatArea { get; set; }

        [Display(Name = "ফ্লাট অঞ্চল")]
        public string StrFlatArea { get; set; }

        [Display(Name = "নিজস্ব বা ভাড়া")]
        public int? OwnOrRent { get; set; }

        
         [Display(Name = "নিজস্ব বা ভাড়া প্রকার")]
        public string OwnOrRentType { get; set; }

        [Display(Name = "স্ব - মালিকানাধীন ? ")]
        public bool? IsSelfOwned { get; set; }

        [Display(Name = "মালিকের নাম")]
        public string OwnerName { get; set; }

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
        [Display(Name = " সক্রিয়তা")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "মাসিক ভাড়া")]
        public decimal? MonthlyRent { get; set; }

        //[Display(Name = "মাসিক ভাড়া")]
        public string StrMonthlyRent { get; set; }
       // [Display(Name = "স্ব-স্ব")]
        public int? SelfOwn { get; set; }
        //[Display(Name = "স্ব-নিজস্ব প্রকার")]
        public string SelfOwnType { get; set; }
       // [Display(Name = "ফ্লোর টাইপের নাম")]
        public string FloorTypeName { get; set; }

        public bool? IsCheckedByHolder { get; set; }
    }
}