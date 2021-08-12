using HoldingTaxWebApp.Gateway.Holding;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Holding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Holding
{
    public class HolderFlatHistoryManager
    {
        private readonly HolderFlatHistoryGateway _HolderFlatHistoryGateway;

        public HolderFlatHistoryManager()
        {
            _HolderFlatHistoryGateway = new HolderFlatHistoryGateway();
        }



        #region HolderFlatHistory
        public List<HolderFlatHistory> GetAllHolderFlatHistory()
        {

            return _HolderFlatHistoryGateway.GetAllHolderFlatHistory();
        }

        public HolderFlatHistory GetHolderFlatHistoryById(int id)
        {

            return _HolderFlatHistoryGateway.GetHolderFlatHistoryById(id);
        }

        public string InsertHolderFlatHistory(HolderFlatHistory model)
        {
            int result = _HolderFlatHistoryGateway.InsertHolderFlatHistory(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string UpdateHolderFlatHistory(HolderFlatHistory model)
        {
            int result = _HolderFlatHistoryGateway.UpdateHolderFlatHistory(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }
        #endregion






    }
}