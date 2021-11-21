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
using HoldingTaxWebApp.Manager.Constant;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using HoldingTaxWebApp.Models;

namespace HoldingTaxWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly FinancialYearManager _yearManager;
        private readonly HoldingTaxManager _holdingTaxManager;
        private readonly HoldingManager _holdingManager;
        private readonly InitialTranscationManager _initialTrnxManager;
        private readonly PrimaryTransactionManager _primaryTrnxManager;
        private readonly ConstantValueManager _constantValueManager;

        public CartController()
        {
            _yearManager = new FinancialYearManager();
            _holdingTaxManager = new HoldingTaxManager();
            _holdingManager = new HoldingManager();
            _initialTrnxManager = new InitialTranscationManager();
            _primaryTrnxManager = new PrimaryTransactionManager();
            _constantValueManager = new ConstantValueManager();
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
                var TransactionCode = YearFirstPart + YearSecondPart + holdingTaxData.HolderId.ToString("D5") + PasswordHelper.TransactionID(7);
                if (_initialTrnxManager.IsTransactionCodeExist(TransactionCode))
                    TransactionCode = YearFirstPart + YearFirstPart + holdingTaxData.HolderId.ToString("D5") + PasswordHelper.TransactionID(4) + PasswordHelper.TransactionID(3);


                var productName = "Holding Tax of Mr./Mrs. " + holderData.HolderName + " for Financial Year " + spaceLessYear;

                var relatableData = _holdingTaxManager.GetRebateAndWrongInfoByHoldingTaxId(HoldingTaxId ?? default(int));
                HoldingTax holdingTax = _holdingTaxManager.GetHoldingTaxById(HoldingTaxId ?? default(int));

                DateTime startDate = new DateTime(DateTime.Now.Year, 6, 30);
                DateTime newstartDate = startDate.Add(new TimeSpan(23, 59, 59));
                DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);
                DateTime newendDate = endDate.Add(new TimeSpan(23, 59, 59));

                decimal? totalHoldingTax = 0; //TotalHoldingTax
                decimal? totalRebate = 0;  // Rebate
                decimal? netTotalTax = 0;  //NetTaxPayableAmount
                decimal? wrongInfoCharge = 0; //WrongInfoCharge

                if (DateTime.Now > newstartDate && DateTime.Now < newendDate)
                {
                    totalHoldingTax = relatableData.TotalHoldingTax;
                    totalRebate = relatableData.RebateValue;
                    wrongInfoCharge = holdingTax.WrongInfoCharge > 0 ? holdingTax.WrongInfoCharge : 0;
                    netTotalTax = relatableData.NetTaxPayableAmount - totalRebate + wrongInfoCharge;
                }
                else
                {
                    totalHoldingTax = relatableData.TotalHoldingTax;
                    totalRebate = 0;
                    wrongInfoCharge = holdingTax.WrongInfoCharge > 0 ? holdingTax.WrongInfoCharge : 0;
                    netTotalTax = relatableData.NetTaxPayableAmount - totalRebate + wrongInfoCharge;
                }

                var price = netTotalTax;

                var baseUrl = Request.Url.Port > 0
                    ? Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port
                    : Request.Url.Scheme + "://" + Request.Url.Host;


                InitialTransaction transactionPayment = new InitialTransaction()
                {
                    HoldingTaxId = HoldingTaxId,
                    IPAddressDetails = Session["_ipDetails"].ToString(),
                    LastUpdated = DateTime.Now,
                    LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                    ProductName = productName,
                    TransactionAmount = price,
                    TransactionCode = TransactionCode,
                    TransactionCurrency = "BDT",
                    TransactionDate = DateTime.Now,
                    TransactionId = 0,
                    ApiDirectPaymentURL = null,
                    ApiDirectPaymentURLBank = null,
                    ApiDirectPaymentURLCard = null,
                    ApiFailedReason = null,
                    ApiGatewayPageURL = null,
                    ApiRedirectGatewayURL = null,
                    ApiRedirectGatewayURLFailed = null,
                    ApiSessionKey = null,
                    ApiStatus = null
                };
                int count = _initialTrnxManager.InsertTranscation(transactionPayment);
                if (count > 0)
                {
                    Session["_TransactionId_"] = count;


                    var logCreId_Usertpye = Session[CommonConstantHelper.LogInCredentialId].ToString() + "/" + Session[CommonConstantHelper.UserTypeId].ToString();
                    var username_holderid = Session[CommonConstantHelper.UserName].ToString() + "/" + Session[CommonConstantHelper.HolderId].ToString();

                    // CREATING LIST OF POST DATA
                    NameValueCollection PostData = new NameValueCollection();

                    PostData.Add("total_amount", $"{price}");
                    PostData.Add("tran_id", $"{TransactionCode}");
                    PostData.Add("success_url", baseUrl + "/Cart/CheckoutConfirmation");
                    PostData.Add("fail_url", baseUrl + "/Cart/CheckoutFail");
                    PostData.Add("cancel_url", baseUrl + "/Cart/CheckoutCancel");

                    //PostData.Add("version", "3.00");
                    //PostData.Add("cus_name", "ABC XY");
                    //PostData.Add("cus_email", "abc.xyz@mail.co");
                    //PostData.Add("cus_add1", "Address Line On");
                    //PostData.Add("cus_add2", "Address Line Tw");
                    //PostData.Add("cus_city", "City Nam");
                    //PostData.Add("cus_state", "State Nam");
                    //PostData.Add("cus_postcode", "Post Cod");
                    //PostData.Add("cus_country", "Countr");
                    //PostData.Add("cus_phone", "0111111111");
                    //PostData.Add("cus_fax", "0171111111");
                    //PostData.Add("ship_name", "ABC XY");
                    //PostData.Add("ship_add1", "Address Line On");
                    //PostData.Add("ship_add2", "Address Line Tw");
                    //PostData.Add("ship_city", "City Nam");
                    //PostData.Add("ship_state", "State Nam");
                    //PostData.Add("ship_postcode", "Post Cod");
                    //PostData.Add("ship_country", "Countr");
                    //PostData.Add("value_a", $"{HoldingTaxId.ToString()}");
                    //PostData.Add("value_b", $"{logCreId_Usertpye.ToString()}");
                    //PostData.Add("value_c", $"{username_holderid.ToString()}");
                    //PostData.Add("value_d", "ref00");
                    //PostData.Add("shipping_method", "NO");
                    //PostData.Add("num_of_item", "1");
                    //PostData.Add("product_name", $"{productName}");
                    //PostData.Add("product_profile", "general");
                    //PostData.Add("product_category", "Demo");



                    var stCode = "du";
                    var userName = "duUser2014";
                    var password = "duUserPayment2014";
        
        // it will be sequence payment id which will start from 1 and before end;
        //var number = rand(1000000, 9999999); 
        var reqId = "0254618";
        
        // student role number/ biller id 
        var refId = "4623476874";

                    /*set transaction param array*/

                    PostData.Add("strRequestId", reqId);
                    PostData.Add("strAmount", "10");
                    PostData.Add("strTranDate", "2021-11-20 12:00:00");
                    PostData.Add("strAccounts", "");
                    PostData.Add("ContactName", "");
                    PostData.Add("ContactNo", "");
                    PostData.Add("Address", "demo add");
                    PostData.Add("purpose", "demo");
                    PostData.Add("onbehalf", "demo");


                    //-------------Change -------------------
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("userName", userName);
                    dictionary.Add("password", password);
                    dictionary.Add("Content-Type", "application/json");



                    //    var header = array(
                    //    "Content-Type: application/json"
                    //);

                    /*set transaction param array*/

                    //var data = array("AccessUser" =>credentials,
                    //           "strUserId" => credentials["userName"],
                    //           "strPassKey" => credentials["password"],
                    //           "strRequestId" => tran_param["strRequestId"],
                    //           "strAmount" => tran_param["strAmount"],
                    //           "strTranDate" => tran_param["strTranDate"],
                    //           "strAccounts" => tran_param["strAccounts"]
                    //       );


                    //we can get from email notificaton
                    //var storeId = "citl61129439348f4";
                    //var storePassword = "citl61129439348f4@ssl";
                    //var isSandboxMood = true;

                    //SSLCommerz sslcz = new SSLCommerz(storeId, storePassword, isSandboxMood);

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://spg.sblesheba.com:6314/api/SpgService/GetSessionKey");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            AccessUser = dictionary,
                            strUserId = "bdtaxUser2014",
                            strPassKey="duUserPayment2014",
                            strRequestId="1231231235",
                            strAmount="10",
                            strTranDate="2021-11-20",
                            strAccounts="0002601020864"
                        });
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }


                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    var sessionKey = "";
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        var item = serializer.Deserialize<CommonConstantHelper>(result);
                        sessionKey = item.scretKey;
                        var ss = item;
                    }

                    //string response = sslcz.InitiateTransaction(PostData);

                    return RedirectToAction("Index", "HoldingTax");
                }
                else
                {
                    TempData["SM"] = "db error";
                    return RedirectToAction("Index", "HoldingTax");
                }
            }
            else
            {
                TempData["SM"] = "bad http request";
                return RedirectToAction("Index", "HoldingTax");
            }
        }

        public ActionResult CheckoutConfirmation()
        {
            try
            {
                if (!(!string.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID"))
                {
                    ViewBag.SuccessInfo = "There some error while processing your payment. Please try again.";
                    return View();
                }
                string trnxCode = Request.Form["tran_id"];
                var trnxData = _initialTrnxManager.GetTranscationByTransactionCode(trnxCode);
                var storeId = "citl61129439348f4";
                var storePassword = "citl61129439348f4@ssl";

                SSLCommerz sslcz = new SSLCommerz(storeId, storePassword, true);
                var resonse = sslcz.OrderValidate(trnxCode, trnxData.TransactionAmount.ToString(), trnxData.TransactionCurrency.ToString(), Request); /// request changes 


                var logCreId_Usertpye = !string.IsNullOrEmpty(Request.Form["value_b"]) ? Request.Form["value_b"].ToString() : null;
                var username_holderid = !string.IsNullOrEmpty(Request.Form["value_c"]) ? Request.Form["value_c"].ToString() : null;
                if (logCreId_Usertpye != null)
                {
                    Session[CommonConstantHelper.LogInCredentialId] = Convert.ToInt32(logCreId_Usertpye.Substring(0, logCreId_Usertpye.IndexOf('/')));
                    Session[CommonConstantHelper.UserTypeId] = Convert.ToInt32(logCreId_Usertpye.Substring(logCreId_Usertpye.IndexOf('/') + 1));
                }

                if (username_holderid != null)
                {
                    Session[CommonConstantHelper.UserName] = Convert.ToString(username_holderid.Substring(0, username_holderid.IndexOf('/')));
                    Session[CommonConstantHelper.HolderId] = Convert.ToInt32(username_holderid.Substring(username_holderid.IndexOf('/') + 1));
                }

                PrimaryTransaction primaryTransaction = new PrimaryTransaction();
                primaryTransaction.Status = !string.IsNullOrEmpty(Request.Form["status"]) ? Request.Form["status"].ToString() : null;
                primaryTransaction.TranDate = !string.IsNullOrEmpty(Request.Form["tran_date"])
                            //(DateTime ?)Convert.ToDateTime(Request.Form["tran_date"].ToString()) : null,
                            ? (DateTime?)Convert.ToDateTime(Request.Form["tran_date"].ToString()) : null;
                primaryTransaction.TranId = !string.IsNullOrEmpty(Request.Form["tran_id"]) ? Request.Form["tran_id"].ToString() : null;
                primaryTransaction.ValId = !string.IsNullOrEmpty(Request.Form["val_id"]) ? Request.Form["val_id"].ToString() : null;
                primaryTransaction.Amount = !string.IsNullOrEmpty(Request.Form["amount"])
                        ? (decimal?)Convert.ToDecimal(Request.Form["amount"]) : null;
                primaryTransaction.StoreAmount = !string.IsNullOrEmpty(Request.Form["store_amount"].ToString())
                       ? (decimal?)Convert.ToDecimal(Request.Form["store_amount"]) : null;
                primaryTransaction.CardType = !string.IsNullOrEmpty(Request.Form["card_type"]) ? Request.Form["card_type"].ToString() : null;
                primaryTransaction.CardNo = !string.IsNullOrEmpty(Request.Form["card_no"]) ? Request.Form["card_no"].ToString() : null;
                primaryTransaction.Currency = !string.IsNullOrEmpty(Request.Form["currency"]) ? Request.Form["currency"].ToString() : null;
                primaryTransaction.BankTranId = !string.IsNullOrEmpty(Request.Form["bank_tran_id"]) ? Request.Form["bank_tran_id"].ToString() : null;
                primaryTransaction.CardIssuer = !string.IsNullOrEmpty(Request.Form["card_issuer"]) ? Request.Form["card_issuer"].ToString() : null;
                primaryTransaction.CardBrand = !string.IsNullOrEmpty(Request.Form["card_brand"]) ? Request.Form["card_brand"].ToString() : null;
                primaryTransaction.CardIssuerCountry = !string.IsNullOrEmpty(Request.Form["card_issuer_country"]) ? Request.Form["card_issuer_country"].ToString() : null;
                primaryTransaction.CardIssuerCountryCode = !string.IsNullOrEmpty(Request.Form["card_issuer_country_code"]) ? Request.Form["card_issuer_country_code"].ToString() : null;
                primaryTransaction.CurrencyType = !string.IsNullOrEmpty(Request.Form["currency_type"]) ? Request.Form["currency_type"].ToString() : null;
                primaryTransaction.CurrencyAmount = !string.IsNullOrEmpty(Request.Form["currency_amount"])
                        ? (decimal?)Convert.ToDecimal(Request.Form["currency_amount"].ToString()) : null;
                primaryTransaction.EmiInstalment = !string.IsNullOrEmpty(Request.Form["emi_instalment"])
                        ? (int?)Convert.ToInt32(Request.Form["emi_instalment"].ToString()) : null;
                primaryTransaction.EmiAmount = !string.IsNullOrEmpty(Request.Form["emi_amount"])
                       ? (decimal?)Convert.ToDecimal(Request.Form["emi_amount"].ToString()) : null;
                primaryTransaction.DiscountAmount = !string.IsNullOrEmpty(Request.Form["discount_amount"])
                       ? (decimal?)Convert.ToDecimal(Request.Form["discount_amount"].ToString()) : null;
                primaryTransaction.DiscountPercentage = !string.IsNullOrEmpty(Request.Form["discount_percentage"])
                        ? (decimal?)Convert.ToDecimal(Request.Form["discount_percentage"].ToString()) : null;
                primaryTransaction.DiscountRemarks = !string.IsNullOrEmpty(Request.Form["discount_remarks"]) ? Request.Form["discount_remarks"].ToString() : null;
                primaryTransaction.ValueA = !string.IsNullOrEmpty(Request.Form["value_a"]) ? Request.Form["value_a"].ToString() : null;
                primaryTransaction.ValueB = logCreId_Usertpye;
                primaryTransaction.ValueC = username_holderid;
                primaryTransaction.ValueD = !string.IsNullOrEmpty(Request.Form["value_d"]) ? Request.Form["value_d"].ToString() : null;
                primaryTransaction.RiskLevel = !string.IsNullOrEmpty(Request.Form["risk_level"])
                       ? (int?)Convert.ToInt32(Request.Form["risk_level"].ToString()) : null;
                primaryTransaction.SecondaryStatus = !string.IsNullOrEmpty(Request.Form["status"]) ? Request.Form["status"].ToString() : null;
                primaryTransaction.RiskTitle = !string.IsNullOrEmpty(Request.Form["risk_title"]) ? Request.Form["risk_title"].ToString() : null;
                primaryTransaction.CreateDate = DateTime.Now;

                var pt = _primaryTrnxManager.PrimaryTransactionGatewayInsert(primaryTransaction);


                var relatableData = _holdingTaxManager.GetRebateAndWrongInfoByHoldingTaxId(Convert.ToInt32(Request.Form["value_a"]));
                HoldingTax holdingTax = _holdingTaxManager.GetHoldingTaxById(Convert.ToInt32(Request.Form["value_a"]));

                DateTime startDate = new DateTime(DateTime.Now.Year, 6, 30);
                DateTime newstartDate = startDate.Add(new TimeSpan(23, 59, 59));
                DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);
                DateTime newendDate = endDate.Add(new TimeSpan(23, 59, 59));

                decimal? totalHoldingTax = 0; //TotalHoldingTax
                decimal? totalRebate = 0;  // Rebate
                decimal? netTotalTax = 0;  //NetTaxPayableAmount
                decimal? wrongInfoCharge = 0; //WrongInfoCharge

                if (DateTime.Now > newstartDate && DateTime.Now < newendDate)
                {
                    totalHoldingTax = relatableData.TotalHoldingTax;
                    totalRebate = relatableData.RebateValue;
                    wrongInfoCharge = holdingTax.WrongInfoCharge > 0 ? holdingTax.WrongInfoCharge : 0;
                    netTotalTax = relatableData.NetTaxPayableAmount - totalRebate + wrongInfoCharge;
                }
                else
                {
                    totalHoldingTax = relatableData.TotalHoldingTax;
                    totalRebate = 0;
                    wrongInfoCharge = holdingTax.WrongInfoCharge > 0 ? holdingTax.WrongInfoCharge : 0;
                    netTotalTax = relatableData.NetTaxPayableAmount - totalRebate + wrongInfoCharge;
                }


                HoldingTax tax = new HoldingTax
                {
                    Rebate = totalRebate,
                    WrongInfoCharge = wrongInfoCharge,
                    isFinalized = null,
                    PaidAmount = netTotalTax,
                    LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                    LastUpdated = DateTime.Now,
                    HoldingTaxId = Convert.ToInt32(Request.Form["value_a"]),
                    NetTaxPayableAmount = netTotalTax,
                    Remarks = null,
                    PaymentDate = DateTime.Now,
                    TotalHoldingTax = null,
                    TotalTaxOfThisYear = null,
                    Surcharge = null
                };


                string updateString = _holdingTaxManager.UpdateTax(tax);

                TempData["SM"] = "আপনার পেমেন্ট সফলভাবে সম্পন্ন হয়েছে";

                var successInfo = "Validation Response: {resonse}";
                ViewBag.SuccessInfo = successInfo;

                Session["_holdingTaxId"] = trnxData.HoldingTaxId;

                return View();
            }
            catch (Exception ex)
            {
                string trnxCode = Request.Form["tran_id"];
                var trnxData = _initialTrnxManager.GetTranscationByTransactionCode(trnxCode);
                var trnxStatus = "ERROR";

                InitialTransaction transactionPayment = new InitialTransaction()
                {
                    HoldingTaxId = 0,
                    IPAddressDetails = null,
                    LastUpdated = DateTime.Now,
                    LastUpdatedBy = null, //Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                    ProductName = null,
                    TransactionAmount = null,
                    TransactionCode = null,
                    TransactionCurrency = null,
                    TransactionDate = null,
                    TransactionId = trnxData.TransactionId,
                    ApiDirectPaymentURL = null,
                    ApiDirectPaymentURLBank = null,
                    ApiDirectPaymentURLCard = null,
                    ApiFailedReason = null,
                    ApiGatewayPageURL = null,
                    ApiRedirectGatewayURL = null,
                    ApiRedirectGatewayURLFailed = null,
                    ApiSessionKey = null,
                    ApiStatus = trnxStatus
                };

                string status = _initialTrnxManager.UpdateTranscation(transactionPayment);

                Session["_holdingTaxId"] = 0;

                TempData["SM"] = ex.Message.ToString();
                return View();
            }

        }

        public ActionResult CheckoutFail()
        {
            string trnxCode = Request.Form["tran_id"];
            var trnxData = _initialTrnxManager.GetTranscationByTransactionCode(trnxCode);
            var trnxStatus = Request.Form["status"];
            InitialTransaction transactionPayment = new InitialTransaction()
            {
                HoldingTaxId = 0,
                IPAddressDetails = null,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = null, //Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                ProductName = null,
                TransactionAmount = null,
                TransactionCode = null,
                TransactionCurrency = null,
                TransactionDate = null,
                TransactionId = trnxData.TransactionId,
                ApiDirectPaymentURL = null,
                ApiDirectPaymentURLBank = null,
                ApiDirectPaymentURLCard = null,
                ApiFailedReason = null,
                ApiGatewayPageURL = null,
                ApiRedirectGatewayURL = null,
                ApiRedirectGatewayURLFailed = null,
                ApiSessionKey = null,
                ApiStatus = trnxStatus
            };

            string status = _initialTrnxManager.UpdateTranscation(transactionPayment);
            if (status != CommonConstantHelper.Success)
            {
                return View();
            }
            else
            {
                ViewBag.FailInfo = "There some error while processing your payment. Please try again.";
                return View();
            }
        }

        public ActionResult CheckoutCancel()
        {
            string trnxCode = Request.Form["tran_id"];
            var trnxData = _initialTrnxManager.GetTranscationByTransactionCode(trnxCode);
            var trnxStatus = Request.Form["status"];

            InitialTransaction transactionPayment = new InitialTransaction()
            {
                HoldingTaxId = 0,
                IPAddressDetails = null,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = null, //Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                ProductName = null,
                TransactionAmount = null,
                TransactionCode = null,
                TransactionCurrency = null,
                TransactionDate = null,
                TransactionId = trnxData.TransactionId,
                ApiDirectPaymentURL = null,
                ApiDirectPaymentURLBank = null,
                ApiDirectPaymentURLCard = null,
                ApiFailedReason = null,
                ApiGatewayPageURL = null,
                ApiRedirectGatewayURL = null,
                ApiRedirectGatewayURLFailed = null,
                ApiSessionKey = null,
                ApiStatus = trnxStatus
            };

            string status = _initialTrnxManager.UpdateTranscation(transactionPayment);
            if (status != CommonConstantHelper.Success)
            {
                return View();
            }
            else
            {
                TempData["SM"] = "আপনার পেমেন্ট বাতিল করা হয়েছে";
                ViewBag.CancelInfo = "Your payment has been cancel";
                return View();
            }
        }

        public ActionResult CheckoutTest()
        {
            try
            {
                //if (!(!string.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "success"))
                //{
                //    ViewBag.SuccessInfo = "There some error while processing your payment. Please try again.";
                //    return View();
                //}
                string bankTrnxId = "210819104248mxlki7iQUe83mXn";
                decimal refundAmount = 100;
                string refundRemarks = "On an test refund";
                string referId = "#" + PasswordHelper.TransactionID(10);
                var storeId = "citl61129439348f4";
                var storePassword = "citl61129439348f4@ssl";

                SSLCommerz sslcz = new SSLCommerz(storeId, storePassword, true);
                var resonse = sslcz.RefundInitiate(bankTrnxId, refundAmount, refundRemarks, referId); /// request changes

                string APIConnect = !string.IsNullOrEmpty(Request.Form["APIConnect"]) ? Request.Form["APIConnect"].ToString() : null;
                string bank_tran_id = !string.IsNullOrEmpty(Request.Form["bank_tran_id"]) ? Request.Form["bank_tran_id"].ToString() : null;
                string trans_id = !string.IsNullOrEmpty(Request.Form["trans_id"]) ? Request.Form["trans_id"].ToString() : null;
                string refund_ref_id = !string.IsNullOrEmpty(Request.Form["refund_ref_id"]) ? Request.Form["refund_ref_id"].ToString() : null;
                string status = !string.IsNullOrEmpty(Request.Form["status"]) ? Request.Form["status"].ToString() : null;
                string errorReason = !string.IsNullOrEmpty(Request.Form["errorReason"]) ? Request.Form["errorReason"].ToString() : null;


                return View();
            }
            catch (Exception ex)
            {
                TempData["SM"] = ex.Message.ToString();
                return View();
            }
        }
    }
}