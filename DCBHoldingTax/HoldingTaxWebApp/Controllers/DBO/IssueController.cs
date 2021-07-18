using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Models.DBO;
using HoldingTaxWebApp.ViewModels.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.DBO
{
    public class IssueController : Controller
    {

        private readonly IssueManager _IssueManager;
        private readonly HoldingManager _HoldingManager;
        private readonly StatusTypeManager _StatusTypeManager;

        public IssueController()
        {
            _IssueManager = new IssueManager();
            _HoldingManager = new HoldingManager();
            _StatusTypeManager = new StatusTypeManager();
        }


        // GET: Issue
        public ActionResult Index()
        {
            try
            {
                var IssueList = new List<Issue>();
                IssueList = _IssueManager.GetAllIssue();

                List<Issue> IssueListVM = new List<Issue>();
                foreach (var item in IssueList)
                {
                    Issue IssueVM = new Issue()
                    {
                        IssueId = item.IssueId,
                        HolderId = item.HolderId,
                        HolderName = item.HolderName,
                        Remarks = item.Remarks,
                        SolvedDate = item.SolvedDate,
                        StatusName = item.StatusName,
                        StatusTypeId = item.StatusTypeId,
                        Subject = item.Subject,



                        IsActive = item.IsActive,
                        IsDeleted = item.IsDeleted,
                        CreateDate = item.CreateDate,
                        CreatedBy = item.CreatedBy,
                        CreatedByUserName = item.CreatedByUserName,
                        LastUpdated = item.LastUpdated,
                        LastUpdatedBy = item.LastUpdatedBy,
                        UpdatedByUserName = item.UpdatedByUserName

                    };
                    IssueListVM.Add(IssueVM);
                }
                return View(IssueListVM.ToList());
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // GET: Issue/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var IssueList = _IssueManager.GetIssueById(id);
                var IssueDetailsList = _IssueManager.GetAllIssueDetailsByIssueId(id);

                if (IssueList == null)
                    return HttpNotFound();
              


                List<IssueDetails> IssueDetailsVM = new List<IssueDetails>();
                foreach (var item in IssueDetailsList)
                {
                    IssueDetails IssueDetails = new IssueDetails()
                    {
                        Doc1 = item.Doc1,
                        Doc2 = item.Doc2,
                        IsRead = item.IsRead,
                        IssueDetailsId = item.IssueDetailsId,
                        IssueId = item.IssueId,
                        MsgDate = item.MsgDate,
                        MsgDetails = item.MsgDetails,
                        StringMsgDate = $"{item.MsgDate:dd/MM/yyyy}",
                        
                    };
                    IssueDetailsVM.Add(IssueDetails);
                }

                IssueCombineVM IssueVM = new IssueCombineVM()
                {
                    CreateDate = IssueList.CreateDate,
                    CreatedBy = IssueList.CreatedBy,
                    IssueId = IssueList.IssueId,
                    CreatedByUserName = IssueList.CreatedByUserName,
                    HolderId = IssueList.HolderId,
                    HolderName = IssueList.HolderName,
                    IsActive = IssueList.IsActive,
                    IsDeleted = IssueList.IsDeleted,
                    IssueDetails = IssueDetailsVM,
                    LastUpdated = IssueList.LastUpdated,
                    LastUpdatedBy = IssueList.LastUpdatedBy,
                    Remarks = IssueList.Remarks,
                    SolvedDate = IssueList.SolvedDate,
                    StatusName = IssueList.StatusName,
                    StatusTypeId = IssueList.StatusTypeId,
                    StringSolvedDate = $"{IssueList.SolvedDate:dd/MM/yyyy}",
                    Subject = IssueList.Subject,
                    UpdatedByUserName = IssueList.UpdatedByUserName

                };



                return View(IssueVM);
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // GET: Issue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Issue/Create
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

        // GET: Issue/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Issue/Edit/5
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

        // GET: Issue/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Issue/Delete/5
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
