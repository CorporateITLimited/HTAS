using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HoldingTaxWebApp.Gateway.Tax;
using HoldingTaxWebApp.Models.Tax;

namespace HoldingTaxWebApp.Manager.Tax
{
    public class HoldingTaxManager
    {

        private readonly HoldingTaxGateway _holdingTaxGateway;
        public HoldingTaxManager()
        {
            _holdingTaxGateway = new HoldingTaxGateway();
        }

        public List<HoldingTax> GetAllHoldingTax()
        {
            return _holdingTaxGateway.GetAllHoldingTax();
        }

        public HoldingTax GetHoldingTaxById(int id)
        {
            return  _holdingTaxGateway.GetHoldingTaxById(id);
        }
    }
}