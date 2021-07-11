using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Dbo
{
    public class BuildingType
    {
        [Display(Name = "বিল্ডিং টাইপ আইডি")]
        public int BuildingTypeId { get; set; }
        [Display(Name = "বিল্ডিং টাইপ নাম")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "তৈরিকারী")]
        public int CreatedBy { get; set; }
        [Display(Name = "সর্বশেষ সংষ্করণ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "সর্বশেষ সংষ্করণ করেছেন")]
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }

    }
}