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
    public class RoleController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly RoleManager _roleManager;
        public RoleController()
        {
            _roleManager = new RoleManager();
            if (System.Web.HttpContext.Current.Session["ListofPermissions"] != null)
            {
                List<UserPermission> userPermisson = (List<UserPermission>)System.Web.HttpContext.Current.Session["ListofPermissions"];
                var single_permission = userPermisson.Where(p => p.ControllerName == "Role").FirstOrDefault();
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
        // GET: Role
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

                        var roleList = new List<Role>();
                        //if (Convert.ToString(Session[CommonConstantHelper.RoleName]) == CommonConstantHelper.RoleAdmin)
                        //    roleList = _roleManager.GetAllRole();
                        //else
                        //    roleList = _roleManager.GetAllRoleNonAdmin();
                        roleList = _roleManager.GetAllRole();

                        List<Role> roleListVM = new List<Role>();
                        foreach (var item in roleList)
                        {
                            Role roleVM = new Role()
                            {
                                LastUpdated = item.LastUpdated,
                                IsActive = item.IsActive,
                                IsDeleted = item.IsDeleted,
                                RoleDetails = item.RoleDetails,
                                RoleId = item.RoleId,
                                RoleName = item.RoleName,
                                LastUpdatedBy = item.LastUpdatedBy,
                                UpdatedByUserName = item.UpdatedByUserName,
                                CreateDate = item.CreateDate,
                                CreatedBy = item.CreatedBy,
                                CreatedByUserName = item.CreatedByUserName
                            };
                            roleListVM.Add(roleVM);
                        }
                        return View(roleListVM.ToList());

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

        // GET: Role/Details/5
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

                        Role roleVM = _roleManager.GetRoleById(id);

                        if (roleVM == null)
                            return HttpNotFound();

                        return View(roleVM);

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

        // GET: Role/Create
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

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    if (role == null)
                        return HttpNotFound();

                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError("", errors.ToString());
                        return View(role);
                    }

                    if (role.RoleName == null)
                    {
                        ModelState.AddModelError("", "Role is required.");
                        return View(role);
                    }

                    role.IsActive = true;
                    role.IsDeleted = false;
                    role.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    role.CreateDate = DateTime.Now;
                    role.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    role.LastUpdated = DateTime.Now;


                    string addRole = _roleManager.RoleInsert(role);

                    if (addRole == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "Successfully added new role.";
                        return RedirectToAction("Index", "Role");
                    }
                    else if (addRole == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "Role already exist.");
                        return View(role);
                    }
                    else if (addRole == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error");
                        TempData["EM"] = "Error.";
                        return View(role);
                    }
                    else if (addRole == CommonConstantHelper.Failed)
                    {
                        ModelState.AddModelError("", "Failed");
                        return View(role);
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

        // GET: Role/Edit/5
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
                        Role role = _roleManager.GetRoleById(id);

                        if (role == null)
                            return HttpNotFound();

                        Role updatableRoleData = new Role()
                        {
                            LastUpdated = role.LastUpdated,
                            LastUpdatedBy = role.LastUpdatedBy,
                            IsActive = role.IsActive,
                            IsDeleted = role.IsDeleted,
                            RoleDetails = role.RoleDetails,
                            RoleId = role.RoleId,
                            RoleName = role.RoleName
                        };

                        return View(updatableRoleData);
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

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    Role roleVM = _roleManager.GetRoleById(role.RoleId);

                    if (roleVM == null)
                        return HttpNotFound();

                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError("", errors.ToString());
                        return View(role);
                    }

                    if (role.RoleName == null)
                    {
                        ModelState.AddModelError("", "Role is required.");
                        return View(role);
                    }

                    role.IsDeleted = null;
                    role.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    role.LastUpdated = DateTime.Now;

                    string updateRole = _roleManager.RoleUpdate(role);

                    if (updateRole == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "Successfully updated Role.";
                        return RedirectToAction("Index", "Role");
                    }
                    else if (updateRole == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "Role already exist.");
                        return View();
                    }
                    else if (updateRole == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error.");
                        return View();
                    }
                    else if (updateRole == CommonConstantHelper.Failed)
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
