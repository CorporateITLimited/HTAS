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
using HoldingTaxWebApp.Models;
using System.Data;
using System.Xml;
using HoldingTaxWebApp.Gateway;
using HoldingTaxWebApp.Models.Users;
using HoldingTaxWebApp.Manager.Users;

namespace HoldingTaxWebApp.Controllers
{
    public class CartController : CommonController
    {
        private readonly FinancialYearManager _yearManager;
        private readonly HoldingTaxManager _holdingTaxManager;
        private readonly HoldingManager _holdingManager;
        private readonly InitialTranscationManager _initialTrnxManager;
        private readonly PrimaryTransactionManager _primaryTrnxManager;
        private readonly ConstantValueManager _constantValueManager;
        private readonly SPGPaymentGateway _sPGPaymentGateway;
        private readonly OtpHistoryManager _OtpHistoryManager;

        public CartController()
        {
            _yearManager = new FinancialYearManager();
            _holdingTaxManager = new HoldingTaxManager();
            _holdingManager = new HoldingManager();
            _initialTrnxManager = new InitialTranscationManager();
            _primaryTrnxManager = new PrimaryTransactionManager();
            _constantValueManager = new ConstantValueManager();
            _sPGPaymentGateway = new SPGPaymentGateway();
            _OtpHistoryManager = new OtpHistoryManager();
        }

