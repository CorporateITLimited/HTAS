using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Helpers
{
    public class CommonConstantHelper
    {


        #region Session

        public const string LogInCredentialId = "LogInCredentialId";
        public const string UserName = "UserName";
        public const string UserTypeId = "UserTypeId";

        public const string UserId = "UserId";
        public const string RoleId = "RoleId";
        public const string RoleName = "RoleName";
        public const string UserFullName = "UserFullName";

        public const string ReportParameter = "ReportParameter";

        public const string HolderName = "HolderName";
        public const string AreaPlotFlatData = "AreaPlotFlatData";
        public const string HolderId = "HolderId";
        public const string AreaId = "AreaId";
        public const string MobileNumber = "MobileNumber";

        #endregion

        #region Display Name Common Fields

        public const string CreatedBy = "Created By";
        public const string UpdatedBy = "Updated By";
        public const string DateCreated = "Created Date";
        public const string DateUpdated = "Updated Date";
        public const string IsActive = "Status";

        #endregion

        #region Database Statement

        public const string Insert = "insert";

        public const string Update = "update";

        public const string Select = "select";

        public const string Delete = "delete";

        public const string Status = "status";

        public const string Details = "details";

        public const string Roleback = "roleback";

        #endregion


        #region Manager Decision

        #region Global

        public const string Success = "success";
        public const string Failed = "failed";
        public const string Conflict = "conflict";
        public const string Error = "error";

        #endregion

        //#region Supplier

        //public const string SupplierCodeConflict = "supplier_code_conflict";

        //#endregion



        #endregion


        #region server
        public const string DevRootDirectoryFaisal = @"C:/Users/acer/Documents/GitHub/HTAS/DCBHoldingTax/HoldingTaxWebApp";

        public const string ServerRootDirectory = @"C:/inetpub/wwwroot/DCB";

        public string scretKey { get; set; }
        #endregion


    }
}