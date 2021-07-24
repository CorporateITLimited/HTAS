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

        public List<HoldingTax> GetAllHoldingTaxForHolder(int HolderId)
        {
            return _holdingTaxGateway.GetAllHoldingTaxForHolder(HolderId);
        }

        public HoldingTax GetHoldingTaxById(int id)
        {
            return  _holdingTaxGateway.GetHoldingTaxById(id);
        }

        public int GenerateTax(int FinYearId)
        {
            return _holdingTaxGateway.GenerateTax(FinYearId);
        }
    }
}