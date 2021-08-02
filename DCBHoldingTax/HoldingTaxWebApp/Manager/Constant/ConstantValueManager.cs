using HoldingTaxWebApp.Gateway.Constant;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Constant
{
    public class ConstantValueManager
    {
        private readonly ConstantValueGateway _ConstantValueGateway;

        public ConstantValueManager()
        {
            _ConstantValueGateway = new ConstantValueGateway();
        }

        #region Constant Value Portion
        //Get All ConstantValue List
        public List<ConstantValue> GetAllConstantValue()
        {
            return _ConstantValueGateway.GetAllConstantValue();

        }

        //Get ConstantValue By Id
        public ConstantValue GetConstantValueById()
        {
            return _ConstantValueGateway.GetConstantValueById();

        }

        //Create ConstantValue  
        public string ConstantValueInsert(ConstantValue model)
        {
            int result = _ConstantValueGateway.ConstantValueInsert(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;

        }

        //Update ConstantValue  
        public string ConstantValueUpdate(ConstantValue model)
        {
            int result = _ConstantValueGateway.ConstantValueUpdate(model);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }
        #endregion

    }
}