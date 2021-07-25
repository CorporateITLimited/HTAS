using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Holding
{
    public class Notice
    {
        public long NoticeId { get; set; }
        public int FinancialYearId { get; set; }
        public string FinancialYear { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int HolderId { get; set; }
        public string HolderName { get; set; }
        public string AreaPlotFlatData { get; set; }
        public int NoticeTypeId { get; set; }
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
        public string NoticeName_1 { get; set; }
        public int? Notice_2 { get; set; }
        public string NoticeName_2 { get; set; }
        public int? Notice_3 { get; set; }
        public string NoticeName_3 { get; set; }
    }
}