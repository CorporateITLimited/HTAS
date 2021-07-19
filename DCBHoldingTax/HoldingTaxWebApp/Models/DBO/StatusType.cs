using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.DBO
{
    public class StatusType
    {
        [Display(Name = "স্ট্যাটাস টাইপ আইডি")]
        public int StatusTypeId { get; set; }
        [Display(Name = "স্ট্যাটাস নাম")]
        public string StatusName { get; set; }
    }
}