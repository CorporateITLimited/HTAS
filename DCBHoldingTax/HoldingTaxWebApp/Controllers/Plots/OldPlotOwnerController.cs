using HoldingTaxWebApp.Helpers;
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
    public class OldPlotOwnerController : Controller
    {
        private readonly OldPlotOwnerManager _OldPlotOwnerManager;
        private readonly PlotOwnerManager _PlotOwnerManager;
        private readonly PlotManager _PlotManager;
        private readonly OfficialStatusManager _OfficialStatusManager;

        public OldPlotOwnerController()
        {
            _OldPlotOwnerManager = new OldPlotOwnerManager();
            _PlotOwnerManager = new PlotOwnerManager();
            _PlotManager = new PlotManager();
            _OfficialStatusManager = new OfficialStatusManager();
        }


        // GET: OldPlotOwner
        public ActionResult Index()
        {
            try
            {
                var OldPlotOwnerList = new List<OldPlotOwner>();
                OldPlotOwnerList = _OldPlotOwnerManager.GetAllOldPlotOwner();

                List<OldPlotOwner> OldPlotOwnerListVM = new List<OldPlotOwner>();
                foreach (var item in OldPlotOwnerList)
                {
                    OldPlotOwner OldPlotOwnerVM = new OldPlotOwner()
                    {
                        OldPlotOwnerId = item.OldPlotOwnerId,
                        IsActive = item.IsActive,
                        IsDeleted = item.IsDeleted,

                        PlotOwnerId = item.PlotOwnerId,
                        OldPlotOwnerName = item.OldPlotOwnerName,

                        CreateDate = item.CreateDate,
                        CreatedBy = item.CreatedBy,
                        CreatedByUserName = item.CreatedByUserName,
                       
                        Email = item.Email,
                      
                        IsAlive = item.IsAlive,
                       
                        LastUpdated = item.LastUpdated,
                        LastUpdatedBy = item.LastUpdatedBy,
                        
                        OfficialStatusId = item.OfficialStatusId,
                        OffStatusName = item.OffStatusName,
                        PermanentAdd = item.PermanentAdd,
                        PhoneNumber = item.PhoneNumber,
                       
                        //PlotIdNumber = item.PlotIdNumber,
                        PresentAdd = item.PresentAdd,
                        
                        //TotalArea = item.TotalArea,
                        UpdatedByUserName = item.UpdatedByUserName

                    };
                    OldPlotOwnerListVM.Add(OldPlotOwnerVM);
                }
                return View(OldPlotOwnerListVM.ToList());
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // GET: OldPlotOwner/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var OldPlotOwnerdetails = _OldPlotOwnerManager.GetOldPlotOwnerById(id);

                if (OldPlotOwnerdetails == null)
                    return HttpNotFound();

                //int plotId = PlotOwnerdetails.PlotId;
                //int ownerId = PlotOwnerdetails.PlotOwnerId;
                //var ConstructionProgress = _PlotOwnerManager.GetConstructionProgressById(plotId);
                //var UnauthPortion = _PlotOwnerManager.GetUnauthPortionById(plotId);
                //var DesignApproval = _PlotOwnerManager.GetDesignApprovalById(plotId);
                //var OthetPlotOwner = _PlotOwnerManager.GetOthetPlotOwnerById(ownerId);


                List<OldOthetPlotOwner> OldOthetPlotOwnerVM = new List<OldOthetPlotOwner>();
                OldOthetPlotOwnerVM = _OldPlotOwnerManager.GetOldOthetPlotOwnerById(id);

                OldPlotOwnerCombineVM OldPlotOwnerVM = new OldPlotOwnerCombineVM()
                {
                    IsActive = OldPlotOwnerdetails.IsActive,
                    IsDeleted = OldPlotOwnerdetails.IsDeleted,

                    PlotOwnerId = OldPlotOwnerdetails.PlotOwnerId,
                    OldPlotOwnerName = OldPlotOwnerdetails.OldPlotOwnerName,

                    CreateDate = OldPlotOwnerdetails.CreateDate,
                    CreatedBy = OldPlotOwnerdetails.CreatedBy,
                    CreatedByUserName = OldPlotOwnerdetails.CreatedByUserName,

                    Email = OldPlotOwnerdetails.Email,

                    IsAlive = OldPlotOwnerdetails.IsAlive,

                    LastUpdated = OldPlotOwnerdetails.LastUpdated,
                    LastUpdatedBy = OldPlotOwnerdetails.LastUpdatedBy,

                    OfficialStatusId = OldPlotOwnerdetails.OfficialStatusId,
                    OffStatusName = OldPlotOwnerdetails.OffStatusName,
                    PermanentAdd = OldPlotOwnerdetails.PermanentAdd,
                    PhoneNumber = OldPlotOwnerdetails.PhoneNumber,

                    //PlotIdNumber = item.PlotIdNumber,
                    PresentAdd = OldPlotOwnerdetails.PresentAdd,

                    //TotalArea = item.TotalArea,
                    UpdatedByUserName = OldPlotOwnerdetails.UpdatedByUserName,
                    OldPlotOwnerId = OldPlotOwnerdetails.OldPlotOwnerId,
                    OldOthetPlotOwner = OldOthetPlotOwnerVM

                };



                return View(OldPlotOwnerVM);
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // GET: OldPlotOwner/Create
        public ActionResult Create()
        {
            ViewBag.PlotId = new SelectList(_OldPlotOwnerManager.GetPlot(), "PlotId", "PlotIdNumber");
            ViewBag.OfficialStatusId = new SelectList(_OfficialStatusManager.GetAllOfficialStatus(), "OfficialStatusId", "OffStatusName");
            return View();
        }

        // POST: OldPlotOwner/Create
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

        // GET: OldPlotOwner/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OldPlotOwner/Edit/5
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

        // GET: OldPlotOwner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OldPlotOwner/Delete/5
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
        public JsonResult AddOrUpdate(OldPlotOwnerCombineVM POVM)
        {
            try
            {
                //var maxId = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string status = "error";

               // if (POVM.OldPlotOwnerId == 0) //IsNullOrEmpty(POVM.NoaId.ToString())
               // {


                    OldPlotOwner plotw = new OldPlotOwner()
                    {
                        ////PlotOwner Portion
                        PhoneNumber = POVM.PhoneNumber,
                        Email = POVM.Email,
                        IsAlive = POVM.IsAlive,
                        OfficialStatusId = POVM.OfficialStatusId,
                        PermanentAdd = POVM.PermanentAdd,
                        //PlotId = POVM.PlotId,
                        OldPlotOwnerName = POVM.OldPlotOwnerName,
                        PresentAdd = POVM.PresentAdd,
                        PlotOwnerId = POVM.PlotOwnerId,
                        IsActive = true,
                        IsDeleted = false,
                        CreateDate = DateTime.Now,
                        CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                        LastUpdated = DateTime.Now,
                        LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]), 
                    };


                int OldownerId =  _OldPlotOwnerManager.OldPlotOwnerInsert(plotw);

                    if (OldownerId > 0)
                    {

                        string returnString = _OldPlotOwnerManager.OldOthetPlotOwnerInsert(POVM.PlotOwnerId, OldownerId);

                    if (returnString == CommonConstantHelper.Success)

                        {
                            foreach (var item in POVM.OldOthetPlotOwner)
                            {
                                OthetPlotOwner Details = new OthetPlotOwner()
                                {
                                    PlotOwnerId = POVM.PlotOwnerId,
                                    OthetOwneeName = item.OldOthetOwneeName,
                                    Address = item.Address,
                                    Remarks = item.Remarks,
                                    IsActive = true,
                                    IsDeleted = false,
                                    CreateDate = DateTime.Now,
                                    CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                    LastUpdated = DateTime.Now,
                                    LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                };
                                //string returnString = CommonConstantHelper.Success;
                                if (Details.OthetOwneeName.Length != 0)
                                {
                                    returnString = _PlotOwnerManager.OthetPlotOwnerInsert(Details);
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



                    }


                    return new JsonResult { Data = new { status } };
                //}
               


            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return new JsonResult { Data = "error" };

            }

        }

        #endregion


        public JsonResult GetData(int PlotId)
        {
            var data = _OldPlotOwnerManager.GetPlotOwnerData(PlotId);
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }




    }
}
