using HoldingTaxWebApp.Gateway.Tax;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Tax
{
    public class PrimaryTransactionManager
    {
        private readonly PrimaryTransactionGateway _PrimaryTransactionGateway;

        public PrimaryTransactionManager()
        {
            _PrimaryTransactionGateway = new PrimaryTransactionGateway();
        }

        public List<PrimaryTransaction> GetAllPrimaryTransaction()
        {
            return _PrimaryTransactionGateway.GetAllPrimaryTransaction();
        }

        public PrimaryTransaction GetPrimaryTransactionById(int id)
        {
            return _PrimaryTransactionGateway.GetPrimaryTransactionById(id);
        }

        public string PrimaryTransactionGatewayInsert(PrimaryTransaction PrimaryTransaction)
        {
            int result = _PrimaryTransactionGateway.PrimaryTransactionGatewayInsert(PrimaryTransaction);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string PrimaryTransactionGatewayUpdate(PrimaryTransaction PrimaryTransaction)
        {

            int result = _PrimaryTransactionGateway.PrimaryTransactionGatewayUpdate(PrimaryTransaction);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }








    }
}