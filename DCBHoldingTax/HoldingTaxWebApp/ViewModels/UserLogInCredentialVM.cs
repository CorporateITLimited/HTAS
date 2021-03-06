using HoldingTaxWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels
{
    public class UserLogInCredentialVM
    {

        // Common portion
        public int LogInCredentialId { get; set; }
        public string UserName { get; set; }
        public int UserTypeId { get; set; }
        public bool LogIsActive { get; set; }
        public bool LogIsDeleted { get; set; }

        // Supplier portion
        //public int? SupplierId { get; set; }
        //public string SupplierCode { get; set; }
        //public string SupplierLegalName { get; set; }
        //public bool? SuplierIsActive { get; set; }
        //public bool? SupplierIsDeleted { get; set; }

        // User Portion
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? UserIsActive { get; set; }
        public bool? UserIsDeleted { get; set; }
        public string UserFullName { get; set; }


        public CommonEntityHelper CommonEntity { get; set; }

        public UserLogInCredentialVM()
        {
            CommonEntity = new CommonEntityHelper();
        }

        // public UserLogInCredentialVM() => CommonEntity = new CommonEntityHelper();


    }
}