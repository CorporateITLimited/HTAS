using HoldingTaxWebApp.Models.Plots;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels.Plots
{
    public class OldPlotOwnerCombineVM
    {

        public int OldPlotOwnerId { get; set; }
        public int PlotOwnerId { get; set; }


        [Display(Name = "আইডি নম্বর")]
        public string PlotIdNumber { get; set; }
        [Display(Name = "প্লট মালিকের নাম")]
        public string OldPlotOwnerName { get; set; }
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



        public List<OldOthetPlotOwner> OldOthetPlotOwner { get; set; }


        public OldPlotOwnerCombineVM()
        {
            OldOthetPlotOwner = new List<OldOthetPlotOwner>();
        }


    }
}