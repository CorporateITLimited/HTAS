using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels.Tax
{
    public class InvoiceVM
    {
        public int  HoldingTaxId { get; set; }
        public decimal? Rebate { get; set; }
        public DateTime? CurrentDate { get; set; }

        public string StringCurrentDate { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? NetTaxPayableAmount { get; set; }
        public decimal? DuesPreviousYear { get; set; }
        public decimal? DuesFineAmount { get; set; }
        public decimal? TotalTaxWithSurchargeDue { get; set; }
        public decimal? rebateNew { get; set; }
        public decimal? TotalNet { get; set; }
        public string FinancialYear { get; set; }
        public DateTime? StartingDate { get; set; }
        public string StringStartingDate { get; set; }

        public DateTime? FEndDate { get; set; }
        public string StringFEndDate { get; set; }

        public DateTime?  oldDate { get; set; }
        public string StringoldDate { get; set; }

        public DateTime?  EndOfYear { get; set; }
        public string StringEndOfYear { get; set; }

        public string HolderName { get; set; }
        public string PresentAdd { get; set; }
        public string PlotNo { get; set; }
        public string RoadNo { get; set; }
        public string RoadName { get; set; }
        public string AreaName { get; set; }
        public string invoiceNo { get; set; }
        public string EmployeeName { get; set; }
        public decimal? RebateRate { get; set; }
        public decimal? DuesChargeRate { get; set; }
        public bool Ispaid { get; set; }

        public decimal? WrongInfoChargeRate { get; set; }
        public decimal? WrongInfoCharge { get; set; }


       
        public decimal? Reduction { get; set; }
    }
}