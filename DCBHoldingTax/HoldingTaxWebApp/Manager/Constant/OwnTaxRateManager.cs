using HoldingTaxWebApp.Gateway;
using HoldingTaxWebApp.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Constant
{
    public class OwnTaxRateManager
    {
        private readonly ConstantGateway _constantGateway;

        public OwnTaxRateManager()
        {
            _constantGateway = new ConstantGateway();
        }

        public List<RentTaxRate> GetAllRentTaxRates()
        {
            return _constantGateway.GetAllRentTaxRates();
        }
    }
}