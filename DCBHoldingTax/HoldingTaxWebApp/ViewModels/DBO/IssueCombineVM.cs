using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels.DBO
{
    public class IssueCombineVM
    {
        [Display(Name = "ইস্যু আইডি")]
        public int IssueId { get; set; }
        [Display(Name = "হোল্ডার আইডি")]
        public int HolderId { get; set; }
        [Display(Name = "স্ট্যাটাস টাইপ আইডি")]
        public int StatusTypeId { get; set; }
        [Display(Name = "বিষয়")]
        public string Subject { get; set; }
        [Display(Name = "সমাধানের তারিখ")]
        //public string Details { get; set; }
        //public string Doc1 { get; set; }
        //public string Doc2 { get; set; }
        public DateTime? SolvedDate { get; set; }
        [Display(Name = "স্ট্রিং সমাধানের তারিখ")]
        public string StringSolvedDate { get; set; }
        [Display(Name = "মন্তব্য")]
        public string Remarks { get; set; }
        [Display(Name = "তারিখ তৈরি করুন")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "তৈরিকারী")]
        public int? CreatedBy { get; set; }
        [Display(Name = "হালনাগাদকরণ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "হালনাগাদকরণ করেছেন")]
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }
        [Display(Name = "ব্যবহারকারীর নাম দ্বারা নির্মিত")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "ব্যবহারকারীর নাম দ্বারা আপডেট")]
        public string UpdatedByUserName { get; set; }


        [Display(Name = "প্রেরণ স্ট্যাটাস")]

        public bool? IsSendToClusUser { get; set; }



        ////out of table
        [Display(Name = "হোল্ডার এর নাম")]
        public string HolderName { get; set; }
        [Display(Name = "বর্তমান স্ট্যাটাস")]
        public string StatusName { get; set; }

        public List<IssueDetails> IssueDetails { get; set; }


        //for child tables
        [Display(Name = "বার্তা বিশদ তালিকা")]
        public string MsgDetailsList { get; set; }
        [Display(Name = "বার্তা তারিখের তালিকা")]
        public DateTime? MsgDateList { get; set; }
        [Display(Name = "বার্তার তারিখ")]
        public string StringMsgDateList { get; set; }
        [Display(Name = "ডকুমেন্ট 1 তালিকা")]
        public string Doc1List { get; set; }
        [Display(Name = "ডকুমেন্ট 2 তালিকা")]
        public string Doc2List { get; set; }
        [Display(Name = "বার্তা প্রেরকের নাম তালিকা")]
        public string MessageSenderNameList { get; set; }
        [Display(Name = "বার্তা প্রেরকের প্রকারের তালিকা")]
        public int MessageSenderTypeList { get; set; }

        public IssueCombineVM()
        {
            IssueDetails = new List<IssueDetails>();
        }


        //new added 
        [Display(Name = "এলাকার নাম")]
        public string AreaName { get; set; }

        [Display(Name = "প্লট নম্বর")]
        public string PlotNo { get; set; }

    }
}