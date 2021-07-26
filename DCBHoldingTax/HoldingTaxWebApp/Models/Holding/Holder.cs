using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Holding
{
    public class Holder
    {
        [Display(Name = "হোল্ডার আইডি")]
        public int HolderId { get; set; }

        [Display(Name = "হোল্ডার নাম")]
        public string HolderName { get; set; }

        [Display(Name = "এলাকা")]
        public int AreaId { get; set; }

        [Display(Name = "এলাকা")]
        public string AreaName { get; set; }

        [Display(Name = "প্লট আইডি")]
        public int PlotId { get; set; }

        [Display(Name = "প্লটের আইডি নম্বর")]
        public string PlotIdNumber { get; set; }

        [Display(Name = "প্লট নম্বর")]
        public string PlotNo { get; set; }

        [Display(Name = "এনআইডি")]
        public string NID { get; set; }

        [Display(Name = "লিঙ্গ")]
        public int? Gender { get; set; }

        [Display(Name = "জেন্ডার প্রকার")]
        public string GenderType { get; set; }

        [Display(Name = "বৈবাহিক অবস্থা")]
        public int? MaritialStatus { get; set; }

        [Display(Name = "বৈবাহিক অবস্থা প্রকার")]
        public string MaritialStatusType { get; set; }

        [Display(Name = "বাবার নাম")]
        public string Father { get; set; }

        [Display(Name = "মায়ের নাম")]
        public string Mother { get; set; }

        [Display(Name = "স্বামী বা স্ত্রী নাম")]
        public string Spouse { get; set; }

        [Display(Name = "ল্যান্ডলাইন নাম্বার")]
        public string Contact1 { get; set; }

        [Display(Name = "মোবাইল নাম্বার")]
        public string Contact2 { get; set; }

        [Display(Name = "ইমেল ")]
        public string Email { get; set; }

        [Display(Name = "বর্তমান ঠিকানা")]
        public string PresentAdd { get; set; }

        [Display(Name = "স্থায়ী ঠিকানা")]
        public string PermanentAdd { get; set; }

        [Display(Name = "যোগাযোগের ঠিকানা")]
        public string ContactAdd { get; set; }

        [Display(Name = "উৎসের নাম")]
        public int OwnershipSourceId { get; set; }

        [Display(Name = "উৎসের নাম")]
        public string SourceName { get; set; }

        [Display(Name = "মালিকের প্রকার")]
        public int? OwnerType { get; set; }

        [Display(Name = "মালিকের প্রকার")]
        public string OwnerTypeName { get; set; }

        [Display(Name = "ভবনের ধরণ")]
        public int BuildingTypeId { get; set; }

        [Display(Name = "ভবনের ধরণ")]
        public string BuildingTypeName { get; set; }

        [Display(Name = "জমির পরিমাণ")]
        public decimal? AmountOfLand { get; set; }

        [Display(Name = "জমির পরিমাণ")]
        public string StrAmountOfLand { get; set; }

        [Display(Name = "টোটাল ফ্লোর")]
        public int? TotalFloor { get; set; }

        [Display(Name = "টোটাল ফ্লোর")]
        public string StrTotalFloor { get; set; }

        [Display(Name = "প্রতিটি ফ্লোর এরিয়া")]
        public decimal? EachFloorArea { get; set; }

        [Display(Name = "প্রতিটি ফ্লোর এরিয়া")]
        public string StrEachFloorArea { get; set; }


        [Display(Name = "মোট ফ্ল্যাট")]
        public int? TotalFlat { get; set; }

        [Display(Name = "মোট ফ্ল্যাট")]
        public string StrTotalFlat { get; set; }

        [Display(Name = "হোল্ডার ফ্ল্যাট নম্বর")]
        public int? HoldersFlatNumber { get; set; }

        [Display(Name = "হোল্ডার ফ্ল্যাট নম্বর")]
        public string StrHoldersFlatNumber { get; set; }

        [Display(Name = "পূর্ববর্তী বকেয়া কর")]
        public decimal? PreviousDueTax { get; set; }

        [Display(Name = "পূর্ববর্তী বকেয়া কর")]
        public string StrPreviousDueTax { get; set; }

        [Display(Name = "চিত্র অবস্থান")]
        public string ImageLocation { get; set; }

        [Display(Name = "নথি 1")]
        public string Document1 { get; set; }

        [Display(Name = "নথি 2")]
        public string Document2 { get; set; }
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

        [Display(Name = "তারিখ তৈরি করুন")]
        public string StringCreateDate { get; set; }

        [Display(Name = "আপডেটের তারিখ")]
        public string StringLastUpdated { get; set; }

        [Display(Name = "রোড নং")]
        public string RoadNo { get; set; }

        [Display(Name = "রোড নং ")]
        public string RoadName { get; set; }


        [Display(Name = "বরাদ্দপত্র")]
        public string AllocationLetterNo { get; set; }
        [Display(Name = "নামজারি চিঠি নং")]
        public string NamjariLetterNo { get; set; }
        [Display(Name = "বরাদ্দের তারিখ")]
        public DateTime? AllocationDate { get; set; }

        [Display(Name = "বরাদ্দের তারিখ")]
        public string StringAllocationDate { get; set; }
        [Display(Name = "নামজারি তারিখ")]
        public DateTime? NamjariDate { get; set; }

        [Display(Name = "নামজারি তারিখ")]
        public string StringNamjariDate { get; set; }
        [Display(Name = "রেকর্ড সংশোধনের তারিখ")]
        public DateTime? RecordCorrectionDate { get; set; }

        [Display(Name = "রেকর্ড সংশোধনের তারিখ")]
        public string StringRecordCorrectionDate { get; set; }
        [Display(Name = "এলাকা প্লটফ্ল্যাট ডেটা")]
        public string AreaPlotFlatData { get; set; }



        [Display(Name = "ব্যবহারকারীর নাম দ্বারা নির্মিত")]
        public string CreatedByUsername { get; set; }

        [Display(Name = "হালনাগাদকরণ করেছেন")]
        public string UpdatedByUsername { get; set; }
    }
}