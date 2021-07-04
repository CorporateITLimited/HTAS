using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class Designation
    {
        public int DesignationId { get; set; }
        [Display(Name = "Designation Name")]
        public string DesignationName { get; set; }
        public string Description { get; set; }

        [Display(Name = "Is Active?")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}