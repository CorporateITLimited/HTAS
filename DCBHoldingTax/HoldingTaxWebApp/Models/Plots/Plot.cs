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

        [Display(Name = "প্লট নং")]
        public string PlotIdNumber { get; set; }

        [Display(Name = "এলাকার নাম")]
        public int AreaId { get; set; }

        [Display(Name = "এলাকার নাম")]
        public string AreaName { get; set; }

        [Display(Name = "রাস্তা নং")]
        public string RoadNo { get; set; }

        [Display(Name = "প্লট নং")]
        public string PlotNo { get; set; }

        [Display(Name = "এলাকার আয়তন")]
        public decimal? TotalArea { get; set; }

        [Display(Name = "অন্তর্ভুক্তির তারিখ ")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "অন্তর্ভুক্তির তারিখ ")]
        public string StrCreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "অন্তর্ভুক্তকারী")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "সর্বশেষ হালনাগাদ ")]
        public DateTime? LastUpdated { get; set; }

        [Display(Name = "সর্বশেষ হালনাগাদ ")]
        public string StrLastUpdated { get; set; }
        [Display(Name = "হালনাগাদকারী  ")]
        public string UpdatedByUsername { get; set; }
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "সক্রিয়তা ")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        [Display(Name = "রাস্তার নাম")]
        public string RoadName { get; set; }
    }
}