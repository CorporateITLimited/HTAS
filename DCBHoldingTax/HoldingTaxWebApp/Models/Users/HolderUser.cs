using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class HolderUser
    {
        public int HolderUserId { get; set; }
        public int HolderId { get; set; }
        public string HolderName { get; set; }
        public string Email { get; set; }
        public bool? IsEmailConfirmed { get; set; }
        public string MobileNumber { get; set; }
        public bool? IsMobileNumberConfirmed { get; set; }
        public int LogInCredentialId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedByUserName { get; set; }
        public string UpdatedByUserName { get; set; }

        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public int UserTypeId { get; set; }
        public bool? LogIsActive { get; set; }
        public bool? LogIsDeleted { get; set; }

        public string StringCreateDate { get; set; }
        public string StringLastUpdated { get; set; }

    }
}