using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Tax
{
    public class PrimaryTransaction
    {
        public Int64 PrimaryTransactionId { get; set; }
        [Display(Name = "ট্রানজেকশন স্ট্যাটাস")]
        public string Status { get; set; }
        public DateTime? TranDate { get; set; }
        [Display(Name = "ট্রানজেকশন তারিখ")]

        public string StringTranDate { get; set; }

        [Display(Name = "ট্রানজেকশন আইডি")]
        public string TranId { get; set; }
        [Display(Name = "বৈধতা আইডি")]

        public string ValId { get; set; }
        [Display(Name = "টাকার পরিমাণ")]

        public decimal? Amount { get; set; }
        [Display(Name = "সঞ্চয়ের পরিমাণ")]

        public decimal? StoreAmount { get; set; }
        [Display(Name = "কার্ড এর ধরন")]

        public string CardType { get; set; }
        [Display(Name = "কার্ড নাম্বার")]

        public string CardNo { get; set; }
        [Display(Name = "মুদ্রা")]

        public string Currency { get; set; }
        [Display(Name = "ব্যাংক ট্রানজেকশন আইডি")]

        public string BankTranId { get; set; }
        [Display(Name = "কার্ড প্রদানকারী")]

        public string CardIssuer { get; set; }
        [Display(Name = "কার্ড ব্র্যান্ড")]

        public string CardBrand { get; set; }
        [Display(Name = "কার্ড প্রদানকারী দেশ")]

        public string CardIssuerCountry { get; set; }
        [Display(Name = "কার্ড প্রদানকারী দেশের কোড")]

        public string CardIssuerCountryCode { get; set; }
        [Display(Name = "মুদ্রার ধরন")]

        public string CurrencyType { get; set; }
        [Display(Name = "মুদ্রার টাকার পরিমাণ")]

        public decimal? CurrencyAmount { get; set; }
        [Display(Name = "ই এম আই কিস্তি")]

        public int? EmiInstalment { get; set; }
        [Display(Name = "ই এম আই পরিমাণ")]

        public decimal? EmiAmount { get; set; }
        [Display(Name = "ছাড়ের পরিমাণ")]

        public decimal? DiscountAmount { get; set; }
        [Display(Name = "ছাড়(শতাংশ)")]

        public decimal? DiscountPercentage { get; set; }
        [Display(Name = "ছাড়ের মন্তব্য")]

        public string DiscountRemarks { get; set; }
     

        public string ValueA { get; set; }
        public string ValueB { get; set; }
        public string ValueC { get; set; }
        public string ValueD { get; set; }
        [Display(Name = "ঝুঁকি স্তর")]

        public int? RiskLevel { get; set; }
        [Display(Name = "মাধ্যমিক স্ট্যাটাস")]

        public string SecondaryStatus { get; set; }
        [Display(Name = "ঝুঁকির শিরোনাম")]

        public string RiskTitle { get; set; }
        [Display(Name = "লেনদেনের তারিখ")]

        public DateTime? CreateDate { get; set; }
        [Display(Name = "লেনদেনের তারিখ")]

        public string StringCreateDate { get; set; }
        [Display(Name = "করদাতা")]

        public string HolderName { get; set; }
        [Display(Name = "আর্থিক সন")]

        public string FinancialYear { get; set; }
    }
}