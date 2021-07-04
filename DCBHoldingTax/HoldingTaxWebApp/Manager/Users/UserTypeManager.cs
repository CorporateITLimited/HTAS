using HoldingTaxWebApp.Gateway.Users;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Manager.Users
{
    public class UserTypeManager
    {
      
            private readonly UserTypeGateway _UserTypeGateway;

            public UserTypeManager()
            {
                _UserTypeGateway = new UserTypeGateway();
            }

            public List<UserType> GetAllUserType()
            {
                return _UserTypeGateway.GetAllUserType();
            }


         
            public UserType GetUserTypeById(int id)
            {
                return _UserTypeGateway.GetUserTypeById(id);
            }


            public string UserTypeInsert(UserType UserType)
            {
                int result = _UserTypeGateway.UserTypeInsert(UserType);

                if (result == 202)
                    return CommonConstantHelper.Success;
                else if (result == 409)
                    return CommonConstantHelper.Conflict;
                else if (result == 500)
                    return CommonConstantHelper.Error;
                else
                    return CommonConstantHelper.Failed;
            }

            public string UserTypeUpdate(UserType UserType)
            {
                int result = _UserTypeGateway.UserTypeUpdate(UserType);

                if (result == 202)
                    return CommonConstantHelper.Success;
                else if (result == 409)
                    return CommonConstantHelper.Conflict;
                else if (result == 500)
                    return CommonConstantHelper.Error;
                else
                    return CommonConstantHelper.Failed;
            }


            public string UserTypeDelete(UserType UserType)
            {
                int result = _UserTypeGateway.UserTypeDelete(UserType);

                if (result == 202)
                    return CommonConstantHelper.Success;

                else
                    return CommonConstantHelper.Failed;
            }
        }
}