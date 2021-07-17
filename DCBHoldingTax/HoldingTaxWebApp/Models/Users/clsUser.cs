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

        [Display(Name = "ব্যবহারকারীর পুরো নাম ")]
        public string UserFullName { get; set; }

        [Display(Name = "ব্যবহারকারীর বিশদ তথ্য ")]
        public string UserDetails { get; set; }

        [Display(Name = "কর্মকর্তার/কর্মচারীর নাম ")]
        public int? EmpolyeeId { get; set; }

        [Display(Name = "কর্মকর্তার/কর্মচারীর নাম ")]
        public string EmployeeName { get; set; }

        [Display(Name = "রোল ")]
        public int RoleId { get; set; }

        [Display(Name = "রোল ")]
        public string RoleName { get; set; }

        [Display(Name = "ইমেইল ")]
        public string Email { get; set; }
        public bool? IsEmailConfirmed { get; set; }
        [Display(Name = "মোবাইল নম্বর ")]
        public string MobileNumber { get; set; }
        public bool? IsMobileNumberConfirmed { get; set; }
        public int LogInCredentialId { get; set; }

        [Display(Name = "যুক্তকরণের তারিখ ")]
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "যুক্ত করেছেন ")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "শেষ হালনাগাদের তারিখ ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "শেষ হালনাগাদ করেছেন  ")]
        public string UpdatedByUserName { get; set; }
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়তা ")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }


        //login 
        [Display(Name = "ইউজারনেইম ")]
        [Required(ErrorMessage = "Username Required.")]
        public string UserName { get; set; }


        [Display(Name = "পাসওয়ার্ড  ")]
        [Required(ErrorMessage = "Password Required.")]
        public string HashPassword { get; set; }

        [Display(Name = "কনফার্ম পাসওয়ার্ড")]
        [NotMapped]
        [Compare("HashPassword", ErrorMessage = "Password does not match.")]
        public string ConfirmPassword { get; set; }

        public int UserTypeId { get; set; }

        [Display(Name = "সক্রিয়তা ")]
        public bool? LogIsActive { get; set; }
        public bool? LogIsDeleted { get; set; }

    }
}