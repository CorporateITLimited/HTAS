using HoldingTaxWebApp.Gateway;
using HoldingTaxWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager
{
    public class CommonListManager
    {
        private readonly CommonListGateway _commonListGateway;
        public CommonListManager()
        {
            _commonListGateway = new CommonListGateway();
        }

        public List<CommonList> GetAllGender()
        {
            return _commonListGateway.GetAllGender();
        }

        public List<CommonList> GetAllMaritalStatus()
        {
            return _commonListGateway.GetAllMaritalStatus();
        }

        public List<CommonList> GetAllOwnerShipType()
        {

            return _commonListGateway.GetAllOwnerShipType();

        }

        public List<CommonList> GetAllFloor()
        {

            return _commonListGateway.GetAllFloor();

        }

        public List<CommonList> GetAllOwnOrRent()
        {
            return _commonListGateway.GetAllOwnOrRent();


        }

        public List<CommonList> GetAllOwnType()
        {
            return _commonListGateway.GetAllOwnType();
        }
    }
}