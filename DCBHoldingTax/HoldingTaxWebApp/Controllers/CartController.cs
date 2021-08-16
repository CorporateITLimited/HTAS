using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.PaymentGateway;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Tax;
using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Models.Tax;

namespace HoldingTaxWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly FinancialYearManager _yearManager;
        private readonly HoldingTaxManager _holdingTaxManager;
        private readonly HoldingManager _holdingManager;
        private readonly TranscationManager _transcationManager;

        public CartController()
        {
            _yearManager = new FinancialYearManager();
            _holdingTaxManager = new HoldingTaxManager();
            _holdingManager = new HoldingManager();
            _transcationManager = new TranscationManager();
        }

        // GET: Cart
        public ActionResult ProductList()
        {
            return View();
        }


        public ActionResult Checkout(int? HoldingTaxId, string FinancialYear)
        {
            if (HoldingTaxId != null && HoldingTaxId > 0)
            {
                var holdingTaxData = _holdingTaxManager.GetHoldingTaxById(HoldingTaxId ?? default(int));
                var holderData = _holdingManager.GetHolderById(holdingTaxData.HolderId);

                string spaceLessYear = FinancialYear.Replace(" ", "");
                string YearFirstPart = spaceLessYear.Substring(2, 2);
                string YearSecondPart = spaceLessYear.Substring(7, 2);
                var TransactionCode = YearFirstPart + YearSecondPart + holdingTaxData.HolderId.ToString("D5") + PasswordHelper.TransactionID(4);
                if (_transcationManager.IsTransactionCodeExist(TransactionCode))
                    TransactionCode = YearFirstPart + YearFirstPart + holdingTaxData.HolderId.ToString("D5") + PasswordHelper.TransactionID(4);


                var productName = "Holding Tax of Mr./Mrs. " + holderData.HolderName + " for Financial Year " + spaceLessYear;

                var relatableData = _holdingTaxManager.GetRebateAndWrongInfoByHoldingTaxId(HoldingTaxId ?? default(int));
                DateTime startDate = new DateTime(DateTime.Now.Year, 6, 30);
                DateTime newstartDate = startDate.Add(new TimeSpan(23, 59, 59));
                DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);
                DateTime newendDate = endDate.Add(new TimeSpan(23, 59, 59));

                decimal? totalRebate = 0;
                decimal? netTotalTax = 0;
                netTotalTax = holdingTaxData.NetTaxPayableAmount;
                if (DateTime.Now > newstartDate && DateTime.Now < newendDate)
                {
                    totalRebate = relatableData.RebateValue;
                    netTotalTax = netTotalTax - totalRebate;
                }

                var price = netTotalTax;//holdingTaxData.NetTaxPayableAmount;

                var baseUrl = Request.Url.Port > 0
                    ? Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port
                    : Request.Url.Scheme + "://" + Request.Url.Host;


                TransactionPayment transactionPayment = new TransactionPayment()
                {
                    HoldingTaxId = HoldingTaxId,
                    IPAddressDetails = Session["_ipDetails"].ToString(),
                    IsSuccessfulTransaction = false,
                    LastUpdated = DateTime.Now,
                    LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                    ProductName = productName,
                    RequestValidationID = null,
                    TransactionAmount = price,
                    TransactionCode = TransactionCode,
                    TransactionCurrency = "BDT",
                    TransactionDate = DateTime.Now,
                    TransactionId = 0
                };

                int count = _transcationManager.InsertTranscation(transactionPayment);


                // CREATING LIST OF POST DATA
                NameValueCollection PostData = new NameValueCollection();

                PostData.Add("total_amount", $"{price}");
                PostData.Add("tran_id", $"{TransactionCode}");
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
            else
            {
                TempData["SM"] = "bad http request";
                return RedirectToAction("Index", "HoldingTax");
            }
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

            var data = _transcationManager.GetTranscationByTransactionCode(TrxID);

            string amount = data.TransactionAmount.ToString();// Convert.ToString(Session["_price_"]);
            string currency = data.TransactionCurrency.ToString();
            long transactionId = data.TransactionId;

            var storeId = "citl61129439348f4";
            var storePassword = "citl61129439348f4@ssl";
            string EncodedValID = HttpUtility.UrlEncode(Request.Form["val_id"]);

            SSLCommerz sslcz = new SSLCommerz(storeId, storePassword, true);
            var resonse = sslcz.OrderValidate(TrxID, amount, currency, Request); /// request changes 

            TransactionPayment transactionPayment = new TransactionPayment()
            {
                HoldingTaxId = 0,
                IPAddressDetails = null,
                IsSuccessfulTransaction = true,
                LastUpdated = null,//DateTime.Now,
                LastUpdatedBy = null, //Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                ProductName = null,
                RequestValidationID = EncodedValID,
                TransactionAmount = null,
                TransactionCode = null,
                TransactionCurrency = null,
                TransactionDate = null,
                TransactionId = transactionId
            };

            var relatableData = _holdingTaxManager.GetRebateAndWrongInfoByHoldingTaxId(data.HoldingTaxId ?? default(int));
            DateTime startDate = new DateTime(DateTime.Now.Year, 6, 30);
            DateTime newstartDate = startDate.Add(new TimeSpan(23, 59, 59));
            DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);
            DateTime newendDate = endDate.Add(new TimeSpan(23, 59, 59));

            decimal? totalRebate = 0;
            decimal? netTotalTax = 0;
            netTotalTax = relatableData.NetTaxPayableAmount;
            if (DateTime.Now > newstartDate && DateTime.Now < newendDate)
            {
                totalRebate = relatableData.RebateValue;
                netTotalTax = netTotalTax - totalRebate;
            }

            HoldingTax tax = new HoldingTax
            {
                Rebate = totalRebate,//holdingTax.RebateInfo == "Yes" ? holdingTax.Rebate : 0,
                PaidAmount = data.TransactionAmount,
                NetTaxPayableAmount = netTotalTax,
                HoldingTaxId = data.HoldingTaxId ?? default(int)
            };

            string updateString = _holdingTaxManager.UpdateTaxForClient(tax);

            TempData["SM"] = _transcationManager.UpdateTranscation(transactionPayment);

            var successInfo = $"Validation Response: {resonse}";
            ViewBag.SuccessInfo = successInfo;

            Session["_holdingTaxId"] = data.HoldingTaxId;

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