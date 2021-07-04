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
    public class DesignationController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly DesignationManager _DesignationManager;
        public DesignationController()
        {
            _DesignationManager = new DesignationManager();
            if (System.Web.HttpContext.Current.Session["ListofPermissions"] != null)
            {
                List<UserPermission> userPermisson = (List<UserPermission>)System.Web.HttpContext.Current.Session["ListofPermissions"];
                var single_permission = userPermisson.Where(p => p.ControllerName == "Designation").FirstOrDefault();
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
        // GET: Designation
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
                        var DesignationList = new List<Designation>();
                        DesignationList = _DesignationManager.GetAllDesignation();

                        List<Designation> DesignationListVM = new List<Designation>();
                        foreach (var item in DesignationList)
                        {
                            Designation DesignationVM = new Designation()
                            {

                                IsActive = item.IsActive,
                                IsDeleted = item.IsDeleted,

                                DesignationId = item.DesignationId,
                                DesignationName = item.DesignationName,
                                Description = item.Description

                            };
                            DesignationListVM.Add(DesignationVM);
                        }
                        return View(DesignationListVM.ToList());
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

        // GET: Designation/Details/5
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
                        Designation DesignationVM = _DesignationManager.GetDesignationById(id);

                        if (DesignationVM == null)
                            return HttpNotFound();

                        return View(DesignationVM);
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

        // GET: Designation/Create
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

        // POST: Designation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Designation Designation)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    // TODO: Add insert logic here

                    if (Designation == null)
                        return HttpNotFound();

                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError("", errors.ToString());
                        return View(Designation);
                    }

                    if (Designation.DesignationName == null)
                    {
                        ModelState.AddModelError("", "Designation is required.");
                        //TempData["EM"] = "User name required.";
                        return View(Designation);
                    }

                    Designation.IsActive = true;
                    Designation.IsDeleted = false;



                    string addDesignation = _DesignationManager.DesignationInsert(Designation);

                    if (addDesignation == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "Successfully added new Designation.";
                        return RedirectToAction("Index", "Designation");
                    }
                    else if (addDesignation == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "Designation already exist.");
                        return View(Designation);
                    }
                    else if (addDesignation == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error");
                        TempData["EM"] = "Error.";
                        return View(Designation);
                    }
                    else if (addDesignation == CommonConstantHelper.Failed)
                    {
                        ModelState.AddModelError("", "Failed");
                        return View(Designation);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error Not Recognized");
                        return View();
                    }
                }
                catch (Exception exception)
                {
                    // ViewBag.DesignationId = new SelectList(_DesignationManager.GetAllDesignationList(), "DesignationListId", "DesignationName", user.DesignationId);
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

        // GET: Designation/Edit/5
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
                        Designation Designation = _DesignationManager.GetDesignationById(id);

                        if (Designation == null)
                            return HttpNotFound();

                        Designation updatableDesignationData = new Designation()
                        {

                            IsActive = Designation.IsActive,
                            IsDeleted = Designation.IsDeleted,
                            Description = Designation.Description,
                            DesignationId = Designation.DesignationId,
                            DesignationName = Designation.DesignationName
                        };

                        return View(updatableDesignationData);
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

        // POST: Designation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Designation Designation)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    Designation DesignationVM = _DesignationManager.GetDesignationById(Designation.DesignationId);

                    if (DesignationVM == null)
                        return HttpNotFound();

                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError("", errors.ToString());
                        return View(Designation);
                    }

                    if (Designation.DesignationName == null)
                    {
                        ModelState.AddModelError("", "Designation is required.");
                        //TempData["EM"] = "User name required.";
                        return View(Designation);
                    }

                    Designation.IsDeleted = null;


                    string updateDesignation = _DesignationManager.DesignationUpdate(Designation);

                    if (updateDesignation == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "Successfully updated Designation.";
                        return RedirectToAction("Index", "Designation");
                    }
                    else if (updateDesignation == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "Designation already exist.");
                        //TempData["EM"] = "User name required.";
                        return View();
                    }
                    else if (updateDesignation == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error.");
                        return View();
                    }
                    else if (updateDesignation == CommonConstantHelper.Failed)
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
