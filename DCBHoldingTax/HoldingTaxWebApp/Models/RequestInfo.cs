using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models
{
  public class RequestInfo
  {
    public string accountnumber1 { get; set; }
    public string accountnumber2 { get; set; }
    public string accountnumber3 { get; set; }

    public decimal amount1{ get; set; }
    public decimal amount2 { get; set; }
    public decimal amount3 { get; set; }
    public string requestid { get; set; }
    public string mobileno { get; set; }
    public string applicentName { get; set; }
    public string applicentAddress { get; set; }
    public string refdate { get; set; }

    
  }
  public class sessionKeyModel
  {
    public string scretKey { get; set; }
  }
    
  public class SpgUserCredentials : System.Web.Services.Protocols.SoapHeader
  {
    public string userName;
    public string password;
  }

  public class GetSessionKeyRequest
  {
    public SpgUserCredentials AccessUser { get; set; }
    public GetSessionKeyRequest()
    {
      AccessUser = new SpgUserCredentials();
    }
    public string strUserId { get; set; }
    public string strPassKey { get; set; }
    public string strRequestId { get; set; }
    public string strAmount { get; set; }
    public string strTranDate { get; set; }
    public string strAccounts { get; set; }
  }

  public class ResponseData
  {
    public string ApiAccessUserId { get; set; }

    public string AuthenticationKey { get; set; }

    public string TransactionId { get; set; }

    public string TranDateTime { get; set; }

    public string RefTranNo { get; set; }

    public string RefTranDateTime { get; set; }

    public string TranAmount { get; set; }

    public string PayAmount { get; set; }

    public string OrgiBrCode { get; set; }

    public string StatusMsg { get; set; }

    public string PayMode { get; set; }

    public string ScrollNo { get; set; }

    public string TransactionStatus { get; set; }

    public string Message { get; set; }
  }

  public class TranVerifyResponse
  {
    public string TransactionId { get; set; }

    public string TransactionDate { get; set; }
    public string ReferenceNo { get; set; }
    public string ReferenceDate { get; set; }
    public string ServiceName { get; set; }

    public string BrCode { get; set; }
    public string ApplicantName { get; set; }
    public string MobileNo { get; set; }
    public string TranAmount { get; set; }

    public string StatusCode { get; set; }
    public string StCode { get; set; }
    public string SpCode { get; set; }
    public string PayMode { get; set; }

    public string PayAmount { get; set; }
    public string Vat { get; set; }
    public string Commission { get; set; }


  }

}