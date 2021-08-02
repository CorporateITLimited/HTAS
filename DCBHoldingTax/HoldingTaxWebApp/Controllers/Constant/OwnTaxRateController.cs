using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager;
using HoldingTaxWebApp.Manager.Constant;
using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Manager.Users;
using HoldingTaxWebApp.Models.Users;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Constant
{
    public class HolderUserController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly HolderUserManager _holderUserManager;
        private readonly HoldingManager _holdingManager;
        private readonly OwnTaxRateManager _ownTaxRateManager;
        private readonly CommonListManager _commonListManager;

        public HolderUserController()
        {
            _holderUserManager = new HolderUserManager();
            _holdingManager = new HoldingManager();
            //if (System.Web.HttpContext.Current.Session["ListofPermissions"] != null)
            //{
            //    List<UserPermission> userPermisson = (List<UserPermission>)System.Web.HttpContext.Current.Session["ListofPermissions"];
            //    var single_permission = userPermisson.Where(p => p.ControllerName == "UserController").FirstOrDefault();
            //    if (single_permission.ReadWriteStatus != null && single_permission.CanAccess != null)
            //    {
            //        if (single_permission.CanAccess == true)
            //        {
            //            CanAccess = true;
            //        }
            //        if (single_permission.ReadWriteStatus == true)
            //        {
            //            CanReadWrite = true;
            //        }
            //    }
            //}
        }

        [HttpGet]
        public ActionResult Index()
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //      && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //      && (Session[CommonConstantHelper.UserId] != null))
            //{
            //    if (CanAccess)
            //    {
            try
            {
                return View(_holderUserManager.GetAllHolderUserList());
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
            }
            //    }
            //    else
            //    {
            //        TempData["PM"] = "Permission Denied.";
            //        return RedirectToAction("Index", "Home");
            //    }
            //}
            //else
            //{
            //    TempData["EM"] = "Session Expired or Internal Error. {Primary User Secondary Index}";
            //    return RedirectToAction("LogIn", "Account");
            //}
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //      && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //      && (Session[CommonConstantHelper.UserId] != null))
            //{
            //    if (CanAccess)
            //    {
            try
            {
                if (id <= 0)
                    return HttpNotFound();

                HolderUser holderUser = _holderUserManager.GetHolderUserById(id);

                if (holderUser == null)
                    return HttpNotFound();

                return View(holderUser);

            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
            }
            //    }
            //    else
            //    {
            //        TempData["PM"] = "Permission Denied.";
            //        return RedirectToAction("Index", "Home");
            //    }

            //}
            //else
            //{
            //    TempData["EM"] = "Session Expired";
            //    return RedirectToAction("LogIn", "Account");
            //}
        }

        [HttpGet]
        public ActionResult Create()
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //     && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //     && (Session[CommonConstantHelper.UserId] != null))
            //{
            //    if (CanAccess && CanReadWrite)
            //    {
            ViewBag.HolderId = new SelectList(_holderUserManager.GetAllHolderListForInsert(), "HolderId", "HolderName");
            ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text");
            return View();
            //    }
            //    else
            //    {
            //        TempData["PM"] = "Permission Denied.";
            //        return RedirectToAction("Index", "Home");
            //    }
            //}
            //else
            //{
            //    TempData["EM"] = "Session Expired";
            //    return RedirectToAction("LogIn", "Account");
            //}

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HolderUser user)
        {
            //if (CanAccess && CanReadWrite)
            //{
            try
            {

                if (user == null)
                    return HttpNotFound();

                ViewBag.HolderId = new SelectList(_holderUserManager.GetAllHolderListForInsert(), "HolderId", "HolderName", user.HolderId);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text");

                if (string.IsNullOrWhiteSpace(user.UserName))
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                if (string.IsNullOrWhiteSpace(user.HashPassword))
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                if (!string.IsNullOrWhiteSpace(user.HashPassword))
                {
                    if (!user.HashPassword.Equals(user.ConfirmPassword))
                    {
                        ModelState.AddModelError("", "পাসওয়ার্ড এবং কনফার্ম পাসওয়ার্ড একই দিন");
                        return View(user);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                if (user.HolderId == 0)
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                if (string.IsNullOrWhiteSpace(user.MobileNumber))
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                user.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                user.CreateDate = DateTime.Now;
                user.IsActive = true;
                user.IsDeleted = false;
                user.IsMobileNumberConfirmed = false;
                user.IsEmailConfirmed = false;
                user.UserTypeId = 2;
                user.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                user.LastUpdated = DateTime.Now;

                user.HashPassword = PasswordHelper.EncryptPass(user.HashPassword);

                string addUser = _holderUserManager.HolderUserInsert(user);

                if (addUser == CommonConstantHelper.Success)
                {
                    return RedirectToAction("Index", "HolderUser");
                }
                else if (addUser == CommonConstantHelper.Conflict)
                {
                    ModelState.AddModelError("", "ইউজারনেইম already exist.");
                    return View(user);
                }
                else if (addUser == CommonConstantHelper.Error)
                {
                    ModelState.AddModelError("", "Error");
                    TempData["EM"] = "Error.";
                    return View(user);
                }
                else if (addUser == CommonConstantHelper.Failed)
                {
                    ModelState.AddModelError("", "Failed");
                    return View(user);
                }
                else
                {
                    ModelState.AddModelError("", "Error Not Recognized");
                    return View();
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message.ToString());
                return View();
            }
            //}
            //else
            //{
            //    TempData["PM"] = "Permission Denied.";
            //    return RedirectToAction("Index", "Home");
            //}
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //      && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //      && (Session[CommonConstantHelper.UserId] != null))
            //{

            //    if (CanAccess && CanReadWrite)
            //    {
            try
            {
                if (id <= 0)
                    return HttpNotFound();

                HolderUser holderUser = _holderUserManager.GetHolderUserById(id);

                if (holderUser == null)
                    return HttpNotFound();

                ViewBag.HolderId = new SelectList(_holderUserManager.GetAllHolderListForUpdate(), "HolderId", "HolderName", holderUser.HolderId);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", holderUser.IsActive);

                return View(holderUser);
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
            }
            //    }
            //    else
            //    {
            //        TempData["PM"] = "Permission Denied.";
            //        return RedirectToAction("Index", "Home");
            //    }
            //}
            //else
            //{
            //    TempData["EM"] = "Session Expired";
            //    return RedirectToAction("LogIn", "Account");
            //}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HolderUser user)
        {
            //if (CanAccess && CanReadWrite)
            //{
            try
            {

                if (user == null)
                    return HttpNotFound();

                ViewBag.HolderId = new SelectList(_holderUserManager.GetAllHolderListForInsert(), "HolderId", "HolderName", user.HolderId);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", user.IsActive);

                if (string.IsNullOrWhiteSpace(user.UserName))
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                if (string.IsNullOrWhiteSpace(user.HashPassword))
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                if (!string.IsNullOrWhiteSpace(user.HashPassword))
                {
                    if (!user.HashPassword.Equals(user.ConfirmPassword))
                    {
                        ModelState.AddModelError("", "পাসওয়ার্ড এবং কনফার্ম পাসওয়ার্ড একই দিন");
                        return View(user);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                if (user.HolderId == 0)
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                if (string.IsNullOrWhiteSpace(user.MobileNumber))
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    ModelState.AddModelError("", "ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(user);
                }

                user.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                user.CreateDate = DateTime.Now;
                user.IsDeleted = null;
                user.IsMobileNumberConfirmed = null;
                user.IsEmailConfirmed = null;
                user.UserTypeId = 2;
                user.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                user.LastUpdated = DateTime.Now;

                user.HashPassword = PasswordHelper.EncryptPass(user.HashPassword);

                string addUser = _holderUserManager.HolderUserUpdate(user);

                if (addUser == CommonConstantHelper.Success)
                {
                    return RedirectToAction("Index", "HolderUser");
                }
                else if (addUser == CommonConstantHelper.Conflict)
                {
                    ModelState.AddModelError("", "ইউজারনেইম already exist.");
                    return View(user);
                }
                else if (addUser == CommonConstantHelper.Error)
                {
                    ModelState.AddModelError("", "Error");
                    TempData["EM"] = "Error.";
                    return View(user);
                }
                else if (addUser == CommonConstantHelper.Failed)
                {
                    ModelState.AddModelError("", "Failed");
                    return View(user);
                }
                else
                {
                    ModelState.AddModelError("", "Error Not Recognized");
                    return View();
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message.ToString());
                return View();
            }
            //}
            //else
            //{
            //    TempData["PM"] = "Permission Denied.";
            //    return RedirectToAction("Index", "Home");
            //}
        }

        //#region Front End JS

        ////public JsonResult LoadDataForRoleList()
        ////{
        ////    List<Role> data = _roleManager.GetAllRole().ToList();
        ////    return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        ////}

        ////public JsonResult LoadDataForEmployeeList()
        ////{
        ////    List<Employee> data = _employee.GetAllEmployeeList().ToList();
        ////    return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        ////}


        public JsonResult GetAllDataByHolderId(int HolderId)
        {
            var data = _holdingManager.GetHolderById(HolderId);
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //


    }
}
