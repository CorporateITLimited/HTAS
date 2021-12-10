using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Holding
{
    public class Notice
    {
        public long NoticeId { get; set; }

        [Display(Name = "আর্থিক বছর ")]
        public int FinancialYearId { get; set; }

        [Display(Name = "আর্থিক বছর ")]
        public int FinancialYearId_Two { get; set; }

        [Display(Name = "আর্থিক বছর ")]
        public string FinancialYear { get; set; }

        [Display(Name = "এলাকা  ")]
        public int AreaId { get; set; }

        [Display(Name = "এলাকা  ")]
        public string AreaName { get; set; }

        public int PlotId { get; set; }

        [Display(Name = "প্লট নম্বর")]
        public string PlotNo { get; set; }

        [Display(Name = "হোল্ডারের নাম  ")]
        public int HolderId { get; set; }

        [Display(Name = "হোল্ডারের নাম  ")]
        public string HolderName { get; set; }

        [Display(Name = "হোল্ডারের প্লট/ফ্ল্যাট/বাড়ী এর বিবরণ ")]
        public string AreaPlotFlatData { get; set; }

        [Display(Name = "বিজ্ঞপ্তির ধরণ")]
        public int NoticeTypeId { get; set; }

        [Display(Name = "বিজ্ঞপ্তির ধরণ")]
        public int NoticeTypeId_Two { get; set; }

        [Display(Name = "বিজ্ঞপ্তির নাম ")]
        public string NoticeName { get; set; }

        [Display(Name = "নোটিশ লিংক ")]
        public string NoticeLinkName { get; set; }

        [Display(Name = "নোটিশ পাঠানো হয়েছে ")]
        public bool? IsNoticeSent { get; set; }

        [Display(Name = "নোটিশ পাঠানো তারিখ")]
        public DateTime? NoticeSentDate { get; set; }

        [Display(Name = "নোটিশ পাঠানো তারিখ")]
        public string StringNoticeSentDate { get; set; }

        [Display(Name = "সংযুক্তের তারিখ ")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "সংযুক্ত করেছেন ")]
        public int? CreatedBy { get; set; }

        [Display(Name = "সংযুক্ত করেছেন ")]
        public string CreatedByUsername { get; set; }

        [Display(Name = "হালনাগাদের তারিখ")]
        public DateTime? LastUpdated { get; set; }

        [Display(Name = "হালনাগাদ করেছেন")]
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "হালনাগাদ করেছেন")]
        public string UpdatedByUsername { get; set; }
        [Display(Name = " সক্রিয়তা")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }


        [Display(Name = "গৃহকরের প্রাথমিক বিজ্ঞপ্তি")]
        public int? Notice_1 { get; set; }

        [Display(Name = "গৃহকরের প্রাথমিক বিজ্ঞপ্তি")]
        public string NoticeName_1 { get; set; }

        [Display(Name = "রিবেটসহ গৃহকর প্রাপ্তির বিজ্ঞপ্তি")]
        public int? Notice_2 { get; set; }

        [Display(Name = "রিবেটসহ গৃহকর প্রাপ্তির বিজ্ঞপ্তি")]
        public string NoticeName_2 { get; set; }

        [Display(Name = "গৃহকরের চূড়ান্ত বিজ্ঞপ্তি")]
        public int? Notice_3 { get; set; }

        [Display(Name = "গৃহকরের চূড়ান্ত বিজ্ঞপ্তি")]
        public string NoticeName_3 { get; set; }


        [Display(Name = "কর্মকর্তার নাম")]
        public int? EmpolyeeId { get; set; }

        [Display(Name = "কর্মকর্তার নাম")]
        public string EmployeeName { get; set; }

        [Display(Name = "পদবির নাম")]
        public string DesignationName { get; set; }

        [Display(Name = "আইডি নম্বর")]
        public string HolderNo { get; set; }
    }
}