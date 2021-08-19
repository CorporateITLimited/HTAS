using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HoldingTaxWebApp.Gateway.Tax;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Holding;
using HoldingTaxWebApp.Models.Tax;
using HoldingTaxWebApp.ViewModels.Tax;

namespace HoldingTaxWebApp.Manager.Tax
{
    public class InitialTranscationManager
    {

        private readonly InitialTransactionGateway _transactionGateway;
        public InitialTranscationManager()
        {
            _transactionGateway = new InitialTransactionGateway();
        }

        public List<InitialTransaction> GetAllTranscation()
        {
            return _transactionGateway.GetAllTranscation();
        }

        public InitialTransaction GetTranscationById(long id)
        {
            return _transactionGateway.GetTranscationById(id);
        }

        public InitialTransaction GetTranscationByTransactionCode(string TransactionCode)
        {
            return _transactionGateway.GetTranscationByTransactionCode(TransactionCode);
        }

        public int InsertTranscation(InitialTransaction trnx)
        {
            return _transactionGateway.InsertTranscation(trnx);
        }

        public bool IsTransactionCodeExist(string TransactionCode)
        {
            return _transactionGateway.IsTransactionCodeExist(TransactionCode);
        }

        public string UpdateTranscation(InitialTransaction trnx)
        {
            int result = _transactionGateway.UpdateTranscation(trnx);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }
    }
}