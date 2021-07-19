using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class Role
    {
        [Display(Name = "ভূমিকা আইডি")]
        public int RoleId { get; set; }
        [Display(Name = "নামভূমিকা")]
        public string RoleName { get; set; }

        [Display(Name = "বিশদ ভূমিকা ")]
        public string RoleDetails { get; set; }
        [Display(Name = "তারিখ তৈরি করুন")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "তৈরিকারী")]
        public int? CreatedBy { get; set; }
        [Display(Name = "সর্বশেষ সংষ্করণ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "সর্বশেষ সংষ্করণ করেছেন")]
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }

        public string CreatedByUserName { get; set; }
        public string UpdatedByUserName { get; set; }

    }
}