using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager;
using HoldingTaxWebApp.Manager.Users;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HoldingTaxWebApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly AccountManager _accountManager;
        private readonly UserManager _userManager;
        public AccountController()
        {
            _accountManager = new AccountManager();
            _userManager = new UserManager();
        }




        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogInVM user)
        {
            try
            {
                if (user == null)
                    return HttpNotFound();

                if (user.UserName == null)
                {
                    ModelState.AddModelError("", "Username is required.");
                    return View(user);
                }

                if (user.Password == null)
                {
                    ModelState.AddModelError("", "Password is required.");
                    return View(user);
                }

               // user.Password = PasswordHelper.EncryptPass(user.Password);

                user.LogInTime = DateTime.Now;
                user.LogOutTime = null;

                string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                user.UserLogDetails = Request.Browser.IsMobileDevice ?
                                        $"mobile {ipAddress}" : $"desktop {ipAddress}";

                UserLogInCredentialVM logInCredentialVM = _accountManager.LogIn(user);

                if (logInCredentialVM.CommonEntity.Result == 202)
                {
                    Session[CommonConstantHelper.LogInCredentialId] = logInCredentialVM.LogInCredentialId;
                    Session[CommonConstantHelper.UserName] = logInCredentialVM.UserName;
                    Session[CommonConstantHelper.UserTypeId] = logInCredentialVM.UserTypeId;

                    Session[CommonConstantHelper.UserId] = logInCredentialVM.UserId;
                    Session[CommonConstantHelper.UserFullName] = logInCredentialVM.UserFullName;
                    Session[CommonConstantHelper.RoleId] = logInCredentialVM.RoleId;
                    Session[CommonConstantHelper.RoleName] = logInCredentialVM.RoleName;

                    //Session[CommonConstantHelper.SupplierId] = logInCredentialVM.SupplierId;
                    //Session[CommonConstantHelper.SupplierCode] = logInCredentialVM.SupplierCode;
                    //Session[CommonConstantHelper.SupplierLegalName] = logInCredentialVM.SupplierLegalName;

                    string message = " " + Session[CommonConstantHelper.UserName];

                    Session["ListofPermissions"] = _userManager.GetUserPermissionListByUserId(logInCredentialVM.UserId ?? default(int));

                    if (logInCredentialVM.UserTypeId == 1)
                    {
                        // TempData["SM"] = "welcome | " + message;
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Index", "Home");
                    //return Content("You are supplier");

                }
                else if (logInCredentialVM.CommonEntity.Result == 401)
                {
                    TempData["EM"] = "warning | Invalid Username or Password.";
                    return View();
                }
                else /*if (logInCredentialVM.CommonEntity.Result == 500)*/
                {
                    TempData["EM"] = "warning | Bad Request.";
                    return View(user);
                }
            }
            catch (Exception exception)
            {
                //throw exception;
                TempData["EM"] = "error | " + exception.Message.ToString();
                //return RedirectToAction("Error", "Home");
                return View();
            }

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
               && (Session[CommonConstantHelper.UserTypeId] != null)
               && (Session[CommonConstantHelper.UserName] != null))
            {
                try
                {
                    LogInVM user = new LogInVM
                    {
                        UserName = Session[CommonConstantHelper.UserName].ToString(),
                        LogInTime = null,
                        LogOutTime = DateTime.Now
                    };

                    string result = _accountManager.LogOut(user).ToString();

                    if (result == "success")
                    {
                        //Session[CommonConstantHelper.LogInCredentialId] = null;
                        Session.Remove(CommonConstantHelper.LogInCredentialId);
                        Session.Remove(CommonConstantHelper.UserName);
                        Session.Remove(CommonConstantHelper.UserTypeId);

                        //Session.Remove(CommonConstantHelper.SupplierId);
                        //Session.Remove(CommonConstantHelper.SupplierCode);
                        //Session.Remove(CommonConstantHelper.SupplierLegalName);

                        Session.Remove(CommonConstantHelper.UserId);
                        Session.Remove(CommonConstantHelper.UserFullName);
                        Session.Remove(CommonConstantHelper.RoleId);
                        Session.Remove(CommonConstantHelper.RoleName);

                        FormsAuthentication.SignOut();
                        //FormsAuthentication.RedirectToLoginPage();

                        Session.RemoveAll();
                        Session.Abandon();

                        // Not working ** Session problems
                        TempData["SM"] = "succcess | Log out successfully.";
                        return RedirectToAction("LogIn", "Account");
                    }
                    else
                    {
                        // TempData["EM"] = "Failed to LogOut";
                        return Content("Failed to LogOut");
                    }
                }
                catch (Exception exception)
                {
                    //throw exception;
                    //TempData["EM"] = "error | " + exception.Message.ToString();
                    //return RedirectToAction("Error", "Home");
                    return Content("Error =>  " + exception.Message.ToString());
                }
            }
            else
            {
                TempData["EM"] = "Session Expired or Internal Error. {Primary Account Secondary LogOut}";
                return RedirectToAction("LogIn", "Account");
                //return RedirectToAction("Error", "Home");
            }
        }

    }
}
