using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.DBO
{
    public class IssueDetails
    {
        public int IssueDetailsId { get; set; }
        public int IssueId { get; set; }
        public string MsgDetails { get; set; }
        public DateTime? MsgDate { get; set; }
        public string StrMsgDate { get; set; }
        public string StringMsgDate { get; set; }
        public string Doc1 { get; set; }
        public string Doc2 { get; set; }

        public bool? IsRead { get; set; }

        public int MessageSender { get; set; }
        public string MessageSenderName { get; set; }
        public int MessageSenderType { get; set; }
    }
}