using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoldingTaxWebApp.Gateway.Dbo;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.Tax;
using HoldingTaxWebApp.Models.Tax;
using HoldingTaxWebApp.Models.Holding;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Plots;

namespace HoldingTaxWebApp.Controllers.Tax
{
    public class HoldingTaxController : Controller
    {
        private readonly HoldingTaxManager _holdingTaxManager;
        private readonly FinancialYearGateway _financialYearGateway;
        private readonly FinancialYearManager _financialYearManager;
        private readonly DOHSAreaManager _dOHSAreaManager;
        private readonly PlotManager _plotManager;

        public HoldingTaxController()
        {
            _holdingTaxManager = new HoldingTaxManager();
            _financialYearGateway = new FinancialYearGateway();

            _financialYearManager = new FinancialYearManager();
            _dOHSAreaManager = new DOHSAreaManager();
            _plotManager = new PlotManager();
        }


        // GET: HoldingTax
        public ActionResult NewIndex()
        {
            try
            {
                List<HoldingTax> holdingTaxes = new List<HoldingTax>();

                if (Session[CommonConstantHelper.UserTypeId].ToString() == "2")
                {
                    var HolderId = Convert.ToInt32(Session[CommonConstantHelper.HolderId]);
                    holdingTaxes = _holdingTaxManager.GetAllHoldingTaxForHolder(HolderId);

                }
                else
                {
                    return RedirectToAction("LogIn", "Account");
                }

                return View(holdingTaxes.ToList());
            }
            catch (Exception ex)
            {
                TempData["EM"] = "Session Expired" + ex.Message;
                return RedirectToAction("LogIn", "Account");
            }
        }

        public ActionResult Index()
        {
            ViewBag.FinancialYearId = new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
            ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo");
            return View();
        }

        public ActionResult PartialIndex(int? AreaId, int? FinancialYearId, int? PlotId)
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //      && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //      && (Session[CommonConstantHelper.UserId] != null))
            //{
            try
            {
                AreaId = AreaId > 0 ? AreaId : null;
                FinancialYearId = FinancialYearId > 0 ? FinancialYearId : null;
                PlotId = PlotId > 0 ? PlotId : null;

                var data = _holdingTaxManager.GetAllHoldingTaxIndex(AreaId, FinancialYearId, PlotId);

                if (data != null && data.Count > 0)
                {
                    var firstRow = data.Take(1).FirstOrDefault();
                    ViewBag.TotalNetPayableAmount = firstRow.TotalNetPayableAmount;
                    ViewBag.TotalPaidAmount = firstRow.TotalPaidAmount;
                    ViewBag.TotalUnPaidAmount = firstRow.TotalUnPaidAmount;

                    return PartialView("~/Views/HoldingTax/_PartialIndex.cshtml", data);
                }
                else
                {
                    return PartialView("~/Views/Home/_NoDataFound.cshtml");
                }
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return PartialView("~/Views/Home/_NoDataFound.cshtml");
                //return View();
            }
            //}
            //else
            //{
            //    TempData["EM"] = "Session Expired.";
            //    return RedirectToAction("LogIn", "Account");
            //}
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

            //DateTime startDate = new DateTime(DateTime.Now.Year, 6, 30);
            //DateTime newstartDate = startDate.Add(new TimeSpan(23, 59, 59));

            //DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);
            //DateTime newendDate = endDate.Add(new TimeSpan(23, 59, 59));

            holdingTax.Rebate = relatedData.RebateValue; //DateTime.Now > newstartDate && DateTime.Now < newendDate ? relatedData.RebateValue : 0;
            holdingTax.SubTotalHoldingTax = holdingTax.Rebate > 0 ? holdingTax.SubTotalHoldingTax - holdingTax.Rebate : holdingTax.SubTotalHoldingTax;

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


                var relatableData = _holdingTaxManager.GetRebateAndWrongInfoByHoldingTaxId(holdingTax.HoldingTaxId);

                decimal? totalRebate = 0;
                decimal? netTotalTax = 0;
                DateTime startDate = new DateTime(DateTime.Now.Year, 6, 30);
                DateTime newstartDate = startDate.Add(new TimeSpan(23, 59, 59));

                DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);
                DateTime newendDate = endDate.Add(new TimeSpan(23, 59, 59));

                holdingTax.PaymentDate = !string.IsNullOrWhiteSpace(holdingTax.StringPaymentDate)
                   ? (DateTime?)DateTime.ParseExact(holdingTax.StringPaymentDate, "dd/MM/yyyy", null)
                   : null;

                if (holdingTax.SubTotalHoldingTax != null && holdingTax.SubTotalHoldingTax > 0)
                {
                    netTotalTax = holdingTax.SubTotalHoldingTax;
                }

                if (holdingTax.RebateInfo == "Yes")
                {
                    totalRebate = holdingTax.Rebate;
                }
                else
                {
                    if (holdingTax.PaymentDate != null)
                    {
                        if (holdingTax.PaymentDate > newstartDate && holdingTax.PaymentDate < newendDate)
                        {
                            totalRebate = relatableData.RebateValue;
                            netTotalTax = netTotalTax - totalRebate;
                        }
                    }
                }


                HoldingTax tax = new HoldingTax
                {
                    Rebate = totalRebate,//holdingTax.RebateInfo == "Yes" ? holdingTax.Rebate : 0,
                    WrongInfoCharge = holdingTax.WrongInfo == "Yes" ? holdingTax.WrongInfoCharge : 0,
                    isFinalized = holdingTax.FinalizeInfo == "Yes" ? true : false,
                    PaidAmount = holdingTax.PaidAmount != null && holdingTax.PaidAmount > 0 ? holdingTax.PaidAmount : 0,
                    LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                    LastUpdated = DateTime.Now,
                    HoldingTaxId = holdingTax.HoldingTaxId,
                    NetTaxPayableAmount = netTotalTax,
                    Remarks = !string.IsNullOrWhiteSpace(holdingTax.Remarks) ? holdingTax.Remarks : null,
                    PaymentDate = holdingTax.PaymentDate
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




        public ActionResult Invoice(int id)
        {



            return View();
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


        public JsonResult GetPaidAmmChart()
        {
            List<ChartPaidAm> dataList = _holdingTaxManager.GetChartPaidAms();
            var li = dataList.Select(s => new int[] { s.MonthDate, s.MonthlyPaidAmount }).ToList();
            int[][] array = li.ToArray();
            //foreach (var item in dataList)
            //{
            //    array= array.Append(item.MonthlyPaidAmount) ([item.MonthDate,item.MonthlyPaidAmount]);
            //}
            //    var ret = new[]
            //{
            //    new { label="PageViews", data = dataList.Select(x=>new int[]{ x.MonthDate, x.MonthlyPaidAmount })},
            //    new { label="Visits", data = dataList.Select(x=>new int[]{ x.MonthDate, x.MonthlyPaidAmount })},
            //    new { label="Visitors", data = dataList.Select(x=>new int[]{ x.MonthDate, x.MonthlyPaidAmount })}

            //};

            return Json(array, JsonRequestBehavior.AllowGet);
        }
    }
}
