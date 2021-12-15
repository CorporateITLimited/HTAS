using HoldingTaxWebApp.Gateway.Users;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Users
{
    public class OtpHistoryManager
    {
        private readonly OtpHistoryGateway _OtpHistoryGateway;

        public OtpHistoryManager()
        {
            _OtpHistoryGateway = new OtpHistoryGateway();
        }


        //get Otp details by Otp
        public OtpHistory GetOtpHistoryById(int otp)
        {
            return _OtpHistoryGateway.GetOtpHistoryById(otp);

        }

        //Create OtpHistory 
        public string OtpHistoryInsert(OtpHistory OtpHistory)
        {
            int result = _OtpHistoryGateway.OtpHistoryInsert(OtpHistory);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }

        //update Otp History
        public string OtpHistoryUpdate(OtpHistory OtpHistory)
        {
            int result = _OtpHistoryGateway.OtpHistoryUpdate(OtpHistory);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }


    }
}