using HoldingTaxWebApp.Gateway.Dbo;
using HoldingTaxWebApp.Models.Dbo;
using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.DBO
{
    public class FinancialYearManager
    {
        private readonly FinancialYearGateway _gateway;
        public FinancialYearManager()
        {
            _gateway = new FinancialYearGateway();
        }

        public List<clsFinancialYear> GetAllFinancialYear()
        {
            return _gateway.GetAllFinancialYear();
        }
    }
}