using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoldingTaxWebApp.Gateway;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Models;
using HoldingTaxWebApp.Models.Constant;
using HoldingTaxWebApp.ViewModels;

namespace HoldingTaxWebApp.Controllers
{
    public class RentTaxTateController : Controller
    {
        private readonly ConstantGateway _constantGateway;
        private readonly BuildingTypeManager _buildingTypeManager;
        private readonly DOHSAreaManager _dOHSAreaManager;

        public RentTaxTateController()
        {
            _constantGateway = new ConstantGateway();
            _buildingTypeManager = new BuildingTypeManager();
            _dOHSAreaManager = new DOHSAreaManager();
        }


        // GET: RentTaxTate
        public ActionResult Index()
        {
            try
            {
                List<RentTaxRate> rentTaxRatesList = new List<RentTaxRate>();
                rentTaxRatesList = _constantGateway.GetAllRentTaxRates();

                return View(rentTaxRatesList.ToList());
            }
            catch (Exception ex)
            {
                TempData["SM"] = ex.Message.ToString();
                return View();
            }
        }

        // GET: RentTaxTate/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if (id <= 0)
                    return HttpNotFound();

                RentTaxRate dataRentTax = _constantGateway.GetAllRentTaxRatesById(id);
                if (dataRentTax == null)
                    return HttpNotFound();

                return View(dataRentTax);
            }
            catch (Exception ex)
            {
                TempData["SM"] = ex.Message.ToString();
                return View();
            }
        }

        // GET: RentTaxTate/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
                ViewBag.BuildingTypeId = new SelectList(_buildingTypeManager.GetAllBuildingType(), "BuildingTypeId", "BuildingTypeName");
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text");

                return View();
            }
            catch (Exception ex)
            {
                TempData["SM"] = ex.Message.ToString();
                return RedirectToAction("Index", "RentTaxTate");
            }
        }

        // POST: RentTaxTate/Create
        [HttpPost]
        public ActionResult Create(RentTaxRate rent)
        {
            try
            {
                if (rent == null)
                    return HttpNotFound();

                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", rent.AreaId);
                ViewBag.BuildingTypeId = new SelectList(_buildingTypeManager.GetAllBuildingType(), "BuildingTypeId", "BuildingTypeName", rent.BuildingTypeId);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", rent.IsActive);

                if (rent.AreaId == null || rent.AreaId <= 0)
                {
                    ModelState.AddModelError("", "এলাকা নির্বাচন করুন");
                    return View(rent);
                }

                if (rent.BuildingTypeId == null || rent.BuildingTypeId <= 0)
                {
                    ModelState.AddModelError("", "ভবনের ধরণ নির্বাচন করুন");
                    return View(rent);
                }

                if (rent.PerSqfRent <= 0)
                {
                    ModelState.AddModelError("", "প্রতি বর্গফুটের ভাড়া দিন");
                    return View(rent);
                }

                rent.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                rent.CreateDate = DateTime.Now;
                rent.IsActive = true;
                rent.IsDeleted = false;
                rent.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                rent.LastUpdated = DateTime.Now;

                int saveData = _constantGateway.RentTaxRatesInsert(rent);

                if (saveData == 202)
                {
                    TempData["SM"] = "সফলভাবে নতুন গৃহকরের হার সাবমিট হয়েছে";
                    return RedirectToAction("Index", "RentTaxTate");
                }
                else if (saveData == 401)
                {
                    ModelState.AddModelError("", "একই এলাকার একই ভবনের ধরণের জন্য বর্গফুটের ভাড়া ডাটাবেজে বিদ্যমান ");
                    return View(rent);
                }
                else if (saveData == 500)
                {
                    ModelState.AddModelError("", "Error");
                    TempData["EM"] = "Error.";
                    return View(rent);
                }
                else
                {
                    ModelState.AddModelError("", "Error Not Recognized");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: RentTaxTate/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (id <= 0)
                    return HttpNotFound();

                RentTaxRate dataRentTax = _constantGateway.GetAllRentTaxRatesById(id);
                if (dataRentTax == null)
                    return HttpNotFound();

                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", dataRentTax.AreaId);
                ViewBag.BuildingTypeId = new SelectList(_buildingTypeManager.GetAllBuildingType(), "BuildingTypeId", "BuildingTypeName", dataRentTax.BuildingTypeId);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", dataRentTax.IsActive);

                return View(dataRentTax);
            }
            catch (Exception ex)
            {
                TempData["SM"] = ex.Message.ToString();
                return View();
            }
        }

        // POST: RentTaxTate/Edit/5
        [HttpPost]
        public ActionResult Edit(RentTaxRate rent)
        {
            try
            {
                if (rent == null)
                    return HttpNotFound();

                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", rent.AreaId);
                ViewBag.BuildingTypeId = new SelectList(_buildingTypeManager.GetAllBuildingType(), "BuildingTypeId", "BuildingTypeName", rent.BuildingTypeId);
                ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", rent.IsActive);


                if (rent.RentTaxRateId <= 0)
                {
                    ModelState.AddModelError("", "নিরাপত্তা ভঙ্গ");
                    return View(rent);
                }

                if (rent.AreaId == null || rent.AreaId <= 0)
                {
                    ModelState.AddModelError("", "এলাকা নির্বাচন করুন");
                    return View(rent);
                }

                if (rent.BuildingTypeId == null || rent.BuildingTypeId <= 0)
                {
                    ModelState.AddModelError("", "ভবনের ধরণ নির্বাচন করুন");
                    return View(rent);
                }

                if (rent.PerSqfRent <= 0)
                {
                    ModelState.AddModelError("", "প্রতি বর্গফুটের ভাড়া দিন");
                    return View(rent);
                }

                rent.CreatedBy = null;
                rent.CreateDate = null;
                rent.IsDeleted = null;
                rent.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                rent.LastUpdated = DateTime.Now;

                int saveData = _constantGateway.RentTaxRatesUpdate(rent);

                if (saveData == 202)
                {
                    TempData["SM"] = "সফলভাবে গৃহকরের হার এর তথ্য হালনাগাদ করা হয়েছে ";
                    return RedirectToAction("Index", "RentTaxTate");
                }
                else if (saveData == 401)
                {
                    ModelState.AddModelError("", "একই এলাকার একই ভবনের ধরণের জন্য বর্গফুটের ভাড়া ডাটাবেজে বিদ্যমান ");
                    return View(rent);
                }
                else if (saveData == 500)
                {
                    ModelState.AddModelError("", "Error");
                    TempData["EM"] = "Error.";
                    return View(rent);
                }
                else
                {
                    ModelState.AddModelError("", "Error Not Recognized");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: RentTaxTate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RentTaxTate/Delete/5
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
    }
}
