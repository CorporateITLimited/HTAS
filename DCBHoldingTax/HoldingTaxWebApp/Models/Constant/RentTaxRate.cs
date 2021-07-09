using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Constant
{
    public class RentTaxRate
    {
        public int? RentTaxRateId { get; set; }
        [Display(Name = "এলাকা")]
        public string AreaName { get; set; }
        public int? AreaId { get; set; }
        [Display(Name = "ভবনের ধরণ ")]
        public string BuildingTypeName { get; set; }
        public int? BuildingTypeId { get; set; }
        [Display(Name = "প্রতি বর্গফুটের ভাড়া")]
        public decimal PerSqfRent { get; set; }
        [Display(Name = "নোট ")]
        public string Remarks { get; set; }



        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }

        [Display(Name = "তৈরিকারী")]
        public string CreatedByName { get; set; }
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "পরিবর্তনকারী")]
        public string LastUpdatedByName { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}