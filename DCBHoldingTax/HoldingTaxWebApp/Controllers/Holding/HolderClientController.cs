using HoldingTaxWebApp.Manager.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Holding
{
    public class HolderClientController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly FinancialYearManager _financialYearManager;

        public HolderClientController()
        {
            _financialYearManager = new FinancialYearManager();
        }

        // GET: HolderClient
        public ActionResult Index()
        {
            //ViewBag.FinancialYearId =  new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
            return View(_financialYearManager.GetAllFinancialYear());
        }

        public ActionResult GetHoldersTaxInformation(int id)
        {
            Session["FinancialYearId"] = id > 0 ? id : (object)null;

            return View();
        }
    }
}