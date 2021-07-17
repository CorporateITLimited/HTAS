using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.Users;
using HoldingTaxWebApp.Models.Users;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Users
{
    public class UserController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly EmployeeManager _employeeOfficialManager;

        public UserController()
        {
            _userManager = new UserManager();
            _roleManager = new RoleManager();
            _employeeOfficialManager = new EmployeeManager();
            if (System.Web.HttpContext.Current.Session["ListofPermissions"] != null)
            {
                List<UserPermission> userPermisson = (List<UserPermission>)System.Web.HttpContext.Current.Session["ListofPermissions"];
                var single_permission = userPermisson.Where(p => p.ControllerName == "UserController").FirstOrDefault();
                if (single_permission.ReadWriteStatus != null && single_permission.CanAccess != null)
                {
                    if (single_permission.CanAccess == true)
                    {
                        CanAccess = true;
                    }
                    if (single_permission.ReadWriteStatus == true)
                    {
                        CanReadWrite = true;
                    }
                }
            }
        }

       [HttpGet]
        public ActionResult Index()
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                  && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                  && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess)
                {
                    try
                    {
                        var userList = new List<clsUser>();
                        //if (Convert.ToString(Session[CommonConstantHelper.RoleName]) == CommonConstantHelper.RoleAdmin)
                        //    userList = _userManager.GetAllUserList();
                        //else
                        //    userList = _userManager.GetAllUserListNonAdmin();


                        userList = _userManager.GetAllUserList();


                        List<clsUser> userListVM = new List<clsUser>();
                        foreach (var item in userList)
                        {
                            clsUser userVM = new clsUser()
                            {
                                CreatedBy = item.CreatedBy,
                                CreatedByUserName = item.CreatedByUserName,
                                CreateDate = item.CreateDate,
                                LastUpdated = item.LastUpdated,
                                Email = item.Email,
                                IsEmailConfirmed = item.IsEmailConfirmed,
                                EmpolyeeId = item.EmpolyeeId,
                                IsActive = item.IsActive,
                                IsDeleted = item.IsDeleted,
                                LogInCredentialId = item.LogInCredentialId,
                                LogIsActive = item.LogIsActive,
                                LogIsDeleted = item.LogIsDeleted,
                                MobileNumber = item.MobileNumber,
                                IsMobileNumberConfirmed = item.IsMobileNumberConfirmed,
                                HashPassword = item.HashPassword,
                                RoleId = item.RoleId,
                                RoleName = item.RoleName,
                                LastUpdatedBy = item.LastUpdatedBy,
                                UpdatedByUserName = item.UpdatedByUserName,
                                UserDetails = item.UserDetails,
                                UserFullName = item.UserFullName,
                                UserId = item.UserId,
                                UserName = item.UserName,
                                UserTypeId = item.UserTypeId,
                                EmployeeName = item.EmployeeName
                            };
                            userListVM.Add(userVM);
                        }

                        return View(userListVM.ToList());

                    }
                    catch (Exception exception)
                    {
                        TempData["EM"] = "error | " + exception.Message.ToString();
                        return View();
                    }
                }
                else
                {
                    TempData["PM"] = "Permission Denied.";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["EM"] = "Session Expired or Internal Error. {Primary User Secondary Index}";
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                  && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                  && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess)
                {
                    try
                    {

                        clsUser userVM = _userManager.GetUserById(id);

                        if (userVM == null)
                            return HttpNotFound();

                        return View(userVM);

                    }
                    catch (Exception exception)
                    {
                        TempData["EM"] = "error | " + exception.Message.ToString();
                        return View();
                    }
                }
                else
                {
                    TempData["PM"] = "Permission Denied.";
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                TempData["EM"] = "Session Expired";
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                 && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                 && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess && CanReadWrite)
                {

                    //if (Convert.ToString(Session[CommonConstantHelper.RoleName]) == CommonConstantHelper.RoleAdmin)
                    //{
                    //    ViewBag.RoleId = new SelectList(_roleManager.GetAllRole(), "RoleId", "RoleName");
                    //}
                    //else
                    //{
                    //    ViewBag.RoleId = new SelectList(_roleManager.GetAllRoleNonAdmin(), "RoleId", "RoleName");
                    //}

                    ViewBag.RoleId = new SelectList(_roleManager.GetAllRole(), "RoleId", "RoleName");
                    ViewBag.EmpolyeeId = new SelectList(_employeeOfficialManager.GetAllEmployeeList(), "EmpolyeeId", "EmployeeName");
                    ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text");

                    return View();

                }
                else
                {
                    TempData["PM"] = "Permission Denied.";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["EM"] = "Session Expired";
                return RedirectToAction("LogIn", "Account");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(clsUser user)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    //if (Convert.ToString(Session[CommonConstantHelper.RoleName]) == CommonConstantHelper.RoleAdmin)
                    //{
                    //    ViewBag.RoleId = new SelectList(_roleManager.GetAllRole(), "RoleId", "RoleName", user.RoleId);
                    //}
                    //else
                    //{
                    //    ViewBag.RoleId = new SelectList(_roleManager.GetAllRoleNonAdmin(), "RoleId", "RoleName", user.RoleId);
                    //}
                    ViewBag.RoleId = new SelectList(_roleManager.GetAllRole(), "RoleId", "RoleName", user.RoleId);
                    ViewBag.EmpolyeeId = new SelectList(_employeeOfficialManager.GetAllEmployeeList(), "EmpolyeeId", "EmployeeName", user.EmpolyeeId);
                    ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text");

                    if (user == null)
                        return HttpNotFound();


                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError("", errors.ToString());
                        return View(user);
                    }

                    if (user.UserName == null)
                    {
                        ModelState.AddModelError("", "Username is required.");
                        return View(user);
                    }

                    if (user.HashPassword == null)
                    {
                        ModelState.AddModelError("", "Password is required.");
                        return View(user);
                    }

                    if (!user.HashPassword.Equals(user.ConfirmPassword))
                    {
                        ModelState.AddModelError("", "Password does not match.");
                        return View(user);
                    }

                    if (user.RoleId == 0)
                    {
                        ModelState.AddModelError("", "Role is required.");
                        return View(user);
                    }

                    if (user.EmpolyeeId == 0)
                    {
                        ModelState.AddModelError("", "Employee is required.");
                        return View(user);
                    }

                    user.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    user.CreateDate = DateTime.Now;
                    user.IsActive = true;
                    user.IsDeleted = false;
                    user.IsMobileNumberConfirmed = false;
                    user.IsEmailConfirmed = false;
                    user.UserTypeId = 1;
                    user.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    user.LastUpdated = DateTime.Now;

                    user.HashPassword = PasswordHelper.EncryptPass(user.HashPassword);

                    string addUser = _userManager.UserInsert(user);

                    if (addUser == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "Successfully added new user.";
                        return RedirectToAction("Index", "User");
                    }
                    else if (addUser == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "Username already exist.");
                        return View(user);
                    }
                    else if (addUser == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error");
                        TempData["EM"] = "Error.";
                        return View(user);
                    }
                    else if (addUser == CommonConstantHelper.Failed)
                    {
                        ModelState.AddModelError("", "Failed");
                        return View(user);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error Not Recognized");
                        return View();
                    }
                }
                catch (Exception exception)
                {
                    TempData["EM"] = "error | " + exception.Message.ToString();
                    return View();
                }
            }
            else
            {
                TempData["PM"] = "Permission Denied.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                  && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                  && (Session[CommonConstantHelper.UserId] != null))
            {

                if (CanAccess && CanReadWrite)
                {
                    try
                    {
                        clsUser user = _userManager.GetUserById(id);

                        if (user == null)
                            return HttpNotFound();

                        clsUser updatableUserData = new clsUser()
                        {
                            Email = user.Email,
                            EmpolyeeId = user.EmpolyeeId,
                            IsActive = user.LogIsActive,
                            LogInCredentialId = user.LogInCredentialId,
                            MobileNumber = user.MobileNumber,
                            HashPassword = user.HashPassword,
                            RoleId = user.RoleId,
                            UserDetails = user.UserDetails,
                            UserFullName = user.UserFullName,
                            UserId = user.UserId,
                            UserName = user.UserName,
                            UserTypeId = user.UserTypeId,
                            CreatedBy = user.CreatedBy,
                            CreateDate = user.CreateDate,
                            LastUpdated = user.LastUpdated,
                            IsEmailConfirmed = user.IsEmailConfirmed,
                            IsDeleted = user.IsDeleted,
                            IsMobileNumberConfirmed = user.IsMobileNumberConfirmed,
                            LastUpdatedBy = user.LastUpdatedBy
                        };

                        //if (Convert.ToString(Session[CommonConstantHelper.RoleName]) == CommonConstantHelper.RoleAdmin)
                        //{
                        //    ViewBag.RoleId = new SelectList(_roleManager.GetAllRole(), "RoleId", "RoleName", updatableUserData.RoleId);
                        //}
                        //else
                        //{
                        //    ViewBag.RoleId = new SelectList(_roleManager.GetAllRoleNonAdmin(), "RoleId", "RoleName", updatableUserData.RoleId);
                        //}

                        ViewBag.RoleId = new SelectList(_roleManager.GetAllRole(), "RoleId", "RoleName", updatableUserData.RoleId);
                        ViewBag.EmpolyeeId = new SelectList(_employeeOfficialManager.GetAllEmployeeList(), "EmpolyeeId", "EmployeeName", updatableUserData.EmpolyeeId);
                        ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", updatableUserData.IsActive);

                        return View(updatableUserData);

                    }
                    catch (Exception exception)
                    {
                        TempData["EM"] = "error | " + exception.Message.ToString();
                        return View();
                    }
                }
                else
                {
                    TempData["PM"] = "Permission Denied.";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["EM"] = "Session Expired";
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(clsUser user)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    if (user == null)
                        return HttpNotFound();

                    clsUser userForUpdate = _userManager.GetUserById(user.LogInCredentialId);
                    if (userForUpdate == null)
                        return HttpNotFound();


                    //if (Convert.ToString(Session[CommonConstantHelper.RoleName]) == CommonConstantHelper.RoleAdmin)
                    //{
                    //    ViewBag.RoleId = new SelectList(_roleManager.GetAllRole(), "RoleId", "RoleName", user.RoleId);
                    //}
                    //else
                    //{
                    //    ViewBag.RoleId = new SelectList(_roleManager.GetAllRoleNonAdmin(), "RoleId", "RoleName", user.RoleId);
                    //}
                    ViewBag.RoleId = new SelectList(_roleManager.GetAllRole(), "RoleId", "RoleName", user.RoleId);
                    ViewBag.EmpolyeeId = new SelectList(_employeeOfficialManager.GetAllEmployeeList(), "EmpolyeeId", "EmployeeName", user.EmpolyeeId);
                    ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", user.IsActive);


                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError("", errors.ToString());
                        return View(user);
                    }

                    if (user.UserName == null)
                    {
                        ModelState.AddModelError("", "Username is required.");
                        TempData["EM"] = "User name required.";
                        return View(user);
                    }

                    if (user.HashPassword == null)
                    {
                        ModelState.AddModelError("", "Password is required.");
                        return View(user);
                    }

                    if (!user.HashPassword.Equals(user.ConfirmPassword))
                    {
                        ModelState.AddModelError("", "Password does not match.");
                        return View(user);
                    }

                    if (user.RoleId == 0)
                    {
                        ModelState.AddModelError("", "Role is required.");
                        return View(user);
                    }

                    user.CreatedBy = null;
                    user.CreateDate = null;
                    user.LastUpdated = DateTime.Now;
                    user.IsEmailConfirmed = null;
                    user.IsDeleted = null;
                    user.IsMobileNumberConfirmed = null;
                    user.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    user.UserTypeId = 1;
                    user.HashPassword = PasswordHelper.EncryptPass(user.HashPassword);

                    string updateUser = _userManager.UserUpdate(user);

                    if (updateUser == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "Successfully updated user.";
                        return RedirectToAction("Index", "User");
                    }
                    else if (updateUser == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "Username already exist.");
                        TempData["EM"] = "User name required.";
                        return View();
                    }
                    else if (updateUser == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error.");
                        return View();
                    }
                    else if (updateUser == CommonConstantHelper.Failed)
                    {
                        ModelState.AddModelError("", "Failed.");
                        return View();
                    }
                    else
                    {
                        return View(user);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception.Message.ToString());
                    return View();
                }
            }
            else
            {
                TempData["PM"] = "Permission Denied.";
                return RedirectToAction("Index", "Home");
            }
        }

        #region Front End JS

        //public JsonResult LoadDataForRoleList()
        //{
        //    List<Role> data = _roleManager.GetAllRole().ToList();
        //    return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        //public JsonResult LoadDataForEmployeeList()
        //{
        //    List<Employee> data = _employee.GetAllEmployeeList().ToList();
        //    return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}


        public JsonResult GetAllDataByEmpolyeeId(int EmpolyeeId)
        {
            var data = _employeeOfficialManager.GetEmployeeById(EmpolyeeId);
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

     

        #endregion


    }
}
