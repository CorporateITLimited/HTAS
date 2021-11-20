using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers
{
    public class SPGPaymentController : Controller
    {
        private const string _uriStringSessionKey = "https://spg.sblesheba.com:6314/api/SpgService/GetSessionKey";
        private static readonly HttpClient _apiClient = new HttpClient();


        public ActionResult Index()
        {
            return View();
        }
    }
}