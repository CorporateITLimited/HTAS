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
        public HoldingTaxController()
        {
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
            Session["HoldingTaxId"] = id > 0 ? id : (object)null;
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id <= 0)
                return HttpNotFound();

            HoldingTax holdingTax = _holdingTaxManager.GetHoldingTaxById(id);
            if (holdingTax == null)
                return HttpNotFound();

            var relatedData = _holdingTaxManager.GetRebateAndWrongInfoByHoldingTaxId(id);

            holdingTax.RebatePercent = relatedData.RebatePercent;
            holdingTax.WrongInfoChargePercent = relatedData.WrongInfoChargePercent;


            return View(holdingTax);
        }

        // POST: HoldingTax/Edit/5
        [HttpPost]
        public ActionResult Edit(HoldingTax holdingTax)
        {
            try
            {
                if (holdingTax == null)
                    return HttpNotFound();

                //decimal? totalRebate = 0;
                //decimal? totalWrongCharge = 0;
                //decimal? totalPaidAmount = 0;
                //decimal? subTotalTax = 0;
                //var relatableData = _holdingTaxManager.GetRebateAndWrongInfoByHoldingTaxId(holdingTax.HoldingTaxId);

                //subTotalTax = relatableData.SubTotalHoldingTax;

                //if (holdingTax.RebateInfo == "Yes")
                //{
                //    totalRebate = relatableData.RebateValue;
                //}
                //else
                //{
                //    totalRebate = 0;
                //}

                //if (holdingTax.WrongInfo == "Yes")
                //{
                //    totalWrongCharge = relatableData.WrongInfoChargeValue;
                //}
                //else
                //{
                //    totalWrongCharge = 0;
                //}

                //if (holdingTax.PaidAmount != null && holdingTax.PaidAmount > 0)
                //{
                //    totalPaidAmount = holdingTax.PaidAmount;
                //}
                //else
                //{
                //    totalPaidAmount = 0;
                //}


                HoldingTax tax = new HoldingTax
                {
                    Rebate = holdingTax.RebateInfo == "Yes" ? holdingTax.Rebate : 0,
                    WrongInfoCharge = holdingTax.WrongInfo == "Yes" ? holdingTax.WrongInfoCharge : 0,
                    isFinalized = holdingTax.FinalizeInfo == "Yes" ? true : false,
                    PaidAmount = holdingTax.PaidAmount != null && holdingTax.PaidAmount > 0 ? holdingTax.PaidAmount : 0,
                    LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                    LastUpdated = DateTime.Now,
                    HoldingTaxId = holdingTax.HoldingTaxId,
                    NetTaxPayableAmount = holdingTax.SubTotalHoldingTax
                };


                string updateString = _holdingTaxManager.UpdateTax(tax);

                if (updateString == CommonConstantHelper.Success)
                {
                    TempData["SM"] = "সফলভাবে হালনাগাদ করা হয়েছে";
                    // return View();
                    return RedirectToAction("Index", "HoldingTax");
                }
                else if (updateString == CommonConstantHelper.Error)
                {
                    ModelState.AddModelError("", "Error");
                    TempData["EM"] = "Error.";
                    return View(holdingTax);
                }
                else if (updateString == CommonConstantHelper.Failed)
                {
                    ModelState.AddModelError("", "Failed");
                    return View(holdingTax);
                }
                else
                {
                    ModelState.AddModelError("", "Error Not Recognized");
                    return View(holdingTax);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message.ToString());
                return View(holdingTax);
            }
        }


        public JsonResult GetRebateAndWrongInfoByHoldingTaxId(int id)
        {
            return new JsonResult { Data = _holdingTaxManager.GetRebateAndWrongInfoByHoldingTaxId(id) };
        }

        public JsonResult GenerateTax(int FinancialYearId)
        {
            int status = _holdingTaxManager.GenerateTax(FinancialYearId);

            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}
