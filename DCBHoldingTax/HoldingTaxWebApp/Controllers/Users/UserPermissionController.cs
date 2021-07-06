using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager;
using HoldingTaxWebApp.Manager.Users;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HoldingTaxWebApp.Controllers.Users
{
    public class UserPermissionController : Controller
    {

        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly AccountManager _accountManager;
        private readonly UserManager _userManager;


        public UserPermissionController()
        {
            _accountManager = new AccountManager();
            _userManager = new UserManager();
            if (System.Web.HttpContext.Current.Session["ListofPermissions"] != null)
            {
                List<UserPermission> userPermisson = (List<UserPermission>)System.Web.HttpContext.Current.Session["ListofPermissions"];
                var single_permission = userPermisson.Where(p => p.ControllerName == "UserPermissionController").FirstOrDefault();
                if (single_permission.ReadWriteStatus != null && single_permission.CanAccess != null)
                {
                    if (single_permission.CanAccess == true)
                    {
                        CanAccess = true;
                    }
                    if (single_permission.ReadWriteStatus == true)
                    {
                        CanReadWrite = true;
                    }
                }
            }
        }
       

        [HttpGet]
        public ActionResult Details(int id)
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                  && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                  && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess)
                {
                    try
                    {

                        var singleData = new UserPermission();
                        List<UserPermission> uP = _userManager.GetUserPermissionListByUserId(id);
                        if (uP != null && uP.Count > 0)
                        {
                            foreach (var item in uP)
                            {
                                if (!string.IsNullOrWhiteSpace(item.UserFullName))
                                {

                                    singleData.UserFullName = item.UserFullName;
                                    singleData.StringCreateDate = $"{item.CreateDate:dd/MM/yyyy hh:mm tt}" ?? "";
                                    singleData.CreatedByUserName = item.CreatedByUserName;
                                    singleData.LastUpdated = item.LastUpdated;
                                    singleData.StringLastUpdated = $"{item.LastUpdated:dd/MM/yyyy hh:mm tt}" ?? "";
                                    singleData.UpdatedByUserName = item.UpdatedByUserName;
                                    singleData.UserId = item.UserId;
                                    break;
                                }
                            }
                        }
                        ViewBag.SingleData = singleData;

                        if (uP == null)
                            return HttpNotFound();

                        return View(uP);

                    }
                    catch (Exception exception)
                    {
                        TempData["EM"] = "error | " + exception.Message.ToString();
                        return View();
                    }
                }
                else
                {
                    TempData["PM"] = "Permission Denied.";
                    return RedirectToAction("Index", "Home");
                }


            }
            else
            {
                TempData["EM"] = "Session Expired.";
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                 && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                 && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess && CanReadWrite)
                {
                    ViewBag.UserId = new SelectList(_userManager.GetAllUserListForPermissionInsert(), "UserId", "UserFullName");
                    return View();
                }
                else
                {
                    TempData["PM"] = "Permission Denied.";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["EM"] = "Session Expired.";
                return RedirectToAction("LogIn", "Account");
            }

        }


        [HttpPost]
        public JsonResult InsertOrUpdateUserPermission(List<UserPermission> Items)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    string status = "error";

                    if (Session[CommonConstantHelper.UserId] != null && Convert.ToInt32(Session[CommonConstantHelper.UserId]) > 0)
                    {
                        if (Items != null && Items.Count > 0)
                        {
                            DateTime? dateTime = DateTime.Now;
                            foreach (var item in Items)
                            {
                                UserPermission userPermission = new UserPermission()
                                {
                                    PermissionId = item.PermissionId,
                                    UserId = item.UserId,
                                    ControllerId = item.ControllerId,
                                    CanAccess = item.CanAccess,
                                    ReadWriteStatus = item.ReadWriteStatus,
                                    CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                    CreateDate = item.PermissionId > 0 ? null : dateTime,
                                    LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                    LastUpdated = dateTime
                                };

                                string returnString = "";
                                if (userPermission.PermissionId > 0)
                                    returnString = _userManager.UserPermissionUpdate(userPermission);
                                else
                                    returnString = _userManager.UserPermissionInsert(userPermission);

                                if (returnString != CommonConstantHelper.Success)
                                {
                                    status = "failed";
                                    break;
                                }
                                else
                                {
                                    status = "success";
                                }
                            }
                            return new JsonResult { Data = new { status } };
                        }
                        else
                        {
                            status = "empty";
                            return new JsonResult { Data = new { status } };
                        }
                    }
                    else
                    {
                        status = "no_user";
                        return new JsonResult
                        {
                            Data = new
                            {
                                status
                            }
                        };
                    }


                }
                catch (Exception exception)
                {
                    TempData["EM"] = "error | " + exception.Message.ToString();
                    return new JsonResult { Data = "error" };
                }
            }
            else
            {
                TempData["PM"] = "Permission Denied.";
                return new JsonResult { Data = "_denied_" };
            }
        }

        public JsonResult GetUserPermissionData(int UserId)
        {
            List<UserPermission> sortedList = new List<UserPermission>();
            var fromDb = _userManager.GetUserPermissionListByUserId(UserId);
            sortedList = fromDb != null && fromDb.Count > 0 ? fromDb : _userManager.GetControllerList();
            return new JsonResult
            {
                Data = sortedList.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public string FindController()
        {
            return RouteData.Values["controller"].ToString();
        }

    }
}