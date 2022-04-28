using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager;
using HoldingTaxWebApp.Manager.Users;
using HoldingTaxWebApp.Models;
using HoldingTaxWebApp.Models.Users;
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
        private readonly OtpHistoryManager _OtpHistoryManager;

        public AccountController()
        {
            _accountManager = new AccountManager();
            _userManager = new UserManager();
            _OtpHistoryManager = new OtpHistoryManager();
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

                user.Password = PasswordHelper.EncryptPass(user.Password);

                user.LogInTime = DateTime.Now;
                user.LogOutTime = null;

                string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                user.UserLogDetails = Request.Browser.IsMobileDevice ?
                                        $"mobile {ipAddress}" : $"desktop {ipAddress}";

                Session["_ipDetails"] = user.UserLogDetails;

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

                    Session[CommonConstantHelper.HolderId] = logInCredentialVM.HolderId;
                    Session[CommonConstantHelper.HolderName] = logInCredentialVM.HolderName;
                    Session[CommonConstantHelper.AreaPlotFlatData] = logInCredentialVM.AreaPlotFlatData;
                    Session[CommonConstantHelper.AreaId] = logInCredentialVM.AreaId;
                    Session[CommonConstantHelper.MobileNumber] = logInCredentialVM.MobileNumber;

                    string message = " " + Session[CommonConstantHelper.UserName];

                    Session["ListofPermissions"] = _userManager.GetUserPermissionListByUserId(logInCredentialVM.UserId ?? default(int));

                    if (logInCredentialVM.UserTypeId == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (logInCredentialVM.UserTypeId == 2)
                    {
                        return RedirectToAction("Holder", "Home");
                    }
                    else
                    {
                        return View();
                    }


                }
                else if (logInCredentialVM.CommonEntity.Result == 401)
                {
                    ModelState.AddModelError("", "সতর্কতা | ভুল ইউজারনেইম বা পাসওয়ার্ড");
                    return View();
                }
                else /*if (logInCredentialVM.CommonEntity.Result == 500)*/
                {
                    ModelState.AddModelError("", "সতর্কতা | সার্ভারের বা নেটওয়ার্কের ত্রুটি");
                    return View();
                }
            }
            catch (Exception exception)
            {
                //return Content();
                ModelState.AddModelError("", "Error =>  " + exception.Message.ToString());
                return View(user);
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

                        Session.Remove(CommonConstantHelper.HolderId);
                        Session.Remove(CommonConstantHelper.HolderName);
                        Session.Remove(CommonConstantHelper.AreaPlotFlatData);

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
                    return Content("Error =>  " + exception.Message.ToString());
                }
            }
            else
            {
                TempData["EM"] = "Session Expired";
                return RedirectToAction("LogIn", "Account");
            }
        }



        #region Forget Password portion

        [HttpGet]
        [AllowAnonymous]

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(ChangePassword cp)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError("", errors.ToString());
                return View(cp);
            }
            if (cp.LogInCredentialId == 0)
            {
                ModelState.AddModelError("", "Username is required.");
                return View(cp);
            }

            if (cp.UserName == null)
            {
                ModelState.AddModelError("", "Username is required.");
                return View(cp);
            }

            //if (cp.otp == 0)
            //{
            //    ModelState.AddModelError("", "Otp is required.");
            //    return View(cp);
            //}


            if (cp.HashPassword == null)
            {
                ModelState.AddModelError("", "Password is required.");
                return View(cp);
            }

            if (!cp.HashPassword.Equals(cp.ConfirmPassword))
            {
                ModelState.AddModelError("", "Password does not match.");
                return View(cp);
            }

            cp.HashPassword = PasswordHelper.EncryptPass(cp.HashPassword);

            string changePassword = _accountManager.passwordUpdate(cp);

            if (changePassword == CommonConstantHelper.Success)
            {
                TempData["SM"] = "সফলভাবে পাসওয়ার্ড হালনাগাদ করা হয়েছে। পুনরায় নুতুন পাসওয়ার্ড দিয়ে লগইন করুন।";
                return RedirectToAction("LogIn", "Account");
            }
            else if (changePassword == CommonConstantHelper.Conflict)
            {
                ModelState.AddModelError("", "পাসওয়ার্ড ডাটাবেজে বিদ্যমান ৱয়েছে");
                TempData["PM"] = "পাসওয়ার্ড ডাটাবেজে বিদ্যমান ৱয়েছে";
                return View(cp);
            }
            else if (changePassword == CommonConstantHelper.Error)
            {
                ModelState.AddModelError("", "Error");
                TempData["EM"] = "Error.";
                return View(cp);
            }
            else if (changePassword == CommonConstantHelper.Failed)
            {
                ModelState.AddModelError("", "Failed");
                return View(cp);
            }
            else
            {
                ModelState.AddModelError("", "Error Not Recognized");
                return View();
            }







            //return View();
        }



        //public ActionResult DestroySession()
        //{
        //    Session = null;
        //}


        public ActionResult ChangeUserName()
        {
            return View();
        }

        #region jason result

        public JsonResult UserNameCheck(string UserName)
        {
            string status = "";
            //ChangePassword data = new ChangePassword();
            var data = _accountManager.findUserName(UserName);
            //IsNullOrEmpty(data.UserName.ToString())

            if(string.IsNullOrEmpty(data.UserName) && string.IsNullOrEmpty(data.MobileNumber))
            {
                status = "Wrong user Name";
            }
            else if (string.IsNullOrEmpty(data.MobileNumber) && data.UserName != null)
            {
                status = "Mobile No Not Found";
            }
            else
            {
                Random random = new Random();
                int otp = random.Next(100000, 999999);

                OtpHistory newHistory = new OtpHistory()
                {
                    LogInCredentialId = data.LogInCredentialId,
                    Otp = otp,
                    UserName = data.UserName,
                    Purpose = "Change Password",
                    CreateDate = DateTime.Now
                };

                string mag = "গৃহসেবা(DCB)ঃ আপনার OTP কোড -" + otp + ", ধন্যবাদ।";
                string result = SmsApi.SendSms(mag, data.MobileNumber);

                newHistory.responseString = result;
                string strmsg = _OtpHistoryManager.OtpHistoryInsert(newHistory);

                status = "Success";
            }



           


            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public JsonResult MobileNo(OtpHistory oh)
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);

            OtpHistory newHistory = new OtpHistory()
            {
                LogInCredentialId = oh.LogInCredentialId,
                Otp = otp,
                UserName = oh.UserName,
                Purpose = "Change Password",
                CreateDate = DateTime.Now
            };

            string mag = "গৃহসেবা অনলাইন(DCB)ঃ আপনার OTP কোড -" + otp + ", ধন্যবাদ।";


            string result = "Hello";//SmsApi.SendSms(mag, oh.MobileNumber);

            newHistory.responseString = result;
            string strmsg = _OtpHistoryManager.OtpHistoryInsert(newHistory);
            //string result = "";
            //if (strmsg == CommonConstantHelper.Success)
            //{

            //}



            //ChangePassword data = new ChangePassword();

            //data = _accountManager.findUserName(UserName);

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }



        public JsonResult getOtp(int otp)
        {
            var data = _OtpHistoryManager.GetOtpHistoryById(otp);

            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult checkDuplicateUsername(string newUserName) {
            bool userExists= true;
            clsUser user = _userManager.checkDuplicateUserName(newUserName);

            if (user.UserId == 0) { userExists = false; }
            return new JsonResult
            {
                Data = userExists,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult jsonChangeUsername(string newUserName)
        {
            var status = false;
            string updateUserName = _userManager.changeUserName(newUserName);

            if (updateUserName == CommonConstantHelper.Success)
            {
                TempData["SM"] = "সফলভাবে ব্যবহারকারীর তথ্য হালনাগাদ করা হয়েছে ";
                status = true;
            }

            else if (updateUserName == CommonConstantHelper.Error)
            {
                ModelState.AddModelError("", "Error.");
                status = false;
            }

            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion






        #endregion




    }
}
