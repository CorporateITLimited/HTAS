using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Models.DBO;
using HoldingTaxWebApp.ViewModels.DBO;
using System;
using System.Collections.Generic;
using System.IO;
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
               

                List<Issue> IssueListVM = new List<Issue>();
                if (Session[CommonConstantHelper.UserTypeId].ToString() == "2")
                {
                    var HolderId = Convert.ToInt32(Session[CommonConstantHelper.HolderId]);
                    IssueList = _IssueManager.GetAllIssueByHolderId(HolderId);
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
                            StringSolvedDate = $"{item.SolvedDate:dd/MM/yyyy}",


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
                }
                else if (Session[CommonConstantHelper.UserTypeId].ToString() == "1")
                {
                    IssueList = _IssueManager.GetAllIssue();
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
                            StringSolvedDate = $"{item.SolvedDate:dd/MM/yyyy}",


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
                }
                else
                {
                    return RedirectToAction("LogIn", "Account");
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
                        MessageSender = item.MessageSender,
                        MessageSenderName = item.MessageSenderName

                        
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

        // GET: Issue/Edit/5
        public ActionResult Edit(int id)
        {
            Issue issue = new Issue();
            issue = _IssueManager.GetIssueById(id);
            List<IssueDetails> issueDetailsList = new List<IssueDetails>();
            issueDetailsList = _IssueManager.GetAllIssueDetailsByIssueId(id);

            IssueCombineVM issueVM = new IssueCombineVM();
            issueVM.IssueId = issue.IssueId ;
            issueVM.HolderId = issue.HolderId;
            issueVM.StatusTypeId = issue.StatusTypeId;
            issueVM.Subject = issue.Subject;
            issueVM.SolvedDate = issue.SolvedDate;
            issueVM.StringSolvedDate = $"{issue.SolvedDate:dd/MM/yyyy}";
            issueVM.Remarks = issue.Remarks;
            issueVM.CreatedByUserName = issue.CreatedByUserName;
            issueVM.LastUpdatedBy = issue.LastUpdatedBy;
            issueVM.UpdatedByUserName = issue.UpdatedByUserName;
            issueVM.HolderName = issue.HolderName;
            issueVM.StatusName = issue.StatusName;
            issueVM.IssueDetails = issueDetailsList;



            ViewBag.StatusTypeId = new SelectList(_StatusTypeManager.GetAllStatusType(), "StatusTypeId", "StatusName", issue.StatusTypeId);
            return View(issueVM);
        }

        // POST: Issue/Edit/5
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


        #region Save Data

        [HttpPost]
        public JsonResult AddOrUpdate(IssueCombineVM issue)
        {
            try
            {
                var maxId = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string status = "error";

                if (issue.IssueId == 0) //IsNullOrEmpty(POVM.NoaId.ToString())
                {
                    if (issue.Subject == null)
                    {
                        status = "অভিযোগের বিষয় লিখতে হবে";
                        return new JsonResult { Data = new { status } };
                    }






                    Issue Iss = new Issue()
                    {
                        HolderId = Convert.ToInt32(Session[CommonConstantHelper.HolderId]),
                        StatusTypeId = 1,
                        Subject = issue.Subject,
                        //IssueDetailsId = issue.IssueDetails,
                        IsActive = true,
                        IsDeleted = false,
                        CreateDate = DateTime.Now,
                        CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                        LastUpdated = DateTime.Now,
                        LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),

                    };

                    int issueId = _IssueManager.IssueInsert(Iss);
                    //int issueId = 4;





                    if (issueId > 0)
                    {


                       

                        foreach (IssueDetails item in issue.IssueDetails)
                        {


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
                                    var newFilename = maxId + "_doc1_" + fileOldName + extension;
                                    newFilename = "/Documents/Issues/" + newFilename;

                                    if (System.IO.File.Exists(newFilename))
                                        System.IO.File.Delete(newFilename);
                                    file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                                    item.Doc1 = newFilename;
                                }
                                else
                                {
                                    item.Doc1 = null;
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
                                    var newFilename = maxId + "_doc2_" + fileOldName + extension;
                                    newFilename = "/Documents/Issues/" + newFilename;

                                    if (System.IO.File.Exists(newFilename))
                                        System.IO.File.Delete(newFilename);
                                    file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                                    item.Doc2 = newFilename;
                                }
                                else
                                {
                                    item.Doc2 = null;
                                }
                            }


                            IssueDetails Details = new IssueDetails()
                            {
                                IssueId = issueId,
                                Doc1 = item.Doc1,
                                Doc2 = item.Doc2,
                                IsRead = item.IsRead,
                                MessageSender = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                MsgDetails = item.MsgDetails,
                                MsgDate = DateTime.Now,
                                
                            };
                            string returnString = CommonConstantHelper.Success;
                            if (Details.MsgDetails.Length != 0)
                            {
                                returnString = _IssueManager.IssueDetailsInsert(Details);
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
                    if (issue.Subject != null)
                    {
                        status = "অভিযোগের বিষয় লিখতে হবে";
                        return new JsonResult { Data = new { status } };
                    }






                    Issue Iss = new Issue()
                    {
                        HolderId = issue.HolderId,
                        IssueId = issue.IssueId,
                        StatusTypeId = issue.StatusTypeId,
                        Subject = issue.Subject,
                      
                        //SolvedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        CreateDate = DateTime.Now,
                        CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                        LastUpdated = DateTime.Now,
                        LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),

                    };

                    var issss = _IssueManager.GetIssueById(issue.IssueId);

                    if(issss.SolvedDate == null) {

                        if (issue.StatusTypeId == 3)
                        {
                            Iss.SolvedDate = DateTime.Now;
                        }
                    }

                   
                    int issueId = _IssueManager.IssueUpdate(Iss);

                    foreach (IssueDetails item in issue.IssueDetails)
                    {

                        //if (Session["DocFile1"] != null)
                        //{
                        //    HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile1"];
                        //    if (file != null && file.ContentLength > 0)
                        //    {
                        //        if (file.ContentLength > 2 * 1024 * 1024)
                        //        {
                        //            status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                        //            return new JsonResult { Data = new { status } };
                        //        }

                        //        var extension = Path.GetExtension(file.FileName);
                        //        var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                        //        fileOldName = fileOldName.Replace(" ", string.Empty);
                        //        var newFilename = maxId + "_doc1_" + fileOldName + extension;
                        //        newFilename = "/Documents/Issues/" + newFilename;

                        //        //if (System.IO.File.Exists(newFilename))
                        //        //    System.IO.File.Delete(newFilename);

                        //        if (!string.IsNullOrWhiteSpace(item.Doc1))
                        //        {
                        //            var deletePathFile = Path.Combine(Server.MapPath(item.Doc1));
                        //            if (System.IO.File.Exists(deletePathFile))
                        //                System.IO.File.Delete(deletePathFile);
                        //        }



                        //        file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                        //        item.Doc1 = newFilename;
                        //    }
                        //    else
                        //    {
                        //        item.Doc1 = null;
                        //    }
                        //}

                        //if (Session["DocFile2"] != null)
                        //{
                        //    HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile2"];
                        //    if (file != null && file.ContentLength > 0)
                        //    {
                        //        if (file.ContentLength > 2 * 1024 * 1024)
                        //        {
                        //            status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                        //            return new JsonResult { Data = new { status } };
                        //        }

                        //        var extension = Path.GetExtension(file.FileName);
                        //        var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                        //        fileOldName = fileOldName.Replace(" ", string.Empty);
                        //        var newFilename = maxId + "_doc2_" + fileOldName + extension;
                        //        newFilename = "/Documents/Issues/" + newFilename;

                        //        //if (System.IO.File.Exists(newFilename))
                        //        //    System.IO.File.Delete(newFilename);
                        //        if (!string.IsNullOrWhiteSpace(item.Doc2))
                        //        {
                        //            var deletePathFile = Path.Combine(Server.MapPath(item.Doc2));
                        //            if (System.IO.File.Exists(deletePathFile))
                        //                System.IO.File.Delete(deletePathFile);
                        //        }

                        //        file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                        //        item.Doc2 = newFilename;
                        //    }
                        //    else
                        //    {
                        //        item.Doc2 = null;
                        //    }
                        //}
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
                                var newFilename = maxId + "_doc1_" + fileOldName + extension;
                                newFilename = "/Documents/Issues/" + newFilename;

                                if (System.IO.File.Exists(newFilename))
                                    System.IO.File.Delete(newFilename);
                                file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                                item.Doc1 = newFilename;
                            }
                            else
                            {
                                item.Doc1 = null;
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
                                var newFilename = maxId + "_doc2_" + fileOldName + extension;
                                newFilename = "/Documents/Issues/" + newFilename;

                                if (System.IO.File.Exists(newFilename))
                                    System.IO.File.Delete(newFilename);
                                file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                                item.Doc2 = newFilename;
                            }
                            else
                            {
                                item.Doc2 = null;
                            }
                        }


                        IssueDetails Details = new IssueDetails()
                        {
                            IssueId = issueId,
                            IssueDetailsId = item.IssueDetailsId,
                            Doc1 = item.Doc1,
                            Doc2 = item.Doc2,
                            //IsRead = item.IsRead,
                            MessageSender = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                            MsgDetails = item.MsgDetails,
                            MsgDate = DateTime.Now,

                        };
                        string returnString = CommonConstantHelper.Success;
                        if (Details.MsgDetails.Length != 0)
                        {
                            returnString = _IssueManager.IssueDetailsInsert(Details);
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









        public ActionResult DownloadFile(string filePath)
        {
            //string fullName = Server.MapPath(filePath);

            string fullName = CommonConstantHelper.ServerRootDirectory + filePath;
            //Request.MapPath(fullPath);

            string nameOfDoc = filePath.Substring(filePath.LastIndexOf('/') + 1);

            byte[] fileBytes = GetFile(fullName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, nameOfDoc);

        }

        byte[] GetFile(string s)
        {
            FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new IOException(s);
            return data;
        }




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


        #endregion




        public JsonResult GetTopFiveIssue()
        {
            var data = _IssueManager.GetTopFiveIssue();

            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}
