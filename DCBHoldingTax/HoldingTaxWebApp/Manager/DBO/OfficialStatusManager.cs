using HoldingTaxWebApp.Gateway.DBO;
using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.DBO
{
    public class OfficialStatusManager
    {
        private readonly OfficialStatusGateway _OfficialStatusGateway;

        public OfficialStatusManager()
        {
            _OfficialStatusGateway = new OfficialStatusGateway();
        }

        public List<OfficialStatus> GetAllOfficialStatus()
        {
            return _OfficialStatusGateway.GetAllOfficialStatus();
        }

    }
}