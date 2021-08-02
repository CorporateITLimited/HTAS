using HoldingTaxWebApp.Gateway;
using HoldingTaxWebApp.Gateway.Constant;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Constant
{
    public class OwnTaxRateManager
    {
        private readonly OwnTaxRateGateway _constantGateway;

        public OwnTaxRateManager()
        {
            _constantGateway = new OwnTaxRateGateway();
        }

        public List<OwnTaxRate> GetList()
        {
            return _constantGateway.GetList();
        }

        public OwnTaxRate GetById(int id)
        {
            return _constantGateway.GetById(id);
        }

        public string Insert(OwnTaxRate rate)
        {
            int result = _constantGateway.Insert(rate);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string Update(OwnTaxRate rate)
        {
            int result = _constantGateway.Update(rate);

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