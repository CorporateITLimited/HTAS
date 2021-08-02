using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Constant
{
    public class ConstantValue
    {
        public int constantValueId { get; set; }
        public int RentMonth { get; set; }
        public string RentMonthRef { get; set; }
        public decimal RentTaxRate { get; set; }
        public string RentTaxRateRef { get; set; }
        public decimal Surcharge { get; set; }
        public string SurchargeRef { get; set; }
        public decimal WrongInfoCharge { get; set; }
        public string WrongInfoChargeRef { get; set; }
        public decimal Rebate { get; set; }
        public string RebateRef { get; set; }
        [Display(Name = "সংযুক্তির তারিখ ")]
        public DateTime? CreateDate { get; set; }

        //[Display(Name = "সংযুক্তির তারিখ ")]
        //public string StrCreateDate { get; set; }

        [Display(Name = "সংযুক্তকারী")]
        public int? CreatedBy { get; set; }

        [Display(Name = "সংযুক্তকারী")]
        public string CreatedByUserName { get; set; }

        [Display(Name = "শেষ হালনাগাদের তারিখ")]
        public DateTime? LastUpdated { get; set; }

        //[Display(Name = "শেষ হালনাগাদের তারিখ")]
        //public string StrLastUpdated { get; set; }

        [Display(Name = "হালনাগাদকারী")]
        public string UpdatedByUserName { get; set; }

        [Display(Name = "হালনাগাদকারী")]
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "সক্রিয়তা")]
        public bool? IsActive { get; set; }

        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }

        public decimal? DueCharge { get; set; }
        public string DueChargeRef { get; set; }
    }
}