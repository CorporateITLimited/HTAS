using HoldingTaxWebApp.Models.Holding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels
{
    public class HolderVM
    {
        public int HolderId { get; set; }

        [Display(Name = "প্লট/ফ্ল্যাট/বাড়ী মালিকের নাম")]
        public string HolderName { get; set; }

        [Display(Name = "এলাকার নাম")]
        public int AreaId { get; set; }

        [Display(Name = "এলাকার নাম")]
        public string AreaName { get; set; }


        [Display(Name = "প্লট/বাড়ী নম্বর")]
        
        public int PlotId { get; set; }

        [Display(Name = "প্লট/বাড়ী নম্বর")]
        public string PlotIdNumber { get; set; }

        [Display(Name = "প্লট/বাড়ী নম্বর")]
        public string PlotNo { get; set; }

        [Display(Name = "এনআইডি নম্বর")]
        public string NID { get; set; }

        [Display(Name = "লিঙ্গ")]
        public int? Gender { get; set; }

        [Display(Name = "লিঙ্গ")]
        public string GenderType { get; set; }

        [Display(Name = "বৈবাহিক অবস্থা")]
        public int? MaritialStatus { get; set; }

        [Display(Name = "বৈবাহিক অবস্থা")]
        public string MaritialStatusType { get; set; }

        [Display(Name = "পিতার নাম")]
        public string Father { get; set; }

        [Display(Name = "মাতার নাম")]
        public string Mother { get; set; }

        [Display(Name = "স্বামী/স্ত্রীর নাম")]
        public string Spouse { get; set; }

        [Display(Name = "ল্যান্ডলাইন নাম্বার")]
        public string Contact1 { get; set; }

        [Display(Name = "মোবাইল নাম্বার")]
        public string Contact2 { get; set; }

        [Display(Name = "ই-মেইল")]
        public string Email { get; set; }

        [Display(Name = "বর্তমান ঠিকানা")]
        public string PresentAdd { get; set; }

        [Display(Name = "স্থায়ী ঠিকানা")]
        public string PermanentAdd { get; set; }

        [Display(Name = "পত্র যোগাযোগের ঠিকানা")]
        public string ContactAdd { get; set; }

        [Display(Name = "মালিকানার সূত্র")]
        public int OwnershipSourceId { get; set; }

        [Display(Name = "মালিকানার সূত্র")]
        public string SourceName { get; set; }

        [Display(Name = "মালিকানার ধরন")]
        public int? OwnerType { get; set; }

        [Display(Name = "মালিকানার ধরন")]
        public string OwnerTypeName { get; set; }

        [Display(Name = "ভবনের ধরন")]
        public int BuildingTypeId { get; set; }

        [Display(Name = "ভবনের ধরন")]
        public string BuildingTypeName { get; set; }

        [Display(Name = "জমির পরিমাণ")]
        public decimal? AmountOfLand { get; set; }

        [Display(Name = "জমির পরিমাণ")]
        public string StrAmountOfLand { get; set; }

        [Display(Name = "মোট তলার সংখ্যা")]
        public int? TotalFloor { get; set; }

        [Display(Name = "মোট তলার সংখ্যা")]
        public string StrTotalFloor { get; set; }

        [Display(Name = "প্রতিতলার আয়তন")]
        public decimal? EachFloorArea { get; set; }

        [Display(Name = "প্রতিতলার আয়তন")]
        public string StrEachFloorArea { get; set; }


        [Display(Name = "মোট ফ্ল্যাট সংখ্যা")]
        public int? TotalFlat { get; set; }

        [Display(Name = "মোট ফ্ল্যাট সংখ্যা")]
        public string StrTotalFlat { get; set; }

        [Display(Name = "নিজ মালিকানাধীন ফ্ল্যাট সংখ্যা")]
        public int? HoldersFlatNumber { get; set; }

        [Display(Name = "নিজ মালিকানাধীন ফ্ল্যাট সংখ্যা")]
        public string StrHoldersFlatNumber { get; set; }

        [Display(Name = "আগের অর্থ বছর পর্যন্ত বকেয়া গৃহকর")]
        public decimal? PreviousDueTax { get; set; }

        [Display(Name = "আগের অর্থ বছর পর্যন্ত বকেয়া গৃহকর")]
        public string StrPreviousDueTax { get; set; }

        [Display(Name = "পাসপোর্ট সাইজের ছবি")]
        public string ImageLocation { get; set; }

        [Display(Name = "মুক্তিযোদ্ধার প্রমাণাদি")]
        public string Document1 { get; set; }

        [Display(Name = "মুক্তিযোদ্ধার প্রমাণাদি")]
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

        [Display(Name = "হালনাগাদকরণ")]
        public string StringLastUpdated { get; set; }



        [Display(Name = "ব্যবহারকারীর নাম দ্বারা নির্মিত")]
        public string CreatedByUsername { get; set; }

        [Display(Name = "হালনাগাদকরণ করেছেন")]
        public string UpdatedByUsername { get; set; }








        [Display(Name = "রোড নম্বর")]
        public string RoadNo { get; set; }

        [Display(Name = "রোডের নাম")]
        public string RoadName { get; set; }



        [Display(Name = "বরাদ্দ পত্র নম্বর")]
        public string AllocationLetterNo { get; set; }

        [Display(Name = "নামজারী আদেশের পত্র নং")]
        public string NamjariLetterNo { get; set; }

        [Display(Name = "তারিখ")]
        public DateTime? AllocationDate { get; set; }

        [Display(Name = "তারিখ")]
        public string StringAllocationDate { get; set; }

        [Display(Name = "তারিখ")]
        public DateTime? NamjariDate { get; set; }

        [Display(Name = "তারিখ")]
        public string StringNamjariDate { get; set; }

        [Display(Name = "রেকর্ড সংশোধনের তারিখ")]
        public DateTime? RecordCorrectionDate { get; set; }

        [Display(Name = "রেকর্ড সংশোধনের তারিখ")]
        public string StringRecordCorrectionDate { get; set; }

        [Display(Name = "এরিয়া প্লট ফ্ল্যাট এর তথ্য")]
        public string AreaPlotFlatData { get; set; }

        [Display(Name = "বরাদ্দ গ্রহীতার নাম")]
        public string PlotOwnerName { get; set; }

        [Display(Name = "ইজারা তারিখ")]
        public DateTime? LeaseDate { get; set; }
        [Display(Name = "ইজারা তারিখ")]
        public string StringLeaseDate { get; set; }

        [Display(Name = "ইজারার মেয়াদ (বছর)")]
        public int? LeasePeriod { get; set; }

        [Display(Name = "ইজারার মেয়াদ (বছর)")]
        public string StrLeasePeriod { get; set; }

        [Display(Name = "মেয়াদোত্তীর্ণের তারিখ")]
        public DateTime? LeaseExpiryDate { get; set; }

        [Display(Name = "মেয়াদোত্তীর্ণের তারিখ")]
        public string StringLeaseExpiryDate { get; set; }

        [Display(Name = "প্রথম নকশা অনুমোদনের তারিখ")]
        public DateTime? FirstApprovalDate { get; set; }

        [Display(Name = "প্রথম নকশা অনুমোদনের তারিখ")]
        public string StringFirstApprovalDate { get; set; }

        [Display(Name = "অনুমোদন নম্বর")]
        public string FirstApprovalLetterNo { get; set; }

        [Display(Name = "সর্বশেষ সংশোধিত নকশা অনুমোদনের তারিখ")]
        public DateTime? LastApprovalDate { get; set; }

        [Display(Name = "সর্বশেষ সংশোধিত নকশা অনুমোদনের তারিখ")]
        public string StringLastApprovalDate { get; set; }

        [Display(Name = "অনুমোদন নম্বর")]
        public string LastApprovalLetterNo { get; set; }

        [Display(Name = "গৃহকরদাতা নিজেই কি প্লটের মালিক?")]
        public bool? IsHolderAnOwner { get; set; }

        [Display(Name = "গৃহকরদাতার আইডি নম্বর")]
        public string HolderNo { get; set; }

        // no need to convert in bangla
        public List<HolderFlat> HolderFlatList { get; set; }
        public List<HolderFlat> HolderFlatListForEdit { get; set; }

        public HolderVM()
        {
            HolderFlatList = new List<HolderFlat>();
        }

    }
}