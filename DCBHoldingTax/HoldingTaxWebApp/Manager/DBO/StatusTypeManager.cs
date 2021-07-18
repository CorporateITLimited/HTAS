using HoldingTaxWebApp.Gateway.DBO;
using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.DBO
{
    public class StatusTypeManager
    {
        private readonly StatusTypeGateway _StatusTypeGateway;

        public StatusTypeManager()
        {
            _StatusTypeGateway = new StatusTypeGateway();
        }

        public List<StatusType> GetAllStatusType()
        {
            return _StatusTypeGateway.GetAllStatusType();
        }
    }
}