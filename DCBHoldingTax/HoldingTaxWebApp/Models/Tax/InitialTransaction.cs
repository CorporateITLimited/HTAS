using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Tax
{
    public class InitialTransaction
    {
        public long TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? TransactionAmount { get; set; }
        public string TransactionCurrency { get; set; }
        public int? HoldingTaxId { get; set; }
        public string HolderName { get; set; }
        public string FinancialYear { get; set; }
        public string ProductName { get; set; }
        public int? LastUpdatedBy { get; set; }
        public string LastUpdatedByUsername { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string IPAddressDetails { get; set; }
        public string ApiSessionKey { get; set; }
        public string ApiStatus { get; set; }
        public string ApiFailedReason { get; set; }
        public string ApiRedirectGatewayURL { get; set; }
        public string ApiDirectPaymentURLBank { get; set; }
        public string ApiDirectPaymentURLCard { get; set; }
        public string ApiDirectPaymentURL { get; set; }
        public string ApiRedirectGatewayURLFailed { get; set; }
        public string ApiGatewayPageURL { get; set; }
    }
}