using HoldingTaxWebApp.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers
{
    public class SPGController : Controller
    {

        private readonly SPGPaymentManager _sPGPayment;

        public SPGController()
        {
            _sPGPayment = new SPGPaymentManager();
        }

        // GET: SPG
        public ActionResult Index()
        {
            try
            {
                return View(_sPGPayment.GetSPGTransaction());
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
            }
        }


        public ActionResult Details(long id)
        {
            try
            {
                if (id <= 0)
                    return HttpNotFound();

                return View(_sPGPayment.GetSPGTransactionById(id));
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
            }
        }
    }
}