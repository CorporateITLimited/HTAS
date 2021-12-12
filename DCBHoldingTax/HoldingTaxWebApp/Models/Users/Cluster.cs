using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class Cluster
    {
        public int ClusterId { get; set; }

        [Display(Name = "ক্লাস্টারের নাম")]
        public string ClusterName { get; set; }

        [Display(Name = "ক্লাস্টারের বিবরণ")]
        public string ClusterDetails { get; set; }
        public int? UserId { get; set; }

        [Display(Name = "ক্লাস্টারের ব্যবস্থাপক")]
        public string UserFullName { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public string UpdatedByUserName { get; set; }
        public string CreatedByUserName { get; set; }
    }
}