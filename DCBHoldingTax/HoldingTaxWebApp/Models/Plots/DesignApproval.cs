using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Plots
{
    public class DesignApproval
    {
        public int DesignAppId { get; set; }
        [Display(Name = "প্লট আইডি নম্বর")]
        public int PlotId { get; set; }
        [Display(Name = "আইডি নম্বর")]
        public string PlotIdNumber { get; set; }

        public DateTime? MEO_NCCDate { get; set; }
        [Display(Name = "এম ই ও এনওসি তারিখ")]
        public string StringMEO_NCCDate { get; set; }
        [Display(Name = "বোর্ড মিটিং রেফারেন্স")]
        public string Reference { get; set; }

        public DateTime ApprovalDate { get; set; }
        [Display(Name = "প্ল্যান আনুমোদন তারিখ")]
        public string StringApprovalDate { get; set; }
        [Display(Name = "অনুমোদন পত্র নং")]
        public string ApprovalLetterNo { get; set; }
        [Display(Name = "তলার সংখ্যা")]
        public int? FlorNumber { get; set; }
        [Display(Name = "গ্রাউন্ড ফ্লোরের আয়তন")]
        public decimal? GroundFlorArea { get; set; }
        [Display(Name = "অন্যান্য ফ্লোরের আয়তন")]
        public decimal? OtherFlorArea { get; set; }
        [Display(Name = "অনুমোদন নং")]
        public int ApprovalNo { get; set; }
        [Display(Name = "অন্তর্ভুক্তির তারিখ ")]
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "যুক্ত করেছেন")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "সর্বশেষ হালনাগাদ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "হালনাগাদকারী")]
        public string UpdatedByUserName { get; set; }
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়তা")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}