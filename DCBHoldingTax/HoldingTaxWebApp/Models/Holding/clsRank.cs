using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Holding
{
    public class clsRank
    {
       
        public int RankId { get; set; }
        [Display(Name = "র‍্যাংক")]
        public string RankName { get; set; }
        [Display(Name = "র‍্যাংক বিস্তারিত")]
        public string RankDetails { get; set; }

        
       [Display(Name = "সক্রিয়তা")]
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}