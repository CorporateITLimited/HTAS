using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class Employee
    {
        [Display(Name = "কর্মকর্তার আইডি")]
        public int EmpolyeeId { get; set; }
        [Display(Name = "কর্মকর্তার নাম")]
        public string EmployeeName { get; set; }

        [Display(Name = "পদবি ")]
        public int DesignationId { get; set; }
        [Display(Name = "পদবি নাম")]
        public string DesignationName { get; set; }
        [Display(Name = "বাবার নাম")]
        public string FatherName { get; set; }
        [Display(Name = "মায়ের নাম")]
        public string MotherName { get; set; }
        [Display(Name = "যোগাযোগের নম্বর")]
        public string ContactNo { get; set; }

        [Display(Name = "ইমেল")]
        public string Email { get; set; }
        [Display(Name = "জন্ম তারিখ")]
        public DateTime? DOB { get; set; }
        [Display(Name = "জন্ম তারিখ")]
        public string StringDOB { get; set; }
        [Display(Name = "লিঙ্গ")]
        public string Sex { get; set; }
        [Display(Name = "এনআইডি")]
        public string NID { get; set; }
        [Display(Name = "কর্মচারী ঠিকানা")]
        public string EmployeeAddress { get; set; }
        [Display(Name = "মন্তব্য")]
        public string Remarks { get; set; }
        [Display(Name = "তারিখ তৈরি করুন")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "তৈরিকারী")]
        public int? CreatedBy { get; set; }
        [Display(Name = "তৈরিকারী")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "আপডেটের তারিখ")]
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "সর্বশেষ আপডেট করেছেন")]
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "আপডেট হয়েছে")]
        public string UpdatedByUserName { get; set; }
        [Display(Name = "সক্রিয়")]
        public bool? IsActive { get; set; }
        [Display(Name = "মুছে ফেলা ")]
        public bool? IsDeleted { get; set; }

    }
}