using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.Users;
using HoldingTaxWebApp.Models.Users;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Users
{
    public class ClusterController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly ClusterManager _cluster;
        public ClusterController()
        {
            _cluster = new ClusterManager();
            CanAccess = true;
            CanReadWrite = true;
            //if (System.Web.HttpContext.Current.Session["ListofPermissions"] != null)
            //{
            //    List<UserPermission> userPermisson = (List<UserPermission>)System.Web.HttpContext.Current.Session["ListofPermissions"];
            //    var single_permission = userPermisson.Where(p => p.ControllerName == "RoleController").FirstOrDefault();
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
        public ActionResult Index()
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                  && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                  && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess)
                {
                    try
                    {
                        return View(_cluster.GetAllCluster());
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
                        if (id <= 0)
                            return HttpNotFound();

                        var vm = _cluster.GetClusterById(id);

                        if (vm == null)
                            return HttpNotFound();

                        return View(vm);

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

        public ActionResult Create()
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                 && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                 && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess && CanReadWrite)
                {

                    ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text");
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cluster cls)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text");
                    if (cls == null)
                        return HttpNotFound();

                    if (string.IsNullOrWhiteSpace(cls.ClusterName))
                    {
                        ModelState.AddModelError("", "ক্লাস্টারের নাম অবশ্যই পূরণ করতে হবে");
                        return View(cls);
                    }

                    if (string.IsNullOrWhiteSpace(cls.ClusterDetails))
                    {
                        ModelState.AddModelError("", "ক্লাস্টারের বিবরণ অবশ্যই পূরণ করতে হবে");
                        return View(cls);
                    }

                    cls.IsActive = true;
                    cls.IsDeleted = false;
                    cls.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    cls.CreateDate = DateTime.Now;
                    cls.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    cls.LastUpdated = DateTime.Now;


                    string addClusterSTR = _cluster.ClusterInsert(cls);

                    if (addClusterSTR == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "সফলভাবে নতুন ক্লাস্টার  সংযুক্ত করা হয়েছে ";
                        return RedirectToAction("Index", "Cluster");
                    }
                    else if (addClusterSTR == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "ক্লাস্টার ডেটাবেজে বিদ্যমান রয়েছে ");
                        return View(cls);
                    }
                    else if (addClusterSTR == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error");
                        TempData["EM"] = "Error.";
                        return View(cls);
                    }
                    else if (addClusterSTR == CommonConstantHelper.Failed)
                    {
                        ModelState.AddModelError("", "Failed");
                        return View(cls);
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
            }
            else
            {
                TempData["PM"] = "Permission Denied.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Edit(int id)
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                 && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                 && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess && CanReadWrite)
                {
                    try
                    {
                        if (id <= 0)
                            return HttpNotFound();
                        var cls = _cluster.GetClusterById(id);
                        ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", cls.IsActive);
                        return View(cls);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cluster cls)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", cls.IsActive);

                    if (cls == null)
                        return HttpNotFound();

                    if (cls.ClusterId <= 0)
                    {
                        ModelState.AddModelError("", "Security ID Bridge");
                        return View(cls);
                    }

                    if (string.IsNullOrWhiteSpace(cls.ClusterName))
                    {
                        ModelState.AddModelError("", "ক্লাস্টারের নাম অবশ্যই পূরণ করতে হবে");
                        return View(cls);
                    }

                    if (string.IsNullOrWhiteSpace(cls.ClusterDetails))
                    {
                        ModelState.AddModelError("", "ক্লাস্টারের বিবরণ অবশ্যই পূরণ করতে হবে");
                        return View(cls);
                    }

                    cls.IsActive = cls.IsActive;
                    cls.IsDeleted = null;
                    cls.CreatedBy = null;
                    cls.CreateDate = null;
                    cls.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    cls.LastUpdated = DateTime.Now;


                    string updateClusterSTR = _cluster.ClusterUpdate(cls);

                    if (updateClusterSTR == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "সফলভাবে ক্লাস্টার হালনাগাদ করা হয়েছে ";
                        return RedirectToAction("Index", "Cluster");
                    }
                    else if (updateClusterSTR == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "ক্লাস্টার ডেটাবেজে বিদ্যমান রয়েছে ");
                        return View(cls);
                    }
                    else if (updateClusterSTR == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error");
                        TempData["EM"] = "Error.";
                        return View(cls);
                    }
                    else if (updateClusterSTR == CommonConstantHelper.Failed)
                    {
                        ModelState.AddModelError("", "Failed");
                        return View(cls);
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
            }
            else
            {
                TempData["PM"] = "Permission Denied.";
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
