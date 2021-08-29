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
        [Display(Name = "কর যোগ্য মাসের সংখ্যা")]
        public int RentMonth { get; set; }
        [Display(Name = "কর যোগ্য মাসের সংখ্যা রেফারেন্স")]
        public string RentMonthRef { get; set; }
        [Display(Name = "গৃহকরের পরিমান (শতাংশ)")]
        public decimal RentTaxRate { get; set; }
        [Display(Name = "গৃহকরের পরিমান রেফারেন্স")]
        public string RentTaxRateRef { get; set; }
        [Display(Name = "সারচার্জ (শতাংশ)")]
        public decimal Surcharge { get; set; }
        [Display(Name = "সারচার্জ রেফারেন্স")]
        public string SurchargeRef { get; set; }
        [Display(Name = "ভুল তথ্য প্রদানের চার্জ (শতাংশ)")]
        public decimal WrongInfoCharge { get; set; }
        [Display(Name = "ভুল তথ্য প্রদানের চার্জ রেফারেন্স")]
        public string WrongInfoChargeRef { get; set; }
        [Display(Name = "রিবেট (শতাংশ)")]
        public decimal Rebate { get; set; }
        [Display(Name = "রিবেট রেফারেন্স")]
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

        [Display(Name = "বকেয়া অতিরিক্ত চার্জ (শতাংশ)")]

        public decimal? DueCharge { get; set; }

        [Display(Name = "বকেয়া অতিরিক্ত চার্জ রেফারেন্স")]
        public string DueChargeRef { get; set; }


        [Display(Name = "ডিওএইচএস ব্যাতিত অন্যান্য এলাকার মালিকদের জন্য প্রযোজ্য ছাড়ের হার(শতাংশ)")]

        public decimal? OwnFlatDiscount { get; set; }

        [Display(Name = "ডিওএইচএস ব্যাতিত অন্যান্য এলাকার মালিকদের জন্য প্রযোজ্য ছাড়ের হার")]
        public string OwnFlatDiscountRef { get; set; }
    }
}