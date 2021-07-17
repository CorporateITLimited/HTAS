using HoldingTaxWebApp.Gateway.Users;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Users;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Users
{
    public class HolderUserManager
    {
        private readonly HolderUserGateway _holderUserGateway;

        public HolderUserManager()
        {
            _holderUserGateway = new HolderUserGateway();
        }

        public List<HolderUser> GetAllHolderUserList()
        {
            return _holderUserGateway.GetAllHolderUserList();
        }
        
        public HolderUser GetHolderUserById(int id)
        {
            return _holderUserGateway.GetHolderUserById(id);
        }

        public string HolderUserInsert(HolderUser user)
        {
            int result = _holderUserGateway.HolderUserInsert(user);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string HolderUserUpdate(HolderUser user)
        {
            int result = _holderUserGateway.HolderUserUpdate(user);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public List<HolderUser> GetAllHolderListForInsert()
        {
            return _holderUserGateway.GetAllHolderListForInsert();
        }

        public List<HolderUser> GetAllHolderListForUpdate()
        {
            return _holderUserGateway.GetAllHolderListForUpdate();
        }
    }
}