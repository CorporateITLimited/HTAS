using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Plots;
using HoldingTaxWebApp.Manager.Users;
using HoldingTaxWebApp.Models.Plots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Plots
{
    public class PlotController : Controller
    {
        private readonly PlotManager _plotManager;
        private readonly DOHSAreaManager _dOHSAreaManager;
        private readonly ClusterManager _clusterManager;

        public PlotController()
        {
            _plotManager = new PlotManager();
            _dOHSAreaManager = new DOHSAreaManager();
            _clusterManager = new ClusterManager();
        }

        // GET: Plot
        public ActionResult Index()
        {
            // use try catch
            return View(_plotManager.GetAllPlot());
        }

        public ActionResult Details(int id)
        {
            // use try catch
            if (id <= 0)
                return HttpNotFound();

            var plot = _plotManager.GetPlotById(id);
            if (plot == null)
                return HttpNotFound();

            return View(plot);
        }


        [HttpGet]
        public ActionResult Create()
        {
            // use try catch

            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
            ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text");
            ViewBag.ClusterId = new SelectList(_clusterManager.GetAllActiveCluster(), "ClusterId", "ClusterName");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Plot plot)
        {
            // use try catch

            if (plot == null)
                return HttpNotFound();

            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", plot.AreaId);
            ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", plot.IsActive);
            ViewBag.ClusterId = new SelectList(_clusterManager.GetAllActiveCluster(), "ClusterId", "ClusterName", plot.ClusterId);

            if (plot.AreaId <= 0)
            {
                ModelState.AddModelError("", "এলাকার নাম অবশ্যই পূরণ করতে হবে");
                return View(plot);
            }

            if (plot.ClusterId <= 0)
            {
                ModelState.AddModelError("", "ক্লাস্টার অবশ্যই পূরণ করতে হবে");
                return View(plot);
            }

            if (string.IsNullOrWhiteSpace(plot.PlotNo))
            {
                ModelState.AddModelError("", "প্লট নং অবশ্যই পূরণ করতে হবে");
                return View(plot);
            }

            if (string.IsNullOrWhiteSpace(plot.RoadNo))
            {
                ModelState.AddModelError("", "রাস্তা নং অবশ্যই পূরণ করতে হবে");
                return View(plot);
            }

            if (plot.TotalArea == null && plot.TotalArea <= 0)
            {
                ModelState.AddModelError("", "এলাকার আয়তন অবশ্যই পূরণ করতে হবে");
                return View(plot);
            }

            plot.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
            plot.CreateDate = DateTime.Now;
            plot.IsActive = true;
            plot.IsDeleted = false;
            plot.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
            plot.LastUpdated = DateTime.Now;


            string addPlot = _plotManager.PlotGatewayInsert(plot);

            if (addPlot == CommonConstantHelper.Success)
            {
                TempData["SM"] = "সফলভাবে নতুন প্লট সাবমিট করা হয়েছে";
                return RedirectToAction("Index", "Plot");
            }
            else if (addPlot == CommonConstantHelper.Conflict)
            {
                ModelState.AddModelError("", "একই এলাকার একই প্লট নম্বর ডাটাবেজে বিদ্যমান ৱয়েছে");
                return View(plot);
            }
            else if (addPlot == CommonConstantHelper.Error)
            {
                ModelState.AddModelError("", "Error");
                return View(plot);
            }
            else if (addPlot == CommonConstantHelper.Failed)
            {
                ModelState.AddModelError("", "Failed");
                return View(plot);
            }
            else
            {
                ModelState.AddModelError("", "Error Not Recognized");
                return View(plot);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // use try catch

            if (id <= 0)
                return HttpNotFound();

            var plot = _plotManager.GetPlotById(id);
            if (plot == null)
                return HttpNotFound();

            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", plot.AreaId);
            ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", plot.IsActive);
            ViewBag.ClusterId = new SelectList(_clusterManager.GetAllActiveCluster(), "ClusterId", "ClusterName", plot.ClusterId);

            return View(plot);
        }

        [HttpPost]
        public ActionResult Edit(Plot plot)
        {
            // use try catch

            if (plot == null)
                return HttpNotFound();

            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", plot.AreaId);
            ViewBag.IsActive = new SelectList(StaticDataHelper.GetActiveStatusForDropdown(), "Value", "Text", plot.IsActive);
            ViewBag.ClusterId = new SelectList(_clusterManager.GetAllActiveCluster(), "ClusterId", "ClusterName", plot.ClusterId);

            if (plot.AreaId <= 0)
            {
                ModelState.AddModelError("", "এলাকার নাম অবশ্যই পূরণ করতে হবে");
                return View(plot);
            }

            if (plot.ClusterId <= 0)
            {
                ModelState.AddModelError("", "ক্লাস্টার অবশ্যই পূরণ করতে হবে");
                return View(plot);
            }

            if (string.IsNullOrWhiteSpace(plot.PlotNo))
            {
                ModelState.AddModelError("", "প্লট নং অবশ্যই পূরণ করতে হবে");
                return View(plot);
            }

            if (string.IsNullOrWhiteSpace(plot.RoadNo))
            {
                ModelState.AddModelError("", "রাস্তা নং অবশ্যই পূরণ করতে হবে");
                return View(plot);
            }

            if (plot.TotalArea == null && plot.TotalArea <= 0)
            {
                ModelState.AddModelError("", "এলাকার আয়তন অবশ্যই পূরণ করতে হবে");
                return View(plot);
            }

            plot.CreatedBy = null;
            plot.CreateDate = null;
            plot.IsDeleted = null;
            plot.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
            plot.LastUpdated = DateTime.Now;


            string updatePlot = _plotManager.PlotGatewayUpdate(plot);

            if (updatePlot == CommonConstantHelper.Success)
            {
                TempData["SM"] = "সফলভাবে প্লট হালনাগাদ করা হয়েছে";
                return RedirectToAction("Index", "Plot");
            }
            else if (updatePlot == CommonConstantHelper.Conflict)
            {
                ModelState.AddModelError("", "একই এলাকার একই প্লট নম্বর ডাটাবেজে বিদ্যমান ৱয়েছে");
                return View(plot);
            }
            else if (updatePlot == CommonConstantHelper.Error)
            {
                ModelState.AddModelError("", "Error");
                return View(plot);
            }
            else if (updatePlot == CommonConstantHelper.Failed)
            {
                ModelState.AddModelError("", "Failed");
                return View(plot);
            }
            else
            {
                ModelState.AddModelError("", "Error Not Recognized");
                return View(plot);
            }
        }




        #region PlotReport
        public ActionResult PlotReport()
        {
            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
            return View();
        }



        public ActionResult rptPlotReport(int? id)
        {
            Session["AreaId_"] = id;
            if(id == 0)
            {
                Session["AreaId_"] = null;
            }
            return View();
        }

        #endregion

    }
}