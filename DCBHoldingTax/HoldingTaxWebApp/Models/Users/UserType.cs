using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class UserType
    {
        public int UserTypeId { get; set; }

        [Display(Name = "User Type Name")]
        public string UserTypeName { get; set; }
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

        [Display(Name = "Is Active?")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

    }
}