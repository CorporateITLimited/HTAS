using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models
{
    public class SPGTransaction
    {
        public long Id { get; set; }
        public string RequestId { get; set; }

        [Display(Name = "রেফারেন্স কোড ")]
        public string RefTranNo { get; set; }

        [Display(Name = "রেফারেন্স তারিখ ")]
        public DateTime? RefTranDate { get; set; }

        [Display(Name = "রেফারেন্স তারিখ ")]
        public string strRefTranDate { get; set; }

        public string TranAmount { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string PayerId { get; set; }

        [Display(Name = "ব্যাংক একাউন্ট নম্বর ")]
        public string CreditAccounts { get; set; }
        public string CrAmount { get; set; }
        public string Purpose { get; set; }

        [Display(Name = "পক্ষে")]
        public string OnBehalf { get; set; }

        [Display(Name = "ট্রানজেকশন আইডি")]
        public string TranactionId { get; set; }

        public DateTime? TranDateTime { get; set; }

        [Display(Name = "ট্রানজেকশন তারিখ")]
        public string strTranDateTime { get; set; }

        [Display(Name = "টাকার পরিমাণ")]
        public string PayAmount { get; set; }
        public string PayMode { get; set; }
        public string OrgiBrCode { get; set; }

        [Display(Name = "ট্রানজেকশন স্ট্যাটাস")]
        public string StatusMsg { get; set; }

        [Display(Name = "ট্রানজেকশন স্ট্যাটাস কোড")]
        public string TransactionStatus { get; set; }
        public string IPAddressDetails { get; set; }
        public string ApiSessionKey { get; set; }
        public string ApiTokenKey { get; set; }
        public DateTime? LastUpdated { get; set; }

        [Display(Name = "শেষ হালনাগাদ তারিখ")]
        public string strLastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? HolderId { get; set; }

        [Display(Name = "শেষ হালনাগাদ করেছেন")]
        public string HolderUserName { get; set; }

        [Display(Name = "গৃহকরদাতা")]
        public string HolderName { get; set; }

        [Display(Name = "আর্থিক সন")]
        public string FinancialYear { get; set; }
    }
}