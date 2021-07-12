using HoldingTaxWebApp.Gateway.DBO;
using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.DBO
{
    public class ConstructionStatusManager
    {
        private readonly ConstructionStatusGateway _ConstructionStatusGateway;

        public ConstructionStatusManager()
        {
            _ConstructionStatusGateway = new ConstructionStatusGateway();
        }

        public List<ConstructionStatus> GetAllConstructionStatus()
        {
            return _ConstructionStatusGateway.GetAllConstructionStatus();
        }

    }
}