using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoldingTaxWebApp.Gateway.Dbo;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.Tax;
using HoldingTaxWebApp.Models.Tax;

namespace HoldingTaxWebApp.Controllers.Tax
{
    public class HoldingTaxController : Controller
    {
        private readonly HoldingTaxManager _holdingTaxManager;
        private readonly FinancialYearGateway _financialYearGateway;
        public HoldingTaxController() {
            _holdingTaxManager = new HoldingTaxManager();
            _financialYearGateway = new FinancialYearGateway();
        }


        // GET: HoldingTax
        public ActionResult Index()
        {
            try
            {
                List<HoldingTax> holdingTaxes = new List<HoldingTax>();

                if (Session[CommonConstantHelper.UserTypeId].ToString() == "2")
                {
                    var HolderId = Convert.ToInt32(Session[CommonConstantHelper.HolderId]);
                    holdingTaxes = _holdingTaxManager.GetAllHoldingTaxForHolder(HolderId);

                }
                else if (Session[CommonConstantHelper.UserTypeId].ToString() == "1")
                {
                    holdingTaxes = _holdingTaxManager.GetAllHoldingTax();
                }
                else
                {
                    return RedirectToAction("LogIn", "Account");
                }

                return View(holdingTaxes.ToList());
            }
            catch
            {
                TempData["EM"] = "Session Expired or Internal Error. {Primary User Secondary Index}";
                return RedirectToAction("LogIn", "Account");
            }
        }

        // GET: HoldingTax/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HoldingTax/Create
        public ActionResult Create()
        {
            ViewBag.FinancialYearId = new SelectList(_financialYearGateway.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
            return View();
        }

        // POST: HoldingTax/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ViewBag.FinancialYearId = new SelectList(_financialYearGateway.GetAllFinancialYear(), "FinancialYearId", "FinancialYearName");

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

        // GET: HoldingTax/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HoldingTax/Edit/5
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

        // GET: HoldingTax/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public JsonResult GenerateTax(int FinancialYearId)
        {
            int status = _holdingTaxManager.GenerateTax(FinancialYearId);
            
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}
