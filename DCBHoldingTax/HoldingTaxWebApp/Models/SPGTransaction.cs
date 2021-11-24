using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models
{
    public class SPGTransaction
    {
        public long Id { get; set; }
        public string RequestId { get; set; }
        public string RefTranNo { get; set; }
        public DateTime? RefTranDate { get; set; }
        public string strRefTranDate { get; set; }
        public string TranAmount { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string PayerId { get; set; }
        public string CreditAccounts { get; set; }
        public string CrAmount { get; set; }
        public string Purpose { get; set; }
        public string OnBehalf { get; set; }
        public string TranactionId { get; set; }
        public DateTime? TranDateTime { get; set; }
        public string strTranDateTime { get; set; }
        public string PayAmount { get; set; }
        public string PayMode { get; set; }
        public string OrgiBrCode { get; set; }
        public string StatusMsg { get; set; }
        public string TransactionStatus { get; set; }
        public string IPAddressDetails { get; set; }
        public string ApiSessionKey { get; set; }
        public string ApiTokenKey { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string strLastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? HolderId { get; set; }
        public string HolderUserName { get; set; }
    }
}