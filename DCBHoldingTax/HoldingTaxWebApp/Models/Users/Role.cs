using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class Role
    {
        public int RoleId { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Role Details")]
        public string RoleDetails { get; set; }
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "Created By")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedByUserName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

    }
}