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
        [Display(Name = "Plot Id Number")]
        public int? PlotId { get; set; }
        [Display(Name = "Plot Id Number")]
        public string PlotIdNumber { get; set; }

        public DateTime? MEO_NCCDate { get; set; }
        [Display(Name = "MEO_NCC Date")]
        public string StringMEO_NCCDate { get; set; }

        public string Reference { get; set; }

        public DateTime ApprovalDate { get; set; }
        [Display(Name = "Approval Date")]
        public string StringApprovalDate { get; set; }
        [Display(Name = "Approval Letter No")]
        public string ApprovalLetterNo { get; set; }
        [Display(Name = "Flor Number")]
        public int? FlorNumber { get; set; }
        [Display(Name = "Ground Flor Area")]
        public decimal? GroundFlorArea { get; set; }
        [Display(Name = "Other Flor Area")]
        public decimal? OtherFlorArea { get; set; }
        [Display(Name = "Approval No")]
        public int ApprovalNo { get; set; }
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "Created By ")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "Updated Date ")]
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedByUserName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}