        // GET: Cart
        [HttpPost, ValidateInput(false)]
        public ActionResult ResponseData(string Request)
        {
            ResponseData objResponseData = new ResponseData();

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(Request);


                DataSet ds = new DataSet();
                XmlReader xr = new XmlNodeReader(xmlDocument);
                ds.ReadXml(xr);

                DataTable dtResponseStatus = ds.Tables["ResponseStatus"];

                if (dtResponseStatus.Rows.Count > 0)
                {
                    objResponseData.ApiAccessUserId = dtResponseStatus.Rows[0]["ApiAccessUserId"].ToString();
                    objResponseData.AuthenticationKey = dtResponseStatus.Rows[0]["AuthenticationKey"].ToString();
                    objResponseData.TransactionId = dtResponseStatus.Rows[0]["TransactionId"].ToString();
                    objResponseData.TranDateTime = dtResponseStatus.Rows[0]["TranDateTime"].ToString();
                    objResponseData.RefTranNo = dtResponseStatus.Rows[0]["RefTranNo"].ToString();
                    objResponseData.RefTranDateTime = dtResponseStatus.Rows[0]["RefTranDateTime"].ToString();
                    objResponseData.TranAmount = dtResponseStatus.Rows[0]["TranAmount"].ToString();
                    objResponseData.PayAmount = dtResponseStatus.Rows[0]["PayAmount"].ToString();
                    objResponseData.OrgiBrCode = dtResponseStatus.Rows[0]["OrgiBrCode"].ToString();
                    objResponseData.StatusMsg = dtResponseStatus.Rows[0]["StatusMsg"].ToString();
                    objResponseData.PayMode = dtResponseStatus.Rows[0]["PayMode"].ToString();
                    objResponseData.ScrollNo = dtResponseStatus.Rows[0]["ScrollNo"].ToString();
                    objResponseData.TransactionStatus = dtResponseStatus.Rows[0]["TransactionStatus"].ToString();
                }

                if (objResponseData.TransactionStatus == "200")
                {
                    ValidationInputClass validationInputClass = new ValidationInputClass
                    {
                        RequiestNo = objResponseData.TransactionId.Substring(6, 10),
                        ReferenceDate = objResponseData.RefTranDateTime.Substring(0, 10)
                    };
                    TranVerifyResponse objTranVerifyResponse = Verification(validationInputClass);


                    if (objTranVerifyResponse.StatusCode == objResponseData.TransactionStatus)
                    {
                        objResponseData.Message = "Payment Successful";

                        SPGTransaction trnxDetails = new SPGTransaction();
                        trnxDetails.RefTranNo = dtResponseStatus.Rows[0]["RefTranNo"].ToString();
                        trnxDetails.RefTranDate = Convert.ToDateTime(dtResponseStatus.Rows[0]["RefTranDateTime"].ToString());
                        trnxDetails.TranAmount = dtResponseStatus.Rows[0]["TranAmount"].ToString();

                        if (!trnxDetails.TranAmount.Contains('.'))
                            trnxDetails.TranAmount = trnxDetails.TranAmount + ".00";


                        var relatableData = _sPGPaymentGateway.GetSPGTransactionByTrnxDetails(trnxDetails);


                        Session[CommonConstantHelper.LogInCredentialId] = Convert.ToInt32(relatableData.LastUpdatedBy);
                        Session[CommonConstantHelper.UserTypeId] = 2;
                        Session[CommonConstantHelper.UserName] = Convert.ToString(relatableData.HolderUserName);
                        Session[CommonConstantHelper.HolderId] = Convert.ToInt32(relatableData.HolderId);




                        SPGTransaction sPGTrnx = new SPGTransaction()
                        {
                            Id = relatableData.Id,
                            TranactionId = dtResponseStatus.Rows[0]["TransactionId"].ToString(),
                            TranDateTime = Convert.ToDateTime(dtResponseStatus.Rows[0]["TranDateTime"].ToString()),
                            PayAmount = dtResponseStatus.Rows[0]["PayAmount"].ToString(),
                            PayMode = dtResponseStatus.Rows[0]["PayMode"].ToString(),
                            OrgiBrCode = dtResponseStatus.Rows[0]["OrgiBrCode"].ToString(),
                            StatusMsg = dtResponseStatus.Rows[0]["StatusMsg"].ToString(),
                            TransactionStatus = dtResponseStatus.Rows[0]["TransactionStatus"].ToString(),
                            LastUpdated = DateTime.Now
                        };

                        var rt = _holdingTaxManager.GetRebateAndWrongInfoByHoldingTaxId(Convert.ToInt32(relatableData.PayerId));
                        HoldingTax holdingTax = _holdingTaxManager.GetHoldingTaxById(Convert.ToInt32(relatableData.PayerId));

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
                            totalHoldingTax = rt.TotalHoldingTax;
                            totalRebate = rt.RebateValue;
                            wrongInfoCharge = holdingTax.WrongInfoCharge > 0 ? holdingTax.WrongInfoCharge : 0;
                            netTotalTax = rt.NetTaxPayableAmount - totalRebate + wrongInfoCharge;
                        }
                        else
                        {
                            totalHoldingTax = rt.TotalHoldingTax;
                            totalRebate = 0;
                            wrongInfoCharge = holdingTax.WrongInfoCharge > 0 ? holdingTax.WrongInfoCharge : 0;
                            netTotalTax = rt.NetTaxPayableAmount - totalRebate + wrongInfoCharge;
                        }

                        HoldingTax tax = new HoldingTax
                        {
                            Rebate = totalRebate,
                            WrongInfoCharge = wrongInfoCharge,
                            isFinalized = null,
                            PaidAmount = netTotalTax,
                            LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                            LastUpdated = DateTime.Now,
                            HoldingTaxId = Convert.ToInt32(relatableData.PayerId),
                            NetTaxPayableAmount = netTotalTax,
                            Remarks = "Online Payment.Bank Transaction Id : " + dtResponseStatus.Rows[0]["TransactionId"].ToString() + " Bank Transaction Date: " + dtResponseStatus.Rows[0]["TranDateTime"].ToString() + ")",
                            PaymentDate = Convert.ToDateTime(dtResponseStatus.Rows[0]["TranDateTime"].ToString()),
                            TotalHoldingTax = null,
                            TotalTaxOfThisYear = null,
                            Surcharge = null
                        };

                        int updateData = _sPGPaymentGateway.SPGTransactionUpdate(sPGTrnx);

                        string updateString = _holdingTaxManager.UpdateTax(tax);

                        var holderMobile = _holdingManager.GetHolderById(Convert.ToInt32(relatableData.HolderId)).Contact2.ToString();
                        if (!string.IsNullOrEmpty(holderMobile))
                        {
                            OtpHistory newHistory = new OtpHistory()
                            {
                                LogInCredentialId = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                Otp = 0,
                                UserName = Session[CommonConstantHelper.UserName].ToString(),
                                Purpose = "Tax Payment",
                                CreateDate = DateTime.Now
                            };

                            string msg = "আপনার পেমেন্ট সফলভাবে সম্পন্ন হয়েছে, ধন্যবাদ।";
                            string result = SmsApi.SendSms(msg, holderMobile);

                            newHistory.responseString = result;
                            string strmsg = _OtpHistoryManager.OtpHistoryInsert(newHistory);
                        }


                        TempData["SM"] = "আপনার পেমেন্ট সফলভাবে সম্পন্ন হয়েছে";

                        Session["_holdingTaxId"] = Convert.ToInt32(relatableData.PayerId);

                    }

                }
                else if ((objResponseData.PayMode == "A01") && (objResponseData.TransactionStatus == "5017"))
                {
                    //generate offline receipt
                    //must carry contact no & Transaction Id,
                    //Reference Date

                    // after payment (branch) we will hit in IPN your system
                }
                else
                {
                    objResponseData.Message = "Fail Transaction";


                    SPGTransaction trnxDetails = new SPGTransaction
                    {
                        RefTranNo = dtResponseStatus.Rows[0]["RefTranNo"].ToString(),
                        RefTranDate = Convert.ToDateTime(dtResponseStatus.Rows[0]["RefTranDateTime"].ToString()),
                        TranAmount = dtResponseStatus.Rows[0]["TranAmount"].ToString()
                    };

                    var relatableData = _sPGPaymentGateway.GetSPGTransactionByTrnxDetails(trnxDetails);


                    Session[CommonConstantHelper.LogInCredentialId] = Convert.ToInt32(relatableData.LastUpdatedBy);
                    Session[CommonConstantHelper.UserTypeId] = 2;
                    Session[CommonConstantHelper.UserName] = Convert.ToString(relatableData.HolderUserName);
                    Session[CommonConstantHelper.HolderId] = Convert.ToInt32(relatableData.HolderId);


                    SPGTransaction sPGTrnx = new SPGTransaction()
                    {
                        Id = relatableData.Id,
                        TranactionId = dtResponseStatus.Rows[0]["TransactionId"].ToString(),
                        TranDateTime = Convert.ToDateTime(dtResponseStatus.Rows[0]["TranDateTime"].ToString()),
                        PayAmount = dtResponseStatus.Rows[0]["PayAmount"].ToString(),
                        PayMode = dtResponseStatus.Rows[0]["PayMode"].ToString(),
                        OrgiBrCode = dtResponseStatus.Rows[0]["OrgiBrCode"].ToString(),
                        StatusMsg = dtResponseStatus.Rows[0]["StatusMsg"].ToString(),
                        TransactionStatus = dtResponseStatus.Rows[0]["TransactionStatus"].ToString(),
                        LastUpdated = DateTime.Now
                    };

                    int updateData = _sPGPaymentGateway.SPGTransactionUpdate(sPGTrnx);

                    TempData["SM"] = "আপনার পেমেন্ট প্রক্রিয়া সম্পাদন করা এখন সম্ভব হচ্ছে না। কিছুক্ষন পরে আবার চেষ্টা করুন।";

                    Session["_holdingTaxId"] = Convert.ToInt32(relatableData.PayerId);
                }
            }
            catch (Exception ex)
            {
                objResponseData.Message = ex.Message.ToString();
            }


