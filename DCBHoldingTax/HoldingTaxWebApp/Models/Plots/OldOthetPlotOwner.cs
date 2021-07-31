using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Plots
{
    public class OldOthetPlotOwner
    {
        public int OldOthetPlotOwnerId { get; set; }
        [Display(Name = "অন্যান্য মালিকের নাম")]
        public string OldOthetOwneeName { get; set; }
        public int PlotOwnerId { get; set; }
        public int OldPlotOwnerId { get; set; }
        [Display(Name = "প্লট মালিকের নাম")]
        public string OldPlotOwnerName { get; set; }
        [Display(Name = "ঠিকানা")]
        public string Address { get; set; }
        [Display(Name = "মন্তব্য")]

        public string Remarks { get; set; }
     


    }
}