using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.PaymentGateway;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web;

namespace HoldingTaxWebApp.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult ProductList()
        {
            return View();
        }


        public ActionResult Checkout()
        {
            var productName = "HoldingTax for 2020/2021 of Mr.Anis";
            var price = 36800;
            var TransactionID = Guid.NewGuid();//PasswordHelper.TransactionID(25);

            var baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;

            // CREATING LIST OF POST DATA
            NameValueCollection PostData = new NameValueCollection();

            PostData.Add("total_amount", $"{price}");
            PostData.Add("tran_id", $"{TransactionID}");
            PostData.Add("success_url", baseUrl + "/Cart/CheckoutConfirmation");
            PostData.Add("fail_url", baseUrl + "/Cart/CheckoutFail");
            PostData.Add("cancel_url", baseUrl + "/Cart/CheckoutCancel");

            PostData.Add("version", "3.00");
            PostData.Add("cus_name", "ABC XY");
            PostData.Add("cus_email", "abc.xyz@mail.co");
            PostData.Add("cus_add1", "Address Line On");
            PostData.Add("cus_add2", "Address Line Tw");
            PostData.Add("cus_city", "City Nam");
            PostData.Add("cus_state", "State Nam");
            PostData.Add("cus_postcode", "Post Cod");
            PostData.Add("cus_country", "Countr");
            PostData.Add("cus_phone", "0111111111");
            PostData.Add("cus_fax", "0171111111");
            PostData.Add("ship_name", "ABC XY");
            PostData.Add("ship_add1", "Address Line On");
            PostData.Add("ship_add2", "Address Line Tw");
            PostData.Add("ship_city", "City Nam");
            PostData.Add("ship_state", "State Nam");
            PostData.Add("ship_postcode", "Post Cod");
            PostData.Add("ship_country", "Countr");
            PostData.Add("value_a", "ref00");
            PostData.Add("value_b", "ref00");
            PostData.Add("value_c", "ref00");
            PostData.Add("value_d", "ref00");
            PostData.Add("shipping_method", "NO");
            PostData.Add("num_of_item", "1");
            PostData.Add("product_name", $"{productName}");
            PostData.Add("product_profile", "general");
            PostData.Add("product_category", "Demo");

            //we can get from email notificaton
            var storeId = "citl61129439348f4";
            var storePassword = "citl61129439348f4@ssl";
            var isSandboxMood = true;




            SSLCommerz sslcz = new SSLCommerz(storeId, storePassword, isSandboxMood);

            string response = sslcz.InitiateTransaction(PostData);

            return Redirect(response);
        }

        public ActionResult CheckoutConfirmation()
        {
            if (!(!string.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID"))
            {
                ViewBag.SuccessInfo = "There some error while processing your payment. Please try again.";
                return View();
            }
            string TrxID = Request.Form["tran_id"];
            // AMOUNT and Currency FROM DB FOR THIS TRANSACTION
            string amount = "36800";
            string currency = "BDT";

            var storeId = "citl61129439348f4";
            var storePassword = "citl61129439348f4@ssl";
            string EncodedValID = HttpUtility.UrlEncode(Request.Form["val_id"]);

            SSLCommerz sslcz = new SSLCommerz(storeId, storePassword, true);
            var resonse = sslcz.OrderValidate(TrxID, amount, currency, Request); /// request changes 
            var successInfo = $"Validation Response: {resonse}";
            ViewBag.SuccessInfo = successInfo;

            return View();

        }

        public ActionResult CheckoutFail()
        {
            ViewBag.FailInfo = "There some error while processing your payment. Please try again.";
            return View();
        }

        public ActionResult CheckoutCancel()
        {
            ViewBag.CancelInfo = "Your payment has been cancel";
            return View();
        }
    }
}