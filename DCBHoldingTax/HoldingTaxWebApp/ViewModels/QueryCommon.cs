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

        public int? HoldingTaxId { get; set; }
        public bool HoldingTaxIdStatus { get; set; }

        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

    }
}