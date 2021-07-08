using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models
{
    public class DOHSArea
    {
        public int AreaId { get; set; }
        [Display(Name = "Area Name")]
        public string AreaName { get; set; }
        [Display(Name = "Total Area")]
        public decimal? TotalArea { get; set; }
        [Display(Name = "Current Plot Number")]
        public int? CurrentPlotNumber { get; set; }
        [Display(Name = "Current Flat Number")]
        public int? CurrentFlatNumber { get; set; }

        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "Created By ")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "Updated Date ")]
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedByUserName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

    }
}