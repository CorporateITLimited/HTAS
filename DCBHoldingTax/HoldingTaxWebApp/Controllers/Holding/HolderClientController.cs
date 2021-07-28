using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Models.Holding;
using HoldingTaxWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Holding
{
    public class HolderClientController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly FinancialYearManager _financialYearManager;
        private readonly HoldingManager _holdingManager;

        public HolderClientController()
        {
            _financialYearManager = new FinancialYearManager();
            _holdingManager = new HoldingManager();
        }

        // GET: HolderClient
        public ActionResult Index()
        {
            //ViewBag.FinancialYearId =  new SelectList(_financialYearManager.GetAllFinancialYear(), "FinancialYearId", "FinancialYear");
            return View(_financialYearManager.GetAllFinancialYear());
        }

        public ActionResult GetHoldersTaxInformation(int id)
        {
            Session["FinancialYearId"] = id > 0 ? id : (object)null;

            return View();
        }

        public ActionResult HolderProfile()
        {
            try
            {
                int id = Session[CommonConstantHelper.HolderId] != null ? Convert.ToInt32(Session[CommonConstantHelper.HolderId]) : 0;
                if (id <= 0)
                    return RedirectToAction("Holder", "Home");

                Holder holder = _holdingManager.GetHolderById(id);
                if (holder == null)
                    return HttpNotFound();

                List<HolderFlat> listOfHolders = _holdingManager.GetHoldersFlatByHolderId(id);
                //if (listOfHolders == null || listOfHolders.Count() <= 0)
                //    return HttpNotFound();

                HolderVM holderVMOtherData = _holdingManager.GetAllotmentNamjariDesignByPlotId(holder.PlotId);

                HolderVM vm = new HolderVM()
                {
                    AmountOfLand = holder.AmountOfLand,
                    AreaName = holder.AreaName,
                    BuildingTypeName = holder.BuildingTypeName,
                    Contact1 = holder.Contact1,
                    Contact2 = holder.Contact2,
                    ContactAdd = holder.ContactAdd,
                    Document1 = holder.Document1,
                    Document2 = holder.Document2,
                    EachFloorArea = holder.EachFloorArea,
                    Email = holder.Email,
                    Father = holder.Father,
                    GenderType = holder.GenderType,
                    HolderId = holder.HolderId,
                    HolderName = holder.HolderName,
                    HoldersFlatNumber = holder.HoldersFlatNumber,
                    ImageLocation = holder.ImageLocation,
                    IsActive = holder.IsActive,
                    MaritialStatusType = holder.MaritialStatusType,
                    Mother = holder.Mother,
                    NID = holder.NID,
                    OwnerTypeName = holder.OwnerTypeName,
                    PermanentAdd = holder.PermanentAdd,
                    PlotNo = holder.PlotNo,
                    PresentAdd = holder.PresentAdd,
                    PreviousDueTax = holder.PreviousDueTax,
                    SourceName = holder.SourceName,
                    Spouse = holder.Spouse,
                    TotalFlat = holder.TotalFlat,
                    TotalFloor = holder.TotalFloor,
                    RoadName = holder.RoadName,
                    RoadNo = holder.RoadNo,
                    AreaPlotFlatData = holder.AreaPlotFlatData,
                    NamjariLetterNo = holder.NamjariLetterNo,
                    AllocationLetterNo = holder.AllocationLetterNo,

                    // list
                    HolderFlatList = listOfHolders,

                    // id values
                    AreaId = holder.AreaId,
                    BuildingTypeId = holder.BuildingTypeId,
                    Gender = holder.Gender,
                    MaritialStatus = holder.MaritialStatus,
                    OwnershipSourceId = holder.OwnershipSourceId,
                    OwnerType = holder.OwnerType,
                    PlotId = holder.PlotId,

                    // converted values
                    StrAmountOfLand = holder.StrAmountOfLand,
                    StrPreviousDueTax = holder.StrPreviousDueTax,
                    StrEachFloorArea = holder.StrEachFloorArea,
                    StrHoldersFlatNumber = holder.StrHoldersFlatNumber,
                    StrTotalFlat = holder.StrTotalFlat,
                    StrTotalFloor = holder.StrTotalFloor,

                    // action by
                    CreatedByUsername = holder.CreatedByUsername,
                    UpdatedByUsername = holder.UpdatedByUsername,

                    // string date
                    StringAllocationDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holder.StringAllocationDate),
                    StringNamjariDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holder.StringNamjariDate),
                    StringRecordCorrectionDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holder.StringRecordCorrectionDate),
                    StringLastUpdated = holder.StringLastUpdated,
                    StringCreateDate = holder.StringCreateDate,

                    // other data allotment namjari design approval
                    FirstApprovalLetterNo = holderVMOtherData.FirstApprovalLetterNo,
                    StringFirstApprovalDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holderVMOtherData.StringFirstApprovalDate),
                    LastApprovalLetterNo = holderVMOtherData.LastApprovalLetterNo,
                    StringLastApprovalDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holderVMOtherData.StringLastApprovalDate),
                    LeasePeriod = holderVMOtherData.LeasePeriod,
                    StrLeasePeriod = BanglaConvertionHelper.IntegerValueEnglish2Bangla(holderVMOtherData.LeasePeriod),
                    StringLeaseExpiryDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holderVMOtherData.StringLeaseExpiryDate),
                    PlotOwnerName = holderVMOtherData.PlotOwnerName
                };


                // rent per square feet
                // boraddo grohitar nam, ejara meyad, meydurtinner tarik, 1st noksa onumodon tarik, onumodon number,  last noksa onumodon tarik,onumodon number

                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["EM"] = "Error " + ex.Message.ToString();
                return RedirectToAction("Holder", "Home");
            }
        }

    }
}