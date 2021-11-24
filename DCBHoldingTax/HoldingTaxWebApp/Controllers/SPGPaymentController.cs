using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers
{
    public class SPGPaymentController : Controller
    {
        private const string _uriStringGetSessionKey = "https://spg.sblesheba.com:6314/api/SpgService/GetSessionKey";
        private static readonly HttpClient _apiClient = new HttpClient();


        public ActionResult Index()
        {

            //NameValueCollection PostData = new NameValueCollection
            //{
            //    { "AccessUser.[].userName", "bdtaxUser2014" },
            //    { "AccessUser.[].password", "duUserPayment2014" },
            //    { "strUserId", "bdtaxUser2014" },
            //    { "strPassKey", "duUserPayment2014" },
            //    { "strRequestId", "1231231233" }, // change
            //    { "strAmount", "10" }, // change
            //    { "strTranDate", $"{DateTime.Now}" }, // change
            //    { "strAccounts", "0002601020864" }
            //};
            ////HttpUtility.UrlEncode(refe_id);
            ////string _url = _uriStringGetSessionKey + "?bank_tran_id=" + EncodedBankTranId + "&refund_amount=" + refund_amount + "&refund_remarks=" + EncodedRefundRemarks + "&refe_id=" + EncodedReferId + "&store_id=" + EncodedStoreID + "&store_passwd=" + EncodedStorePassword + "&v=1&format=json";

            ////HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            ////HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //string response = Post(_uriStringGetSessionKey, PostData);



            return View();
        }


        public static string Post(string uri, NameValueCollection PostData)
        {
            byte[] response = null;
            using (WebClient client = new WebClient())
            {
                response = client.UploadValues(uri, PostData);
            }
            return System.Text.Encoding.UTF8.GetString(response);
        }
    }
}