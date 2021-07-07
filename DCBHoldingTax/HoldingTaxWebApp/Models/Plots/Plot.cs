using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Plots
{
    public class Plot
    {
        public int PlotId { get; set; }
        [Display(Name = "Plot Id Number")]
        public string PlotIdNumber { get; set; }
        [Display(Name = "Area Name")]
        public int? AreaId { get; set; }

        [Display(Name = "Area Name")]
        public string AreaName { get; set; }

        [Display(Name = "Road No")]
        public string RoadNo { get; set; }
        [Display(Name = "Plot No")]
        public string PlotNo { get; set; }

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