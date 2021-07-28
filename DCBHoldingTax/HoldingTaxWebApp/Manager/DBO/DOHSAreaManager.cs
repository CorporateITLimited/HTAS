using HoldingTaxWebApp.Gateway.Dbo;
using HoldingTaxWebApp.Gateway.DBO;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Dbo;
using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.DBO
{
    public class DOHSAreaManager
    {
        private readonly DOHSAreaGateway _dOHSAreaGateway;
        public DOHSAreaManager()
        {
            _dOHSAreaGateway = new DOHSAreaGateway();
        }

        public List<DOHSArea> GetAllDOHSArea()
        {
            return _dOHSAreaGateway.GetAllDOHSArea();
        }

        public DOHSArea GetDOHSAreaId(int id)
        {
            return _dOHSAreaGateway.GetDOHSAreaId(id);
        }

        public string DOHSAreaInsert(DOHSArea DOHS)
        {
            int result = _dOHSAreaGateway.DOHSAreaInsert(DOHS);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 401)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string DOHSAreaUpdate(DOHSArea DOHS)
        {
            int result = _dOHSAreaGateway.DOHSAreaUpdate(DOHS);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 401)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }
    }
}