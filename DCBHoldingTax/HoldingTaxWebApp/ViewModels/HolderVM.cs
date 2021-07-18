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
         [Display(Name = "হোল্ডার আইডি")]
        public int HolderId { get; set; }

        [Display(Name = "হোল্ডার নাম")]
        public string HolderName { get; set; }

        [Display(Name = "অঞ্চল এর আইডি")]
        public int AreaId { get; set; }

        [Display(Name = "অঞ্চল এর নাম")]
        public string AreaName { get; set; }


        [Display(Name = "প্লট আইডি")]
        
        public int PlotId { get; set; }

        [Display(Name = "প্লটের আইডি নম্বর")]
        public string PlotIdNumber { get; set; }

        [Display(Name = "প্লট নম্বর")]
        public string PlotNo { get; set; }

        [Display(Name = "জাতীয় পরিচয়পত্র")]
        public string NID { get; set; }

        [Display(Name = "লিঙ্গ")]
        public int? Gender { get; set; }

        [Display(Name = "লিঙ্গ এর প্রকার")]
        public string GenderType { get; set; }

        [Display(Name = "বৈবাহিক অবস্থা")]
        public int? MaritialStatus { get; set; }

        [Display(Name = "বৈবাহিক স্থিতির ধরণ")]
        public string MaritialStatusType { get; set; }

        [Display(Name = "বাবার নাম")]
        public string Father { get; set; }

        [Display(Name = "মায়ের নাম")]
        public string Mother { get; set; }

        [Display(Name = "স্বামী বা স্ত্রী ")]
        public string Spouse { get; set; }

        [Display(Name = "ল্যান্ডলাইন নাম্বার")]
        public string Contact1 { get; set; }

        [Display(Name = "মোবাইল নাম্বার")]
        public string Contact2 { get; set; }

        [Display(Name = "ইমেল")]
        public string Email { get; set; }

        [Display(Name = "বর্তমান ঠিকানা")]
        public string PresentAdd { get; set; }

        [Display(Name = "স্থায়ী ঠিকানা")]
        public string PermanentAdd { get; set; }

        [Display(Name = "যোগাযোগের ঠিকানা")]
        public string ContactAdd { get; set; }

        [Display(Name = "উৎসের আইডি")]
        public int OwnershipSourceId { get; set; }

        [Display(Name = "উৎসের নাম")]
        public string SourceName { get; set; }

        [Display(Name = "মালিকের প্রকার")]
        public int? OwnerType { get; set; }

        [Display(Name = "মালিকের প্রকারের নাম")]
        public string OwnerTypeName { get; set; }

        [Display(Name = "বিল্ডিং টাইপ আইডি")]
        public int BuildingTypeId { get; set; }

        [Display(Name = "বিল্ডিং টাইপ নাম")]
        public string BuildingTypeName { get; set; }

        [Display(Name = "জমির পরিমাণ")]
        public decimal? AmountOfLand { get; set; }

        [Display(Name = "জমির পরিমাণ")]
        public string StrAmountOfLand { get; set; }

        [Display(Name = "টোটাল ফ্লোর")]
        public int? TotalFloor { get; set; }

        [Display(Name = "টোটাল ফ্লোর")]
        public string StrTotalFloor { get; set; }

        [Display(Name = "প্রতিটি তল এলাকা")]
        public decimal? EachFloorArea { get; set; }

        [Display(Name = "প্রতিটি তল এলাকা")]
        public string StrEachFloorArea { get; set; }


        [Display(Name = "মোট ফ্ল্যাট")]
        public int? TotalFlat { get; set; }

        [Display(Name = "মোট ফ্ল্যাট")]
        public string StrTotalFlat { get; set; }

        [Display(Name = " হোল্ডার ফ্ল্যাট নম্বর")]
        public int? HoldersFlatNumber { get; set; }

        [Display(Name = "হোল্ডার ফ্ল্যাট নম্বর")]
        public string StrHoldersFlatNumber { get; set; }

        [Display(Name = "পূর্ববর্তী বকেয়া কর")]
        public decimal? PreviousDueTax { get; set; }

        [Display(Name = "পূর্ববর্তী বকেয়া কর")]
        public string StrPreviousDueTax { get; set; }

        [Display(Name = "চিত্র অবস্থান")]
        public string ImageLocation { get; set; }

        [Display(Name = "নথি এক")]
        public string Document1 { get; set; }

        [Display(Name = "দলিল")]
        public string Document2 { get; set; }

        [Display(Name = "তারিখ তৈরি করুন")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "দ্বারা সৃষ্টি ")]
        public int? CreatedBy { get; set; }

        [Display(Name = "ব্যবহারকারীর নাম দ্বারা নির্মিত")]
        public string CreatedByUsername { get; set; }

        [Display(Name = "Updated Date ")]
        public DateTime? LastUpdated { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedByUsername { get; set; }

        [Display(Name = "Status")]
        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        [Display(Name = "Create Date")]
        public string StringCreateDate { get; set; }

        [Display(Name = "Updated Date ")]
        public string StringLastUpdated { get; set; }

        [Display(Name = "Road No ")]
        public string RoadNo { get; set; }

        [Display(Name = "Road No ")]
        public string RoadName { get; set; }



        [Display(Name = "AllocationLetterNo")]
        public string AllocationLetterNo { get; set; }
        [Display(Name = "NamjariLetterNo")]
        public string NamjariLetterNo { get; set; }
        [Display(Name = "Allocation Date")]
        public DateTime? AllocationDate { get; set; }

        [Display(Name = "Allocation Date")]
        public string StringAllocationDate { get; set; }
        [Display(Name = "Namjari Date")]
        public DateTime? NamjariDate { get; set; }

        [Display(Name = "Namjari Date")]
        public string StringNamjariDate { get; set; }
        [Display(Name = "RecordCorrection Date")]
        public DateTime? RecordCorrectionDate { get; set; }

        [Display(Name = "RecordCorrection Date")]
        public string StringRecordCorrectionDate { get; set; }
        public string AreaPlotFlatData { get; set; }


        public string PlotOwnerName { get; set; }
        public DateTime? LeaseDate { get; set; }
        public string StringLeaseDate { get; set; }
        public int? LeasePeriod { get; set; }
        public DateTime? LeaseExpiryDate { get; set; }
        public string StringLeaseExpiryDate { get; set; }
        public DateTime? FirstApprovalDate { get; set; }
        public string StringFirstApprovalDate { get; set; }
        public string FirstApprovalLetterNo { get; set; }
        public DateTime? LastApprovalDate { get; set; }
        public string StringLastApprovalDate { get; set; }
        public string LastApprovalLetterNo { get; set; }



        public List<HolderFlat> HolderFlatList { get; set; }

        public HolderVM()
        {
            HolderFlatList = new List<HolderFlat>();
        }

    }
}