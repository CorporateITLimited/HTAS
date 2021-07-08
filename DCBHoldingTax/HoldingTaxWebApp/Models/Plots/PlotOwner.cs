using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Plots
{
    public class PlotOwner
    {
        public int PlotOwnerId { get; set; }
        [Display(Name = "Plot Id Number")]
        public int? PlotId { get; set; }

        [Display(Name = "Plot Id Number")]
        public string PlotIdNumber { get; set; }
        [Display(Name = "Plot Owner Name")]
        public string PlotOwnerName { get; set; }
        [Display(Name = "Is Alive?")]
        public bool? IsAlive { get; set; }
        [Display(Name = "Official Status")]
        public int? OfficialStatusId { get; set; }
        [Display(Name = "Official Status")]
        public string OffStatusName { get; set; }
        [Display(Name = "Present Address")]
        public string PresentAdd { get; set; }
        [Display(Name = "Permanent Address")]
        public string PermanentAdd { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? LeaveDate { get; set; }
        [Display(Name = "Leave Date")]
        public string StringLeaveDate { get; set; }
        [Display(Name = "Leave Authority")]
        public string LeaveAuthority { get; set; }
        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }
        [Display(Name = "Leave Period")]
        public int? LeavePeriod { get; set; }
        [Display(Name = "Leave Quota")]
        public int? LeaveQuotaId { get; set; }
        [Display(Name = "Leave Quota")]
        public string LeaveQuotaName { get; set; }
        [Display(Name = "HandOver Office")]
        public string HandOverOffice { get; set; }
        [Display(Name = "HandOver Letter No")]

        public string HandOverLetterNo { get; set; }
        [Display(Name = "Land Develop Change")]
        public decimal? LandDevelopChange { get; set; }
        [Display(Name = "Constraction Status")]
        public int? ConsStatusId { get; set; }
        [Display(Name = "Constraction Status")]
        public string ConsStatusName { get; set; }
        [Display(Name = "Document 1")]
        public string Doc1 { get; set; }
        [Display(Name = "Document 2")]
        public string Doc2 { get; set; }
        [Display(Name = "Document 3")]
        public string Doc3 { get; set; }
        [Display(Name = "Document 4")]
        public string Doc4 { get; set; }
        [Display(Name = "Document 5")]
        public string Doc5 { get; set; }
        [Display(Name = "Document 6")]
        public string Doc6 { get; set; }

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