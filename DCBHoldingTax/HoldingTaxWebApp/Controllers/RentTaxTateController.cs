using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoldingTaxWebApp.Gateway;
using HoldingTaxWebApp.Models;
using HoldingTaxWebApp.Models.Constant;
using HoldingTaxWebApp.ViewModels;

namespace HoldingTaxWebApp.Controllers
{
    public class RentTaxTateController : Controller
    {
        private readonly ConstantGateway _constantGateway;

        public RentTaxTateController()
        {
            _constantGateway = new ConstantGateway();
        }


        // GET: RentTaxTate
        public ActionResult Index()
        {
            try
            {
                List<RentTaxRate> rentTaxRatesList = new List<RentTaxRate>();
                rentTaxRatesList = _constantGateway.GetAllRentTaxRates();

                return View(rentTaxRatesList.ToList()) ;
            }
            catch
            {
                TempData["EM"] = "Session Expired or Internal Error. {Primary User Secondary Index}";
                return RedirectToAction("LogIn", "Account");
            }
        }

        // GET: RentTaxTate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RentTaxTate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RentTaxTate/Create
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

        // GET: RentTaxTate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RentTaxTate/Edit/5
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

        // GET: RentTaxTate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RentTaxTate/Delete/5
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
    }
}
