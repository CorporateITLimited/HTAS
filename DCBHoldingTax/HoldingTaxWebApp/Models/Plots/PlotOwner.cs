using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Plots
{
    public class PlotOwner
    {
        public int PlotOwnerId { get; set; }
        [Display(Name = "আইডি নম্বর")]
        public int PlotId { get; set; }

        [Display(Name = "আইডি নম্বর")]
        public string PlotIdNumber { get; set; }
        [Display(Name = "প্লট মালিকের নাম")]
        public string PlotOwnerName { get; set; }
        [Display(Name = "প্লট মালিকের বর্তমান অবস্থা")]
        public bool? IsAlive { get; set; }
        [Display(Name = "প্লট মালিকের অফিসিয়াল অবস্থা")]
        public int OfficialStatusId { get; set; }
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
        public int LeaseQuotaId { get; set; }
        [Display(Name = "লিজ কোটা")]
        public string LeaseQuotaName { get; set; }





        [Display(Name = "হস্তান্তরকারী দপ্তরের নাম")]
        public string HandOverOffice { get; set; }
        [Display(Name = "হস্তান্তর পত্র নং")]
        public string HandOverLetterNo { get; set; }
        [Display(Name = "ভূমি উন্নয়ন চার্জ")]
        public decimal? LandDevelopChange { get; set; }
        [Display(Name = "জমির বর্তমান অবস্থা")]
        public int ConsStatusId { get; set; }
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
    }
}