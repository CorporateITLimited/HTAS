using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Plots;
using HoldingTaxWebApp.Models.Plots;
using HoldingTaxWebApp.ViewModels.Plots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Plots
{
    public class PlotOwnerController : Controller
    {
        private readonly PlotOwnerManager _PlotOwnerManager;
        private readonly PlotManager _PlotManager;
        //private readonly OwnershipSourceManager _OwnershipSourceManager;
        private readonly LeaseQuotaManager _LeaseQuotaManager;
        private readonly OfficialStatusManager _OfficialStatusManager;
        private readonly ConstructionStatusManager _ConstructionStatusManager;

        public PlotOwnerController()
        {
            _PlotOwnerManager = new PlotOwnerManager();
            _PlotManager = new PlotManager();
           //_OwnershipSourceManager = new OwnershipSourceManager();
            _LeaseQuotaManager = new LeaseQuotaManager();
            _OfficialStatusManager = new OfficialStatusManager();
            _ConstructionStatusManager = new ConstructionStatusManager();
        }

        // GET: PlotOwner
        public ActionResult Index()
        {
            try
            {
                var PlotOwnerList = new List<PlotOwner>();
                PlotOwnerList = _PlotOwnerManager.GetAllPlotOwner();

                List<PlotOwner> PlotOwnerListVM = new List<PlotOwner>();
                foreach (var item in PlotOwnerList)
                {
                    PlotOwner PlotOwnerVM = new PlotOwner()
                    {

                       IsActive = item.IsActive,
                       IsDeleted = item.IsDeleted,

                       PlotOwnerId = item.PlotOwnerId,
                       PlotOwnerName = item.PlotOwnerName,
                       ConsStatusId = item.ConsStatusId,
                       ConsStatusName = item.ConsStatusName,
                       CreateDate = item.CreateDate,
                       CreatedBy = item.CreatedBy,
                       CreatedByUserName = item.CreatedByUserName,
                       Doc1 = item.Doc1,
                       Doc2 = item.Doc2,
                       Doc3 = item.Doc3,
                       Doc4 = item.Doc4,
                       Doc5 = item.Doc5,
                       Doc6 = item.Doc6,
                       Email = item.Email,
                       HandOverLetterNo = item.HandOverLetterNo,
                       HandOverOffice = item.HandOverOffice,
                       IsAlive = item.IsAlive,
                       LandDevelopChange = item.LandDevelopChange,
                       LastUpdated = item.LastUpdated,
                       LastUpdatedBy = item.LastUpdatedBy,
                       LeaseAuthority = item.LeaseAuthority,
                       LeasePeriod = item.LeasePeriod,
                       LeaseQuotaId = item.LeaseQuotaId,
                       LeaseQuotaName = item.LeaseQuotaName,
                       LeaseType = item.LeaseType,
                       LeaveDate = item.LeaveDate,
                       LeaveExPeriod = item.LeaveExPeriod,
                       OfficialStatusId = item.OfficialStatusId,
                       OffStatusName = item.OffStatusName,
                       PermanentAdd = item.PermanentAdd,
                       PhoneNumber = item.PhoneNumber,
                       PlotId = item.PlotId,
                       PlotIdNumber = item.PlotIdNumber,
                       PresentAdd = item.PresentAdd,
                       StringLeaveDate = $"{item.LeaveDate:dd/MM/yyyy}",
                       TotalArea = item.TotalArea,
                       UpdatedByUserName = item.UpdatedByUserName

                    };
                    PlotOwnerListVM.Add(PlotOwnerVM);
                }
                return View(PlotOwnerListVM.ToList());
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // GET: PlotOwner/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var PlotOwnerdetails = _PlotOwnerManager.GetPlotOwnerById(id);

                if (PlotOwnerdetails == null)
                    return HttpNotFound();
                int plotId = PlotOwnerdetails.PlotId;
                int ownerId = PlotOwnerdetails.PlotOwnerId;
                var ConstructionProgress = _PlotOwnerManager.GetConstructionProgressById(plotId);
                var UnauthPortion = _PlotOwnerManager.GetUnauthPortionById(plotId);
                var DesignApproval = _PlotOwnerManager.GetDesignApprovalById(plotId);
                var OthetPlotOwner = _PlotOwnerManager.GetOthetPlotOwnerById(ownerId);


                List<DesignApproval> DesignApprovalVM = new List<DesignApproval>();
                foreach (var item in DesignApproval)
                {
                    DesignApproval Approval = new DesignApproval()
                    {
                        StringApprovalDate = $"{item.ApprovalDate:dd/MM/yyyy}",
                        StringMEO_NCCDate = $"{item.MEO_NCCDate:dd/MM/yyyy}"
                    };
                    DesignApprovalVM.Add(Approval);
                }

                PlotOwnerCombineVM plotOwnerVM = new PlotOwnerCombineVM()
                {
                    PlotOwnerId = PlotOwnerdetails.PlotOwnerId,
                    PlotId = PlotOwnerdetails.PlotId,
                    PlotIdNumber = PlotOwnerdetails.PlotIdNumber,
                    PlotOwnerName = PlotOwnerdetails.PlotOwnerName,
                    IsAlive = PlotOwnerdetails.IsAlive,
                    OfficialStatusId = PlotOwnerdetails.OfficialStatusId,
                    OffStatusName = PlotOwnerdetails.OffStatusName,
                    PresentAdd = PlotOwnerdetails.PresentAdd,
                    PermanentAdd = PlotOwnerdetails.PermanentAdd,
                    PhoneNumber = PlotOwnerdetails.PhoneNumber,
                    Email = PlotOwnerdetails.Email,
                    LeaveDate = PlotOwnerdetails.LeaveDate,
                    StringLeaveDate = $"{PlotOwnerdetails.LeaveDate:dd/MM/yyyy}",
                    LeaseAuthority = PlotOwnerdetails.LeaseAuthority,
                    LeaseType = PlotOwnerdetails.LeaseType,
                    LeasePeriod = PlotOwnerdetails.LeasePeriod,
                    LeaseQuotaId = PlotOwnerdetails.LeaseQuotaId,
                    LeaseQuotaName = PlotOwnerdetails.LeaseQuotaName,
                    HandOverOffice = PlotOwnerdetails.HandOverOffice,
                    HandOverLetterNo = PlotOwnerdetails.HandOverLetterNo,
                    LandDevelopChange = PlotOwnerdetails.LandDevelopChange,
                    ConsStatusId = PlotOwnerdetails.ConsStatusId,
                    ConsStatusName = PlotOwnerdetails.ConsStatusName,
                    Doc1 = PlotOwnerdetails.Doc1,
                    Doc2 = PlotOwnerdetails.Doc2,
                    Doc3 = PlotOwnerdetails.Doc3,
                    Doc4 = PlotOwnerdetails.Doc4,
                    Doc5 = PlotOwnerdetails.Doc5,
                    Doc6 = PlotOwnerdetails.Doc6,
                    CreateDate = PlotOwnerdetails.CreateDate,
                    CreatedBy = PlotOwnerdetails.CreatedBy,
                    LastUpdated = PlotOwnerdetails.LastUpdated,
                    LastUpdatedBy = PlotOwnerdetails.LastUpdatedBy,
                    IsActive = PlotOwnerdetails.IsActive,
                    IsDeleted = PlotOwnerdetails.IsDeleted,
                    /////

                    ConsProgressId = ConstructionProgress.ConsProgressId,
                    OwnerDeclaration = ConstructionProgress.OwnerDeclaration,
                    RealBuilder = ConstructionProgress.RealBuilder,
                    DevelopDeposit = ConstructionProgress.DevelopDeposit,
                    FloorNumber = ConstructionProgress.FloorNumber,
                    CompletionDate = ConstructionProgress.CompletionDate,
                    GroundFCDate = ConstructionProgress.GroundFCDate,
                    FirstFCDate = ConstructionProgress.FirstFCDate,
                    SccFCDate = ConstructionProgress.SccFCDate,
                    ThirdFCDate = ConstructionProgress.ThirdFCDate,
                    ForthFCDate = ConstructionProgress.ForthFCDate,
                    FivthFCDate = ConstructionProgress.FivthFCDate,
                    SixFCDate = ConstructionProgress.SixFCDate,
                    OtherFCDate = ConstructionProgress.OtherFCDate,
                    OwnerPortion = ConstructionProgress.OwnerPortion,
                    DeveloperPortion = ConstructionProgress.DeveloperPortion,
                    BuyerPortion = ConstructionProgress.BuyerPortion,
                    SubmittedPortion = ConstructionProgress.SubmittedPortion,



                    StringCompletionDate = $"{ConstructionProgress.CompletionDate:dd/MM/yyyy}",
                    StringGroundFCDate = $"{ConstructionProgress.GroundFCDate:dd/MM/yyyy}",
                    StringFirstFCDate = $"{ConstructionProgress.FirstFCDate:dd/MM/yyyy}",
                    StringSccFCDate = $"{ConstructionProgress.SccFCDate:dd/MM/yyyy}",
                    StringThirdFCDate = $"{ConstructionProgress.ThirdFCDate:dd/MM/yyyy}",
                    StringForthFCDate = $"{ConstructionProgress.ForthFCDate:dd/MM/yyyy}",
                    StringFivthFCDate = $"{ConstructionProgress.FivthFCDate:dd/MM/yyyy}",
                    StringSixFCDate = $"{ConstructionProgress.SixFCDate:dd/MM/yyyy}",
                    StringOtherFCDate = $"{ConstructionProgress.OtherFCDate:dd/MM/yyyy}",
                    ///////////

                    UnauthComId = UnauthPortion.UnauthComId,
                    TotalUnauthArea = UnauthPortion.TotalUnauthArea,
                    FineFreeArea = UnauthPortion.FineFreeArea,
                    WithFineUnauth = UnauthPortion.WithFineUnauth,
                    RemovedUnauthArea = UnauthPortion.RemovedUnauthArea,
                    NonRemovedUnauth = UnauthPortion.NonRemovedUnauth,
                    FineRate = UnauthPortion.FineRate,
                    FineAmount = UnauthPortion.FineAmount,
                    ////////////

                    DesignApproval = DesignApprovalVM,
                    OthetPlotOwner = OthetPlotOwner,

                    ////////////
                    CreatedByUserName = PlotOwnerdetails.CreatedByUserName,
                    TotalArea = PlotOwnerdetails.TotalArea,
                    UpdatedByUserName = PlotOwnerdetails.UpdatedByUserName,

                };


                
                return View(plotOwnerVM);
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // GET: PlotOwner/Create
        public ActionResult Create()
        {
            ViewBag.PlotId = new SelectList(_PlotManager.GetAllPlot(), "PlotId", "PlotIdNumber");
            ViewBag.LeaseQuotaId = new SelectList(_LeaseQuotaManager.GetAllLeaseQuota(), "LeaseQuotaId", "LeaseQuotaName");
            ViewBag.OfficialStatusId = new SelectList(_OfficialStatusManager.GetAllOfficialStatus(), "OfficialStatusId", "OffStatusName");
            //ViewBag.OwnershipSourceId = new SelectList(_OwnershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName");
            ViewBag.ConsStatusId = new SelectList(_ConstructionStatusManager.GetAllConstructionStatus(), "ConsStatusId", "ConsStatusName");
            return View();
        }

        // POST: PlotOwner/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: PlotOwner/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var PlotOwnerdetails = _PlotOwnerManager.GetPlotOwnerById(id);

                if (PlotOwnerdetails == null)
                    return HttpNotFound();
                int plotId = PlotOwnerdetails.PlotId;
                int ownerId = PlotOwnerdetails.PlotOwnerId;
                var ConstructionProgress = _PlotOwnerManager.GetConstructionProgressById(plotId);
                var UnauthPortion = _PlotOwnerManager.GetUnauthPortionById(plotId);
                var DesignApproval = _PlotOwnerManager.GetDesignApprovalById(plotId);
                var OthetPlotOwner = _PlotOwnerManager.GetOthetPlotOwnerById(ownerId);


                List<DesignApproval> DesignApprovalVM = new List<DesignApproval>();
                foreach (var item in DesignApproval)
                {
                    DesignApproval Approval = new DesignApproval()
                    {
                        StringApprovalDate = $"{item.ApprovalDate:dd/MM/yyyy}",
                        StringMEO_NCCDate = $"{item.MEO_NCCDate:dd/MM/yyyy}"
                    };
                    DesignApprovalVM.Add(Approval);
                }

                PlotOwnerCombineVM plotOwnerVM = new PlotOwnerCombineVM()
                {
                    PlotOwnerId = PlotOwnerdetails.PlotOwnerId,
                    PlotId = PlotOwnerdetails.PlotId,
                    PlotIdNumber = PlotOwnerdetails.PlotIdNumber,
                    PlotOwnerName = PlotOwnerdetails.PlotOwnerName,
                    IsAlive = PlotOwnerdetails.IsAlive,
                    OfficialStatusId = PlotOwnerdetails.OfficialStatusId,
                    OffStatusName = PlotOwnerdetails.OffStatusName,
                    PresentAdd = PlotOwnerdetails.PresentAdd,
                    PermanentAdd = PlotOwnerdetails.PermanentAdd,
                    PhoneNumber = PlotOwnerdetails.PhoneNumber,
                    Email = PlotOwnerdetails.Email,
                    LeaveDate = PlotOwnerdetails.LeaveDate,
                    StringLeaveDate = $"{PlotOwnerdetails.LeaveDate:dd/MM/yyyy}",
                    LeaseAuthority = PlotOwnerdetails.LeaseAuthority,
                    LeaseType = PlotOwnerdetails.LeaseType,
                    LeasePeriod = PlotOwnerdetails.LeasePeriod,
                    LeaseQuotaId = PlotOwnerdetails.LeaseQuotaId,
                    LeaseQuotaName = PlotOwnerdetails.LeaseQuotaName,
                    HandOverOffice = PlotOwnerdetails.HandOverOffice,
                    HandOverLetterNo = PlotOwnerdetails.HandOverLetterNo,
                    LandDevelopChange = PlotOwnerdetails.LandDevelopChange,
                    ConsStatusId = PlotOwnerdetails.ConsStatusId,
                    ConsStatusName = PlotOwnerdetails.ConsStatusName,
                    Doc1 = PlotOwnerdetails.Doc1,
                    Doc2 = PlotOwnerdetails.Doc2,
                    Doc3 = PlotOwnerdetails.Doc3,
                    Doc4 = PlotOwnerdetails.Doc4,
                    Doc5 = PlotOwnerdetails.Doc5,
                    Doc6 = PlotOwnerdetails.Doc6,
                    CreateDate = PlotOwnerdetails.CreateDate,
                    CreatedBy = PlotOwnerdetails.CreatedBy,
                    LastUpdated = PlotOwnerdetails.LastUpdated,
                    LastUpdatedBy = PlotOwnerdetails.LastUpdatedBy,
                    IsActive = PlotOwnerdetails.IsActive,
                    IsDeleted = PlotOwnerdetails.IsDeleted,
                    /////

                    ConsProgressId = ConstructionProgress.ConsProgressId,
                    OwnerDeclaration = ConstructionProgress.OwnerDeclaration,
                    RealBuilder = ConstructionProgress.RealBuilder,
                    DevelopDeposit = ConstructionProgress.DevelopDeposit,
                    FloorNumber = ConstructionProgress.FloorNumber,
                    CompletionDate = ConstructionProgress.CompletionDate,
                    GroundFCDate = ConstructionProgress.GroundFCDate,
                    FirstFCDate = ConstructionProgress.FirstFCDate,
                    SccFCDate = ConstructionProgress.SccFCDate,
                    ThirdFCDate = ConstructionProgress.ThirdFCDate,
                    ForthFCDate = ConstructionProgress.ForthFCDate,
                    FivthFCDate = ConstructionProgress.FivthFCDate,
                    SixFCDate = ConstructionProgress.SixFCDate,
                    OtherFCDate = ConstructionProgress.OtherFCDate,
                    OwnerPortion = ConstructionProgress.OwnerPortion,
                    DeveloperPortion = ConstructionProgress.DeveloperPortion,
                    BuyerPortion = ConstructionProgress.BuyerPortion,
                    SubmittedPortion = ConstructionProgress.SubmittedPortion,



                    StringCompletionDate = $"{ConstructionProgress.CompletionDate:dd/MM/yyyy}",
                    StringGroundFCDate = $"{ConstructionProgress.GroundFCDate:dd/MM/yyyy}",
                    StringFirstFCDate = $"{ConstructionProgress.FirstFCDate:dd/MM/yyyy}",
                    StringSccFCDate = $"{ConstructionProgress.SccFCDate:dd/MM/yyyy}",
                    StringThirdFCDate = $"{ConstructionProgress.ThirdFCDate:dd/MM/yyyy}",
                    StringForthFCDate = $"{ConstructionProgress.ForthFCDate:dd/MM/yyyy}",
                    StringFivthFCDate = $"{ConstructionProgress.FivthFCDate:dd/MM/yyyy}",
                    StringSixFCDate = $"{ConstructionProgress.SixFCDate:dd/MM/yyyy}",
                    StringOtherFCDate = $"{ConstructionProgress.OtherFCDate:dd/MM/yyyy}",
                    ///////////

                    UnauthComId = UnauthPortion.UnauthComId,
                    TotalUnauthArea = UnauthPortion.TotalUnauthArea,
                    FineFreeArea = UnauthPortion.FineFreeArea,
                    WithFineUnauth = UnauthPortion.WithFineUnauth,
                    RemovedUnauthArea = UnauthPortion.RemovedUnauthArea,
                    NonRemovedUnauth = UnauthPortion.NonRemovedUnauth,
                    FineRate = UnauthPortion.FineRate,
                    FineAmount = UnauthPortion.FineAmount,
                    ////////////

                    DesignApproval = DesignApprovalVM,
                    OthetPlotOwner = OthetPlotOwner,

                    ////////////
                    CreatedByUserName = PlotOwnerdetails.CreatedByUserName,
                    TotalArea = PlotOwnerdetails.TotalArea,
                    UpdatedByUserName = PlotOwnerdetails.UpdatedByUserName,

                };

                ViewBag.PlotId = new SelectList(_PlotManager.GetAllPlot(), "PlotId", "PlotIdNumber", plotOwnerVM.PlotId);
                ViewBag.LeaseQuotaId = new SelectList(_LeaseQuotaManager.GetAllLeaseQuota(), "LeaseQuotaId", "LeaseQuotaName", plotOwnerVM.LeaseQuotaId);
                ViewBag.OfficialStatusId = new SelectList(_OfficialStatusManager.GetAllOfficialStatus(), "OfficialStatusId", "OffStatusName", plotOwnerVM.OfficialStatusId);
                //ViewBag.OwnershipSourceId = new SelectList(_OwnershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName");
                ViewBag.ConsStatusId = new SelectList(_ConstructionStatusManager.GetAllConstructionStatus(), "ConsStatusId", "ConsStatusName", plotOwnerVM.ConsStatusId);

                return View(plotOwnerVM);
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // POST: PlotOwner/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: PlotOwner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlotOwner/Delete/5
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
