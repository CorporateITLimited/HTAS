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
        public string FinancialYear { get; set; }

        [Display(Name = "এলাকা  ")]
        public int AreaId { get; set; }

        [Display(Name = "এলাকা  ")]
        public string AreaName { get; set; }

        [Display(Name = "হোল্ডারের নাম  ")]
        public int HolderId { get; set; }

        [Display(Name = "হোল্ডারের নাম  ")]
        public string HolderName { get; set; }

        [Display(Name = "হোল্ডারের প্লট/ফ্ল্যাট/বাড়ী এর বিবরণ ")]
        public string AreaPlotFlatData { get; set; }

        [Display(Name = "বিজ্ঞপ্তির ধরণ")]
        public int NoticeTypeId { get; set; }

        [Display(Name = "বিজ্ঞপ্তির নাম ")]
        public string NoticeName { get; set; }
        public string NoticeLinkName { get; set; }
        public bool? IsNoticeSent { get; set; }
        public DateTime? NoticeSentDate { get; set; }
        public string StringNoticeSentDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByUsername { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public string UpdatedByUsername { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }



        public int? Notice_1 { get; set; }

        [Display(Name = "বিজ্ঞপ্তি নম্বর ১")]
        public string NoticeName_1 { get; set; }
        public int? Notice_2 { get; set; }

        [Display(Name = "বিজ্ঞপ্তি নম্বর ২")]
        public string NoticeName_2 { get; set; }
        public int? Notice_3 { get; set; }

        [Display(Name = "বিজ্ঞপ্তি নম্বর ৩")]
        public string NoticeName_3 { get; set; }
    }
}