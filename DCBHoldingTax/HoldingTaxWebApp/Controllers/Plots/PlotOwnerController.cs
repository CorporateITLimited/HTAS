using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Manager.Plots;
using HoldingTaxWebApp.Models.Plots;
using HoldingTaxWebApp.ViewModels.Plots;
using System;
using System.Collections.Generic;
using System.IO;
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

        private readonly DOHSAreaManager _dOHSAreaManager;
        private readonly RankManager _RankManager;
        public PlotOwnerController()
        {
            _PlotOwnerManager = new PlotOwnerManager();
            _PlotManager = new PlotManager();
            //_OwnershipSourceManager = new OwnershipSourceManager();
            _LeaseQuotaManager = new LeaseQuotaManager();
            _OfficialStatusManager = new OfficialStatusManager();
            _ConstructionStatusManager = new ConstructionStatusManager();
            _dOHSAreaManager = new DOHSAreaManager();

            _RankManager = new RankManager();
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
                        UpdatedByUserName = item.UpdatedByUserName,
                        AreaName = item.AreaName,
                        RankId = item.RankId,
                        RankName = item.RankName,
                        PlotOwnerNamecon = item.PlotOwnerNamecon

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
                        StringMEO_NCCDate = $"{item.MEO_NCCDate:dd/MM/yyyy}",

                        ApprovalLetterNo = item.ApprovalLetterNo,
                        ApprovalDate = item.ApprovalDate,
                        ApprovalNo = item.ApprovalNo,
                        CreateDate = item.CreateDate,
                        CreatedBy = item.CreatedBy,
                        DesignAppId = item.DesignAppId,
                        FlorNumber = item.FlorNumber,
                        GroundFlorArea = item.GroundFlorArea,
                        MEO_NCCDate = item.MEO_NCCDate,
                        Reference = item.Reference,
                        OtherFlorArea = item.OtherFlorArea
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

                    RankId = PlotOwnerdetails.RankId,
                    RankName = PlotOwnerdetails.RankName,
                    PlotOwnerNamecon = PlotOwnerdetails.PlotOwnerNamecon,

                    //File1 = PlotOwnerdetails.Doc1.Substring(40),
                    //File2 = PlotOwnerdetails.Doc2.Substring(40),
                    //File3 = PlotOwnerdetails.Doc3.Substring(40),
                    //File4 = PlotOwnerdetails.Doc4.Substring(40),
                    //File5 = PlotOwnerdetails.Doc5.Substring(40),
                    //File6 = PlotOwnerdetails.Doc6.Substring(40),
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
                    AreaName = PlotOwnerdetails.AreaName,

                };

                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc1))
                {
                    plotOwnerVM.File1 = PlotOwnerdetails.Doc1.Substring(40);
                }
                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc2))
                {
                    plotOwnerVM.File2 = PlotOwnerdetails.Doc2.Substring(40);
                }
                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc3))
                {
                    plotOwnerVM.File3 = PlotOwnerdetails.Doc3.Substring(40);
                }
                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc4))
                {
                    plotOwnerVM.File4 = PlotOwnerdetails.Doc4.Substring(40);
                }
                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc5))
                {
                    plotOwnerVM.File5 = PlotOwnerdetails.Doc5.Substring(40);
                }
                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc6))
                {
                    plotOwnerVM.File6 = PlotOwnerdetails.Doc6.Substring(40);
                }

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
            ViewBag.PlotId = new SelectList(_PlotOwnerManager.GetPlot(), "PlotId", "PlotIdNumber");
            ViewBag.LeaseQuotaId = new SelectList(_LeaseQuotaManager.GetAllLeaseQuota(), "LeaseQuotaId", "LeaseQuotaName");
            ViewBag.OfficialStatusId = new SelectList(_OfficialStatusManager.GetAllOfficialStatus(), "OfficialStatusId", "OffStatusName");
            //ViewBag.OwnershipSourceId = new SelectList(_OwnershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName");
            ViewBag.ConsStatusId = new SelectList(_ConstructionStatusManager.GetAllConstructionStatus(), "ConsStatusId", "ConsStatusName");
            ViewBag.RankId = new SelectList(_RankManager.GetAllRank(), "RankId", "RankName",1);
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
                        StringMEO_NCCDate = $"{item.MEO_NCCDate:dd/MM/yyyy}",
                        ApprovalLetterNo = item.ApprovalLetterNo,
                        ApprovalDate = item.ApprovalDate,
                        ApprovalNo = item.ApprovalNo,
                        CreateDate = item.CreateDate,
                        CreatedBy = item.CreatedBy,
                        DesignAppId = item.DesignAppId,
                        FlorNumber = item.FlorNumber,
                        GroundFlorArea = item.GroundFlorArea,
                        MEO_NCCDate = item.MEO_NCCDate,
                        Reference = item.Reference,
                        OtherFlorArea = item.OtherFlorArea

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
                    RankId = PlotOwnerdetails.RankId,
                    RankName = PlotOwnerdetails.RankName,
                    //File1 = PlotOwnerdetails.Doc1.Substring(40),
                    //File2 = PlotOwnerdetails.Doc2.Substring(40),
                    //File3 = PlotOwnerdetails.Doc3.Substring(40),
                    //File4 = PlotOwnerdetails.Doc4.Substring(40),
                    //File5 = PlotOwnerdetails.Doc5.Substring(40),
                    //File6 = PlotOwnerdetails.Doc6.Substring(40),
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

                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc1))
                {
                    plotOwnerVM.File1 = PlotOwnerdetails.Doc1.Substring(40);
                }
                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc2))
                {
                    plotOwnerVM.File2 = PlotOwnerdetails.Doc2.Substring(40);
                }
                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc3))
                {
                    plotOwnerVM.File3 = PlotOwnerdetails.Doc3.Substring(40);
                }
                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc4))
                {
                    plotOwnerVM.File4 = PlotOwnerdetails.Doc4.Substring(40);
                }
                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc5))
                {
                    plotOwnerVM.File5 = PlotOwnerdetails.Doc5.Substring(40);
                }
                if (!String.IsNullOrEmpty(PlotOwnerdetails.Doc6))
                {
                    plotOwnerVM.File6 = PlotOwnerdetails.Doc6.Substring(40);
                }



                ViewBag.PlotId = new SelectList(_PlotManager.GetAllPlot(), "PlotId", "PlotIdNumber", plotOwnerVM.PlotId);
                ViewBag.LeaseQuotaId = new SelectList(_LeaseQuotaManager.GetAllLeaseQuota(), "LeaseQuotaId", "LeaseQuotaName", plotOwnerVM.LeaseQuotaId);
                ViewBag.OfficialStatusId = new SelectList(_OfficialStatusManager.GetAllOfficialStatus(), "OfficialStatusId", "OffStatusName", plotOwnerVM.OfficialStatusId);
                //ViewBag.OwnershipSourceId = new SelectList(_OwnershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName");
                ViewBag.ConsStatusId = new SelectList(_ConstructionStatusManager.GetAllConstructionStatus(), "ConsStatusId", "ConsStatusName", plotOwnerVM.ConsStatusId);
                ViewBag.RankId = new SelectList(_RankManager.GetAllRank(), "RankId", "RankName", plotOwnerVM.RankId);
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




        #region Save Data

        [HttpPost]
        public JsonResult AddOrUpdate(PlotOwnerCombineVM POVM)
        {
            try
            {
                var maxId = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string status = "success";

                if (POVM.PlotOwnerId == 0) //IsNullOrEmpty(POVM.NoaId.ToString())
                {
                    if (POVM.StringCompletionDate != null)
                    {
                        POVM.CompletionDate = DateTime.ParseExact(POVM.StringCompletionDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringFirstFCDate != null)
                    {
                        POVM.FirstFCDate = DateTime.ParseExact(POVM.StringFirstFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringFivthFCDate != null)
                    {
                        POVM.FivthFCDate = DateTime.ParseExact(POVM.StringFivthFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringForthFCDate != null)
                    {
                        POVM.ForthFCDate = DateTime.ParseExact(POVM.StringForthFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringGroundFCDate != null)
                    {
                        POVM.GroundFCDate = DateTime.ParseExact(POVM.StringGroundFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringGroundFCDate != null)
                    {
                        POVM.GroundFCDate = DateTime.ParseExact(POVM.StringGroundFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringLeaveDate != null)
                    {
                        POVM.LeaveDate = DateTime.ParseExact(POVM.StringLeaveDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringOtherFCDate != null)
                    {
                        POVM.OtherFCDate = DateTime.ParseExact(POVM.StringOtherFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringSccFCDate != null)
                    {
                        POVM.SccFCDate = DateTime.ParseExact(POVM.StringSccFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringSixFCDate != null)
                    {
                        POVM.SixFCDate = DateTime.ParseExact(POVM.StringSixFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringThirdFCDate != null)
                    {
                        POVM.ThirdFCDate = DateTime.ParseExact(POVM.StringThirdFCDate, "dd/MM/yyyy", null);
                    }




                    PlotOwnerCombineVM plotw = new PlotOwnerCombineVM()
                    {
                        ////PlotOwner Portion
                        PhoneNumber = POVM.PhoneNumber,
                        ConsStatusId = POVM.ConsStatusId,
                        Email = POVM.Email,
                        HandOverLetterNo = POVM.HandOverLetterNo,
                        HandOverOffice = POVM.HandOverOffice,
                        IsAlive = POVM.IsAlive,
                        LandDevelopChange = POVM.LandDevelopChange,
                        LeaseAuthority = POVM.LeaseAuthority,
                        LeasePeriod = POVM.LeasePeriod,
                        LeaseQuotaId = POVM.LeaseQuotaId,
                        LeaseType = POVM.LeaseType,
                        LeaveDate = POVM.LeaveDate,
                        OfficialStatusId = POVM.OfficialStatusId,
                        PermanentAdd = POVM.PermanentAdd,
                        PlotId = POVM.PlotId,
                        PlotOwnerName = POVM.PlotOwnerName,
                        PresentAdd = POVM.PresentAdd,
                        RankId = POVM.RankId,

                        /////Construction Progress

                        OwnerDeclaration = POVM.OwnerDeclaration,
                        RealBuilder = POVM.RealBuilder,
                        DevelopDeposit = POVM.DevelopDeposit,
                        FloorNumber = POVM.FloorNumber,
                        CompletionDate = POVM.CompletionDate,
                        GroundFCDate = POVM.GroundFCDate,
                        FirstFCDate = POVM.FirstFCDate,
                        SccFCDate = POVM.SccFCDate,
                        ThirdFCDate = POVM.ThirdFCDate,
                        ForthFCDate = POVM.ForthFCDate,
                        FivthFCDate = POVM.FivthFCDate,
                        SixFCDate = POVM.SixFCDate,
                        OtherFCDate = POVM.OtherFCDate,
                        OwnerPortion = POVM.OwnerPortion,
                        DeveloperPortion = POVM.DeveloperPortion,
                        BuyerPortion = POVM.BuyerPortion,
                        SubmittedPortion = POVM.SubmittedPortion,

                        ////UnauthPortion
                        TotalUnauthArea = POVM.TotalUnauthArea,
                        FineFreeArea = POVM.FineFreeArea,
                        WithFineUnauth = POVM.WithFineUnauth,
                        RemovedUnauthArea = POVM.RemovedUnauthArea,
                        NonRemovedUnauth = POVM.NonRemovedUnauth,
                        FineRate = POVM.FineRate,
                        FineAmount = POVM.FineAmount,


                        ////File Name Portion 

                        File1 = POVM.File1,
                        File2 = POVM.File2,
                        File3 = POVM.File3,
                        File4 = POVM.File4,
                        File5 = POVM.File5,
                        File6 = POVM.File6,


                        ////Common portion

                        IsActive = true,
                        IsDeleted = false,
                        CreateDate = DateTime.Now,
                        CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                        LastUpdated = DateTime.Now,
                        LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),


                    };



                    if (Session["DocFile1"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile1"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc1_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File1))
                            {
                                newFilename = maxId + "_doc1_" + POVM.File1 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc1_" + fileOldName + extension;
                            }

                            newFilename = "/Documents/Plots/" + newFilename;

                            if (System.IO.File.Exists(newFilename))
                                System.IO.File.Delete(newFilename);
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc1 = newFilename;
                        }
                        else
                        {
                            plotw.Doc1 = null;
                        }
                    }

                    if (Session["DocFile2"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile2"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc2_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File2))
                            {
                                newFilename = maxId + "_doc2_" + POVM.File2 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc2_" + fileOldName + extension;
                            }

                            newFilename = "/Documents/Plots/" + newFilename;

                            if (System.IO.File.Exists(newFilename))
                                System.IO.File.Delete(newFilename);
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc2 = newFilename;
                        }
                        else
                        {
                            plotw.Doc2 = null;
                        }
                    }

                    if (Session["DocFile3"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile3"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc3_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File3))
                            {
                                newFilename = maxId + "_doc3_" + POVM.File3 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc3_" + fileOldName + extension;
                            }
                            newFilename = "/Documents/Plots/" + newFilename;

                            if (System.IO.File.Exists(newFilename))
                                System.IO.File.Delete(newFilename);
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc3 = newFilename;
                        }
                        else
                        {
                            plotw.Doc3 = null;
                        }
                    }

                    if (Session["DocFile4"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile4"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc4_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File4))
                            {
                                newFilename = maxId + "_doc4_" + POVM.File4 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc4_" + fileOldName + extension;
                            }
                            newFilename = "/Documents/Plots/" + newFilename;

                            if (System.IO.File.Exists(newFilename))
                                System.IO.File.Delete(newFilename);
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc4 = newFilename;
                        }
                        else
                        {
                            plotw.Doc4 = null;
                        }
                    }

                    if (Session["DocFile5"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile5"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc5_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File5))
                            {
                                newFilename = maxId + "_doc5_" + POVM.File5 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc5_" + fileOldName + extension;
                            }
                            newFilename = "/Documents/Plots/" + newFilename;

                            if (System.IO.File.Exists(newFilename))
                                System.IO.File.Delete(newFilename);
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc5 = newFilename;
                        }
                        else
                        {
                            plotw.Doc5 = null;
                        }
                    }

                    if (Session["DocFile6"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile6"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc6_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File6))
                            {
                                newFilename = maxId + "_doc6_" + POVM.File6 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc6_" + fileOldName + extension;
                            }
                            newFilename = "/Documents/Plots/" + newFilename;

                            if (System.IO.File.Exists(newFilename))
                                System.IO.File.Delete(newFilename);
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc6 = newFilename;
                        }
                        else
                        {
                            plotw.Doc6 = null;
                        }
                    }



                    int ownerId = _PlotOwnerManager.PlotOwnerInsert(plotw);

                    if (ownerId > 0)
                    {

                        foreach (OthetPlotOwner item2 in POVM.OthetPlotOwner)
                        {
                            OthetPlotOwner Details2 = new OthetPlotOwner()
                            {
                                PlotOwnerId = ownerId,
                                OthetOwneeName = item2.OthetOwneeName,
                                Address = item2.Address,
                                Remarks = item2.Remarks,
                                IsActive = true,
                                IsDeleted = false,
                                CreateDate = DateTime.Now,
                                CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                LastUpdated = DateTime.Now,
                                LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),


                            };
                            string returnString = CommonConstantHelper.Success;
                            if (Details2.OthetOwneeName.Length != 0)
                            {
                                returnString = _PlotOwnerManager.OthetPlotOwnerInsert(Details2);
                            }
                            if (returnString != CommonConstantHelper.Success)

                            {
                                status = "error_details";
                                break;
                            }
                            else
                            {
                                status = "success";
                            }
                        }

                        foreach (DesignApproval item in POVM.DesignApproval)
                        {
                            DesignApproval Details = new DesignApproval()
                            {
                                PlotId = POVM.PlotId,
                                ApprovalDate = item.ApprovalDate,
                                ApprovalLetterNo = item.ApprovalLetterNo,
                                ApprovalNo = item.ApprovalNo,
                                FlorNumber = item.FlorNumber,
                                Reference = item.Reference,
                                GroundFlorArea = item.GroundFlorArea,
                                MEO_NCCDate = item.MEO_NCCDate,
                                OtherFlorArea = item.OtherFlorArea,
                                IsActive = true,
                                IsDeleted = false,
                                CreateDate = DateTime.Now,
                                CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                LastUpdated = DateTime.Now,
                                LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),

                            };
                            if (item.StringApprovalDate != null)
                            {
                                Details.ApprovalDate = DateTime.ParseExact(item.StringApprovalDate, "dd/MM/yyyy", null);
                            }
                            if (item.StringMEO_NCCDate != null)
                            {
                                Details.MEO_NCCDate = DateTime.ParseExact(item.StringMEO_NCCDate, "dd/MM/yyyy", null);
                            }

                            string returnString = CommonConstantHelper.Success;
                            if (Details.ApprovalNo > 0)
                            {
                                returnString = _PlotOwnerManager.DesignApprovalInsert(Details);
                            }
                            if (returnString != CommonConstantHelper.Success)

                            {
                                status = "error_details";
                                break;
                            }
                            else
                            {
                                status = "success";
                            }
                        }

                    }





                    return new JsonResult { Data = new { status } };
                }
                else
                {
                    if (POVM.StringCompletionDate != null)
                    {
                        POVM.CompletionDate = DateTime.ParseExact(POVM.StringCompletionDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringFirstFCDate != null)
                    {
                        POVM.FirstFCDate = DateTime.ParseExact(POVM.StringFirstFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringFivthFCDate != null)
                    {
                        POVM.FivthFCDate = DateTime.ParseExact(POVM.StringFivthFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringForthFCDate != null)
                    {
                        POVM.ForthFCDate = DateTime.ParseExact(POVM.StringForthFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringGroundFCDate != null)
                    {
                        POVM.GroundFCDate = DateTime.ParseExact(POVM.StringGroundFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringGroundFCDate != null)
                    {
                        POVM.GroundFCDate = DateTime.ParseExact(POVM.StringGroundFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringLeaveDate != null)
                    {
                        POVM.LeaveDate = DateTime.ParseExact(POVM.StringLeaveDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringOtherFCDate != null)
                    {
                        POVM.OtherFCDate = DateTime.ParseExact(POVM.StringOtherFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringSccFCDate != null)
                    {
                        POVM.SccFCDate = DateTime.ParseExact(POVM.StringSccFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringSixFCDate != null)
                    {
                        POVM.SixFCDate = DateTime.ParseExact(POVM.StringSixFCDate, "dd/MM/yyyy", null);
                    }
                    if (POVM.StringThirdFCDate != null)
                    {
                        POVM.ThirdFCDate = DateTime.ParseExact(POVM.StringThirdFCDate, "dd/MM/yyyy", null);
                    }


                    PlotOwnerCombineVM plotw = new PlotOwnerCombineVM()
                    {
                        ////PlotOwner Portion
                        PlotOwnerId = POVM.PlotOwnerId,
                        PhoneNumber = POVM.PhoneNumber,
                        ConsStatusId = POVM.ConsStatusId,
                        Email = POVM.Email,
                        HandOverLetterNo = POVM.HandOverLetterNo,
                        HandOverOffice = POVM.HandOverOffice,
                        IsAlive = POVM.IsAlive,
                        LandDevelopChange = POVM.LandDevelopChange,
                        LeaseAuthority = POVM.LeaseAuthority,
                        LeasePeriod = POVM.LeasePeriod,
                        LeaseQuotaId = POVM.LeaseQuotaId,
                        LeaseType = POVM.LeaseType,
                        LeaveDate = POVM.LeaveDate,
                        OfficialStatusId = POVM.OfficialStatusId,
                        PermanentAdd = POVM.PermanentAdd,
                        PlotId = POVM.PlotId,
                        PlotOwnerName = POVM.PlotOwnerName,
                        PresentAdd = POVM.PresentAdd,
                        Doc1 = POVM.Doc1,
                        Doc2 = POVM.Doc2,
                        Doc3 = POVM.Doc3,
                        Doc4 = POVM.Doc4,
                        Doc5 = POVM.Doc5,
                        Doc6 = POVM.Doc6,
                        RankId = POVM.RankId,
                        /////Construction Progress
                        ConsProgressId = POVM.ConsProgressId,
                        OwnerDeclaration = POVM.OwnerDeclaration,
                        RealBuilder = POVM.RealBuilder,
                        DevelopDeposit = POVM.DevelopDeposit,
                        FloorNumber = POVM.FloorNumber,
                        CompletionDate = POVM.CompletionDate,
                        GroundFCDate = POVM.GroundFCDate,
                        FirstFCDate = POVM.FirstFCDate,
                        SccFCDate = POVM.SccFCDate,
                        ThirdFCDate = POVM.ThirdFCDate,
                        ForthFCDate = POVM.ForthFCDate,
                        FivthFCDate = POVM.FivthFCDate,
                        SixFCDate = POVM.SixFCDate,
                        OtherFCDate = POVM.OtherFCDate,
                        OwnerPortion = POVM.OwnerPortion,
                        DeveloperPortion = POVM.DeveloperPortion,
                        BuyerPortion = POVM.BuyerPortion,
                        SubmittedPortion = POVM.SubmittedPortion,

                        ////UnauthPortion
                        UnauthComId = POVM.UnauthComId,
                        TotalUnauthArea = POVM.TotalUnauthArea,
                        FineFreeArea = POVM.FineFreeArea,
                        WithFineUnauth = POVM.WithFineUnauth,
                        RemovedUnauthArea = POVM.RemovedUnauthArea,
                        NonRemovedUnauth = POVM.NonRemovedUnauth,
                        FineRate = POVM.FineRate,
                        FineAmount = POVM.FineAmount,

                        ////File Name Portion 

                        File1 = POVM.File1,
                        File2 = POVM.File2,
                        File3 = POVM.File3,
                        File4 = POVM.File4,
                        File5 = POVM.File5,
                        File6 = POVM.File6,


                        ////Common portion

                        IsActive = true,
                        IsDeleted = false,
                        CreateDate = DateTime.Now,
                        CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                        LastUpdated = DateTime.Now,
                        LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),


                    };


                    if (Session["DocFile1"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile1"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc1_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File1))
                            {
                                newFilename = maxId + "_doc1_" + POVM.File1 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc1_" + fileOldName + extension;
                            }
                            newFilename = "/Documents/Plots/" + newFilename;

                            //if (System.IO.File.Exists(newFilename))
                            //    System.IO.File.Delete(newFilename);

                            if (!string.IsNullOrWhiteSpace(plotw.Doc1))
                            {
                                var deletePathFile = Path.Combine(Server.MapPath(plotw.Doc1));
                                if (System.IO.File.Exists(deletePathFile))
                                    System.IO.File.Delete(deletePathFile);
                            }



                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc1 = newFilename;
                        }
                        else
                        {
                            plotw.Doc1 = null;
                        }
                    }

                    if (Session["DocFile2"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile2"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc2_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File2))
                            {
                                newFilename = maxId + "_doc2_" + POVM.File2 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc2_" + fileOldName + extension;
                            }
                            newFilename = "/Documents/Plots/" + newFilename;

                            //if (System.IO.File.Exists(newFilename))
                            //    System.IO.File.Delete(newFilename);
                            if (!string.IsNullOrWhiteSpace(plotw.Doc2))
                            {
                                var deletePathFile = Path.Combine(Server.MapPath(plotw.Doc2));
                                if (System.IO.File.Exists(deletePathFile))
                                    System.IO.File.Delete(deletePathFile);
                            }

                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc2 = newFilename;
                        }
                        else
                        {
                            plotw.Doc2 = null;
                        }
                    }

                    if (Session["DocFile3"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile3"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc3_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File3))
                            {
                                newFilename = maxId + "_doc3_" + POVM.File3 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc3_" + fileOldName + extension;
                            }
                            newFilename = "/Documents/Plots/" + newFilename;

                            //if (System.IO.File.Exists(newFilename))
                            //    System.IO.File.Delete(newFilename);
                            if (!string.IsNullOrWhiteSpace(plotw.Doc3))
                            {
                                var deletePathFile = Path.Combine(Server.MapPath(plotw.Doc3));
                                if (System.IO.File.Exists(deletePathFile))
                                    System.IO.File.Delete(deletePathFile);
                            }
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc3 = newFilename;
                        }
                        else
                        {
                            plotw.Doc3 = null;
                        }
                    }

                    if (Session["DocFile4"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile4"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc4_" + fileOldName + extension;

                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File4))
                            {
                                newFilename = maxId + "_doc4_" + POVM.File4 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc4_" + fileOldName + extension;
                            }

                            newFilename = "/Documents/Plots/" + newFilename;

                            //if (System.IO.File.Exists(newFilename))
                            //    System.IO.File.Delete(newFilename);
                            if (!string.IsNullOrWhiteSpace(plotw.Doc4))
                            {
                                var deletePathFile = Path.Combine(Server.MapPath(plotw.Doc4));
                                if (System.IO.File.Exists(deletePathFile))
                                    System.IO.File.Delete(deletePathFile);
                            }
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc4 = newFilename;
                        }
                        else
                        {
                            plotw.Doc4 = null;
                        }
                    }

                    if (Session["DocFile5"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile5"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc5_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File5))
                            {
                                newFilename = maxId + "_doc5_" + POVM.File5 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc5_" + fileOldName + extension;
                            }
                            newFilename = "/Documents/Plots/" + newFilename;

                            //if (System.IO.File.Exists(newFilename))
                            //    System.IO.File.Delete(newFilename);
                            if (!string.IsNullOrWhiteSpace(plotw.Doc5))
                            {
                                var deletePathFile = Path.Combine(Server.MapPath(plotw.Doc5));
                                if (System.IO.File.Exists(deletePathFile))
                                    System.IO.File.Delete(deletePathFile);
                            }
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc5 = newFilename;
                        }
                        else
                        {
                            plotw.Doc5 = null;
                        }
                    }

                    if (Session["DocFile6"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile6"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            //var newFilename = maxId + "_doc6_" + fileOldName + extension;
                            var newFilename = "";
                            if (!String.IsNullOrEmpty(POVM.File6))
                            {
                                newFilename = maxId + "_doc6_" + POVM.File6 + extension;
                            }
                            else
                            {
                                newFilename = maxId + "_doc6_" + fileOldName + extension;
                            }
                            newFilename = "/Documents/Plots/" + newFilename;

                            //if (System.IO.File.Exists(newFilename))
                            //    System.IO.File.Delete(newFilename);
                            if (!string.IsNullOrWhiteSpace(plotw.Doc6))
                            {
                                var deletePathFile = Path.Combine(Server.MapPath(plotw.Doc6));
                                if (System.IO.File.Exists(deletePathFile))
                                    System.IO.File.Delete(deletePathFile);
                            }
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            plotw.Doc6 = newFilename;
                        }
                        else
                        {
                            plotw.Doc6 = null;
                        }
                    }








                    int ownerId = _PlotOwnerManager.PlotOwnerUpdate(plotw);
                    if (ownerId > 0)
                    {
                        _PlotOwnerManager.OthetPlotOwnerDelete(ownerId);

                        foreach (OthetPlotOwner item2 in POVM.OthetPlotOwner)
                        {
                            OthetPlotOwner Details2 = new OthetPlotOwner()
                            {
                                OthetPlotOwnerId = item2.OthetPlotOwnerId,
                                PlotOwnerId = POVM.PlotOwnerId,
                                OthetOwneeName = item2.OthetOwneeName,
                                Address = item2.Address,
                                Remarks = item2.Remarks,
                                IsActive = true,
                                IsDeleted = false,
                                CreateDate = DateTime.Now,
                                CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                LastUpdated = DateTime.Now,
                                LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),


                            };
                            string returnString = CommonConstantHelper.Success;
                            if (Details2.OthetOwneeName.Length != 0)
                            {
                                returnString = _PlotOwnerManager.OthetPlotOwnerInsert(Details2);
                            }
                            if (returnString != CommonConstantHelper.Success)

                            {
                                status = "error_details";
                                break;
                            }
                            else
                            {
                                status = "success";
                            }
                        }

                        _PlotOwnerManager.DesignApprovalDelete(POVM.PlotId);

                        foreach (DesignApproval item in POVM.DesignApproval)
                        {
                            DesignApproval Details = new DesignApproval()
                            {
                                DesignAppId = item.DesignAppId,
                                PlotId = POVM.PlotId,
                                ApprovalDate = DateTime.ParseExact(item.StringApprovalDate, "dd/MM/yyyy", null),
                                ApprovalLetterNo = item.ApprovalLetterNo,
                                ApprovalNo = item.ApprovalNo,
                                FlorNumber = item.FlorNumber,
                                Reference = item.Reference,
                                GroundFlorArea = item.GroundFlorArea,
                                MEO_NCCDate = DateTime.ParseExact(item.StringMEO_NCCDate, "dd/MM/yyyy", null),
                                OtherFlorArea = item.OtherFlorArea,
                                IsActive = true,
                                IsDeleted = false,
                                CreateDate = DateTime.Now,
                                CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                LastUpdated = DateTime.Now,
                                LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),


                            };
                            if (item.StringApprovalDate != null)
                            {
                                Details.ApprovalDate = DateTime.ParseExact(item.StringApprovalDate, "dd/MM/yyyy", null);
                            }
                            if (item.StringMEO_NCCDate != null)
                            {
                                Details.MEO_NCCDate = DateTime.ParseExact(item.StringMEO_NCCDate, "dd/MM/yyyy", null);
                            }
                            string returnString = CommonConstantHelper.Success;
                            if (Details.ApprovalNo > 0)
                            {
                                returnString = _PlotOwnerManager.DesignApprovalInsert(Details);
                            }
                            if (returnString != CommonConstantHelper.Success)

                            {
                                status = "error_details";
                                break;
                            }
                            else
                            {
                                status = "success";
                            }
                        }

                    }





                    return new JsonResult { Data = new { status } };
                }


            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return new JsonResult { Data = "error" };

            }

        }

        #endregion


        #region Document Upload

        public JsonResult GetDocFile1(HttpPostedFileBase fileBase)
        {
            if (fileBase != null)
            {
                Session["DocFile1"] = null;
                Session["DocFile1"] = fileBase;
                return new JsonResult { Data = "done" };
            }
            else
            {
                Session["DocFile1"] = null;
                return new JsonResult { Data = "not_done" };
            }
        }

        public JsonResult GetDocFile2(HttpPostedFileBase fileBase)
        {
            if (fileBase != null)
            {
                Session["DocFile2"] = null;
                Session["DocFile2"] = fileBase;
                return new JsonResult { Data = "done" };
            }
            else
            {
                Session["DocFile2"] = null;
                return new JsonResult { Data = "not_done" };
            }
        }

        public JsonResult GetDocFile3(HttpPostedFileBase fileBase)
        {
            if (fileBase != null)
            {
                Session["DocFile3"] = null;
                Session["DocFile3"] = fileBase;
                return new JsonResult { Data = "done" };
            }
            else
            {
                Session["DocFile3"] = null;
                return new JsonResult { Data = "not_done" };
            }
        }

        public JsonResult GetDocFile4(HttpPostedFileBase fileBase)
        {
            if (fileBase != null)
            {
                Session["DocFile4"] = null;
                Session["DocFile4"] = fileBase;
                return new JsonResult { Data = "done" };
            }
            else
            {
                Session["DocFile4"] = null;
                return new JsonResult { Data = "not_done" };
            }
        }

        public JsonResult GetDocFile5(HttpPostedFileBase fileBase)
        {
            if (fileBase != null)
            {
                Session["DocFile5"] = null;
                Session["DocFile5"] = fileBase;
                return new JsonResult { Data = "done" };
            }
            else
            {
                Session["DocFile5"] = null;
                return new JsonResult { Data = "not_done" };
            }
        }

        public JsonResult GetDocFile6(HttpPostedFileBase fileBase)
        {
            if (fileBase != null)
            {
                Session["DocFile6"] = null;
                Session["DocFile6"] = fileBase;
                return new JsonResult { Data = "done" };
            }
            else
            {
                Session["DocFile6"] = null;
                return new JsonResult { Data = "not_done" };
            }
        }
        #endregion



        #region Plot Owner list Report
        public ActionResult PlotOwnerReport()
        {
            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
            return View();
        }



        public ActionResult rptPlotOwnerList(int? id)
        {
            Session["AreaId_"] = id;
            if (id == 0)
            {
                Session["AreaId_"] = null;
            }
            return View();
        }

        #endregion


        #region Plot Owner Details Report
        public ActionResult PlotOwnerDetailsReport()
        {
            ViewBag.PlotId = new SelectList(_PlotManager.GetAllPlot(), "PlotId", "PlotNo");
            return View();
        }

        public ActionResult OldPlotOwnerList()
        {
            ViewBag.PlotId = new SelectList(_PlotManager.GetAllPlot(), "PlotId", "PlotNo");
            return View();
        }


        public ActionResult rptPlotOwnerDetails(int id)
        {
            Session["PlotId_"] = id;
            if (id == 0)
            {
                Session["PlotId_"] = null;
            }
            return View();
        }

        public ActionResult rptOldPlotOwnerList(int id)
        {
            Session["PlotId_"] = id;
            if (id == 0)
            {
                Session["PlotId_"] = null;
            }
            return View();
        }

        #endregion

        #region Get Present Address By Plot Id Change Event
        public JsonResult GetPresentAddress(int PlotId)
        {
            var data = _PlotOwnerManager.GetPresentAddress(PlotId);
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

    }
}
