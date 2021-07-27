using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Models.DBO;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Tax
{
    public class TaxReportsController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly FinancialYearManager _FinancialYearManager;

        public TaxReportsController()
        {
            _FinancialYearManager = new FinancialYearManager();

            if (System.Web.HttpContext.Current.Session["ListofPermissions"] != null)
            {
                List<UserPermission> userPermisson = (List<UserPermission>)System.Web.HttpContext.Current.Session["ListofPermissions"];
                var single_permission = userPermisson.Where(p => p.ControllerName == "TaxReportsController").FirstOrDefault();
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

        // GET: TaxReports
        public ActionResult Index(int rptId)
        {

            ViewBag.ReportId = rptId;
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                   && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                   && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess)
                {
                    clsFinancialYear year = new clsFinancialYear();
                    ViewBag.FinancialYearId = new SelectList(_FinancialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");

                    return View(year);
                }
                else
                {
                    TempData["PM"] = "Permission Denied.";
                    return RedirectToAction("LogIn", "Account");
                }
            }
            else
            {
                TempData["EM"] = "Session Expired.";
                return RedirectToAction("LogIn", "Account");
            }
        }

        // GET: TaxReports/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TaxReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaxReports/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TaxReports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaxReports/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TaxReports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaxReports/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        #region For Report Added by Hasan
        public ActionResult rptRecoverabletax(int? FinancialYearId)
        {
            if(FinancialYearId == 0)
            {
                Session["FinancialYearId"] = null;
            }
            else
            {
                Session["FinancialYearId"] = FinancialYearId;
            }
            
            return View();
        }
        public ActionResult rptPaidTax(int? FinancialYearId)
        {
            if (FinancialYearId == 0)
            {
                Session["FinancialYearId"] = null;
            }
            else
            {
                Session["FinancialYearId"] = FinancialYearId;
            }
            return View();
        }
        public ActionResult rptUnPaidTax(int? FinancialYearId)
        {
            if (FinancialYearId == 0)
            {
                Session["FinancialYearId"] = null;
            }
            else
            {
                Session["FinancialYearId"] = FinancialYearId;
            }
            return View();
        }
        public ActionResult rptTaxPlayers(int? FinancialYearId)
        {
            if (FinancialYearId == 0)
            {
                Session["FinancialYearId"] = null;
            }
            else
            {
                Session["FinancialYearId"] = FinancialYearId;
            }
            return View();
        }
        public ActionResult rptNonTaxPlayers(int? FinancialYearId)
        {
            if (FinancialYearId == 0)
            {
                Session["FinancialYearId"] = null;
            }
            else
            {
                Session["FinancialYearId"] = FinancialYearId;
            }
            return View();
        }
        #endregion

    }
}