            return View(objResponseData);
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
                var _transactionCode = YearFirstPart + YearSecondPart + holdingTaxData.HolderId.ToString("D5") + PasswordHelper.TransactionID(11);
                if (_sPGPaymentGateway.IsTransactionCodeExist(_transactionCode))
                    _transactionCode = YearFirstPart + YearFirstPart + holdingTaxData.HolderId.ToString("D5") + PasswordHelper.TransactionID(6) + PasswordHelper.TransactionID(5);

                var reqCode = new Random().Next(111, 999).ToString() + new Random().Next(1111111, 9999999).ToString();
                var currDate = DateTime.Now;


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

                RequestInfo requestInfo = new RequestInfo
                {
                    requestid = reqCode,
                    mobileno = holderData.Contact2.ToString(),
                    applicentName = holderData.HolderName.ToString() + " (" + holderData.HolderNo.ToString() + ")",
                    applicentAddress = holderData.ContactAdd.ToString(),
                    refdate = currDate.ToString("yyyy-MM-dd"),
                    accountnumber1 = "0002601020864",
                    amount1 = Convert.ToDecimal(price)
                };

                decimal totalAmount = requestInfo.amount1;
                string creditAccout = requestInfo.accountnumber1;


                string SectKey = GetNewSessionKey(requestInfo.requestid, totalAmount.ToString(), requestInfo.refdate, creditAccout);
                if ((SectKey == "Authorization is not valid") || (SectKey == ""))
                {
                    TempData["SM"] = "SectKey is not valid";
                    return RedirectToAction("Index", "HoldingTax");
                }

