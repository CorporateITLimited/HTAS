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

        [Display(Name = "এলাকা")]
        public int? AreaId { get; set; }

        [Display(Name = "ভবনের ধরণ ")]
        public string BuildingTypeName { get; set; }

        [Display(Name = "ভবনের ধরণ ")]
        public int? BuildingTypeId { get; set; }

        [Display(Name = "প্রতি বর্গফুটের ভাড়া")]
        public decimal PerSqfRent { get; set; }

        [Display(Name = "প্রতি বর্গফুটের ভাড়া")]
        public string StrPerSqfRent { get; set; }

        [Display(Name = "নোট ")]
        public string Remarks { get; set; }


        [Display(Name = "সংযুক্তির তারিখ ")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "সংযুক্তির তারিখ ")]
        public string StrCreateDate { get; set; }

        [Display(Name = "সংযুক্তকারী")]
        public int? CreatedBy { get; set; }

        [Display(Name = "সংযুক্তকারী")]
        public string CreatedByUsername { get; set; }

        [Display(Name = "শেষ হালনাগাদের তারিখ")]
        public DateTime? LastUpdated { get; set; }

        [Display(Name = "শেষ হালনাগাদের তারিখ")]
        public string StrLastUpdated { get; set; }

        [Display(Name = "হালনাগাদকারী")]
        public string UpdatedByUsername { get; set; }

        [Display(Name = "হালনাগাদকারী")]
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "সক্রিয়তা")]
        public bool? IsActive { get; set; }

        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }
    }
}