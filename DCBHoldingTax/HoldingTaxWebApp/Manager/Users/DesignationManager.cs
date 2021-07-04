using HoldingTaxWebApp.Gateway.Users;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Users
{
    public class DesignationManager
    {
        private readonly DesignationGateway _DesignationGateway;

        public DesignationManager()
        {
            _DesignationGateway = new DesignationGateway();
        }

        public List<Designation> GetAllDesignation()
        {
            return _DesignationGateway.GetAllDesignation();
        }


        public Designation GetDesignationById(int id)
        {
            return _DesignationGateway.GetDesignationById(id);
        }


        public string DesignationInsert(Designation Designation)
        {
            int result = _DesignationGateway.DesignationInsert(Designation);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string DesignationUpdate(Designation Designation)
        {
            int result = _DesignationGateway.DesignationUpdate(Designation);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }


        //public string DesignationDelete(Designation Designation)
        //{
        //    int result = _DesignationGateway.DesignationDelete(Designation);

        //    if (result == 202)
        //        return CommonConstantHelper.Success;

        //    else
        //        return CommonConstantHelper.Failed;
        //}
    }
}