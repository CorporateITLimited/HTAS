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
    public class UserManager
    {
        private readonly UserGateway _userGateway;

        public UserManager()
        {
            _userGateway = new UserGateway();
        }

        public List<clsUser> GetAllUserList()
        {
            return _userGateway.GetAllUserList();
        }


        //public List<clsUser> GetAllUserListNonAdmin()
        //{
        //    return _userGateway.GetAllUserListNonAdmin();
        //}
        public clsUser GetUserById(int id)
        {
            return _userGateway.GetUserById(id);
        }

        public string UserInsert(clsUser user)
        {
            int result = _userGateway.UserInsert(user);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string UserUpdate(clsUser user)
        {
            int result = _userGateway.UserUpdate(user);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        public string UserDelete(clsUser user)
        {
            int result = _userGateway.UserDelete(user);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }


        public clsUser checkDuplicateUserName(string name)
        {
            return _userGateway.checkDuplicateUserName(name);
        }

        public string changeUserName(string name)
        {
            int result = _userGateway.changeUserName(name);

            if (result == 202)
                return CommonConstantHelper.Success;
            else if (result == 409)
                return CommonConstantHelper.Conflict;
            else if (result == 500)
                return CommonConstantHelper.Error;
            else
                return CommonConstantHelper.Failed;
        }

        #region user-permission

        public List<clsUser> GetAllUserListForPermissionInsert()
        {
            return _userGateway.GetAllUserListForPermissionInsert();
        }

        public List<clsUser> GetAllUserListForPermissionUpdate()
        {
            return _userGateway.GetAllUserListForPermissionUpdate();
        }
        public List<UserPermission> GetControllerList()
        {
            return _userGateway.GetControllerList();
        }

        public string UserPermissionInsert(UserPermission uP)
        {
            int result = _userGateway.UserPermissionInsert(uP);

            return result == 202 ? CommonConstantHelper.Success : result == 500 ? CommonConstantHelper.Error : CommonConstantHelper.Failed;
        }

        public string UserPermissionUpdate(UserPermission uP)
        {
            int result = _userGateway.UserPermissionUpdate(uP);

            return result == 202 ? CommonConstantHelper.Success : result == 500 ? CommonConstantHelper.Error : CommonConstantHelper.Failed;
        }

        public List<UserPermission> GetUserPermissionListByUserId(int UserId)
        {
            return _userGateway.GetUserPermissionListByUserId(UserId);
        }

        public List<UserPermission> GetUserPermissionList()
        {
            return _userGateway.GetUserPermissionList();
        }
        #endregion

    }
}