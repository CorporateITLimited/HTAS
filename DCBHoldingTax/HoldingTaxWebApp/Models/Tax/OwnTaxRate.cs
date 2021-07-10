using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Tax
{
    public class OwnTaxRate
    {
        public int OwnTaxRateId { get; set; }
        [Display(Name ="সামরিক/বেসামরিক")]
        public int? Mill_Civil { get; set; }
        public decimal? AreaSF { get; set; }
        public decimal? Amount { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}