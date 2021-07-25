using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.DBO
{
    public class Issue
    {
        [Display(Name = "ইস্যু আইডি")]
        public int IssueId { get; set; }
        [Display(Name = "হোল্ডার আইডি")]
        public int HolderId { get; set; }
        [Display(Name = "স্ট্যাটাস টাইপ আইডি")]
        public int StatusTypeId { get; set; }
        //[Display(Name = "তৈরিকারী")]
        public string Subject { get; set; }
        //[Display(Name = "তৈরিকারী")]
        //public string Details { get; set; }
        //public string Doc1 { get; set; }
        //public string Doc2 { get; set; }
      
        public DateTime? SolvedDate { get; set; }
        //[Display(Name = "তৈরিকারী")]
        public string StringSolvedDate { get; set; }
        [Display(Name = "মন্তব্য")]
        public string Remarks { get; set; }
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

        ////out of table
        public string HolderName { get; set; }
        public string StatusName { get; set; }


    }
}