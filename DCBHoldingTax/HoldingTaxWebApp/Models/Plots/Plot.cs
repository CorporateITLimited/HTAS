using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Plots
{
    public class Plot
    {
        public int PlotId { get; set; }
        [Display(Name = "আইডি নম্বর")]
        public string PlotIdNumber { get; set; }
        [Display(Name = "অবস্থান এলাকা")]
        public int AreaId { get; set; }

        [Display(Name = "অবস্থান এলাকা")]
        public string AreaName { get; set; }

        [Display(Name = "রাস্তা নং")]
        public string RoadNo { get; set; }
        [Display(Name = "প্লট নং")]
        public string PlotNo { get; set; }

        [Display(Name = "এলাকা")]
        public decimal? TotalArea { get; set; }

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