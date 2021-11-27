using HoldingTaxWebApp.Gateway;
using HoldingTaxWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager
{
    public class SPGPaymentManager
    {
        private readonly SPGPaymentGateway _trnx;
        public SPGPaymentManager()
        {
            _trnx = new SPGPaymentGateway();
        }

        public List<SPGTransaction> GetSPGTransaction()
        {
            return _trnx.GetSPGTransaction();
        }

        public SPGTransaction GetSPGTransactionById(long id)
        {
            return _trnx.GetSPGTransactionById(id);
        }

        public SPGTransaction GetSPGTransactionByHolderId(int id)
        {
            return _trnx.GetSPGTransactionByHolderId(id);
        }


        public SPGTransaction GetSPGTransactionByTrnxDetails(SPGTransaction spg)
        {
            return _trnx.GetSPGTransactionByTrnxDetails(spg);
        }

        public bool IsTransactionCodeExist(string TransactionCode)
        {
            return _trnx.IsTransactionCodeExist(TransactionCode);
        }

        public int SPGTransactionInsert(SPGTransaction sPG)
        {
            return _trnx.SPGTransactionInsert(sPG);
        }

        public int SPGTransactionUpdate(SPGTransaction sPG)
        {
            return _trnx.SPGTransactionUpdate(sPG);
        }


    }
}