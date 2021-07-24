using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Plots
{
    public class ConstructionProgress
    {
        public int ConsProgressId { get; set; }
        [Display(Name = "প্লট আইডি নম্বর")]
        public int PlotId { get; set; }
        [Display(Name = "আইডি নম্বর")]
        public string PlotIdNumber { get; set; }
        [Display(Name = "প্লট মালিকের ঘোষণা")]
        public string OwnerDeclaration { get; set; }
        [Display(Name = "প্রকৃত নির্মানকারী")]
        public string RealBuilder { get; set; }
        [Display(Name = "ডেভোলপারের জমার পরিমান")]
        public decimal? DevelopDeposit { get; set; }
        [Display(Name = "ভবনের তলার সংখ্যা")]
        public int? FloorNumber { get; set; }
        public DateTime? CompletionDate { get; set; }
        [Display(Name = "কাজ শেষ হওয়ার তারিখ")]

        public string StringCompletionDate { get; set; }
        public DateTime? GroundFCDate { get; set; }
        [Display(Name = "নিচ তলা সমাপ্তের তারিখ")]

        public string StringGroundFCDate { get; set; }

        public DateTime? FirstFCDate { get; set; }
        [Display(Name = "২য় তলা সমাপ্তের তারিখ")]
        public string StringFirstFCDate { get; set; }

        public DateTime? SccFCDate { get; set; }
        [Display(Name = "৩য় তলা সমাপ্তের তারিখ")]
        public string StringSccFCDate { get; set; }

        public DateTime? ThirdFCDate { get; set; }
        [Display(Name = "৪র্থ তলা সমাপ্তের তারিখ")]
        public string StringThirdFCDate { get; set; }

        public DateTime? ForthFCDate { get; set; }
        [Display(Name = "৫ম তলা সমাপ্তের তারিখ")]
        public string StringForthFCDate { get; set; }

        public DateTime? FivthFCDate { get; set; }
        [Display(Name = "৬ষ্ঠ তলা সমাপ্তের তারিখ")]

        public string StringFivthFCDate { get; set; }

        public DateTime? SixFCDate { get; set; }
        [Display(Name = "৭ম তলা সমাপ্তের তারিখ")]
        public string StringSixFCDate { get; set; }

        public DateTime? OtherFCDate { get; set; }

        [Display(Name = "অন্যান্য তলা সমাপ্তের তারিখ")]
        public string StringOtherFCDate { get; set; }

        [Display(Name = "মালিকের অংশ")]
        public string OwnerPortion { get; set; }
        [Display(Name = "ডেভোলপারের অংশ")]
        public string DeveloperPortion { get; set; }
        [Display(Name = "ক্রেতার অংশ")]
        public string BuyerPortion { get; set; }
        [Display(Name = "হস্তান্তরিত অংশ")]
        public string SubmittedPortion { get; set; }
        [Display(Name = "অন্তর্ভুক্তির তারিখ ")]
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "অন্তর্ভুক্তকারী")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "সর্বশেষ হালনাগাদ ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "হালনাগাদকারী  ")]
        public string UpdatedByUserName { get; set; }
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়তা ")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}