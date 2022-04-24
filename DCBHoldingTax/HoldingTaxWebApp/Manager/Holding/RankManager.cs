using HoldingTaxWebApp.Gateway.Holding;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Holding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Holding
{
    public class RankManager
    {
        private readonly RankGateway _RankGateway;

        public RankManager()
        {
            _RankGateway = new RankGateway();
        }


        public List<clsRank> GetAllRank()
        {
            return _RankGateway.GetAllRank();
        }

        public clsRank GetRankById(int id)
        {
            return _RankGateway.GetRankById(id);
        }

        public string RankInsert(clsRank rank)
        {
            int result = _RankGateway.RankInsert(rank);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string RankUpdate(clsRank rank)
        {
            int result = _RankGateway.RankUpdate(rank);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string RankDelete(int id)
        {
            int result = _RankGateway.RankDelete(id);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }







    }
}