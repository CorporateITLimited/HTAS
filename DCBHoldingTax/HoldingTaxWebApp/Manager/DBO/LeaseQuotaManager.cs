using HoldingTaxWebApp.Gateway.DBO;
using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.DBO
{
    public class LeaseQuotaManager
    {
        private readonly LeaseQuotaGateway _LeaseQuotaGateway;

        public LeaseQuotaManager()
        {
            _LeaseQuotaGateway = new LeaseQuotaGateway();
        }
        public List<LeaseQuota> GetAllLeaseQuota()
        {
            return _LeaseQuotaGateway.GetAllLeaseQuota();
        }
    }
}