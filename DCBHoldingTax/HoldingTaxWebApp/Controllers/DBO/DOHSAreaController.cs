using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.DBO
{
    public class DOHSAreaController : Controller
    {

        private readonly DOHSAreaManager _dOHSAreaManager;

        public DOHSAreaController()
        {
            _dOHSAreaManager = new DOHSAreaManager();
        }
        // GET: DOHSArea
        public ActionResult Index()
        {
            try
            {
                return View(_dOHSAreaManager.GetAllDOHSArea());
            }
            catch (Exception exception)
            {
                TempData["SM"] = "error | " + exception.Message.ToString();
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                if (id <= 0)
                    return HttpNotFound();
                DOHSArea area = _dOHSAreaManager.GetDOHSAreaId(id);

                return View(area);
            }
            catch (Exception exception)
            {
                TempData["SM"] = "error | " + exception.Message.ToString();
                return RedirectToAction("Index", "DOHSArea");
            }
        }

        public ActionResult Create()
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //     && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //     && (Session[CommonConstantHelper.UserId] != null))
            //{
            //if (CanAccess && CanReadWrite)
            //{
            try
            {
                ViewBag.AreaType = new SelectList(StaticDataHelper.GetAreaTypeForDropdown(), "Value", "Text");
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text");
                return View();
            }
            catch (Exception exception)
            {
                TempData["SM"] = "error | " + exception.Message.ToString();
                return RedirectToAction("Index", "DOHSArea");
            }
            //}
            //else
            //{
            //    TempData["PM"] = "Permission Denied.";
            //    return RedirectToAction("Index", "Home");
            //}
            //}
            //else
            //{
            //    TempData["EM"] = "Session Expired.";
            //    return RedirectToAction("LogIn", "Account");
            //}

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DOHSArea area)
        {
            //if (CanAccess && CanReadWrite)
            //{
            try
            {
                if (area == null)
                    return HttpNotFound();
                ViewBag.AreaType = new SelectList(StaticDataHelper.GetAreaTypeForDropdown(), "Value", "Text", area.AreaType);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", area.IsActive);

                if (string.IsNullOrWhiteSpace(area.AreaName))
                {
                    ModelState.AddModelError("", "এলাকার নাম অবশ্যই পূরণ করতে হবে");
                    return View(area);
                }

                if (area.AreaType == null || area.AreaType <= 0)
                {
                    ModelState.AddModelError("", "এলাকার ধরণ অবশ্যই পূরণ করতে হবে");
                    return View(area);
                }

                if (area.TotalArea == null || area.TotalArea <= 0)
                    area.TotalArea = 0;

                area.IsActive = true;
                area.IsDeleted = false;
                area.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                area.CreateDate = DateTime.Now;
                area.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                area.LastUpdated = DateTime.Now;
                area.CurrentFlatNumber = 0;
                area.CurrentPlotNumber = 0;


                string addArea = _dOHSAreaManager.DOHSAreaInsert(area);

                if (addArea == CommonConstantHelper.Success)
                {
                    TempData["SM"] = "সফলভাবে নতুন এলাকা সাবমিট করা হয়েছে";
                    return RedirectToAction("Index", "DOHSArea");
                }
                else if (addArea == CommonConstantHelper.Conflict)
                {
                    ModelState.AddModelError("", "এলাকার নাম ডাটাবেজে বিদ্যমান ৱয়েছে");
                    return View(area);
                }
                else if (addArea == CommonConstantHelper.Error)
                {
                    ModelState.AddModelError("", "Error");
                    TempData["EM"] = "Error.";
                    return View(area);
                }
                else if (addArea == CommonConstantHelper.Failed)
                {
                    ModelState.AddModelError("", "Failed");
                    return View(area);
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

        public ActionResult Edit(int id)
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //     && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //     && (Session[CommonConstantHelper.UserId] != null))
            //{
            //    if (CanAccess && CanReadWrite)
            //    {
            try
            {

                if (id <= 0)
                    return HttpNotFound();

                DOHSArea area = _dOHSAreaManager.GetDOHSAreaId(id);

                if (area == null)
                    return HttpNotFound();
                ViewBag.AreaType = new SelectList(StaticDataHelper.GetAreaTypeForDropdown(), "Value", "Text", area.AreaType);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", area.IsActive);

                return View(area);
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
            //    TempData["EM"] = "Session Expired.";
            //    return RedirectToAction("LogIn", "Account");
            //}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DOHSArea area)
        {
            //if (CanAccess && CanReadWrite)
            //{
            try
            {
                if (area == null)
                    return HttpNotFound();

                if (area.AreaId <= 0)
                {
                    ModelState.AddModelError("", "আইডি পাওয়া যায় নি");
                    return View(area);
                }
                ViewBag.AreaType = new SelectList(StaticDataHelper.GetAreaTypeForDropdown(), "Value", "Text", area.AreaType);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", area.IsActive);

                if (string.IsNullOrWhiteSpace(area.AreaName))
                {
                    ModelState.AddModelError("", "এলাকার নাম অবশ্যই পূরণ করতে হবে");
                    return View(area);
                }

                if (area.AreaType == null || area.AreaType <= 0)
                {
                    ModelState.AddModelError("", "এলাকার ধরণ অবশ্যই পূরণ করতে হবে");
                    return View(area);
                }

                if (area.TotalArea == null || area.TotalArea <= 0)
                    area.TotalArea = 0;

                area.IsDeleted = null;
                area.CreatedBy = null;
                area.CreateDate = null;
                area.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                area.LastUpdated = DateTime.Now;
                area.CurrentPlotNumber = null;
                area.CurrentFlatNumber = null;

                string addArea = _dOHSAreaManager.DOHSAreaUpdate(area);

                if (addArea == CommonConstantHelper.Success)
                {
                    TempData["SM"] = "সফলভাবে এলাকা হালনাগাদ করা হয়েছে";
                    return RedirectToAction("Index", "DOHSArea");
                }
                else if (addArea == CommonConstantHelper.Conflict)
                {
                    ModelState.AddModelError("", "এলাকার নাম ডাটাবেজে বিদ্যমান ৱয়েছে");
                    return View(area);
                }
                else if (addArea == CommonConstantHelper.Error)
                {
                    ModelState.AddModelError("", "Error");
                    TempData["EM"] = "Error.";
                    return View(area);
                }
                else if (addArea == CommonConstantHelper.Failed)
                {
                    ModelState.AddModelError("", "Failed");
                    return View(area);
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
            //else
            //{
            //    TempData["PM"] = "Permission Denied.";
            //    return RedirectToAction("Index", "Home");
            //}
        }

    }
}