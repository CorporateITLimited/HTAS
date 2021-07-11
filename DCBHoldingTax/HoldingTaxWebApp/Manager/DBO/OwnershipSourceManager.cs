using HoldingTaxWebApp.Gateway.DBO;
using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.DBO
{
    public class OwnershipSourceManager
    {
        private readonly OwnershipSourceGateway _ownershipSourceGateway;

        public OwnershipSourceManager()
        {
            _ownershipSourceGateway = new OwnershipSourceGateway();
        }

        public List<OwnershipSource> GetAllOwnershipSource()
        {
            return _ownershipSourceGateway.GetAllOwnershipSource();
        }
    }
}