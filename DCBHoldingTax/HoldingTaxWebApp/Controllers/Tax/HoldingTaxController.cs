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
using HoldingTaxWebApp.ViewModels.Tax;
using HoldingTaxWebApp.ViewModels;
using HoldingTaxWebApp.Manager.Constant;

namespace HoldingTaxWebApp.Controllers.Tax
{
    public class HoldingTaxController : Controller
    {
        private readonly HoldingTaxManager _holdingTaxManager;
        private readonly FinancialYearGateway _financialYearGateway;
        private readonly FinancialYearManager _financialYearManager;
        private readonly DOHSAreaManager _dOHSAreaManager;
        private readonly PlotManager _plotManager;
        private readonly ConstantValueManager _constantValueManager;

        public HoldingTaxController()
        {
            _holdingTaxManager = new HoldingTaxManager();
            _financialYearGateway = new FinancialYearGateway();

            _financialYearManager = new FinancialYearManager();
            _dOHSAreaManager = new DOHSAreaManager();
            _plotManager = new PlotManager();
            _constantValueManager = new ConstantValueManager();
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

            var data = _holdingTaxManager.GetAllHoldingTaxIndex(null, null, null);
            if (data != null && data.Count > 0)
            {
                var firstRow = data.Take(1).FirstOrDefault();
                ViewBag.TotalNetPayableAmount_ = firstRow.TotalNetPayableAmount ?? 0;
                ViewBag.TotalPaidAmount_ = firstRow.TotalPaidAmount ?? 0;
                ViewBag.TotalUnPaidAmount_ = firstRow.TotalUnPaidAmount ?? 0;
            }
            else
            {
                ViewBag.TotalNetPayableAmount_ = null;
                ViewBag.TotalPaidAmount_ = null;
                ViewBag.TotalUnPaidAmount_ = null;
            }

            if (Session[CommonConstantHelper.UserTypeId].ToString() == "2")
            {
                var HolderId = Convert.ToInt32(Session[CommonConstantHelper.HolderId]);
                ViewBag.ListOfData = _holdingTaxManager.GetAllHoldingTaxForHolder(HolderId);
                ViewBag.IsHolder = "yes";
            }
            else
            {
                ViewBag.ListOfData = data;
                ViewBag.IsHolder = "no";
            }



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
                    ViewBag.ListOfData = null;
                    ViewBag.TotalNetPayableAmount_ = null;
                    ViewBag.TotalPaidAmount_ = null;
                    ViewBag.TotalUnPaidAmount_ = null;

                    var firstRow = data.Take(1).FirstOrDefault();
                    ViewBag.TotalNetPayableAmount = firstRow.TotalNetPayableAmount ?? 0;
                    ViewBag.TotalPaidAmount = firstRow.TotalPaidAmount ?? 0;
                    ViewBag.TotalUnPaidAmount = firstRow.TotalUnPaidAmount ?? 0;

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
            ViewBag.FinancialYearId_Two = new SelectList(_financialYearGateway.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
            ViewBag.FinancialYearId_Three = new SelectList(_financialYearGateway.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
            ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo");
            return View();
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
            var constantSurcharge = _constantValueManager.GetConstantValueById().Surcharge / 100;
            //DateTime startDate = new DateTime(DateTime.Now.Year, 6, 30);
            //DateTime newstartDate = startDate.Add(new TimeSpan(23, 59, 59));

            //DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);
            //DateTime newendDate = endDate.Add(new TimeSpan(23, 59, 59));
            // decimal? rebate = holdingTax.Rebate > 0 ? holdingTax.Rebate : relatedData.RebateValue;
            holdingTax.Rebate = holdingTax.Rebate; //DateTime.Now > newstartDate && DateTime.Now < newendDate ? relatedData.RebateValue : 0;

            if (holdingTax.Rebate > 0)
                ViewBag.IsRebate = 1;
            else
                ViewBag.IsRebate = 0;

            holdingTax.TotalHoldingTaxWithRebate = holdingTax.TotalHoldingTax - (holdingTax.Rebate ?? 0);
            holdingTax.TotalHoldingTaxWithRebateAndSurcharge = holdingTax.TotalHoldingTaxWithRebate + (holdingTax.TotalHoldingTaxWithRebate * constantSurcharge);

            holdingTax.RebatePercent = relatedData.RebatePercent;
            holdingTax.WrongInfoChargePercent = relatedData.WrongInfoChargePercent;

            holdingTax.DuesPreviousYear = (holdingTax.DuesPreviousYear ?? 0) + (holdingTax.DuesFineAmount ?? 0);

            holdingTax.NetTaxPayableAmount = holdingTax.TotalHoldingTaxWithRebateAndSurcharge + holdingTax.DuesPreviousYear + (holdingTax.WrongInfoCharge ?? 0);

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

                if (string.IsNullOrEmpty(holdingTax.Remarks))
                {
                    ModelState.AddModelError("", "তথ্য হালনাগাদ করতে হলে নোট অবশ্যই পূরণ করতে হবে");
                    return View(holdingTax);
                }

                var relatableData = _holdingTaxManager.GetRebateAndWrongInfoByHoldingTaxId(holdingTax.HoldingTaxId);
                var constantSurcharge = _constantValueManager.GetConstantValueById().Surcharge / 100;

                decimal? totalHoldingTax = 0; //TotalHoldingTax
                decimal? totalRebate = 0;  // Rebate
                decimal? totalHoldingTaxtWithSurcharge = 0; // TotalTaxOfThisYear
                decimal? netTotalTax = 0;  //NetTaxPayableAmount
                decimal? wrongInfoCharge = 0; //WrongInfoCharge
                decimal? totalPreviousYearAmountAndFine = 0;
                decimal? surcharge = 0;
                DateTime startDate = new DateTime(DateTime.Now.Year, 6, 30);
                DateTime newstartDate = startDate.Add(new TimeSpan(23, 59, 59));

                DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);
                DateTime newendDate = endDate.Add(new TimeSpan(23, 59, 59));

                holdingTax.PaymentDate = !string.IsNullOrWhiteSpace(holdingTax.StringPaymentDate)
                   ? (DateTime?)DateTime.ParseExact(holdingTax.StringPaymentDate, "dd/MM/yyyy", null)
                   : null;

                if (holdingTax.RebateInfo == "Yes")
                {
                    totalRebate = holdingTax.Rebate;
                    totalHoldingTax = holdingTax.TotalHoldingTaxWithRebate != null && holdingTax.TotalHoldingTaxWithRebate > 0
                                        ? holdingTax.TotalHoldingTaxWithRebate
                                        : relatableData.TotalHoldingTax;
                    totalHoldingTaxtWithSurcharge = holdingTax.TotalHoldingTaxWithRebateAndSurcharge != null && holdingTax.TotalHoldingTaxWithRebateAndSurcharge > 0
                                        ? holdingTax.TotalHoldingTaxWithRebateAndSurcharge
                                        : relatableData.TotalTaxOfThisYear;
                    surcharge = totalHoldingTaxtWithSurcharge - totalHoldingTax;

                }
                else
                {
                    totalHoldingTax = holdingTax.TotalHoldingTax != null && holdingTax.TotalHoldingTax > 0
                                        ? holdingTax.TotalHoldingTax
                                        : relatableData.TotalHoldingTax;
                    totalHoldingTaxtWithSurcharge = holdingTax.TotalTaxOfThisYear != null && holdingTax.TotalTaxOfThisYear > 0
                                        ? holdingTax.TotalTaxOfThisYear
                                        : relatableData.TotalTaxOfThisYear;
                    surcharge = totalHoldingTaxtWithSurcharge - totalHoldingTax;
                    if (holdingTax.PaymentDate != null)
                    {
                        if (holdingTax.PaymentDate > newstartDate && holdingTax.PaymentDate < newendDate)
                            totalRebate = relatableData.RebateValue;
                    }
                    else
                    {
                        totalRebate = 0;
                    }
                }

                //newTotalHoldingTax = totalHoldingTax - totalRebate;

                //totalHoldingTaxtWithSurcharge = newTotalHoldingTax + (newTotalHoldingTax * constantSurcharge);

                wrongInfoCharge = holdingTax.WrongInfo == "Yes" ? holdingTax.WrongInfoCharge : 0;

                totalPreviousYearAmountAndFine = holdingTax.DuesPreviousYear != null && holdingTax.DuesPreviousYear > 0 ? holdingTax.DuesPreviousYear : 0;

                netTotalTax = relatableData.NetTaxPayableAmount + wrongInfoCharge + totalPreviousYearAmountAndFine;

                if (holdingTax.NetTaxPayableAmount != null && holdingTax.NetTaxPayableAmount > 0)
                    netTotalTax = holdingTax.NetTaxPayableAmount;
                else
                    netTotalTax = relatableData.NetTaxPayableAmount + wrongInfoCharge - totalRebate;

                HoldingTax tax = new HoldingTax
                {
                    Rebate = totalRebate,//holdingTax.RebateInfo == "Yes" ? holdingTax.Rebate : 0,
                    WrongInfoCharge = wrongInfoCharge, //holdingTax.WrongInfo == "Yes" ? holdingTax.WrongInfoCharge : 0,
                    isFinalized = holdingTax.FinalizeInfo == "Yes" ? true : false,
                    PaidAmount = holdingTax.PaidAmount != null && holdingTax.PaidAmount > 0 ? holdingTax.PaidAmount : 0,
                    LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                    LastUpdated = DateTime.Now,
                    HoldingTaxId = holdingTax.HoldingTaxId,
                    NetTaxPayableAmount = netTotalTax,
                    Remarks = !string.IsNullOrWhiteSpace(holdingTax.Remarks) ? holdingTax.Remarks : null,
                    PaymentDate = holdingTax.PaymentDate,
                    TotalHoldingTax = null,
                    TotalTaxOfThisYear = null,
                    Surcharge = null
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
            InvoiceVM invoice = _holdingTaxManager.GetInvoiceId(id);

            if (invoice == null)
                return HttpNotFound();

            invoice.StringCurrentDate = $"{invoice.CurrentDate:dd/MM/yyyy}";
            invoice.StringEndOfYear = $"{invoice.EndOfYear:dd/MM/yyyy}";
            invoice.StringStartingDate = $"{invoice.StartingDate:dd/MM/yyyy}";
            invoice.StringFEndDate = $"{invoice.FEndDate:dd/MM/yyyy}";
            invoice.StringoldDate = $"{invoice.oldDate:dd/MM/yyyy}";

            //if (invoice.NetTaxPayableAmount >= invoice.PaidAmount)
            //{
            //    invoice.Ispaid = true;
            //}
            //else
            //{
            //    invoice.Ispaid = false;
            //}

            return View(invoice);
        }

        public ActionResult RegenerateTax()
        {
            ViewBag.FinancialYearId = new SelectList(_financialYearGateway.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
            ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo");
            return View();
        }

        [HttpPost]
        public JsonResult RegenerateTaxUpdate(List<QueryCommon> Items)
        {
            string status = "প্রাথমিক ত্রুটি";
            try
            {
                if (Session[CommonConstantHelper.UserId] != null && Convert.ToInt32(Session[CommonConstantHelper.UserId]) > 0)
                {
                    var trueItems = Items.Where(i => i.HoldingTaxIdStatus == true).ToList();
                    if (trueItems != null && trueItems.Count > 0)
                    {
                        foreach (var item in trueItems)
                        {
                            QueryCommon query = new QueryCommon()
                            {
                                HolderId = item.HolderId,
                                FinancialYearId = item.FinancialYearId,
                                HoldingTaxId = item.HoldingTaxId,
                                HoldingTaxIdStatus = true,
                                CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                CreateDate = DateTime.Now,
                                LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                LastUpdated = DateTime.Now
                            };

                            string returnString = _holdingTaxManager.ReGenerateTax(query);
                            if (returnString != CommonConstantHelper.Success)
                            {
                                status = "তথ্য হালনাগাদ সফল হয়নি";
                                return new JsonResult { Data = new { status } };
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
                        status = "কোনো তথ্য পাওয়া যায়নি বা টিক দেয়া হয়নি";
                        return new JsonResult { Data = new { status } };
                    }
                }
                else
                {
                    status = "সেশনের মেয়াদ শেষ";
                    return new JsonResult { Data = new { status } };
                }
            }
            catch (Exception exception)
            {
                status = exception.Message.ToString();
                return new JsonResult { Data = new { status } };
            }
        }


        public JsonResult GetAllPlotByAreaId(int AreaId)
        {
            var data = _plotManager.GetPlotByAreaId(AreaId);

            return new JsonResult { Data = data ?? null };
        }

        public JsonResult GetHolderForRegenerateTAX(int FinancialYearId, int AreaId, int PlotId)
        {
            QueryCommon query = new QueryCommon
            {
                FinancialYearId = FinancialYearId > 0 ? FinancialYearId : (int?)null,
                AreaId = AreaId > 0 ? AreaId : (int?)null,
                PlotId = PlotId > 0 ? PlotId : (int?)null
            };
            var data = _holdingTaxManager.GetHolderForRegenerateTAX(query);
            return Json(data ?? null, JsonRequestBehavior.AllowGet);
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

        public JsonResult FinalizeHoldingTax(int FinancialYearId)
        {
            var logCreId = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
            int status = _holdingTaxManager.FinalizeHoldingTax(FinancialYearId, logCreId, DateTime.Now);
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
