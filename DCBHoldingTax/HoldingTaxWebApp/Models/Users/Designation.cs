using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class Designation
    {
        [Display(Name = "পদবি আইডি")]
        public int DesignationId { get; set; }
        [Display(Name = "পদবি নাম")]
        public string DesignationName { get; set; }
        [Display(Name = "বর্ণনা")]
        public string Description { get; set; }

        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }
    }
}