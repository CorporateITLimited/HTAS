using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Models.Holding;
using HoldingTaxWebApp.ViewModels;

namespace HoldingTaxWebApp.Controllers.Holding
{
    public class HoldingController : Controller
    {
        // GET: Holding
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddOrUpdate(HolderVM hvm)
        {
            //if (CanAccess && CanReadWrite)
            //{
            try
            {
                string status = "error";

                if (Session["ImageFile"] != null)
                {
                    var file = (HttpPostedFileBase)Session["ImageFile"];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                }

               // int uid = 0;
                return new JsonResult { Data = new { status } };

                #region old 
                //if (Session[CommonConstantHelper.UserId] != null && Convert.ToInt32(Session[CommonConstantHelper.UserId]) > 0)
                //{
                //    uid = Convert.ToInt32(Session[CommonConstantHelper.UserId]);
                //}
                //else
                //{
                //    status = "no_user";
                //    return new JsonResult
                //    {
                //        Data = new
                //        {
                //            status
                //        }
                //    };
                //}
                //if (hvm.HolderId > 0) //IsNullOrEmpty(BillItem.NoaId.ToString())
                //{

                //hvm.BGPGExpiaryDate = DateTime.ParseExact(hvm.StringBGPGExpiaryDate, "dd/MM/yyyy", null);
                //hvm.EstimatedDeliveryTime = DateTime.ParseExact(hvm.StringEstimatedDeliveryTime, "dd/MM/yyyy", null);
                //hvm.StartTime = DateTime.ParseExact(hvm.StringStartTime, "dd/MM/yyyy", null);

                //Project Project = new Project()
                //{

                //    LastUpdated = DateTime.Now,
                //    IsActive = true,
                //    IsDeleted = false,
                //    BGDetails = hvm.BGDetails,
                //    BGPGAmount = hvm.BGPGAmount,
                //    BGPGExpiaryDate = hvm.BGPGExpiaryDate,
                //    BGPGRemarks = hvm.BGPGRemarks,
                //    ClientName = hvm.ClientName,
                //    EstimatedDeliveryTime = hvm.EstimatedDeliveryTime,
                //    NetProfit = hvm.NetProfit,
                //    ProjectDescription = hvm.ProjectDescription,
                //    ProjectId = hvm.ProjectId,
                //    ProjectName = hvm.ProjectName,
                //    ProjectValue = hvm.ProjectValue,
                //    StartTime = hvm.StartTime,
                //    FinalAmount = hvm.FinalAmount,
                //    OtherCost = hvm.OtherCost,
                //    OtherCostRemarks = hvm.OtherCostRemarks,
                //    VatTax = hvm.VatTax,
                //    ClientId = hvm.ClientId,

                //    LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),

                //    CreateDate = DateTime.Now,
                //    CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),


                //};

                //int projectId = _ProjectManager.ProjectInsert(Project);

                //if (projectId > 0)
                //{
                //    foreach (ProjectCost item in hvm.ProjectCost)
                //    {
                //        ProjectCost CostDetails = new ProjectCost()
                //        {
                //            ProjectId = projectId,
                //            CostCategoryId = item.CostCategoryId,
                //            CostingName = item.CostingName,
                //            ProjectCostId = 0,
                //            Ref = item.Ref,
                //            Value = item.Value
                //        };

                //        string returnString = _ProjectManager.ProjectCostInsertOrUpdate(CostDetails);

                //        if (returnString != CommonConstantHelper.Success)

                //        {
                //            status = "error_details";
                //            break;
                //        }
                //        else
                //        {
                //            status = "success";
                //        }
                //    }
                //}


                //return new JsonResult { Data = new { status } };
                //}
                //else
                //{
                //        ProjectItem.BGPGExpiaryDate = DateTime.ParseExact(ProjectItem.StringBGPGExpiaryDate, "dd/MM/yyyy", null);
                //        ProjectItem.EstimatedDeliveryTime = DateTime.ParseExact(ProjectItem.StringEstimatedDeliveryTime, "dd/MM/yyyy", null);
                //        ProjectItem.StartTime = DateTime.ParseExact(ProjectItem.StringStartTime, "dd/MM/yyyy", null);


                //        Project Project = new Project()
                //        {

                //            LastUpdated = DateTime.Now,
                //            //IsActive = true,
                //            //IsDeleted = false,
                //            BGDetails = ProjectItem.BGDetails,
                //            BGPGAmount = ProjectItem.BGPGAmount,
                //            BGPGExpiaryDate = ProjectItem.BGPGExpiaryDate,
                //            BGPGRemarks = ProjectItem.BGPGRemarks,
                //            ClientName = ProjectItem.ClientName,
                //            EstimatedDeliveryTime = ProjectItem.EstimatedDeliveryTime,
                //            NetProfit = ProjectItem.NetProfit,
                //            ProjectDescription = ProjectItem.ProjectDescription,
                //            ProjectId = ProjectItem.ProjectId,
                //            ProjectName = ProjectItem.ProjectName,
                //            ProjectValue = ProjectItem.ProjectValue,
                //            StartTime = ProjectItem.StartTime,
                //            FinalAmount = ProjectItem.FinalAmount,
                //            OtherCost = ProjectItem.OtherCost,
                //            OtherCostRemarks = ProjectItem.OtherCostRemarks,
                //            VatTax = ProjectItem.VatTax,
                //            ClientId = ProjectItem.ClientId,

                //            LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),

                //            //CreateDate = DateTime.Now,
                //            //CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),


                //        };

                //        int projectId = _ProjectManager.ProjectUpdate(Project);

                //        if (projectId > 0)
                //        {
                //            foreach (ProjectCost item in ProjectItem.ProjectCost)
                //            {
                //                ProjectCost CostDetails = new ProjectCost()
                //                {
                //                    ProjectId = projectId,
                //                    CostCategoryId = item.CostCategoryId,
                //                    CostingName = item.CostingName,
                //                    ProjectCostId = item.ProjectCostId,
                //                    Ref = item.Ref,
                //                    Value = item.Value
                //                };

                //                string returnString = _ProjectManager.ProjectCostInsertOrUpdate(CostDetails);

                //                if (returnString != CommonConstantHelper.Success)

                //                {
                //                    status = "error_details";
                //                    break;
                //                }
                //                else
                //                {
                //                    status = "success";
                //                }
                //            }
                //        }



                //        return new JsonResult { Data = new { status } };
                //    }
                #endregion

            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return new JsonResult { Data = "error" };

            }
            //}
            //else
            //{
            //    TempData["PM"] = "Permission Denied.";
            //    return new JsonResult { Data = "forbidden" };
            //}
            
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Holder holder)
        {
            return View();
        }

