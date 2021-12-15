using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class OtpHistory
    {
        public int HistoryId { get; set; }
        public int? LogInCredentialId { get; set; }
        public string UserName { get; set; }
        public int Otp { get; set; }
        public string Purpose { get; set; }
        public string responseString { get; set; }
        public DateTime? CreateDate { get; set; }

        //out of table 
        public string MobileNumber { get; set; }
    }
}