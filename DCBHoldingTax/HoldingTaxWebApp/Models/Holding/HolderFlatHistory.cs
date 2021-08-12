using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Holding
{
    public class HolderFlatHistory
    {
        public int FlatHistoryId { get; set; }
        public int? AreaId { get; set; }
        public int? PlotId { get; set; }

        public int? OldHolderFlatId { get; set; }
        public int? NewHolderFlatId { get; set; }
        public int? OldHolderId { get; set; }
        public int? NewHolderId { get; set; }
        [Display(Name = "রেফারেন্স নাম্বার")]

        public string ReferenceNo { get; set; }
        public DateTime? ReferenceDate { get; set; }
        [Display(Name = "রেফারেন্সের তারিখ")]

        public string StringReferenceDate { get; set; }
        [Display(Name = "হালনাগাদের সময়")]
        public DateTime? TransactionDate { get; set; }
        //public string StringTransactionDate { get; set; }
        public int? TransactionBy { get; set; }



        ////For View Model
        [Display(Name = "এলাকার নাম")]
        public string AreaName { get; set; }
        [Display(Name = "প্লট নম্বর")]
        public string PlotNo { get; set; }

        [Display(Name = "ফ্ল্যাট নং")]
        public string OldFlatNo { get; set; }
        [Display(Name = "ফ্ল্যাট নং")]
        public string NewFlatNo { get; set; }

        [Display(Name = "পুরাতন র্প্লট/ফ্ল্যাট/বাড়ী মালিকের নাম")]
        public string OldHolderName { get; set; }

        [Display(Name = "বর্তমান প্লট/ফ্ল্যাট/বাড়ী মালিকের নাম")]
        public string NewHolderName { get; set; }

        [Display(Name = "হালনাগাদকরি")]

        public string TransactionByUserName { get; set; }

    }
}