        public JsonResult GetImage(HttpPostedFileBase fileBase)
        {
            if (fileBase != null)
            {
                Session["ImageFile"] = null;
                int fileSize = fileBase.ContentLength;
                string fileName = fileBase.FileName;
                string mimeType = fileBase.ContentType;
                System.IO.Stream fileContent = fileBase.InputStream;
                Session["ImageFile"] = fileBase;
                return new JsonResult { Data = "done" };
            }
            else
            {
                Session["ImageFile"] = null;
                return new JsonResult { Data = "not_done" };
            }
           
        }

        public JsonResult GetDocFile1(HttpPostedFileBase fileBase)
        {
            if (fileBase != null)
            {
                Session["DocFile1"] = null;
                int fileSize = fileBase.ContentLength;
                string fileName = fileBase.FileName;
                string mimeType = fileBase.ContentType;
                System.IO.Stream fileContent = fileBase.InputStream;
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
                int fileSize = fileBase.ContentLength;
                string fileName = fileBase.FileName;
                string mimeType = fileBase.ContentType;
                System.IO.Stream fileContent = fileBase.InputStream;
                Session["DocFile2"] = fileBase;
                return new JsonResult { Data = "done" };
            }
            else
            {
                Session["DocFile2"] = null;
                return new JsonResult { Data = "not_done" };
            }
        }

    }
}