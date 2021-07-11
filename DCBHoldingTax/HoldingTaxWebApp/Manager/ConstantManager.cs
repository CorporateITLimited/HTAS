using HoldingTaxWebApp.Gateway;
using HoldingTaxWebApp.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager
{
    public class ConstantManager
    {
        private readonly ConstantGateway _constantGateway;

        public ConstantManager()
        {
            _constantGateway = new ConstantGateway();
        }

        public List<RentTaxRate> GetAllRentTaxRates()
        {
            return _constantGateway.GetAllRentTaxRates();
        }
    }
}