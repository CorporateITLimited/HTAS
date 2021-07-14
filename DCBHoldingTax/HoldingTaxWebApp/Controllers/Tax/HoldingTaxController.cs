using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoldingTaxWebApp.Manager.Tax;

namespace HoldingTaxWebApp.Controllers.Tax
{
    public class HoldingTaxController : Controller
    {
        private readonly HoldingTaxManager _holdingTaxManager;
        public HoldingTaxController() {
            _holdingTaxManager = new HoldingTaxManager();
        }


        // GET: HoldingTax
        public ActionResult Index()
        {

            return View();
        }

        // GET: HoldingTax/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HoldingTax/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HoldingTax/Create
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

        // POST: HoldingTax/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
