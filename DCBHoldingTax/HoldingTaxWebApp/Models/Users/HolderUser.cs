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
        [Display(Name = "হোল্ডার ইউজার আইডি")]
        public int HolderUserId { get; set; }

        [Display(Name = "হোল্ডার আইডি")]
        public int HolderId { get; set; }

        [Display(Name = "হোল্ডার নাম")]
        public string HolderName { get; set; }
        [Display(Name = "ইমেল")]
        public string Email { get; set; }
        [Display(Name = "ইমেল নিশ্চিত করা হয়েছে")]
        public bool? IsEmailConfirmed { get; set; }
        [Display(Name = "মোবাইল নম্বর")]
        public string MobileNumber { get; set; }
        [Display(Name = "মোবাইল নম্বর নিশ্চিত করা হয়েছে ")]
        public bool? IsMobileNumberConfirmed { get; set; }
        [Display(Name = "লগইন শংসাপত্র আইডি")]
        public int LogInCredentialId { get; set; }
        [Display(Name = "তারিখ তৈরি করুন")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "তৈরিকারী")]
        public int? CreatedBy { get; set; }
        [Display(Name = "হালনাগাদকরণ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "হালনাগাদকরণ করেছেন")]
        public int? LastUpdatedBy { get; set; }
        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }
        [Display(Name = "ব্যবহারকারীর নাম দ্বারা নির্মিত")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "ব্যবহারকারীর নাম দ্বারা আপডেট")]
        public string UpdatedByUserName { get; set; }
        [Display(Name = "ব্যবহারকারীর নাম")]
        public string UserName { get; set; }
        [Display(Name = "হ্যাশ পাসওয়ার্ড")]
        public string HashPassword { get; set; }
        [Display(Name = "পাসওয়ার্ড নিশ্চিত করুন")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "ব্যবহারকারীর প্রকারের আইডি")]
        public int UserTypeId { get; set; }
        [Display(Name = "সক্রিয় লগ")]
        public bool? LogIsActive { get; set; }
        [Display(Name = "লগ মুছে ফেলা হয়েছে")]
        public bool? LogIsDeleted { get; set; }
        [Display(Name = "স্ট্রিং তৈরি তারিখ")]
        public string StringCreateDate { get; set; }
        [Display(Name = "স্ট্রিং সর্বশেষ আপডেট হয়েছে")]
        public string StringLastUpdated { get; set; }

    }
}