using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels
{
    public class QueryCommon
    {
        [Display(Name = "আর্থিক বছর")]
        public int? FinancialYearId { get; set; }

        [Display(Name = "আর্থিক বছর")]
        public int? FinancialYearId_Two { get; set; }

        [Display(Name = "বিজ্ঞপ্তির ধরণ")]
        public int? NoticeTypeId { get; set; }

        [Display(Name = "এলাকার নাম")]
        public int? AreaId { get; set; }

        [Display(Name = "প্লট নম্বর")]
        public int? PlotId { get; set; }

    }
}