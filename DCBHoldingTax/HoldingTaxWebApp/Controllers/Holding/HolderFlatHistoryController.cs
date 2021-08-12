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


        public HolderFlatHistoryController()
        {
            _HolderFlatHistoryManager = new HolderFlatHistoryManager();
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
    }
}
