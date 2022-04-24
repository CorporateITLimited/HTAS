using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Models.Holding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Holding
{
    public class RankController : Controller
    {
        private readonly RankManager _RankManager;


        public RankController()
        {
            _RankManager = new RankManager();
        }



        // GET: Rank
        public ActionResult Index()
        {
            return View(_RankManager.GetAllRank());
        }

        // GET: Rank/Details/5
        public ActionResult Details(int id)
        {
            // use try catch
            if (id <= 0)
                return HttpNotFound();

            var Rank = _RankManager.GetRankById(id);
            if (Rank == null)
                return HttpNotFound();

            return View(Rank);
        }

        // GET: Rank/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rank/Create
        [HttpPost]
        public ActionResult Create(clsRank Rank)
        {
            if (Rank == null)
                return HttpNotFound();

            if (string.IsNullOrWhiteSpace(Rank.RankName))
            {
                ModelState.AddModelError("", "র‍্যাংক অবশ্যই পূরণ করতে হবে");
                return View(Rank);
            }


            Rank.IsActive = true;
            Rank.IsDelete = false;
          


            string addRank = _RankManager.RankInsert(Rank);

            if (addRank == CommonConstantHelper.Success)
            {
                TempData["SM"] = "সফলভাবে নতুন র‍্যাংক সাবমিট করা হয়েছে";
                return RedirectToAction("Index", "Rank");
            }
            else if (addRank == CommonConstantHelper.Conflict)
            {
                ModelState.AddModelError("", "একই র‍্যাংক ডাটাবেজে বিদ্যমান ৱয়েছে");
                return View(Rank);
            }
            else if (addRank == CommonConstantHelper.Error)
            {
                ModelState.AddModelError("", "Error");
                return View(Rank);
            }
            else if (addRank == CommonConstantHelper.Failed)
            {
                ModelState.AddModelError("", "Failed");
                return View(Rank);
            }
            else
            {
                ModelState.AddModelError("", "Error Not Recognized");
                return View(Rank);
            }
        }

        // GET: Rank/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
                return HttpNotFound();

            var Rank = _RankManager.GetRankById(id);
            if (Rank == null)
                return HttpNotFound();

            return View(Rank);
        }

        // POST: Rank/Edit/5
        [HttpPost]
        public ActionResult Edit(clsRank Rank)
        {
            if (Rank == null)
                return HttpNotFound();

            if (string.IsNullOrWhiteSpace(Rank.RankName))
            {
                ModelState.AddModelError("", "র‍্যাংক অবশ্যই পূরণ করতে হবে");
                return View(Rank);
            }

            string addRank = _RankManager.RankUpdate(Rank);

            if (addRank == CommonConstantHelper.Success)
            {
                TempData["SM"] = "সফলভাবে র‍্যাংক হালনাগাদ করা হয়েছে";
                return RedirectToAction("Index", "Rank");
            }
            else if (addRank == CommonConstantHelper.Conflict)
            {
                ModelState.AddModelError("", "একই র‍্যাংক ডাটাবেজে বিদ্যমান ৱয়েছে");
                return View(Rank);
            }
            else if (addRank == CommonConstantHelper.Error)
            {
                ModelState.AddModelError("", "Error");
                return View(Rank);
            }
            else if (addRank == CommonConstantHelper.Failed)
            {
                ModelState.AddModelError("", "Failed");
                return View(Rank);
            }
            else
            {
                ModelState.AddModelError("", "Error Not Recognized");
                return View(Rank);
            }
        }

        // GET: Rank/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
                return HttpNotFound();

            var Rank = _RankManager.GetRankById(id);
            if (Rank == null)
                return HttpNotFound();

            return View(Rank);
        }

        // POST: Rank/Delete/5
        [HttpPost]
        public ActionResult Delete(clsRank Rank)
        {
            string addRank = _RankManager.RankDelete(Rank.RankId);

            if (addRank == CommonConstantHelper.Success)
            {
                TempData["SM"] = "সফলভাবে র‍্যাংক বাদ দেয়া হয়েছে";
                return RedirectToAction("Index", "Rank");
            }
            else
            {
                return View(Rank);
            }
        }




    }
}
