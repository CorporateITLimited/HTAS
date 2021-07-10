using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Plots
{
    public class UnauthPortion
    {
        public int UnauthComId { get; set; }
        [Display(Name = "প্লট আইডি নম্বর")]
        public int PlotId { get; set; }
        [Display(Name = "আইডি নম্বর")]
        public string PlotIdNumber { get; set; }
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
        [Display(Name = "যুক্তকরণের তারিখ")]
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "যুক্ত করেছেন")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "শেষ হালনাগাদের তারিখ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "শেষ হালনাগাদ করেছেন")]
        public string UpdatedByUserName { get; set; }
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়তা")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}