                TokenClass tokenClass = new TokenClass();
                tokenClass.Authentication.ApiAccessPassKey = SectKey;

                ReferenceInfo referenceInfo = new ReferenceInfo
                {
                    RequestId = requestInfo.requestid,
                    RefTranNo = _transactionCode, //new Random().Next(111111, 999999).ToString();
                    RefTranDateTime = currDate.ToString("yyyy-MM-dd HH:mm:ss"),  //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    ReturnUrl = ResponseUrl,
                    ReturnMethod = "POST",
                    TranAmount = totalAmount,
                    ContactName = requestInfo.applicentName,
                    ContactNo = requestInfo.mobileno,
                    PayerId = requestInfo.requestid, //any value
                    Address = requestInfo.applicentAddress
                };
                tokenClass.ReferenceInfo = referenceInfo;


                CreditInformations creditInformations = new CreditInformations
                {
                    SLNO = "01",
                    CreditAccount = requestInfo.accountnumber1,
                    CrAmount = requestInfo.amount1,
                    Purpose = "TRN", //for account transasfer, for Challan "CHL"
                    Onbehalf = "CBD"
                };
                tokenClass.CreditInformations.Add(creditInformations);

                ResponseToken responseToken = GetToken(tokenClass);

                if (responseToken.status == "200")
                {
                    SPGTransaction sPGTrnx = new SPGTransaction();
                    sPGTrnx.Address = requestInfo.applicentAddress;
                    sPGTrnx.ApiSessionKey = SectKey;
                    sPGTrnx.ApiTokenKey = responseToken.session_token;
                    sPGTrnx.ContactName = requestInfo.applicentName;
                    sPGTrnx.ContactNo = requestInfo.mobileno;
                    sPGTrnx.CrAmount = totalAmount.ToString();
                    sPGTrnx.CreditAccounts = creditAccout;
                    sPGTrnx.HolderId = int.Parse(Session[CommonConstantHelper.HolderId].ToString());
                    sPGTrnx.HolderUserName = Session[CommonConstantHelper.UserName].ToString();
                    sPGTrnx.Id = 0;

                    string ipDetails = "";
                    if (Session["_ipDetails"] != null)
                    {
                        ipDetails = Session["_ipDetails"].ToString();
                    }
                    else
                    {
                        string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (string.IsNullOrEmpty(ipAddress))
                            ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                        ipDetails = Request.Browser.IsMobileDevice ?
                                                $"mobile {ipAddress}" : $"desktop {ipAddress}";
                    }

                    sPGTrnx.IPAddressDetails = ipDetails;
                    sPGTrnx.LastUpdated = DateTime.Now;
                    sPGTrnx.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    sPGTrnx.OnBehalf = holderData.HolderNo;
                    sPGTrnx.PayerId = HoldingTaxId.ToString();
                    sPGTrnx.Purpose = creditInformations.Purpose;
                    sPGTrnx.RefTranDate = Convert.ToDateTime(referenceInfo.RefTranDateTime);
                    sPGTrnx.RefTranNo = referenceInfo.RefTranNo;
                    sPGTrnx.RequestId = requestInfo.requestid;
                    sPGTrnx.TranAmount = totalAmount.ToString();

                    sPGTrnx.TranactionId = null;
                    sPGTrnx.TranDateTime = null;
                    sPGTrnx.PayAmount = null;
                    sPGTrnx.PayMode = null;
                    sPGTrnx.OrgiBrCode = null;
                    sPGTrnx.StatusMsg = null;
                    sPGTrnx.TransactionStatus = null;
                    int insertData = _sPGPaymentGateway.SPGTransactionInsert(sPGTrnx);


                    string srtdata = LandingUI + responseToken.session_token;
                    return Redirect(srtdata);
                }

                TempData["SM"] = "Bad http request";
                return RedirectToAction("Index", "HoldingTax");
            }
            else
            {
                TempData["SM"] = "Bad http request";
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

                var successInfo = $"Validation Response: {resonse}";
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

    }
}