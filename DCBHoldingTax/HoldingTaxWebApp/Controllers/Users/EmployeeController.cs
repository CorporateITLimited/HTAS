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
    public class EmployeeController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly EmployeeManager _employeeManager;
        private readonly DesignationManager _DesignationManager;

        public EmployeeController()
        {
            _employeeManager = new EmployeeManager();
            _DesignationManager = new DesignationManager();
            if (System.Web.HttpContext.Current.Session["ListofPermissions"] != null)
            {
                List<UserPermission> userPermisson = (List<UserPermission>)System.Web.HttpContext.Current.Session["ListofPermissions"];
                var single_permission = userPermisson.Where(p => p.ControllerName == "EmployeeController").FirstOrDefault();
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
                        var EmployeeList = _employeeManager.GetAllEmployeeList();
                        List<Employee> EmployeeListVM = new List<Employee>();
                        foreach (var item in EmployeeList)
                        {
                            Employee Employee = new Employee()
                            {
                                ContactNo = item.ContactNo,
                                CreateDate = item.CreateDate,
                                CreatedByUserName = item.CreatedByUserName,
                                // Description = item.Description,
                                DesignationId = item.DesignationId,
                                DesignationName = item.DesignationName,
                                DOB = item.DOB,
                                EmployeeName = item.EmployeeName,
                                EmpolyeeId = item.EmpolyeeId,
                                FatherName = item.FatherName,
                                MotherName = item.MotherName,
                                NID = item.NID,
                                Remarks = item.Remarks,
                                Sex = item.Sex,
                                CreatedBy = item.CreatedBy,
                                Email = item.Email,
                                IsActive = item.IsActive,
                                IsDeleted = item.IsDeleted,
                                LastUpdated = item.LastUpdated,
                                LastUpdatedBy = item.LastUpdatedBy,
                                UpdatedByUserName = item.UpdatedByUserName,
                                StringDOB = $"{item.DOB:dd/MM/yyyy}",
                                EmployeeAddress = item.EmployeeAddress
                            };
                            EmployeeListVM.Add(Employee);
                        }

                        return View(EmployeeListVM.ToList());
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
                TempData["EM"] = "Session Expired.";
                return RedirectToAction("LogIn", "Account");
            }
        }

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
                        Employee Employee = _employeeManager.GetEmployeeById(id);

                        if (Employee == null)
                            return HttpNotFound();
                        Employee.StringDOB = $"{Employee.DOB:dd/MM/yyyy}";

                        return View(Employee);
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
                TempData["EM"] = "Session Expired.";
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
                    ViewBag.DesignationId = new SelectList(_DesignationManager.GetAllDesignation(), "DesignationId", "DesignationName");

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
                TempData["EM"] = "Session Expired.";
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    ViewBag.DesignationId = new SelectList(_DesignationManager.GetAllDesignation(), "DesignationId", "DesignationName", employee.DesignationId);

                    if (employee == null)
                        return HttpNotFound();

                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError("", errors.ToString());
                        return View(employee);
                    }

                    if (string.IsNullOrEmpty(employee.EmployeeName))
                    {
                        ModelState.AddModelError("", "কর্মকর্তা বা কর্মচারীর নাম অবশ্যই পূরণ করতে হবে");
                        return View(employee);
                    }

                    if (string.IsNullOrEmpty(employee.NID))
                    {
                        ModelState.AddModelError("", "এনআইডি নম্বর অবশ্যই পূরণ করতে হবে");
                        return View(employee);
                    }

                    if (employee.DesignationId == 0)
                    {
                        ModelState.AddModelError("", "পদবি অবশ্যই পূরণ করতে হবে");
                        return View(employee);
                    }

                    if (employee.Sex == null)
                    {
                        ModelState.AddModelError("", "লিঙ্গ নির্বাচন করুন");
                        return View(employee);
                    }

                    if (employee.StringDOB != null)
                        employee.DOB = DateTime.ParseExact(employee.StringDOB, "dd/MM/yyyy", null);

                    employee.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                    employee.CreateDate = DateTime.Now;
                    employee.IsActive = true;
                    employee.IsDeleted = false;
                    employee.LastUpdated = DateTime.Now;
                    employee.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);


                    string addEmployee = _employeeManager.EmployeeInsert(employee);

                    if (addEmployee == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "সফলভাবে নতুন কর্মকর্তা বা কর্মচারী সংযুক্ত করা হয়েছে";
                        return RedirectToAction("Index", "Employee");
                    }
                    else if (addEmployee == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "কর্মকর্তা বা কর্মচারী ডেটাবেজে বিদ্যমান রয়েছে");
                        return View(employee);
                    }
                    else if (addEmployee == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error");
                        return View();
                    }
                    else if (addEmployee == CommonConstantHelper.Failed)
                    {
                        ModelState.AddModelError("", "Failed");
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error Not Recognized");
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
                        Employee Employee = _employeeManager.GetEmployeeById(id);

                        if (Employee == null)
                            return HttpNotFound();


                        Employee updatableLAData = new Employee()
                        {
                            ContactNo = Employee.ContactNo,
                            CreateDate = Employee.CreateDate,
                            CreatedByUserName = Employee.CreatedByUserName,
                            //Description = Employee.Description,
                            DesignationId = Employee.DesignationId,
                            DesignationName = Employee.DesignationName,
                            DOB = Employee.DOB,
                            StringDOB = $"{Employee.DOB:dd/MM/yyyy}",
                            EmployeeName = Employee.EmployeeName,
                            EmpolyeeId = Employee.EmpolyeeId,
                            FatherName = Employee.FatherName,
                            MotherName = Employee.MotherName,
                            NID = Employee.NID,
                            Remarks = Employee.Remarks,
                            Sex = Employee.Sex,
                            CreatedBy = Employee.CreatedBy,
                            Email = Employee.Email,
                            IsActive = Employee.IsActive,
                            IsDeleted = Employee.IsDeleted,
                            LastUpdated = Employee.LastUpdated,
                            LastUpdatedBy = Employee.LastUpdatedBy,
                            EmployeeAddress = Employee.EmployeeAddress
                            //UpdatedByUserName = Employee.UpdatedByUserName,
                        };

                        ViewBag.designationId = new SelectList(_DesignationManager.GetAllDesignation(), "designationId", "DesignationName", updatableLAData.DesignationId);
                        return View(updatableLAData);
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
                TempData["EM"] = "Session Expired.";
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (CanAccess && CanReadWrite)
            {
                try
                {
                    if (employee == null)
                        return HttpNotFound();

                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError("", errors.ToString());
                        return View(employee);
                    }

                    if (string.IsNullOrEmpty(employee.EmployeeName))
                    {
                        ModelState.AddModelError("", "কর্মকর্তা বা কর্মচারীর নাম অবশ্যই পূরণ করতে হবে");
                        return View(employee);
                    }

                    if (string.IsNullOrEmpty(employee.NID))
                    {
                        ModelState.AddModelError("", "এনআইডি নম্বর অবশ্যই পূরণ করতে হবে");
                        return View(employee);
                    }

                    if (employee.DesignationId == 0)
                    {
                        ModelState.AddModelError("", "পদবি অবশ্যই পূরণ করতে হবে");
                        return View(employee);
                    }

                    if (employee.Sex == null)
                    {
                        ModelState.AddModelError("", "লিঙ্গ নির্বাচন করুন");
                        return View(employee);
                    }
                    if (employee.StringDOB != null)
                        employee.DOB = DateTime.ParseExact(employee.StringDOB, "dd/MM/yyyy", null);

                    employee.CreatedBy = null;
                    employee.CreateDate = null;
                    employee.IsActive = null;
                    employee.IsDeleted = null;
                    employee.LastUpdated = DateTime.Now;
                    employee.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);

                    string updateEmployee = _employeeManager.EmployeeUpdate(employee);

                    if (updateEmployee == CommonConstantHelper.Success)
                    {
                        TempData["SM"] = "সফলভাবে কর্মকর্তা বা কর্মচারীর তথ্য হালনাগাদ করা হয়েছে";
                        return RedirectToAction("Index", "Employee");
                    }
                    else if (updateEmployee == CommonConstantHelper.Conflict)
                    {
                        ModelState.AddModelError("", "কর্মকর্তা বা কর্মচারী ডেটাবেজে বিদ্যমান রয়েছে");
                        return View(employee);
                    }
                    else if (updateEmployee == CommonConstantHelper.Error)
                    {
                        ModelState.AddModelError("", "Error.");
                        return View();
                    }
                    else if (updateEmployee == CommonConstantHelper.Failed)
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
