using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class clsUser
    {
        public int UserId { get; set; }
        [Display(Name = "User Full Name")]
        public string UserFullName { get; set; }
        [Display(Name = "User Details")]
        public string UserDetails { get; set; }

        [Display(Name = "Employee")]
        public int? EmpolyeeId { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        public int RoleId { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public string Email { get; set; }
        public bool? IsEmailConfirmed { get; set; }
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        public bool? IsMobileNumberConfirmed { get; set; }
        public int LogInCredentialId { get; set; }
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "Created By ")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "Updated Date ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "Updated By")]
        public string UpdatedByUserName { get; set; }
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "Is Active?")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }


        //login 
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username Required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Required.")]
        public string HashPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [NotMapped]
        [Compare("HashPassword", ErrorMessage = "Password does not match.")]
        public string ConfirmPassword { get; set; }

        public int UserTypeId { get; set; }

        [Display(Name = "Status")]
        public bool? LogIsActive { get; set; }
        public bool? LogIsDeleted { get; set; }

    }
}