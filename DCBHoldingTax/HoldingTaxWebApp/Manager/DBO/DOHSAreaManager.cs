using HoldingTaxWebApp.Gateway.Dbo;
using HoldingTaxWebApp.Models.Dbo;
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
    }
}