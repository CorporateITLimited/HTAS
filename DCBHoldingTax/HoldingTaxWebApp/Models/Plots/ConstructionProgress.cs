using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Plots
{
    public class ConstructionProgress
    {
        public int ConsProgressId { get; set; }
        [Display(Name = "Plot Id Number")]
        public int? PlotId { get; set; }
        [Display(Name = "Plot Id Number")]
        public string PlotIdNumber { get; set; }
        [Display(Name = "Owner Declaration")]
        public string OwnerDeclaration { get; set; }
        [Display(Name = "Real Builder")]
        public string RealBuilder { get; set; }
        [Display(Name = "Developer Deposit")]
        public decimal? DevelopDeposit { get; set; }
        [Display(Name = "Floor Number")]
        public int? FloorNumber { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string StringCompletionDate { get; set; }
        public DateTime? GroundFCDate { get; set; }
        public string StringGroundFCDate { get; set; }

        public DateTime? FirstFCDate { get; set; }
        [Display(Name = "First Floor completed Date")]
        public string StringFirstFCDate { get; set; }

        public DateTime? SccFCDate { get; set; }
        [Display(Name = "Second Floor completed Date")]
        public string StringSccFCDate { get; set; }

        public DateTime? ThirdFCDate { get; set; }
        [Display(Name = "Third Floor completed Date")]
        public string StringThirdFCDate { get; set; }

        public DateTime? ForthFCDate { get; set; }
        [Display(Name = "Forth Floor completed Date")]
        public string StringForthFCDate { get; set; }

        public DateTime? FivthFCDate { get; set; }
        [Display(Name = "Fifth Floor completed Date")]

        public string StringFivthFCDate { get; set; }

        public DateTime? SixFCDate { get; set; }
        [Display(Name = "Sixth Floor completed Date")]
        public string StringSixFCDate { get; set; }

        public DateTime? OtherFCDate { get; set; }
        [Display(Name = "Owner Portion")]
        public string OwnerPortion { get; set; }
        [Display(Name = "Developer Portion")]
        public string DeveloperPortion { get; set; }
        [Display(Name = "Buyer Portion")]
        public string BuyerPortion { get; set; }
        [Display(Name = "Submitted Portion")]
        public string SubmittedPortion { get; set; }
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