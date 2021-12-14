using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class ChangePassword
    {

        public int LogInCredentialId { get; set; }

        [Display(Name = "মোবাইল নম্বর ")]
        public string MobileNumber { get; set; }

        [Display(Name = "ইউজারনেইম ")]
        public string UserName { get; set; }

        public int otp { get; set; }

        [Display(Name = "পাসওয়ার্ড")]
        public string HashPassword { get; set; }

        [Display(Name = "কনফার্ম পাসওয়ার্ড")]
        [NotMapped]
        [Compare("HashPassword", ErrorMessage = "Password does not match.")]
        public string ConfirmPassword { get; set; }
    }
}