using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.Constant;
using HoldingTaxWebApp.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Constant
{
    public class ConstantValueController : Controller
    {
        private readonly ConstantValueManager _ConstantValueManager;

        public ConstantValueController()
        {
            _ConstantValueManager = new ConstantValueManager();
        }


        // GET: ConstantValue
        public ActionResult Index()
        {
            try
            {
                var ConstantValueList = new List<ConstantValue>();


                List<ConstantValue> ConstantValueListVM = new List<ConstantValue>();
               
                if (Session[CommonConstantHelper.UserTypeId].ToString() == "1")
                {
                    ConstantValueList = _ConstantValueManager.GetAllConstantValue();
                    foreach (var item in ConstantValueList)
                    {
                        ConstantValue ConstantValueVM = new ConstantValue()
                        {
                            constantValueId = item.constantValueId,
                            DueCharge = item.DueCharge,
                            DueChargeRef = item.DueChargeRef,
                            Rebate = item.Rebate,
                            RebateRef = item.RebateRef,
                            RentMonth = item.RentMonth,
                            RentMonthRef = item.RentMonthRef,
                            RentTaxRate = item.RentTaxRate,
                            RentTaxRateRef = item.RentTaxRateRef,
                            Surcharge = item.Surcharge,
                            SurchargeRef = item.SurchargeRef,
                            WrongInfoCharge = item.WrongInfoCharge,
                            WrongInfoChargeRef = item.WrongInfoChargeRef,
                            OwnFlatDiscount = item.OwnFlatDiscount,
                            OwnFlatDiscountRef = item.OwnFlatDiscountRef,

                            IsActive = item.IsActive,
                            IsDeleted = item.IsDeleted,
                            CreateDate = item.CreateDate,
                            CreatedBy = item.CreatedBy,
                            CreatedByUserName = item.CreatedByUserName,
                            LastUpdated = item.LastUpdated,
                            LastUpdatedBy = item.LastUpdatedBy,
                            UpdatedByUserName = item.UpdatedByUserName

                        };
                        ConstantValueListVM.Add(ConstantValueVM);
                    }
                }
                else
                {
                    return RedirectToAction("LogIn", "Account");
                }

                return View(ConstantValueListVM);
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // GET: ConstantValue/Details/5
        public ActionResult Details()
        {
            try
            {
                var item = _ConstantValueManager.GetConstantValueById();
              
                if (item == null)
                    return HttpNotFound();


                ConstantValue ConstantValueVM = new ConstantValue()
                {
                    constantValueId = item.constantValueId,
                    DueCharge = item.DueCharge,
                    DueChargeRef = item.DueChargeRef,
                    Rebate = item.Rebate,
                    RebateRef = item.RebateRef,
                    RentMonth = item.RentMonth,
                    RentMonthRef = item.RentMonthRef,
                    RentTaxRate = item.RentTaxRate,
                    RentTaxRateRef = item.RentTaxRateRef,
                    Surcharge = item.Surcharge,
                    SurchargeRef = item.SurchargeRef,
                    WrongInfoCharge = item.WrongInfoCharge,
                    WrongInfoChargeRef = item.WrongInfoChargeRef,
                    OwnFlatDiscount = item.OwnFlatDiscount,
                    OwnFlatDiscountRef = item.OwnFlatDiscountRef,

                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted,
                    CreateDate = item.CreateDate,
                    CreatedBy = item.CreatedBy,
                    CreatedByUserName = item.CreatedByUserName,
                    LastUpdated = item.LastUpdated,
                    LastUpdatedBy = item.LastUpdatedBy,
                    UpdatedByUserName = item.UpdatedByUserName

                };



                return View(ConstantValueVM);
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // GET: ConstantValue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConstantValue/Create
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

        // GET: ConstantValue/Edit/5
        public ActionResult Edit()
        {
            try
            {
                var item = _ConstantValueManager.GetConstantValueById();

                if (item == null)
                    return HttpNotFound();


                ConstantValue ConstantValueVM = new ConstantValue()
                {
                    constantValueId = item.constantValueId,
                    DueCharge = item.DueCharge,
                    DueChargeRef = item.DueChargeRef,
                    Rebate = item.Rebate,
                    RebateRef = item.RebateRef,
                    RentMonth = item.RentMonth,
                    RentMonthRef = item.RentMonthRef,
                    RentTaxRate = item.RentTaxRate,
                    RentTaxRateRef = item.RentTaxRateRef,
                    Surcharge = item.Surcharge,
                    SurchargeRef = item.SurchargeRef,
                    WrongInfoCharge = item.WrongInfoCharge,
                    WrongInfoChargeRef = item.WrongInfoChargeRef,
                    OwnFlatDiscount = item.OwnFlatDiscount,
                    OwnFlatDiscountRef = item.OwnFlatDiscountRef,

                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted,
                    CreateDate = item.CreateDate,
                    CreatedBy = item.CreatedBy,
                    CreatedByUserName = item.CreatedByUserName,
                    LastUpdated = item.LastUpdated,
                    LastUpdatedBy = item.LastUpdatedBy,
                    UpdatedByUserName = item.UpdatedByUserName

                };



                return View(ConstantValueVM);
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // POST: ConstantValue/Edit/5
        [HttpPost]
        public ActionResult Edit(ConstantValue Item)
        {
            try
            {
                if (Item == null)
                    return HttpNotFound();

                Item.IsActive = true;
                Item.IsDeleted = false;
                //Item.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                //Item.CreateDate = DateTime.Now;
                Item.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                Item.LastUpdated = DateTime.Now;


                string addConstantValue = _ConstantValueManager.ConstantValueUpdate(Item);

                if (addConstantValue == CommonConstantHelper.Success)
                {
                    TempData["SM"] = "সফলভাবে হালনাগাদ করা হয়েছে";
                    return RedirectToAction("Details", "ConstantValue");
                }
                else if (addConstantValue == CommonConstantHelper.Conflict)
                {
                    ModelState.AddModelError("", "তথ্য ডাটাবেজে বিদ্যমান ৱয়েছে");
                    return View(Item);
                }
                else if (addConstantValue == CommonConstantHelper.Error)
                {
                    ModelState.AddModelError("", "Error");
                    TempData["EM"] = "Error.";
                    return View(Item);
                }
                else if (addConstantValue == CommonConstantHelper.Failed)
                {
                    ModelState.AddModelError("", "Failed");
                    return View(Item);
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

        // GET: ConstantValue/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ConstantValue/Delete/5
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
