using HoldingTaxWebApp.Models.Plots;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels.Plots
{
    public class PlotOwnerCombineVM
    {

        //Plot Owner Portion Start
        #region Plot Owner Portion Start
        public int PlotOwnerId { get; set; }
        [Display(Name = "আইডি নম্বর")]
        public int? PlotId { get; set; }

        [Display(Name = "আইডি নম্বর")]
        public string PlotIdNumber { get; set; }
        [Display(Name = "প্লট মালিকের নাম")]
        public string PlotOwnerName { get; set; }
        [Display(Name = "প্লট মালিকের বর্তমান অবস্থা")]
        public bool? IsAlive { get; set; }
        [Display(Name = "প্লট মালিকের অফিসিয়াল অবস্থা")]
        public int? OfficialStatusId { get; set; }
        [Display(Name = "প্লট মালিকের অফিসিয়াল অবস্থা")]
        public string OffStatusName { get; set; }
        [Display(Name = "বর্তমান ঠিকানা")]
        public string PresentAdd { get; set; }
        [Display(Name = "স্থায়ী ঠিকানা")]
        public string PermanentAdd { get; set; }
        [Display(Name = "ফোন")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ইমেল")]
        public string Email { get; set; }
        [Display(Name = "প্লটের আয়তন")]

        public decimal? TotalArea { get; set; }
        public DateTime? LeaveDate { get; set; }
        [Display(Name = "ইজারার তারিখ")]
        public string StringLeaveDate { get; set; }
        [Display(Name = "ইজারা কর্তৃপক্ষ")]
        public string LeaseAuthority { get; set; }
        [Display(Name = "লিজের ধরণ")]
        public string LeaseType { get; set; }
        [Display(Name = "লিজের সময়কাল")]
        public int? LeasePeriod { get; set; }
        [Display(Name = "লিজের অবশিষ্ট সময়")]
        public int? LeaveExPeriod { get; set; }

        [Display(Name = "লিজ কোটা")]
        public int? LeaseQuotaId { get; set; }
        [Display(Name = "লিজ কোটা")]
        public string LeaseQuotaName { get; set; }

        [Display(Name = "হস্তান্তরকারী দপ্তরের নাম")]
        public string HandOverOffice { get; set; }
        [Display(Name = "হস্তান্তর পত্র নং")]
        public string HandOverLetterNo { get; set; }
        [Display(Name = "ভূমি উন্নয়ন চার্জ")]
        public decimal? LandDevelopChange { get; set; }
        [Display(Name = "জমির বর্তমান অবস্থা")]
        public int? ConsStatusId { get; set; }
        [Display(Name = "জমির বর্তমান অবস্থা")]
        public string ConsStatusName { get; set; }
        [Display(Name = "নথি নং ১")]
        public string Doc1 { get; set; }
        [Display(Name = "নথি নং ২")]
        public string Doc2 { get; set; }
        [Display(Name = "নথি নং ৩")]
        public string Doc3 { get; set; }
        [Display(Name = "নথি নং ৪")]
        public string Doc4 { get; set; }
        [Display(Name = "নথি নং ৫")]
        public string Doc5 { get; set; }
        [Display(Name = "নথি নং ৬")]
        public string Doc6 { get; set; }

        #endregion
        //Plot Owner Portion End

        //Design Approval Start

        #region Design Approval Start
        public List<DesignApproval> DesignApproval { get; set; }
        #endregion
        //Design Approval End


        //Construction Progress Start

        #region Construction Progress Start

        public int ConsProgressId { get; set; }
        //[Display(Name = "প্লট আইডি নম্বর")]
        //public int? PlotId { get; set; }
        //[Display(Name = "আইডি নম্বর")]
        //public string PlotIdNumber { get; set; }
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
        #endregion
        //Construction Progress End


        //UnauthPortion Start

        #region Unauth Portion  Start

        public int UnauthComId { get; set; }
        //[Display(Name = "প্লট আইডি নম্বর")]
        //public int? PlotId { get; set; }
        //[Display(Name = "আইডি নম্বর")]
        //public string PlotIdNumber { get; set; }
        [Display(Name = "মোট অননুমোদিত নির্মান এলাকা")]
        public decimal? TotalUnauthArea { get; set; }
        [Display(Name = "জরিমানা ব্যাতিত অননুমোদিত নির্মান")]

        public decimal? FineFreeArea { get; set; }
        [Display(Name = "জরিমানাকৃত অননুমোদিত")]

        public decimal? WithFineUnauth { get; set; }
        [Display(Name = "অপসারিত অননুমোদিত নির্মান")]

        public decimal? RemovedUnauthArea { get; set; }
        [Display(Name = "অননুমোদিত নির্মান কিন্ত আপসারন হয়নি")]

        public decimal? NonRemovedUnauth { get; set; }
        [Display(Name = "জরিমানার হার")]

        public decimal? FineRate { get; set; }
        [Display(Name = "টাকার পরিমান")]

        public decimal? FineAmount { get; set; }
        #endregion
        //UnauthPortion End


        //Common Portion
        [Display(Name = "যুক্তকরণের তারিখ ")]
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "যুক্ত করেছেন ")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "শেষ হালনাগাদের তারিখ ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "শেষ হালনাগাদ করেছেন  ")]
        public string UpdatedByUserName { get; set; }
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়তা ")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }





        //Othet Plot Owner  Start

        #region  Othet Plot Owner Start
        public List<OthetPlotOwner> OthetPlotOwner { get; set; }
        #endregion
        //Othet Plot Owner End

        public PlotOwnerCombineVM()
        {
            DesignApproval = new List<DesignApproval>();
            OthetPlotOwner = new List<OthetPlotOwner>();
        }


    }
}