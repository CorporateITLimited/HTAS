using HoldingTaxWebApp.Gateway.Users;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Users
{
    public class RoleManager
    {
        private readonly RoleGateway _roleGateway;

        public RoleManager()
        {
            _roleGateway = new RoleGateway();
        }

        public List<Role> GetAllRole()
        {
            return _roleGateway.GetAllRole();
        }


        //public List<Role> GetAllRoleNonAdmin()
        //{
        //    return _roleGateway.GetAllRoleNonAdmin();
        //}
        public Role GetRoleById(int id)
        {
            return _roleGateway.GetRoleById(id);
        }


        public string RoleInsert(Role role)
        {
            int result = _roleGateway.RoleInsert(role);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string RoleUpdate(Role role)
        {
            int result = _roleGateway.RoleUpdate(role);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }


        public string RoleDelete(Role role)
        {
            int result = _roleGateway.RoleDelete(role);

            if (result == 202)
                return CommonConstantHelper.Success;

            else
                return CommonConstantHelper.Failed;
        }
    }
}