using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Plots
{
    public class OthetPlotOwner
    {
        public int OthetPlotOwnerId { get; set; }
        [Display(Name = "অন্যান্য মালিকের নাম")]
        public string OthetOwneeName { get; set; }
        public int? PlotOwnerId { get; set; }
        [Display(Name = "প্লট মালিকের নাম")]
        public string PlotOwnerName { get; set; }
        [Display(Name = "ঠিকানা")]
        public string Address { get; set; }
        [Display(Name = "মন্তব্য")]

        public string Remarks { get; set; }
        [Display(Name = "যুক্তকরণের তারিখ")]
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