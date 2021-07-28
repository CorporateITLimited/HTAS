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
                DOHSArea area = new DOHSArea();

                return View(area);
            }
            catch (Exception exception)
            {
                TempData["SM"] = "error | " + exception.Message.ToString();
                return RedirectToAction("Index", "DOHSArea");
            }
        }
    }
}