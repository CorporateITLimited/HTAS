using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels
{
    public class UserPermission
    {
        public int PermissionId { get; set; }

        [Display(Name = "User Full Name")]
        public int UserId { get; set; }

        [Display(Name = "User Full Name")]
        public string UserFullName { get; set; }

        [Display(Name = "User Permission")]
        public int ControllerId { get; set; }

        [Display(Name = "User Permission")]
        public string ControllerName { get; set; }
        public bool? ReadWriteStatus { get; set; }
        public bool? CanAccess { get; set; }
        public DateTime? CreateDate { get; set; }
        public string StringCreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string StringLastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public string UpdatedByUserName { get; set; }

        // controller
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}