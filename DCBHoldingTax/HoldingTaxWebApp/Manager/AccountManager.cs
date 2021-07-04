using HoldingTaxWebApp.Gateway;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager
{
    public class AccountManager
    {

        private readonly AccountGateway _accountGateway;
        public AccountManager()
        {
            _accountGateway = new AccountGateway();
        }

        public UserLogInCredentialVM LogIn(LogInVM user)
        {
            //if(user != null)
            return _accountGateway.LogIn(user);
        }

        public string LogOut(LogInVM user)
        {
            //try
            //{
            int rowAffected = _accountGateway.LogOut(user);
            return rowAffected > 0 ? "success" : "failed";
            //}
            //catch (Exception)
            //{
            //    return "error";
            //    throw;
            //}
        }

        public UserPermission GetUserPermissionByUserAndController(int UserId, int ControllerId)
        {
            return _accountGateway.GetUserPermissionByUserAndController(UserId, ControllerId);
        }
    }
}