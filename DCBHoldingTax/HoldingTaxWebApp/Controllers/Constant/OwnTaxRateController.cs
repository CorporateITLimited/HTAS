using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager;
using HoldingTaxWebApp.Manager.Constant;
using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Manager.Users;
using HoldingTaxWebApp.Models.Constant;
using HoldingTaxWebApp.Models.Users;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Constant
{
    public class OwnTaxRateController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly OwnTaxRateManager _ownTaxRateManager;
        private readonly CommonListManager _commonListManager;

        public OwnTaxRateController()
        {
            _ownTaxRateManager = new OwnTaxRateManager();
            _commonListManager = new CommonListManager();
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
                return View(_ownTaxRateManager.GetList());
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

                var rate = _ownTaxRateManager.GetById(id);

                if (rate == null)
                    return HttpNotFound();

                return View(rate);

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
            ViewBag.Mill_Civil = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName");
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
        public ActionResult Create(OwnTaxRate rate)
        {
            //if (CanAccess && CanReadWrite)
            //{
            try
            {

                if (rate == null)
                    return HttpNotFound();

                ViewBag.Mill_Civil = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName", rate.Mill_Civil);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", rate.IsActive);

                if (rate.Mill_Civil <= 0)
                {
                    ModelState.AddModelError("", "মালিকানার ধরন ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(rate);
                }

                if (rate.AreaSF <= 0)
                {
                    ModelState.AddModelError("", "ফ্লোরের আয়তন ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(rate);
                }

                if (rate.Amount <= 0)
                {
                    ModelState.AddModelError("", "প্রতিমাসের ভাড়া ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(rate);
                }

                rate.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                rate.CreateDate = DateTime.Now;
                rate.IsActive = true;
                rate.IsDeleted = false;
                rate.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                rate.LastUpdated = DateTime.Now;


                string addRate = _ownTaxRateManager.Insert(rate);

                if (addRate == CommonConstantHelper.Success)
                {
                    TempData["SM"] = "সফলভাবে সাবমিট সম্পন্ন করা হয়েছে";
                    return RedirectToAction("Index", "OwnTaxRate");
                }
                else if (addRate == CommonConstantHelper.Conflict)
                {
                    ModelState.AddModelError("", "নির্বাচিত মালিকানার ধরণের ডাটা বিদ্যমান রয়েছে");
                    return View(rate);
                }
                else if (addRate == CommonConstantHelper.Error)
                {
                    ModelState.AddModelError("", "Error");
                    TempData["EM"] = "Error.";
                    return View(rate);
                }
                else if (addRate == CommonConstantHelper.Failed)
                {
                    ModelState.AddModelError("", "Failed");
                    return View(rate);
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

                var rate = _ownTaxRateManager.GetById(id);

                if (rate == null)
                    return HttpNotFound();

                ViewBag.Mill_Civil = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName", rate.Mill_Civil);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", rate.IsActive);

                return View(rate);
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
        public ActionResult Edit(OwnTaxRate rate)
        {
            //if (CanAccess && CanReadWrite)
            //{
            try
            {

                if (rate == null)
                    return HttpNotFound();

                ViewBag.Mill_Civil = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName", rate.Mill_Civil);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", rate.IsActive);

                if (rate.OwnTaxRateId <= 0)
                {
                    ModelState.AddModelError("", "নিরাপত্তা ভঙ্গ");
                    return View(rate);
                }

                if (rate.Mill_Civil <= 0)
                {
                    ModelState.AddModelError("", "মালিকানার ধরন ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(rate);
                }

                if (rate.AreaSF <= 0)
                {
                    ModelState.AddModelError("", "ফ্লোরের আয়তন ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(rate);
                }

                if (rate.Amount <= 0)
                {
                    ModelState.AddModelError("", "প্রতিমাসের ভাড়া ঘরটি অবশ্যই পূরণ করতে হবে");
                    return View(rate);
                }

                rate.CreatedBy = null;
                rate.CreateDate = null;
                rate.IsDeleted = null;
                rate.IsActive = true;
                rate.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                rate.LastUpdated = DateTime.Now;


                string addRate = _ownTaxRateManager.Update(rate);

                if (addRate == CommonConstantHelper.Success)
                {
                    TempData["SM"] = "সফলভাবে হালনাগাদ সম্পন্ন করা হয়েছে";
                    return RedirectToAction("Index", "OwnTaxRate");
                }
                else if (addRate == CommonConstantHelper.Conflict)
                {
                    ModelState.AddModelError("", "নির্বাচিত মালিকানার ধরণের ডাটা বিদ্যমান রয়েছে");
                    return View(rate);
                }
                else if (addRate == CommonConstantHelper.Error)
                {
                    ModelState.AddModelError("", "Error");
                    TempData["EM"] = "Error.";
                    return View(rate);
                }
                else if (addRate == CommonConstantHelper.Failed)
                {
                    ModelState.AddModelError("", "Failed");
                    return View(rate);
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


        //


    }
}
