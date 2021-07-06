using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Users
{
    public class Employee
    {
        public int EmpolyeeId { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        [Display(Name = "Designation Name")]
        public string DesignationName { get; set; }
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        [Display(Name = "DOB")]
        public string StringDOB { get; set; }
        public string Sex { get; set; }
        public int? NID { get; set; }
        [Display(Name = "Employee Address")]
        public string EmployeeAddress { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        [Display(Name = "Created By ")]
        public string CreatedByUserName { get; set; }
        [Display(Name = "Updated Date ")]
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedByUserName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

    }
}