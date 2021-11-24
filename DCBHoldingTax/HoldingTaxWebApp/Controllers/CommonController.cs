using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using Newtonsoft.Json;
using HoldingTaxWebApp.Models;

namespace HoldingTaxWebApp.Controllers
{
    public class AccessUser
    {
        public AccessUser()
        {
            userName = Convert.ToString(ConfigurationManager.AppSettings["dueluser"]);
            password = Convert.ToString(ConfigurationManager.AppSettings["duelpassword"]);
        }
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class ValidationInputClass
    {
        public ValidationInputClass()
        {
            AccessUser = new AccessUser();
            isEncPwd = true;
            OwnerCode = Convert.ToString(ConfigurationManager.AppSettings["duelownercode"]);
        }
        public AccessUser AccessUser { get; set; }
        public string OwnerCode { get; set; }
        public string ReferenceDate { get; set; }
        public string RequiestNo { get; set; }
        public bool isEncPwd { get; set; }
    }


    public class GetsessionModel
    {
        public GetsessionModel()
        {
            AccessUser = new AccessUser();
            strUserId = AccessUser.userName;
            strPassKey = AccessUser.password;
        }
        public AccessUser AccessUser { get; set; }
        public string strUserId { get; set; }
        public string strPassKey { get; set; }
        public string strRequestId { get; set; }
        public string strAmount { get; set; }
        public string strTranDate { get; set; }
        public string strAccounts { get; set; }
    }

    public class Authentication
    {
        public Authentication()
        {
            ApiAccessUserId = Convert.ToString(ConfigurationManager.AppSettings["dueluser"]);
        }
        public string ApiAccessUserId { get; set; }
        public string ApiAccessPassKey { get; set; }
    }

    public class ReferenceInfo
    {
        public string RequestId { get; set; }
        public string RefTranNo { get; set; }
        public string RefTranDateTime { get; set; }
        public string ReturnUrl { get; set; }
        public string ReturnMethod { get; set; }
        public decimal TranAmount { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string PayerId { get; set; }
        public string Address { get; set; }
    }

    public class TokenClass
    {
        public TokenClass()
        {
            Authentication = new Authentication();
            ReferenceInfo = new ReferenceInfo();
            CreditInformations = new List<CreditInformations>();
        }
        public Authentication Authentication { get; set; }
        public ReferenceInfo ReferenceInfo { get; set; }
        public List<CreditInformations> CreditInformations { get; set; }

    }
    public class CreditInformations
    {
        public string SLNO { get; set; }
        public string CreditAccount { get; set; }
        public decimal CrAmount { get; set; }
        public string Purpose { get; set; }
        public string Onbehalf { get; set; }

    }

    public class ResponseSession
    {
        public string scretKey { get; set; }
    }

    public class ResponseToken
    {
        public string status { get; set; }
        public string session_token { get; set; }
    }



    public class CommonController : Controller
    {

        public string user = Convert.ToString(ConfigurationManager.AppSettings["dueluser"]);
        public string password = Convert.ToString(ConfigurationManager.AppSettings["duelpassword"]);
        public string ownercode = Convert.ToString(ConfigurationManager.AppSettings["duelownercode"]);

        protected static string ResponseUrl = ConfigurationManager.AppSettings["ResponseUrl"];
        protected static string LandingUI = ConfigurationManager.AppSettings["LandingUI"];
        protected static string apiUrl = ConfigurationManager.AppSettings["apiUrl"];


        public string GetNewSessionKey(string strRequestId, string strAmount, string strTranDate, string strAccounts)
        {
            GetsessionModel getsessionModel = new GetsessionModel();
            getsessionModel.strUserId = getsessionModel.AccessUser.userName;
            getsessionModel.strPassKey = getsessionModel.AccessUser.password;
            getsessionModel.strRequestId = strRequestId;
            getsessionModel.strAmount = strAmount;
            getsessionModel.strTranDate = strTranDate;
            getsessionModel.strAccounts = strAccounts;

            ResponseSession responseSession = new ResponseSession();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl + "api/SpgService/GetSessionKey");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.UserAgent = "Client Cert Sample";
            httpWebRequest.Method = "POST";

            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(getsessionModel);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();

                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(result);
                    responseSession = new JavaScriptSerializer().Deserialize<ResponseSession>(json);
                }
            }
            catch (Exception ex)
            {
                responseSession.scretKey = "Authorization is not valid " + ex.Message.ToString();
            }

            return responseSession.scretKey;
        }

        public TranVerifyResponse Verification(ValidationInputClass validationInputClass)
        {
            TranVerifyResponse tranVerifyResponse = new TranVerifyResponse();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl + "api/SpgService/TransactionVerification");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.UserAgent = "Client Cert Sample";
            httpWebRequest.Method = "POST";

            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(validationInputClass);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(result);
                    tranVerifyResponse = JsonConvert.DeserializeObject<TranVerifyResponse>(json);
                }
            }
            catch (Exception ex)
            {
                tranVerifyResponse.StatusCode = ex.Message.ToString();
            }

            return tranVerifyResponse;
        }

        public ResponseToken GetToken(TokenClass tokenClass)
        {

            ResponseToken responseToken = new ResponseToken();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl + "api/SpgService/PaymentByPortal");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.UserAgent = "Client Cert Sample";
            httpWebRequest.Method = "POST";

            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(tokenClass);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();

                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(result);
                    responseToken = new JavaScriptSerializer().Deserialize<ResponseToken>(json);
                }
            }
            catch (Exception ex)
            {
                responseToken.status = ex.Message.ToString();
            }

            return responseToken;
        }
    }

}