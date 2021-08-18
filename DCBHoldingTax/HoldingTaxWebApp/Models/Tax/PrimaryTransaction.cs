using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Tax
{
    public class PrimaryTransaction
    {
        public Int64 PrimaryTransactionId { get; set; }
        public string Status { get; set; }
        public DateTime? TranDate { get; set; }
        public string StringTranDate { get; set; }
        public string TranId { get; set; }
        public string ValId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? StoreAmount { get; set; }
        public string CardType { get; set; }
        public string CardNo { get; set; }
        public string Currency { get; set; }
        public string BankTranId { get; set; }
        public string CardIssuer { get; set; }
        public string CardBrand { get; set; }
        public string CardIssuerCountry { get; set; }
        public string CardIssuerCountryCode { get; set; }
        public string CurrencyType { get; set; }
        public decimal? CurrencyAmount { get; set; }
        public int? EmiInstalment { get; set; }
        public decimal? EmiAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public string DiscountRemarks { get; set; }
        public string ValueA { get; set; }
        public string ValueB { get; set; }
        public string ValueC { get; set; }
        public string ValueD { get; set; }
        public int? RiskLevel { get; set; }
        public string SecondaryStatus { get; set; }
        public string RiskTitle { get; set; }
        public DateTime? CreateDate { get; set; }
        public string StringCreateDate { get; set; }
    }
}