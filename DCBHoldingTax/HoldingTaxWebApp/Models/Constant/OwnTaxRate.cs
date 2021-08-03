using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Constant
{
    public class OwnTaxRate
    {
        public int? OwnTaxRateId { get; set; }

        [Display(Name = "মালিকানার ধরন")]
        public int Mill_Civil { get; set; }

        [Display(Name = "মালিকানার ধরন")]
        public string TypeName { get; set; }

        [Display(Name = "ফ্লোরের আয়তন (বর্গফুট)")]
        public decimal AreaSF { get; set; }

        [Display(Name = "ফ্লোরের আয়তন (বর্গফুট)")]
        public string StrAreaSF { get; set; }

        [Display(Name = "প্রতিমাসের ভাড়া")]
        public decimal Amount { get; set; }

        [Display(Name = "প্রতিমাসের ভাড়া")]
        public string StrAmount { get; set; }


        [Display(Name = "প্রতি বর্গফুটের কর")]
        public decimal? PerSqfTax { get; set; }

        [Display(Name = "প্রতি বর্গফুটের কর")]
        public string StrPerSqfRent { get; set; }

        [Display(Name = "নোট ")]
        public string Remarks { get; set; }


        [Display(Name = "সংযুক্তির তারিখ")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "সংযুক্তির তারিখ")]
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