using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Models.Holding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Holding
{
    public class HolderFlatHistoryController : Controller
    {

        private readonly HolderFlatHistoryManager _HolderFlatHistoryManager;
        private readonly HoldingManager _HoldingManager;


        public HolderFlatHistoryController()
        {
            _HolderFlatHistoryManager = new HolderFlatHistoryManager();
            _HoldingManager = new HoldingManager();
        }

        // GET: HolderFlatHistory
        public ActionResult Index()
        {
            try
            {
                var HolderFlatHistoryList = new List<HolderFlatHistory>();
                HolderFlatHistoryList = _HolderFlatHistoryManager.GetAllHolderFlatHistory();

                List<HolderFlatHistory> HolderFlatHistoryListVM = new List<HolderFlatHistory>();
                foreach (var item in HolderFlatHistoryList)
                {
                    HolderFlatHistory HolderFlatHistoryVM = new HolderFlatHistory()
                    {
                        AreaId = item.AreaId,
                        AreaName = item.AreaName,
                        FlatHistoryId = item.FlatHistoryId,
                        NewFlatNo = item.NewFlatNo,
                        NewHolderFlatId = item.NewHolderFlatId,
                        NewHolderId = item.NewHolderId,
                        NewHolderName = item.NewHolderName,
                        OldFlatNo = item.OldFlatNo,
                        OldHolderFlatId = item.OldHolderFlatId,
                        OldHolderId = item.OldHolderId,
                        OldHolderName = item.OldHolderName,
                        PlotId = item.PlotId,
                        PlotNo = item.PlotNo,
                        ReferenceDate = item.ReferenceDate,
                        StringReferenceDate = $"{item.ReferenceDate:dd/MM/yyyy}",
                        StringTransactionDate = $"{item.TransactionDate:dd/MM/yyyy}",
                        ReferenceNo = item.ReferenceNo,
                        TransactionBy = item.TransactionBy,
                        TransactionByUserName = item.TransactionByUserName,
                        TransactionDate = item.TransactionDate

                    };
                    HolderFlatHistoryListVM.Add(HolderFlatHistoryVM);
                }
                return View(HolderFlatHistoryListVM.ToList());
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // GET: HolderFlatHistory/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var item = _HolderFlatHistoryManager.GetHolderFlatHistoryById(id);

                if (item == null)
                    return HttpNotFound();
             


               

                HolderFlatHistory HolderFlatHistoryVM = new HolderFlatHistory()
                {

                    AreaId = item.AreaId,
                    AreaName = item.AreaName,
                    FlatHistoryId = item.FlatHistoryId,
                    NewFlatNo = item.NewFlatNo,
                    NewHolderFlatId = item.NewHolderFlatId,
                    NewHolderId = item.NewHolderId,
                    NewHolderName = item.NewHolderName,
                    OldFlatNo = item.OldFlatNo,
                    OldHolderFlatId = item.OldHolderFlatId,
                    OldHolderId = item.OldHolderId,
                    OldHolderName = item.OldHolderName,
                    PlotId = item.PlotId,
                    PlotNo = item.PlotNo,
                    ReferenceDate = item.ReferenceDate,
                    StringReferenceDate = $"{item.ReferenceDate:dd/MM/yyyy}",
                    ReferenceNo = item.ReferenceNo,
                    TransactionBy = item.TransactionBy,
                    TransactionByUserName = item.TransactionByUserName,
                    TransactionDate = item.TransactionDate
                };



                return View(HolderFlatHistoryVM);
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }


        //// GET: HolderFlatHistory/NewHolderDetails
        //public ActionResult PartialHolderDetails(int id)
        //{
        //    try
        //    {
        //        //id = id > 0 ? id : null;
               

        //        var data = _HoldingManager.GetHolderById(id);

        //        if (data != null)
        //        {
        //            return PartialView("~/Views/HolderFlatHistory/_PartialHolderDetails.cshtml", data);
        //        }
        //        else
        //        {
        //            return PartialView("~/Views/Home/_NoDataFound.cshtml");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        TempData["EM"] = "error | " + exception.Message.ToString();
        //        return PartialView("~/Views/Home/_NoDataFound.cshtml");
        //        //return View();
        //    }
        //}



        // GET: HolderFlatHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HolderFlatHistory/Create
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

        // GET: HolderFlatHistory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HolderFlatHistory/Edit/5
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

        // GET: HolderFlatHistory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HolderFlatHistory/Delete/5
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




        public JsonResult GetData(int HolderId)
        {
            var data = _HoldingManager.GetHolderById(HolderId);
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }





    }
}
