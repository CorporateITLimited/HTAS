using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Manager.Plots;
using HoldingTaxWebApp.Manager.Users;
using HoldingTaxWebApp.Models.Holding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Holding
{
    public class NoticeController : Controller
    {
        private readonly NoticeManager _noticeManager;
        private readonly FinancialYearManager _financialYearManager;
        private readonly DOHSAreaManager _dOHSAreaManager;
        private readonly EmployeeManager _employeeManager;
        private readonly PlotManager _plotManager;

        public NoticeController()
        {
            _noticeManager = new NoticeManager();
            _financialYearManager = new FinancialYearManager();
            _dOHSAreaManager = new DOHSAreaManager();
            _employeeManager = new EmployeeManager();
            _plotManager = new PlotManager();
        }

        // GET: Notice
        public ActionResult NewIndex()
        {
            try
            {
                List<Notice> noticeList = new List<Notice>();

                if (Session[CommonConstantHelper.UserTypeId].ToString() == "2")
                {
                    var HolderId = Convert.ToInt32(Session[CommonConstantHelper.HolderId]);
                    noticeList = _noticeManager.GetAllNoticeForHolder(HolderId);

                }
                else
                {
                    return RedirectToAction("LogIn", "Account");
                }

                return View(noticeList.ToList());
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                TempData["EM"] = "Session Expired " + msg;
                return RedirectToAction("LogIn", "Account");
            }
        }

        public ActionResult Index()
        {
            ViewBag.FinancialYearId = new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
            ViewBag.NoticeTypeId = new SelectList(StaticDataHelper.GetNoticeTypeNameStatusForDropdown(), "Value", "Text");
            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
            ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo");

            ViewBag.ListOfData = _noticeManager.GetAllNoticeFiltering(null, null, null, null);

            return View();
        }

        public ActionResult PartialIndex(int? FinancialYearId, int? NoticeTypeId, int? AreaId, int? PlotId)
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //      && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //      && (Session[CommonConstantHelper.UserId] != null))
            //{
            try
            {
                FinancialYearId = FinancialYearId > 0 ? FinancialYearId : null;
                NoticeTypeId = NoticeTypeId > 0 ? NoticeTypeId : null;
                AreaId = AreaId > 0 ? AreaId : null;
                PlotId = PlotId > 0 ? PlotId : null;

                var data = _noticeManager.GetAllNoticeFiltering(FinancialYearId, NoticeTypeId, AreaId, PlotId);

                if (data != null && data.Count > 0)
                {
                    ViewBag.ListOfData = null;
                    return PartialView("~/Views/Notice/_PartialIndex.cshtml", data);
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

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ViewBag.FinancialYearId = new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
                ViewBag.FinancialYearId_Two = new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
                ViewBag.NoticeTypeId = new SelectList(StaticDataHelper.GetNoticeTypeNameStatusForDropdown(), "Value", "Text");
                ViewBag.NoticeTypeId_Two = new SelectList(StaticDataHelper.GetNoticeTypeNameStatusForDropdown(), "Value", "Text");
                ViewBag.EmpolyeeId = new SelectList(_employeeManager.GetAllEmployeeListForSelect(), "EmpolyeeId", "EmployeeName");
                //ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo");
                //ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo");



                var currDate = DateTime.Now;
                ViewBag.DateTimeStr = "আজকের তারিখ : " + BanglaConvertionHelper.StringEnglish2StringBanglaDate(currDate.ToString("dd/MM/yyyy"));

                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message.ToString());
                return View();
            }

        }

        [HttpPost]
        public ActionResult Create(Notice notice)
        {
            try
            {
                if (notice == null)
                    return HttpNotFound();

                ViewBag.FinancialYearId = new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear", notice.FinancialYearId);
                ViewBag.FinancialYearId_Two = new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear", notice.FinancialYearId_Two);
                ViewBag.NoticeTypeId = new SelectList(StaticDataHelper.GetNoticeTypeNameStatusForDropdown(), "Value", "Text", notice.NoticeTypeId);
                ViewBag.NoticeTypeId_Two = new SelectList(StaticDataHelper.GetNoticeTypeNameStatusForDropdown(), "Value", "Text", notice.NoticeTypeId_Two);

                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", notice.AreaId);
                ViewBag.EmpolyeeId = new SelectList(_employeeManager.GetAllEmployeeListForSelect(), "EmpolyeeId", "EmployeeName", notice.EmpolyeeId);


                var currDate = DateTime.Now;
                ViewBag.DateTimeStr = "আজকের তারিখ : " + BanglaConvertionHelper.StringEnglish2StringBanglaDate(currDate.ToString("dd/MM/yyyy"));

                if (notice.FinancialYearId <= 0)
                {
                    ModelState.AddModelError("", "আর্থিক বছর নির্বাচন করুন");
                    return View(notice);
                }

                if (notice.NoticeTypeId <= 0)
                {
                    ModelState.AddModelError("", "বিজ্ঞপ্তির ধরণ নির্বাচন করুন");
                    return View(notice);
                }

                if (notice.EmpolyeeId <= 0 || notice.EmpolyeeId == null)
                {
                    ModelState.AddModelError("", "কর্মকর্তার নাম নির্বাচন করুন");
                    return View(notice);
                }

                notice.CreateDate = DateTime.Now;
                notice.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                notice.HolderId = 0;
                notice.IsActive = true;
                notice.IsDeleted = false;
                notice.IsNoticeSent = false
;
                notice.LastUpdated = DateTime.Now;
                notice.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                notice.NoticeId = 0;
                notice.NoticeLinkName = "";
                notice.NoticeSentDate = null;
                notice.AreaId = 0;

                string sendNotice = _noticeManager.PrepareNotice(notice);

                if (sendNotice == CommonConstantHelper.Success)
                {
                    TempData["SM"] = "সফলভাবে বিজ্ঞপ্তি প্রস্তুত করা হয়েছে";
                    return RedirectToAction("CreateReport", "Notice", new { notice.FinancialYearId, notice.NoticeTypeId, notice.AreaId });
                    //return RedirectToAction("Index", "Notice");
                }
                else if (sendNotice == CommonConstantHelper.Conflict)
                {
                    ModelState.AddModelError("", "ইতিমধ্যে বিজ্ঞপ্তিটি প্রস্তুত করা হয়েছে");
                    return View(notice);
                }
                else if (sendNotice == "404")
                {
                    ModelState.AddModelError("", "নির্বাচিত বছরের জন্য এখনো গৃহকৰ প্রস্তুত করা হয়নি");
                    return View(notice);
                }
                else if (sendNotice == CommonConstantHelper.Error)
                {
                    ModelState.AddModelError("", "Error");
                    return View(notice);
                }
                else if (sendNotice == CommonConstantHelper.Failed)
                {
                    ModelState.AddModelError("", "Failed");
                    return View(notice);
                }
                else
                {
                    ModelState.AddModelError("", "Error Not Recognized");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message.ToString());
                return View();
            }
        }

        [HttpPost]
        public ActionResult NoticeSendUpdate(Notice notice)
        {
            try
            {
                if (notice == null)
                    return HttpNotFound();

                ViewBag.FinancialYearId = new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear", notice.FinancialYearId);
                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", notice.AreaId);
                ViewBag.NoticeTypeId = new SelectList(StaticDataHelper.GetNoticeTypeNameStatusForDropdown(), "Value", "Text", notice.NoticeTypeId);
                ViewBag.EmpolyeeId = new SelectList(_employeeManager.GetAllEmployeeListForSelect(), "EmpolyeeId", "EmployeeName", notice.EmpolyeeId);
                ViewBag.FinancialYearId_Two = new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear", notice.FinancialYearId_Two);
                ViewBag.NoticeTypeId_Two = new SelectList(StaticDataHelper.GetNoticeTypeNameStatusForDropdown(), "Value", "Text", notice.NoticeTypeId_Two);

                var currDate = DateTime.Now;
                ViewBag.DateTimeStr = "আজকের তারিখ : " + BanglaConvertionHelper.StringEnglish2StringBanglaDate(currDate.ToString("dd/MM/yyyy"));

                if (notice.FinancialYearId_Two <= 0)
                {
                    ModelState.AddModelError("", "আর্থিক বছর নির্বাচন করুন");
                    return View("Create", notice);
                }

                if (notice.NoticeTypeId_Two <= 0)
                {
                    ModelState.AddModelError("", "বিজ্ঞপ্তির ধরণ নির্বাচন করুন");
                    return View("Create", notice);
                }


                bool IsNoticeSentUi = false;

                //if (notice.NoticeTypeId == 1)
                //{
                //    DateTime startDate_notice_1 = new DateTime(DateTime.Now.Year, 7, 1);
                //    DateTime endDate_notice_1 = new DateTime(DateTime.Now.Year, 7, 31);
                //    DateTime newendDate_notice_1 = endDate_notice_1.Add(new TimeSpan(23, 59, 59));

                //    if (DateTime.Now >= startDate_notice_1 && DateTime.Now <= newendDate_notice_1)
                //    {
                //        IsNoticeSentUi = true;
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("", "গৃহকরের প্রাথমিক বিজ্ঞপ্তি পাঠানোর সময় এখনো হয়নি বা সময়সীমা অতিবাহিত হয়েছে");
                //        return View(notice);
                //    }
                //}


                //if (notice.NoticeTypeId == 2)
                //{
                //    DateTime startDate_notice_2 = new DateTime(DateTime.Now.Year, 11, 1);
                //    DateTime endDate_notice_2 = new DateTime(DateTime.Now.Year, 11, 30);
                //    DateTime newendDate_notice_2 = endDate_notice_2.Add(new TimeSpan(23, 59, 59));

                //    if (DateTime.Now >= startDate_notice_2 && DateTime.Now <= newendDate_notice_2)
                //    {
                //        IsNoticeSentUi = true;
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("", "রিবেটসহ গৃহকর প্রাপ্তির বিজ্ঞপ্তি পাঠানোর সময় এখনো হয়নি বা সময়সীমা অতিবাহিত হয়েছে");
                //        return View(notice);
                //    }
                //}


                //if (notice.NoticeTypeId == 3)
                //{
                //    DateTime startDate_notice_3 = new DateTime(DateTime.Now.Year, 5, 1);
                //    DateTime endDate_notice_3 = new DateTime(DateTime.Now.Year, 5, 30);
                //    DateTime newendDate_notice_3 = endDate_notice_3.Add(new TimeSpan(23, 59, 59));

                //    if (DateTime.Now >= startDate_notice_3 && DateTime.Now <= newendDate_notice_3)
                //    {
                //        IsNoticeSentUi = true;
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("", "গৃহকরের চূড়ান্ত বিজ্ঞপ্তি পাঠানোর সময় এখনো হয়নি বা সময়সীমা অতিবাহিত হয়েছে");
                //        return View(notice);
                //    }
                //}



                notice.CreateDate = null;
                notice.CreatedBy = null;
                //notice.HolderId = 0;
                notice.IsActive = null;
                notice.IsDeleted = null;
                notice.IsNoticeSent = true;
                notice.LastUpdated = DateTime.Now;
                notice.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                notice.NoticeId = 0;
                notice.NoticeLinkName = null;
                notice.NoticeSentDate = DateTime.Now;

                IsNoticeSentUi = true;

                if (!IsNoticeSentUi)
                {
                    ModelState.AddModelError("", "বিজ্ঞপ্তি পাঠানোর সময় এখনো হয়নি বা সময়সীমা অতিবাহিত হয়েছে");
                    return View("Create", notice);
                }
                else
                {
                    string sendNotice = _noticeManager.SendNotice(notice);

                    if (sendNotice == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "সফলভাবে বিজ্ঞপ্তি পাঠানো হয়েছে";
                        return RedirectToAction("Index", "Notice");
                    }
                    else if (sendNotice == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "এখনো বিজ্ঞপ্তিটি প্রস্তুত করা হয়নি");
                        return View("Create", notice);
                    }
                    else if (sendNotice == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error");
                        return View("Create", notice);
                    }
                    else if (sendNotice == CommonConstantHelper.Failed)
                    {
                        ModelState.AddModelError("", "Failed");
                        return View("Create", notice);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error Not Recognized");
                        return View("Create");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message.ToString());
                return View("Create");
            }
        }

        public ActionResult CreateReport(int FinancialYearId, int NoticeTypeId, int AreaId)
        {
            Session["FinancialYearId"] = FinancialYearId > 0 ? FinancialYearId : (object)null;
            Session["NoticeTypeId"] = NoticeTypeId > 0 ? NoticeTypeId : (object)null;
            Session["AreaId"] = AreaId > 0 ? AreaId : (object)null;
            Session["Type"] = "new";
            return View();
        }

        public JsonResult GetDataForSentNotices(int NoticeTypeId, int FinancialYearId)
        {
            var data = _noticeManager.GetAllNotice();
            var newData = data.Where(d => d.NoticeTypeId == NoticeTypeId && d.FinancialYearId == FinancialYearId).ToList();

            string areaNameList = "";
            foreach (var item in newData)
            {
                areaNameList += Convert.ToString(item.AreaName) + ",";
            }

            if (areaNameList != "")
                areaNameList = areaNameList.TrimEnd().Substring(0, areaNameList.Length - 1);

            return new JsonResult { Data = data ?? null };
        }

        public ActionResult NoticeReport(int FYID, int NTID, int HID)
        {
            Session["FinacialYearID"] = FYID > 0 ? FYID : (object)null;
            Session["NoticeTypeID"] = NTID > 0 ? NTID : (object)null;
            Session["HolderID"] = HID > 0 ? HID : (object)null;
            Session["Type"] = null;
            return View();
        }

        #region Create page Change Event Added By Hasan 09/12/2021

        public JsonResult LoadPlotData(int AreaId)
        {
            return new JsonResult
            {
                Data = _noticeManager.GetPlotByAreaId(AreaId).ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult LoadHolderData(int PlotId)
        {
            return new JsonResult
            {
                Data = _noticeManager.GetHolderByPlotId(PlotId).ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }



        #endregion



    }
}