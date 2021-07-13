using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoldingTaxWebApp.Helpers;
using HoldingTaxWebApp.Manager;
using HoldingTaxWebApp.Manager.DBO;
using HoldingTaxWebApp.Manager.Holding;
using HoldingTaxWebApp.Manager.Plots;
using HoldingTaxWebApp.Models.Holding;
using HoldingTaxWebApp.ViewModels;

namespace HoldingTaxWebApp.Controllers.Holding
{
    public class HoldingController : Controller
    {
        private readonly bool CanAccess = false;
        private readonly bool CanReadWrite = false;
        private readonly CommonListManager _commonListManager;
        private readonly HoldingManager _holdingManager;
        private readonly PlotManager _plotManager;
        private readonly OwnershipSourceManager _ownershipSourceManager;

        public HoldingController()
        {
            _commonListManager = new CommonListManager();
            _holdingManager = new HoldingManager();
            _plotManager = new PlotManager();
            _ownershipSourceManager = new OwnershipSourceManager();
        }

        // GET: Holding
        public ActionResult Index()
        {
            return View(_holdingManager.GetAllHolder());
        }

        public ActionResult Details(int id)
        {
            if (id <= 0)
                return HttpNotFound();

            Holder holder = _holdingManager.GetHolderById(id);
            if (holder == null)
                return HttpNotFound();

            List<HolderFlat> listOfHolders = _holdingManager.GetHoldersFlatByHolderId(id);
            //if (listOfHolders == null || listOfHolders.Count() <= 0)
            //    return HttpNotFound();

            HolderVM vm = new HolderVM()
            {
                AmountOfLand = holder.AmountOfLand,
                AreaName = holder.AreaName,
                BuildingTypeName = holder.BuildingTypeName,
                Contact1 = holder.Contact1,
                Contact2 = holder.Contact2,
                ContactAdd = holder.ContactAdd,
                StringCreateDate = holder.StringCreateDate,
                CreatedByUsername = holder.CreatedByUsername,
                Document1 = holder.Document1,
                Document2 = holder.Document2,
                EachFloorArea = holder.EachFloorArea,
                Email = holder.Email,
                Father = holder.Father,
                GenderType = holder.GenderType,
                HolderFlatList = listOfHolders,
                HolderId = holder.HolderId,
                HolderName = holder.HolderName,
                HoldersFlatNumber = holder.HoldersFlatNumber,
                ImageLocation = holder.ImageLocation,
                IsActive = holder.IsActive,
                MaritialStatusType = holder.MaritialStatusType,
                Mother = holder.Mother,
                NID = holder.NID,
                OwnerTypeName = holder.OwnerTypeName,
                PermanentAdd = holder.PermanentAdd,
                PlotNo = holder.PlotNo,
                PresentAdd = holder.PresentAdd,
                PreviousDueTax = holder.PreviousDueTax,
                SourceName = holder.SourceName,
                Spouse = holder.Spouse,
                StringLastUpdated = holder.StringLastUpdated,
                TotalFlat = holder.TotalFlat,
                TotalFloor = holder.TotalFloor,
                UpdatedByUsername = holder.UpdatedByUsername,
                RoadName = holder.RoadName,
                RoadNo = holder.RoadNo,

                // id values
                AreaId = holder.AreaId,
                BuildingTypeId = holder.BuildingTypeId,
                Gender = holder.Gender,
                MaritialStatus = holder.MaritialStatus,
                OwnershipSourceId = holder.OwnershipSourceId,
                OwnerType = holder.OwnerType,
                PlotId = holder.PlotId,

                // converted values
               StrAmountOfLand = holder.StrAmountOfLand,
               StrPreviousDueTax = holder.StrPreviousDueTax,
               StrEachFloorArea = holder.StrEachFloorArea,
               StrHoldersFlatNumber = holder.StrHoldersFlatNumber,
               StrTotalFlat = holder.StrTotalFlat,
               StrTotalFloor = holder.StrTotalFloor
            };

            return View(vm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.AreaId = new SelectList(_constantManager.GetAllRentTaxRates(), "AreaId", "AreaName");
            ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo");
            ViewBag.OwnershipSourceId = new SelectList(_ownershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName");
            //ViewBag.BuildingTypeId = new SelectList(_constantManager.GetAllRentTaxRates(), "AreaId", "AreaName");

            ViewBag.Gender = new SelectList(_commonListManager.GetAllGender(), "TypeId", "TypeName");
            ViewBag.MaritialStatus = new SelectList(_commonListManager.GetAllMaritalStatus(), "TypeId", "TypeName");
            ViewBag.OwnerType = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName");
            ViewBag.FlorNo = _commonListManager.GetAllFloor();
            ViewBag.OwnOrRent = _commonListManager.GetAllOwnOrRent();
            ViewBag.SelfOwned = _commonListManager.GetAllOwnType();
            return View();
        }

        [HttpPost]
        public JsonResult AddData(HolderVM hvm)
        {
            //if (CanAccess && CanReadWrite)
            //{
            try
            {
                string status = "error";

                //return new JsonResult { Data = new { status } };

                //checking session first
                int uid = 0;
                if (Session[CommonConstantHelper.UserId] != null && Convert.ToInt32(Session[CommonConstantHelper.UserId]) > 0)
                {
                    uid = Convert.ToInt32(Session[CommonConstantHelper.UserId]);
                }
                else
                {
                    status = "সেশন এর মেয়াদ শেষ";
                    return new JsonResult { Data = new { status } };
                }

                //checking if data is null
                if (hvm == null)
                {
                    status = "কোনো ডাটা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                // global declaration
                Holder holder = new Holder();
                int maxId = _holdingManager.GetMAXId();


                // validations

                if ( hvm.AreaId > 0)
                {
                    holder.AreaId =  hvm.AreaId;
                }
                else
                {
                    status = "এলাকার নাম পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (  hvm.PlotId > 0)
                {
                    holder.PlotId =   hvm.PlotId;
                }
                else
                {
                    status = "প্লট/বাড়ী/ফ্ল্যাট নম্বর পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(  hvm.HolderName))
                {
                    holder.HolderName =   hvm.HolderName;
                }
                else
                {
                    status = "প্লট/ফ্ল্যাট/বাড়ী মালিকের নাম পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(  hvm.NID))
                {
                    holder.NID =   hvm.NID;
                }
                else
                {
                    status = "জাতীয় পরিচয়পত্র পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (  hvm.Gender > 0)
                {
                    holder.Gender =   hvm.Gender;
                }
                else
                {
                    status = "লিঙ্গ পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (  hvm.MaritialStatus > 0)
                {
                    holder.MaritialStatus =   hvm.MaritialStatus;
                }
                else
                {
                    status = "বৈবাহিক অবস্থা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(  hvm.Father))
                {
                    holder.Father =   hvm.Father;
                }
                else
                {
                    status = "পিতার নাম পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(  hvm.Mother))
                {
                    holder.Mother =   hvm.Mother;
                }
                else
                {
                    status = "মাতার নাম পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                holder.Spouse = !string.IsNullOrWhiteSpace(  hvm.Spouse) ?   hvm.Spouse : string.Empty;
                holder.Contact1 = !string.IsNullOrWhiteSpace(  hvm.Contact1) ?   hvm.Contact1 : string.Empty;

                if (!string.IsNullOrWhiteSpace(  hvm.Contact2))
                {
                    holder.Contact2 =   hvm.Contact2;
                }
                else
                {
                    status = "মোবাইল নম্বর পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                holder.Email = !string.IsNullOrWhiteSpace(  hvm.Email) ?   hvm.Email : string.Empty;

                if (!string.IsNullOrWhiteSpace(  hvm.PresentAdd))
                {
                    holder.PresentAdd =   hvm.PresentAdd;
                }
                else
                {
                    status = "বর্তমান ঠিকানা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(  hvm.PermanentAdd))
                {
                    holder.PermanentAdd =   hvm.PermanentAdd;
                }
                else
                {
                    status = "স্থায়ী ঠিকানা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(  hvm.ContactAdd))
                {
                    holder.ContactAdd =   hvm.ContactAdd;
                }
                else
                {
                    status = "পত্র যোগাযোগের ঠিকানা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                holder.PreviousDueTax =   hvm.PreviousDueTax != null &&   hvm.PreviousDueTax > 0 ?   hvm.PreviousDueTax : 0;


                if (Session["ImageFile"] != null)
                {
                    HttpPostedFileBase file = (HttpPostedFileBase)Session["ImageFile"];
                    if (file != null && file.ContentLength > 0)
                    {
                        string extensions = file.ContentType.ToLower();
                        if (extensions != "image/jpg" && extensions != "image/jpeg" && extensions != "image/pjpeg" &&
                           extensions != "image/gif" && extensions != "image/png" && extensions != "image/x-png")
                        {
                            status = "পাসপোর্ট সাইজের ছবির ফরমেট ঠিক নেই";
                            return new JsonResult { Data = new { status } };
                        }
                        var extension = Path.GetExtension(file.FileName);
                        var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                        fileOldName = fileOldName.Replace(" ", string.Empty);
                        var newFilename = maxId + "_" + fileOldName + extension;
                        newFilename = "/Documents/Holders/Images/" + newFilename;

                        if (System.IO.File.Exists(newFilename))
                            System.IO.File.Delete(newFilename);
                        file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                        holder.ImageLocation = newFilename;
                    }
                    else
                    {
                        status = "পাসপোর্ট সাইজের ছবি পাওয়া যাই নি";
                        return new JsonResult { Data = new { status } };
                    }
                }
                else
                {
                    status = "পাসপোর্ট সাইজের ছবি পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.OwnershipSourceId > 0)
                {
                    holder.OwnershipSourceId = hvm.OwnershipSourceId;
                }
                else
                {
                    status = "মালিকানার সূত্র পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.OwnerType > 0)
                {
                    holder.OwnerType = hvm.OwnerType;
                    if (holder.OwnerType == 3 && Session["DocFile1"] == null && Session["DocFile2"] == null)
                    {
                        status = "খেতাব প্রাপ্ত মুক্তিযোদ্ধার ক্ষেত্রে  প্রমাণাদি সংযুক্ত করুন (একটি বা দুইটি)";
                        return new JsonResult { Data = new { status } };
                    }
                    if (holder.OwnerType == 3 && Session["DocFile1"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile1"];
                        if (file != null && file.ContentLength > 0)
                        {
                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            var newFilename = maxId + "_doc1_" + fileOldName + extension;
                            newFilename = "/Documents/Holders/FFDocuments/" + newFilename;

                            if (System.IO.File.Exists(newFilename))
                                System.IO.File.Delete(newFilename);
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            holder.Document1 = newFilename;
                        }
                        else
                        {
                            holder.Document1 = string.Empty;
                        }
                    }

                    if (holder.OwnerType == 3 && Session["DocFile2"] != null)
                    {
                        HttpPostedFileBase file = (HttpPostedFileBase)Session["DocFile2"];
                        if (file != null && file.ContentLength > 0)
                        {
                            var extension = Path.GetExtension(file.FileName);
                            var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileOldName = fileOldName.Replace(" ", string.Empty);
                            var newFilename = maxId + "_doc2_" + fileOldName + extension;
                            newFilename = "/Documents/Holders/FFDocuments/" + newFilename;

                            if (System.IO.File.Exists(newFilename))
                                System.IO.File.Delete(newFilename);
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            holder.Document2 = newFilename;
                        }
                        else
                        {
                            holder.Document2 = string.Empty;
                        }
                    }
                }
                else
                {
                    status = "মালিকানার ধরন পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.BuildingTypeId > 0)
                {
                    holder.BuildingTypeId = hvm.BuildingTypeId;
                }
                else
                {
                    status = "ভবনের ধরন পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.AmountOfLand != null && hvm.AmountOfLand > 0)
                {
                    holder.AmountOfLand = hvm.AmountOfLand;
                }
                else
                {
                    status = "জমির পরিমাণ পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.TotalFloor != null && hvm.TotalFloor > 0)
                {
                    holder.TotalFloor = hvm.TotalFloor;
                }
                else
                {
                    status = "মোট তলার সংখ্যা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.EachFloorArea != null && hvm.EachFloorArea > 0)
                {
                    holder.EachFloorArea = hvm.EachFloorArea;
                }
                else
                {
                    status = "প্রতিতলার আয়তন পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.TotalFlat != null && hvm.TotalFlat > 0)
                {
                    holder.TotalFlat = hvm.TotalFlat;
                }
                else
                {
                    status = "মোট ফ্ল্যাট সংখ্যা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.HoldersFlatNumber != null && hvm.HoldersFlatNumber > 0)
                {
                    holder.HoldersFlatNumber = hvm.HoldersFlatNumber;
                }
                else
                {
                    status = "নিজ মালিকানাধীন ফ্ল্যাট সংখ্যা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                holder.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                holder.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                holder.CreateDate = DateTime.Now;
                holder.LastUpdated = DateTime.Now;
                holder.IsActive = true;
                holder.IsDeleted = false;

                int holderId = _holdingManager.InsertHolder(holder);

                if (holderId > 0 && holderId == maxId)
                {
                    foreach (HolderFlat item in hvm.HolderFlatList)
                    {
                        HolderFlat deatils = new HolderFlat()
                        {
                            CreateDate = DateTime.Now,
                            CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                            FlatArea = item.FlatArea != null && item.FlatArea > 0 ? item.FlatArea : 0,
                            FlatNo = !string.IsNullOrWhiteSpace(item.FlatNo) ? item.FlatNo : string.Empty,
                            FlorNo = item.FlorNo,
                            HolderFlatId = 0,
                            HolderId = holderId,
                            IsActive = true,
                            IsDeleted = false,
                            LastUpdated = DateTime.Now,
                            LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                            IsSelfOwned = item.SelfOwn == 1 ? true : false,
                            MonthlyRent = item.MonthlyRent != null && item.MonthlyRent > 0 ? item.MonthlyRent : 0,
                            OwnerName = !string.IsNullOrWhiteSpace(item.OwnerName) ? item.OwnerName : string.Empty,
                            OwnOrRent = item.OwnOrRent,
                            SelfOwn = item.SelfOwn
                        };

                        string returnString = _holdingManager.HoldersFlatInsert(deatils);

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
            catch (Exception exception)
            {
                string status = exception.Message.ToString();
                return new JsonResult { Data = new { status } };
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

        public JsonResult GetRatePerSquareFeet(int AreaId, int BuildingTypeId)
        {
            return new JsonResult { Data = _holdingManager.GetPerSqrFeetPrice(AreaId, BuildingTypeId) };
        }

        public JsonResult GetPlotDetails(int PlotId)
        {
            return new JsonResult { Data = _plotManager.GetPlotById(PlotId) };
        }

        public JsonResult GetImage(HttpPostedFileBase fileBase)
        {
            if (fileBase != null)
            {
                Session["ImageFile"] = null;
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

    }
}