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


        [Display(Name = "তারিখ তৈরি করুন")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "তৈরিকারী")]
        public int? CreatedBy { get; set; }

        [Display(Name = "তৈরিকারী")]
        public string CreatedByName { get; set; }
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "হালনাগাদকারী")]
        public string LastUpdatedByName { get; set; }
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }
    }
}