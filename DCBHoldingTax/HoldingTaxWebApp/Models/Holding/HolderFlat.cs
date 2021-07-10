using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Holding
{
    public class HolderFlat
    {
        public int HolderFlatId { get; set; }

        [Display(Name = "Holder Name")]
        public int? HolderId { get; set; }
        [Display(Name = "Holder Name")]
        public string HolderName { get; set; }
        [Display(Name = "Flor No")]
        public int? FlorNo { get; set; }
        [Display(Name = "Flat No")]
        public string FlatNo { get; set; }
        [Display(Name = "Flat Area")]
        public decimal? FlatArea { get; set; }

        [Display(Name = "Own Or Rent")]
        public int? OwnOrRent { get; set; }
        [Display(Name = "Is Self Owned?")]
        public bool? IsSelfOwned { get; set; }
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }
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
        public decimal? MonthlyRent { get; set; }
        public int SelfOwned { get; set; }
    }
}