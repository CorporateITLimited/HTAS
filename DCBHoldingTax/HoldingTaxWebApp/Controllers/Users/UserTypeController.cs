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
    public class UserTypeController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly UserTypeManager _UserTypeManager;
        public UserTypeController()
        {
            _UserTypeManager = new UserTypeManager();
            if (System.Web.HttpContext.Current.Session["ListofPermissions"] != null)
            {
                List<UserPermission> userPermisson = (List<UserPermission>)System.Web.HttpContext.Current.Session["ListofPermissions"];
                var single_permission = userPermisson.Where(p => p.ControllerName == "UserType").FirstOrDefault();
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
        // GET: UserType
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
                        var UserTypeList = new List<UserType>();

                        UserTypeList = _UserTypeManager.GetAllUserType();

                        List<UserType> UserTypeListVM = new List<UserType>();
                        foreach (var item in UserTypeList)
                        {
                            UserType UserTypeVM = new UserType()
                            {
                                LastUpdated = item.LastUpdated,
                                IsActive = item.IsActive,
                                IsDeleted = item.IsDeleted,
                                UserTypeId = item.UserTypeId,
                                UserTypeName = item.UserTypeName,
                                LastUpdatedBy = item.LastUpdatedBy,
                                UpdatedByUserName = item.UpdatedByUserName,
                                CreateDate = item.CreateDate,
                                CreatedBy = item.CreatedBy,
                                CreatedByUserName = item.CreatedByUserName
                            };
                            UserTypeListVM.Add(UserTypeVM);
                        }
                        return View(UserTypeListVM.ToList());
                    }
                    catch (Exception exception)
                    {
                        //throw exception;
                        TempData["EM"] = "error | " + exception.Message.ToString();
                        //return RedirectToAction("Error", "Home");
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


            //return View();
        }

        // GET: UserType/Details/5
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
                        UserType UserTypeVM = _UserTypeManager.GetUserTypeById(id);

                        if (UserTypeVM == null)
                            return HttpNotFound();

                        return View(UserTypeVM);
                    }
                    catch (Exception exception)
                    {
                        //throw exception;
                        TempData["EM"] = "error | " + exception.Message.ToString();
                        //return RedirectToAction("Error", "Home");
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

        // GET: UserType/Create
        public ActionResult Create()
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                 && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                 && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess && CanReadWrite)
                {
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
                TempData["EM"] = "Session Expired or Internal Error. {Primary User Secondary Index}";
                return RedirectToAction("LogIn", "Account");
            }

        }

        // POST: UserType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserType UserType)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    // TODO: Add insert logic here

                    if (UserType == null)
                        return HttpNotFound();

                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError("", errors.ToString());
                        return View(UserType);
                    }

                    if (UserType.UserTypeName == null)
                    {
                        ModelState.AddModelError("", "UserType is required.");
                        //TempData["EM"] = "User name required.";
                        return View(UserType);
                    }

                    UserType.IsActive = true;
                    UserType.IsDeleted = false;
                    UserType.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    UserType.CreateDate = DateTime.Now;
                    UserType.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    UserType.LastUpdated = DateTime.Now;


                    string addUserType = _UserTypeManager.UserTypeInsert(UserType);

                    if (addUserType == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "Successfully added new UserType.";
                        return RedirectToAction("Index", "UserType");
                    }
                    else if (addUserType == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "UserType already exist.");
                        return View(UserType);
                    }
                    else if (addUserType == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error");
                        TempData["EM"] = "Error.";
                        return View(UserType);
                    }
                    else if (addUserType == CommonConstantHelper.Failed)
                    {
                        ModelState.AddModelError("", "Failed");
                        return View(UserType);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error Not Recognized");
                        return View();
                    }
                }
                catch (Exception exception)
                {
                    //throw exception;
                    TempData["EM"] = "error | " + exception.Message.ToString();
                    //return RedirectToAction("Error", "Home");
                    return View();
                }
            }
            else
            {
                TempData["PM"] = "Permission Denied.";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: UserType/Edit/5
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
                        UserType UserType = _UserTypeManager.GetUserTypeById(id);

                        if (UserType == null)
                            return HttpNotFound();

                        UserType updatableUserTypeData = new UserType()
                        {
                            LastUpdated = UserType.LastUpdated,
                            LastUpdatedBy = UserType.LastUpdatedBy,
                            IsActive = UserType.IsActive,
                            IsDeleted = UserType.IsDeleted,
                            UserTypeId = UserType.UserTypeId,
                            UserTypeName = UserType.UserTypeName
                        };

                        return View(updatableUserTypeData);
                    }
                    catch (Exception exception)
                    {
                        //throw exception;
                        TempData["EM"] = "error | " + exception.Message.ToString();
                        //return RedirectToAction("Error", "Home");
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

        // POST: UserType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserType UserType)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    UserType UserTypeVM = _UserTypeManager.GetUserTypeById(UserType.UserTypeId);

                    if (UserTypeVM == null)
                        return HttpNotFound();

                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError("", errors.ToString());
                        return View(UserType);
                    }

                    if (UserType.UserTypeName == null)
                    {
                        ModelState.AddModelError("", "UserType is required.");
                        //TempData["EM"] = "User name required.";
                        return View(UserType);
                    }

                    UserType.IsDeleted = null;
                    UserType.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    UserType.LastUpdated = DateTime.Now;

                    string updateUserType = _UserTypeManager.UserTypeUpdate(UserType);

                    if (updateUserType == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "Successfully updated UserType.";
                        return RedirectToAction("Index", "UserType");
                    }
                    else if (updateUserType == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "UserType already exist.");
                        //TempData["EM"] = "User name required.";
                        return View();
                    }
                    else if (updateUserType == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error.");
                        return View();
                    }
                    else if (updateUserType == CommonConstantHelper.Failed)
                    {
                        ModelState.AddModelError("", "Failed.");
                        return View();
                    }
                    else
                    {
                        return View();
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
    }
}
