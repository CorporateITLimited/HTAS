using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Holding;
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

        public NoticeController()
        {
            _noticeManager = new NoticeManager();
            _financialYearManager = new FinancialYearManager();
            _dOHSAreaManager = new DOHSAreaManager();
        }

        // GET: Notice
        public ActionResult Index()
        {
            try
            {
                List<Notice> noticeList = new List<Notice>();

                if (Session[CommonConstantHelper.UserTypeId].ToString() == "2")
                {
                    var HolderId = Convert.ToInt32(Session[CommonConstantHelper.HolderId]);
                    noticeList = _noticeManager.GetAllNoticeForHolder(HolderId);

                }
                else if (Session[CommonConstantHelper.UserTypeId].ToString() == "1")
                {
                    noticeList = _noticeManager.GetAllNotice();
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
                TempData["EM"] = "Session Expired or Internal Error. {Primary User Secondary Index}" + msg;
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ViewBag.FinancialYearId = new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
                ViewBag.NoticeTypeId = new SelectList(StaticDataHelper.GetNoticeTypeNameStatusForDropdown(), "Value", "Text");

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
                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", notice.AreaId);
                ViewBag.NoticeTypeId = new SelectList(StaticDataHelper.GetNoticeTypeNameStatusForDropdown(), "Value", "Text", notice.NoticeTypeId);

                if (notice.FinancialYearId <= 0)
                {
                    ModelState.AddModelError("", "আর্থিক বছর নির্বাচন করুন");
                    return View(notice);
                }

                if (notice.NoticeTypeId <= 0)
                {
                    ModelState.AddModelError("", "নোটিশ নম্বর নির্বাচন করুন");
                    return View(notice);
                }

                notice.CreateDate = DateTime.Now;
                notice.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                notice.HolderId = 0;
                notice.IsActive = true;
                notice.IsDeleted = false;
                notice.IsNoticeSent = true;
                notice.LastUpdated = DateTime.Now;
                notice.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                notice.NoticeId = 0;
                notice.NoticeLinkName = "";
                notice.NoticeSentDate = DateTime.Now;

                string sendNotice = _noticeManager.SendNotice(notice);

                if (sendNotice == CommonConstantHelper.Success)
                {
                    return RedirectToAction("Index", "Notice");
                }
                else if (sendNotice == CommonConstantHelper.Conflict)
                {
                    ModelState.AddModelError("", "Notice already sent.");
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

        public ActionResult NoticeReport(int FYID, int NTID, int HID)
        {
            Session["FinacialYearID"] = FYID > 0 ? FYID : (object)null;
            Session["NoticeTypeID"] = NTID > 0 ? NTID : (object)null;
            Session["HolderID"] = HID > 0 ? HID : (object)null;
            return View();
        }
    }
}