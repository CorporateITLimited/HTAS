using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COMSApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly bool CanAccess = false;
        //private readonly bool CanReadWrite = false;
        public HomeController()
        {
            if (System.Web.HttpContext.Current.Session["ListofPermissions"] != null)
            {
                List<UserPermission> userPermisson = (List<UserPermission>)System.Web.HttpContext.Current.Session["ListofPermissions"];
                var single_permission = userPermisson.Where(p => p.ControllerName == "Home").FirstOrDefault();
                if (single_permission.ReadWriteStatus != null && single_permission.CanAccess != null)
                {
                    if (single_permission.CanAccess == true)
                    {
                        CanAccess = true;
                    }
                    if (single_permission.ReadWriteStatus == true)
                    {
                        //CanReadWrite = true;
                    }
                }
            }
        }


        // GET: Home
        public ActionResult Index()
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //        && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //        && (Session[CommonConstantHelper.UserId] != null))
            //{
            //    if (CanAccess)
            //    {
            return View();
            //    }
            //    else
            //    {
            //        TempData["PM"] = "Permission Denied.";
            //        return RedirectToAction("LogIn", "Account");
            //    }
            //}
            //else
            //{
            //    TempData["EM"] = "Session Expired.";
            //    return RedirectToAction("LogIn", "Account");
            //}
        }


        public ActionResult OfficeExpense()
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //        && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //        && (Session[CommonConstantHelper.UserId] != null))
            //{
            return View();
            //}
            //else
            //{
            //    TempData["EM"] = "Session Expired.";
            //    return RedirectToAction("LogIn", "Account");
            //}
        }



        public ActionResult EmployeeManagement()
        {
            if ((Session[CommonConstantHelper.LogInCredentialId] != null)
                    && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
                    && (Session[CommonConstantHelper.UserId] != null))
            {
                if (CanAccess)
                {
                    return View();
                }
                else
                {
                    TempData["PM"] = "Permission Denied.";
                    return RedirectToAction("LogIn", "Account");
                }
            }
            else
            {
                TempData["EM"] = "Session Expired.";
                return RedirectToAction("LogIn", "Account");
            }
        }


        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Reports()
        {
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewDynamicReports(string param)
        {
            try
            {
                Session[CommonConstantHelper.ReportParameter] = param;
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
