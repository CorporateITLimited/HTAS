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
        private readonly DOHSAreaManager _dOHSAreaManager;
        private readonly BuildingTypeManager _buildingTypeManager;
        private readonly HolderFlatHistoryManager _flatHistoryManager;

        public HoldingController()
        {
            _commonListManager = new CommonListManager();
            _holdingManager = new HoldingManager();
            _plotManager = new PlotManager();
            _ownershipSourceManager = new OwnershipSourceManager();
            _dOHSAreaManager = new DOHSAreaManager();
            _buildingTypeManager = new BuildingTypeManager();
            _flatHistoryManager = new HolderFlatHistoryManager();
        }

        // GET: Holding
        public ActionResult Index()
        {
            return View(_holdingManager.GetAllHolder());
        }

        public ActionResult NewIndex()
        {
            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
            ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo");
            return View();
        }

        public ActionResult PartialIndex(int? AreaId, int? PlotId)
        {
            //if ((Session[CommonConstantHelper.LogInCredentialId] != null)
            //      && (Convert.ToInt32(Session[CommonConstantHelper.UserTypeId]) == 1)
            //      && (Session[CommonConstantHelper.UserId] != null))
            //{
            try
            {
                AreaId = AreaId > 0 ? AreaId : null;
                PlotId = PlotId > 0 ? PlotId : null;

                var data = _holdingManager.GetHolderIndexData(AreaId, PlotId);

                if (data != null && data.Count > 0)
                {
                    return PartialView("~/Views/Holding/_PartialIndex.cshtml", data);
                }
                else
                {
                    return PartialView("~/Views/Home/_NoDataFound.cshtml");
                }
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return PartialView("~/Views/Home/_NoDataFound.cshtml");
                //return View();
            }
            //}
            //else
            //{
            //    TempData["EM"] = "Session Expired.";
            //    return RedirectToAction("LogIn", "Account");
            //}
        }

        public ActionResult Details(int id)
        {
            if (id <= 0)
                return HttpNotFound();

            Holder holder = _holdingManager.GetHolderById(id);
            if (holder == null)
                return HttpNotFound();

            List<HolderFlat> listOfHolders = new List<HolderFlat>();
            if (holder.IsHolderAnOwner == true)
                listOfHolders = _holdingManager.GetHoldersFlatByHolderId(id);
            else
                listOfHolders = _holdingManager.GetHoldersFlatByMainHolderId(id);
            //if (listOfHolders == null || listOfHolders.Count() <= 0)
            //    return HttpNotFound();

            HolderVM holderVMOtherData = _holdingManager.GetAllotmentNamjariDesignByPlotId(holder.PlotId);

            HolderVM vm = new HolderVM()
            {
                AmountOfLand = holder.AmountOfLand,
                AreaName = holder.AreaName,
                BuildingTypeName = holder.BuildingTypeName,
                Contact1 = holder.Contact1,
                Contact2 = holder.Contact2,
                ContactAdd = holder.ContactAdd,
                Document1 = holder.Document1,
                Document2 = holder.Document2,
                EachFloorArea = holder.EachFloorArea,
                Email = holder.Email,
                Father = holder.Father,
                GenderType = holder.GenderType,
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
                TotalFlat = holder.TotalFlat,
                TotalFloor = holder.TotalFloor,
                RoadName = holder.RoadName,
                RoadNo = holder.RoadNo,
                AreaPlotFlatData = holder.AreaPlotFlatData,
                NamjariLetterNo = holder.NamjariLetterNo,
                AllocationLetterNo = holder.AllocationLetterNo,

                // list
                HolderFlatList = listOfHolders,

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
                StrTotalFloor = holder.StrTotalFloor,

                // action by
                CreatedByUsername = holder.CreatedByUsername,
                UpdatedByUsername = holder.UpdatedByUsername,

                // string date
                StringAllocationDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holder.StringAllocationDate),
                StringNamjariDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holder.StringNamjariDate),
                StringRecordCorrectionDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holder.StringRecordCorrectionDate),
                StringLastUpdated = holder.StringLastUpdated,
                StringCreateDate = holder.StringCreateDate,

                // other data allotment namjari design approval
                FirstApprovalLetterNo = holderVMOtherData.FirstApprovalLetterNo,
                StringFirstApprovalDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holderVMOtherData.StringFirstApprovalDate),
                LastApprovalLetterNo = holderVMOtherData.LastApprovalLetterNo,
                StringLastApprovalDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holderVMOtherData.StringLastApprovalDate),
                LeasePeriod = holderVMOtherData.LeasePeriod,
                StrLeasePeriod = BanglaConvertionHelper.IntegerValueEnglish2Bangla(holderVMOtherData.LeasePeriod),
                StringLeaseExpiryDate = BanglaConvertionHelper.StringEnglish2StringBanglaDate(holderVMOtherData.StringLeaseExpiryDate),
                PlotOwnerName = holderVMOtherData.PlotOwnerName,
                IsHolderAnOwner = holder.IsHolderAnOwner
            };


            // rent per square feet
            // boraddo grohitar nam, ejara meyad, meydurtinner tarik, 1st noksa onumodon tarik, onumodon number,  last noksa onumodon tarik,onumodon number

            return View(vm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
            ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo");
            ViewBag.OwnershipSourceId = new SelectList(_ownershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName");
            ViewBag.BuildingTypeId = new SelectList(_buildingTypeManager.GetAllBuildingType(), "BuildingTypeId", "BuildingTypeName");
            ViewBag.Gender = new SelectList(_commonListManager.GetAllGender(), "TypeId", "TypeName");
            ViewBag.MaritialStatus = new SelectList(_commonListManager.GetAllMaritalStatus(), "TypeId", "TypeName");
            ViewBag.OwnerType = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName");
            ViewBag.IsHolderAnOwner = new SelectList(StaticDataHelper.GetOwnerForDropdown(), "Value", "Text");
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

                if (hvm.HolderFlatList.Count == 0 || hvm.HolderFlatList == null || !hvm.HolderFlatList.Any())
                {
                    status = "কমপক্ষে একটি ফ্ল্যাটের বিবরণ সাবমিট করুন";
                    return new JsonResult { Data = new { status } };
                }


                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", hvm.AreaId);
                ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo", hvm.PlotId);
                ViewBag.OwnershipSourceId = new SelectList(_ownershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName", hvm.OwnershipSourceId);
                ViewBag.BuildingTypeId = new SelectList(_buildingTypeManager.GetAllBuildingType(), "BuildingTypeId", "BuildingTypeName", hvm.BuildingTypeId);
                ViewBag.Gender = new SelectList(_commonListManager.GetAllGender(), "TypeId", "TypeName", hvm.Gender);
                ViewBag.MaritialStatus = new SelectList(_commonListManager.GetAllMaritalStatus(), "TypeId", "TypeName", hvm.MaritialStatus);
                ViewBag.OwnerType = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName", hvm.OwnerType);
                ViewBag.IsHolderAnOwner = new SelectList(StaticDataHelper.GetOwnerForDropdown(), "Value", "Text", hvm.IsHolderAnOwner);

                ViewBag.FlorNo = _commonListManager.GetAllFloor();
                ViewBag.OwnOrRent = _commonListManager.GetAllOwnOrRent();
                ViewBag.SelfOwned = _commonListManager.GetAllOwnType();


                // global declaration
                Holder holder = new Holder();
                var maxId = DateTime.Now.ToString("yyyyMMddHHmmssfff");//_holdingManager.GetMAXId();

                // validations

                if (hvm.AreaId > 0)
                {
                    holder.AreaId = hvm.AreaId;
                }
                else
                {
                    status = "এলাকার নাম পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.PlotId > 0)
                {
                    holder.PlotId = hvm.PlotId;
                }
                else
                {
                    status = "প্লট/বাড়ী/ফ্ল্যাট নম্বর পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.HolderName))
                {
                    holder.HolderName = hvm.HolderName;
                }
                else
                {
                    status = "প্লট/ফ্ল্যাট/বাড়ী মালিকের নাম পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.NID))
                {
                    holder.NID = hvm.NID;
                }
                else
                {
                    status = "জাতীয় পরিচয়পত্র পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.Gender > 0)
                {
                    holder.Gender = hvm.Gender;
                }
                else
                {
                    status = "লিঙ্গ পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.MaritialStatus > 0)
                {
                    holder.MaritialStatus = hvm.MaritialStatus;
                }
                else
                {
                    status = "বৈবাহিক অবস্থা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.Father))
                {
                    holder.Father = hvm.Father;
                }
                else
                {
                    holder.Father = null;
                    //status = "পিতার নাম পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.Mother))
                {
                    holder.Mother = hvm.Mother;
                }
                else
                {
                    holder.Mother = null;
                    //status = "মাতার নাম পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.Spouse = !string.IsNullOrWhiteSpace(hvm.Spouse) ? hvm.Spouse : null;
                holder.Contact1 = !string.IsNullOrWhiteSpace(hvm.Contact1) ? hvm.Contact1 : null;

                if (!string.IsNullOrWhiteSpace(hvm.Contact2))
                {
                    holder.Contact2 = hvm.Contact2;
                }
                else
                {
                    holder.Contact2 = null;
                    //status = "মোবাইল নম্বর পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.Email = !string.IsNullOrWhiteSpace(hvm.Email) ? hvm.Email : null;

                if (!string.IsNullOrWhiteSpace(hvm.PresentAdd))
                {
                    holder.PresentAdd = hvm.PresentAdd;
                }
                else
                {
                    holder.PresentAdd = null;
                    //status = "বর্তমান ঠিকানা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.PermanentAdd))
                {
                    holder.PermanentAdd = hvm.PermanentAdd;
                }
                else
                {
                    holder.PermanentAdd = null;
                    //status = "স্থায়ী ঠিকানা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.ContactAdd))
                {
                    holder.ContactAdd = hvm.ContactAdd;
                }
                else
                {
                    holder.ContactAdd = null;
                    //status = "পত্র যোগাযোগের ঠিকানা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.PreviousDueTax = hvm.PreviousDueTax != null && hvm.PreviousDueTax > 0 ? hvm.PreviousDueTax : 0;

                if (hvm.IsHolderAnOwner != null)
                {
                    holder.IsHolderAnOwner = hvm.IsHolderAnOwner;
                }
                else
                {
                    status = "গৃহকরদাতা নিজেই কি মালিক? পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }


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
                        if (file.ContentLength > 2 * 1024 * 1024)
                        {
                            status = "আপলোড করা ছবি এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ছবি আপলোড করুন";
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
                        holder.ImageLocation = null;
                        //status = "পাসপোর্ট সাইজের ছবি পাওয়া যাই নি";
                        //return new JsonResult { Data = new { status } };
                    }
                }
                else
                {
                    holder.ImageLocation = null;
                    //status = "পাসপোর্ট সাইজের ছবি পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
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
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

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
                            holder.Document1 = null;
                        }
                    }

                    if (holder.OwnerType == 3 && Session["DocFile2"] != null)
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
                            newFilename = "/Documents/Holders/FFDocuments/" + newFilename;

                            if (System.IO.File.Exists(newFilename))
                                System.IO.File.Delete(newFilename);
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            holder.Document2 = newFilename;
                        }
                        else
                        {
                            holder.Document2 = null;
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
                    holder.AmountOfLand = 0;
                    //status = "জমির পরিমাণ পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.TotalFloor != null && hvm.TotalFloor > 0)
                {
                    holder.TotalFloor = hvm.TotalFloor;
                }
                else
                {
                    holder.TotalFloor = 0;
                    //status = "মোট তলার সংখ্যা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.EachFloorArea != null && hvm.EachFloorArea > 0)
                {
                    holder.EachFloorArea = hvm.EachFloorArea;
                }
                else
                {
                    holder.EachFloorArea = 0;
                    //status = "প্রতিতলার আয়তন পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.TotalFlat != null && hvm.TotalFlat > 0)
                {
                    holder.TotalFlat = hvm.TotalFlat;
                }
                else
                {
                    holder.TotalFlat = 0;
                    //status = "মোট ফ্ল্যাট সংখ্যা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.HoldersFlatNumber != null && hvm.HoldersFlatNumber > 0)
                {
                    holder.HoldersFlatNumber = hvm.HoldersFlatNumber;
                }
                else
                {
                    holder.HoldersFlatNumber = 0;
                    //status = "নিজ মালিকানাধীন ফ্ল্যাট সংখ্যা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.AllocationLetterNo = !string.IsNullOrWhiteSpace(hvm.AllocationLetterNo) ? hvm.AllocationLetterNo : null;
                holder.AllocationDate = !string.IsNullOrWhiteSpace(hvm.StringAllocationDate)
                    ? (DateTime?)DateTime.ParseExact(hvm.StringAllocationDate, "dd/MM/yyyy", null)
                    : null;
                holder.NamjariLetterNo = !string.IsNullOrWhiteSpace(hvm.NamjariLetterNo) ? hvm.NamjariLetterNo : null;
                holder.NamjariDate = !string.IsNullOrWhiteSpace(hvm.StringNamjariDate)
                    ? (DateTime?)DateTime.ParseExact(hvm.StringNamjariDate, "dd/MM/yyyy", null)
                    : null;
                holder.RecordCorrectionDate = !string.IsNullOrWhiteSpace(hvm.StringRecordCorrectionDate)
                   ? (DateTime?)DateTime.ParseExact(hvm.StringRecordCorrectionDate, "dd/MM/yyyy", null)
                   : null;

                holder.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                holder.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                holder.CreateDate = DateTime.Now;
                holder.LastUpdated = DateTime.Now;
                holder.IsActive = true;
                holder.IsDeleted = false;

                bool isValid = true;
                bool isNullValid = true; //hvm.HolderFlatList.Count == 0 || hvm.HolderFlatList == null || !hvm.HolderFlatList.Any() ? false : true;
                if (isNullValid)
                {
                    if (hvm.HolderFlatList.Count() == 1)
                    {
                        foreach (HolderFlat item in hvm.HolderFlatList)
                        {
                            isValid = item.HolderFlatId == 0 && item.FlorNo == 0 && string.IsNullOrWhiteSpace(item.FlatNo) && item.OwnOrRent == 1 && item.MonthlyRent == 0
                                    && item.SelfOwn == 1 && string.IsNullOrWhiteSpace(item.OwnerName) && item.FlatArea == 0
                                ? false
                                : true;
                        }
                    }
                }

                if (!isValid)
                {
                    status = "কমপক্ষে একটি ফ্ল্যাট সঠিক  বিবরণসহ  সাবমিট করুন ";
                    return new JsonResult { Data = new { status } };
                }


                int holderId = _holdingManager.InsertHolder(holder);

                if (holderId > 0)
                {

                    if (isNullValid)
                    {
                        if (isValid)
                        {
                            var newListFroUi = hvm.HolderFlatList.OrderBy(f => f.FlorNo);
                            bool canInert = true;
                            foreach (HolderFlat ui_item in newListFroUi)
                            {
                                canInert = ui_item.HolderFlatId == 0 && ui_item.FlorNo == 0 && string.IsNullOrWhiteSpace(ui_item.FlatNo) && ui_item.OwnOrRent == 1 && ui_item.MonthlyRent == 0
                                && ui_item.SelfOwn == 1 && string.IsNullOrWhiteSpace(ui_item.OwnerName) && ui_item.FlatArea == 0
                                    ? false
                                    : true;
                                if (canInert)
                                {
                                    if (hvm.IsHolderAnOwner == true)
                                    {
                                        HolderFlat details = new HolderFlat()
                                        {
                                            CreateDate = DateTime.Now,
                                            CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                            FlatArea = ui_item.FlatArea != null && ui_item.FlatArea > 0 ? ui_item.FlatArea : null,
                                            FlatNo = !string.IsNullOrWhiteSpace(ui_item.FlatNo) ? ui_item.FlatNo.Trim() : null,
                                            FlorNo = ui_item.FlorNo,
                                            HolderFlatId = 0,
                                            HolderId = holderId,
                                            IsActive = true,
                                            IsDeleted = false,
                                            LastUpdated = DateTime.Now,
                                            LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                            IsSelfOwned = ui_item.SelfOwn == 1 ? true : false,
                                            MonthlyRent = ui_item.MonthlyRent != null && ui_item.MonthlyRent > 0 ? ui_item.MonthlyRent : null,
                                            OwnerName = ui_item.SelfOwn == 1 ? holder.HolderName : null,
                                            //holder.HolderName, //!string.IsNullOrWhiteSpace(ui_item.OwnerName) ? ui_item.OwnerName : null,
                                            OwnOrRent = ui_item.OwnOrRent,
                                            SelfOwn = ui_item.SelfOwn,
                                            IsCheckedByHolder = false,
                                            MainHolderId = ui_item.SelfOwn == 1 ? (int?)holderId : null,
                                        };

                                        string returnString = _holdingManager.HoldersFlatInsert(details);
                                        if (returnString != CommonConstantHelper.Success)
                                        {
                                            status = "Operation failed in child table";
                                            return new JsonResult { Data = new { status } };
                                        }
                                        else
                                        {
                                            status = "success";
                                        }
                                    }
                                    else
                                    {
                                        if (ui_item.IsCheckedByHolder == true)
                                        {
                                            HolderFlat details = new HolderFlat()
                                            {
                                                CreateDate = null,
                                                CreatedBy = null,
                                                FlatArea = null,
                                                FlatNo = null,
                                                FlorNo = null,
                                                HolderFlatId = ui_item.HolderFlatId,
                                                HolderId = null,
                                                IsActive = null,
                                                IsDeleted = null,
                                                LastUpdated = DateTime.Now,
                                                LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                                IsSelfOwned = null,
                                                MonthlyRent = ui_item.MonthlyRent != null && ui_item.MonthlyRent > 0 ? ui_item.MonthlyRent : null,
                                                OwnerName = ui_item.SelfOwn == 1 ? holder.HolderName : null,
                                                OwnOrRent = ui_item.OwnOrRent,
                                                SelfOwn = null,
                                                IsCheckedByHolder = true,
                                                MainHolderId = ui_item.SelfOwn == 1 ? (int?)holderId : null
                                            };

                                            string returnString = _holdingManager.HoldersFlatUpdateForMainHolder(details);
                                            if (returnString != CommonConstantHelper.Success)
                                            {
                                                status = "Operation failed in child table";
                                                return new JsonResult { Data = new { status } };
                                            }
                                            else
                                            {
                                                status = "success";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    status = "success";
                                }
                            }
                        }
                        else
                        {
                            status = "success";
                        }
                    }
                    else
                    {
                        status = "success";
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
        public ActionResult Edit(int id)
        {
            if (id <= 0)
                return HttpNotFound();

            Holder holder = _holdingManager.GetHolderById(id);
            if (holder == null)
                return HttpNotFound();

            List<HolderFlat> listOfHoldersFlat = _holdingManager.GetHoldersFlatByHolderId(id);
            List<HolderFlat> listOfHoldersFlatEdit = _holdingManager.GetAllFlatByPlotIdForEdit(holder.PlotId, id);
            //if (listOfHolders == null || listOfHolders.Count() <= 0)
            //    return HttpNotFound();

            HolderVM holderVMOtherData = _holdingManager.GetAllotmentNamjariDesignByPlotId(holder.PlotId);

            HolderVM hvm = new HolderVM()
            {
                AmountOfLand = holder.AmountOfLand,
                AreaName = holder.AreaName,
                BuildingTypeName = holder.BuildingTypeName,
                Contact1 = holder.Contact1,
                Contact2 = holder.Contact2,
                ContactAdd = holder.ContactAdd,
                CreatedByUsername = holder.CreatedByUsername,
                Document1 = holder.Document1,
                Document2 = holder.Document2,
                EachFloorArea = holder.EachFloorArea,
                Email = holder.Email,
                Father = holder.Father,
                GenderType = holder.GenderType,
                HolderFlatList = listOfHoldersFlat,
                HolderFlatListForEdit = listOfHoldersFlatEdit,
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
                TotalFlat = holder.TotalFlat,
                TotalFloor = holder.TotalFloor,
                UpdatedByUsername = holder.UpdatedByUsername,
                RoadName = holder.RoadName,
                RoadNo = holder.RoadNo,
                AreaPlotFlatData = holder.AreaPlotFlatData,
                NamjariLetterNo = holder.NamjariLetterNo,
                AllocationLetterNo = holder.AllocationLetterNo,

                // id values
                AreaId = holder.AreaId,
                BuildingTypeId = holder.BuildingTypeId,
                Gender = holder.Gender,
                MaritialStatus = holder.MaritialStatus,
                OwnershipSourceId = holder.OwnershipSourceId,
                OwnerType = holder.OwnerType,
                PlotId = holder.PlotId,
                StringLastUpdated = holder.StringLastUpdated,

                // string date
                StringAllocationDate = holder.StringAllocationDate,
                StringNamjariDate = holder.StringNamjariDate,
                StringRecordCorrectionDate = holder.StringRecordCorrectionDate,
                StringCreateDate = holder.StringCreateDate,


                FirstApprovalLetterNo = holderVMOtherData.FirstApprovalLetterNo,
                StringFirstApprovalDate = holderVMOtherData.StringFirstApprovalDate,
                LastApprovalLetterNo = holderVMOtherData.LastApprovalLetterNo,
                StringLastApprovalDate = holderVMOtherData.StringLastApprovalDate,
                LeasePeriod = holderVMOtherData.LeasePeriod,
                StringLeaseExpiryDate = holderVMOtherData.StringLeaseExpiryDate,
                PlotOwnerName = holderVMOtherData.PlotOwnerName,
                IsHolderAnOwner = holder.IsHolderAnOwner
            };

            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", hvm.AreaId);
            ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo", hvm.PlotId);
            ViewBag.OwnershipSourceId = new SelectList(_ownershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName", hvm.OwnershipSourceId);
            ViewBag.BuildingTypeId = new SelectList(_buildingTypeManager.GetAllBuildingType(), "BuildingTypeId", "BuildingTypeName", hvm.BuildingTypeId);
            ViewBag.Gender = new SelectList(_commonListManager.GetAllGender(), "TypeId", "TypeName", hvm.Gender);
            ViewBag.MaritialStatus = new SelectList(_commonListManager.GetAllMaritalStatus(), "TypeId", "TypeName", hvm.MaritialStatus);
            ViewBag.OwnerType = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName", hvm.OwnerType);
            ViewBag.IsHolderAnOwner = new SelectList(StaticDataHelper.GetOwnerForDropdown(), "Value", "Text", hvm.IsHolderAnOwner);
            ViewBag.FlorNo = _commonListManager.GetAllFloor();
            ViewBag.OwnOrRent = _commonListManager.GetAllOwnOrRent();
            ViewBag.SelfOwned = _commonListManager.GetAllOwnType();


            return View(hvm);
        }

        [HttpPost]
        public JsonResult UpdateData(HolderVM hvm, string oldImg, string oldDoc1, string oldDoc2)
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

                if (hvm.HolderId <= 0)
                {
                    status = "নিরাপত্তা ভঙ্গ";
                    return new JsonResult { Data = new { status } };
                }


                if (hvm.HolderFlatList.Count == 0 || hvm.HolderFlatList == null || !hvm.HolderFlatList.Any())
                {
                    status = "কমপক্ষে একটি ফ্ল্যাটের বিবরণ সাবমিট করুন";
                    return new JsonResult { Data = new { status } };
                }

                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", hvm.AreaId);
                ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo", hvm.PlotId);
                ViewBag.OwnershipSourceId = new SelectList(_ownershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName", hvm.OwnershipSourceId);
                ViewBag.BuildingTypeId = new SelectList(_buildingTypeManager.GetAllBuildingType(), "BuildingTypeId", "BuildingTypeName", hvm.BuildingTypeId);
                ViewBag.Gender = new SelectList(_commonListManager.GetAllGender(), "TypeId", "TypeName", hvm.Gender);
                ViewBag.MaritialStatus = new SelectList(_commonListManager.GetAllMaritalStatus(), "TypeId", "TypeName", hvm.MaritialStatus);
                ViewBag.OwnerType = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName", hvm.OwnerType);
                ViewBag.IsHolderAnOwner = new SelectList(StaticDataHelper.GetOwnerForDropdown(), "Value", "Text", hvm.IsHolderAnOwner);
                ViewBag.FlorNo = _commonListManager.GetAllFloor();
                ViewBag.OwnOrRent = _commonListManager.GetAllOwnOrRent();
                ViewBag.SelfOwned = _commonListManager.GetAllOwnType();


                // global declaration
                Holder holder = new Holder();
                var maxId = DateTime.Now.ToString("yyyyMMddHHmmssfff");//_holdingManager.GetMAXId();


                holder.HolderId = hvm.HolderId;

                // validations

                if (hvm.AreaId > 0)
                {
                    holder.AreaId = hvm.AreaId;
                }
                else
                {
                    status = "এলাকার নাম পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.PlotId > 0)
                {
                    holder.PlotId = hvm.PlotId;
                }
                else
                {
                    status = "প্লট/বাড়ী/ফ্ল্যাট নম্বর পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.HolderName))
                {
                    holder.HolderName = hvm.HolderName;
                }
                else
                {
                    status = "প্লট/ফ্ল্যাট/বাড়ী মালিকের নাম পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.NID))
                {
                    holder.NID = hvm.NID;
                }
                else
                {
                    status = "জাতীয় পরিচয়পত্র পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.Gender > 0)
                {
                    holder.Gender = hvm.Gender;
                }
                else
                {
                    status = "লিঙ্গ পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.MaritialStatus > 0)
                {
                    holder.MaritialStatus = hvm.MaritialStatus;
                }
                else
                {
                    status = "বৈবাহিক অবস্থা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.Father))
                {
                    holder.Father = hvm.Father;
                }
                else
                {
                    holder.Father = null;
                    //status = "পিতার নাম পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.Mother))
                {
                    holder.Mother = hvm.Mother;
                }
                else
                {
                    holder.Mother = null;
                    //status = "মাতার নাম পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.Spouse = !string.IsNullOrWhiteSpace(hvm.Spouse) ? hvm.Spouse : null;
                holder.Contact1 = !string.IsNullOrWhiteSpace(hvm.Contact1) ? hvm.Contact1 : null;

                if (!string.IsNullOrWhiteSpace(hvm.Contact2))
                {
                    holder.Contact2 = hvm.Contact2;
                }
                else
                {
                    holder.Contact2 = null;
                    //status = "মোবাইল নম্বর পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.Email = !string.IsNullOrWhiteSpace(hvm.Email) ? hvm.Email : null;

                if (!string.IsNullOrWhiteSpace(hvm.PresentAdd))
                {
                    holder.PresentAdd = hvm.PresentAdd;
                }
                else
                {
                    holder.PresentAdd = null;
                    //status = "বর্তমান ঠিকানা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.PermanentAdd))
                {
                    holder.PermanentAdd = hvm.PermanentAdd;
                }
                else
                {
                    holder.PermanentAdd = null;
                    //status = "স্থায়ী ঠিকানা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.ContactAdd))
                {
                    holder.ContactAdd = hvm.ContactAdd;
                }
                else
                {
                    holder.ContactAdd = null;
                    //status = "পত্র যোগাযোগের ঠিকানা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.PreviousDueTax = hvm.PreviousDueTax != null && hvm.PreviousDueTax > 0 ? hvm.PreviousDueTax : 0;

                if (hvm.IsHolderAnOwner != null)
                {
                    holder.IsHolderAnOwner = hvm.IsHolderAnOwner;
                }
                else
                {
                    status = "গৃহকরদাতা নিজেই কি মালিক? পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

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
                        if (file.ContentLength > 2 * 1024 * 1024)
                        {
                            status = "আপলোড করা ছবি এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ছবি আপলোড করুন";
                            return new JsonResult { Data = new { status } };
                        }

                        var extension = Path.GetExtension(file.FileName);
                        var fileOldName = Path.GetFileNameWithoutExtension(file.FileName);
                        fileOldName = fileOldName.Replace(" ", string.Empty);
                        var newFilename = maxId + "_" + fileOldName + extension;
                        newFilename = "/Documents/Holders/Images/" + newFilename;

                        if (!string.IsNullOrWhiteSpace(oldImg))
                        {
                            var deletePathFile = Path.Combine(Server.MapPath(oldImg));
                            if (System.IO.File.Exists(deletePathFile))
                                System.IO.File.Delete(deletePathFile);
                        }

                        file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                        holder.ImageLocation = newFilename;
                    }
                    else
                    {
                        holder.ImageLocation = null;
                        //status = "পাসপোর্ট সাইজের ছবি পাওয়া যাই নি";
                        //return new JsonResult { Data = new { status } };
                    }
                }
                else
                {
                    holder.ImageLocation = null;
                    //status = "পাসপোর্ট সাইজের ছবি পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
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
                    //if (holder.OwnerType == 3 && Session["DocFile1"] == null && Session["DocFile2"] == null)
                    //{
                    //    status = "খেতাব প্রাপ্ত মুক্তিযোদ্ধার ক্ষেত্রে  প্রমাণাদি সংযুক্ত করুন (একটি বা দুইটি)";
                    //    return new JsonResult { Data = new { status } };
                    //}
                    if (holder.OwnerType == 3 && Session["DocFile1"] != null)
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
                            newFilename = "/Documents/Holders/FFDocuments/" + newFilename;

                            if (!string.IsNullOrWhiteSpace(oldDoc1))
                            {
                                var deletePathFile = Path.Combine(Server.MapPath(oldDoc1));
                                if (System.IO.File.Exists(deletePathFile))
                                    System.IO.File.Delete(deletePathFile);
                            }

                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            holder.Document1 = newFilename;
                        }
                        else
                        {
                            holder.Document1 = null;
                        }
                    }

                    if (holder.OwnerType == 3 && Session["DocFile2"] != null)
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
                            newFilename = "/Documents/Holders/FFDocuments/" + newFilename;

                            if (!string.IsNullOrWhiteSpace(oldDoc2))
                            {
                                var deletePathFile = Path.Combine(Server.MapPath(oldDoc2));
                                if (System.IO.File.Exists(deletePathFile))
                                    System.IO.File.Delete(deletePathFile);
                            }

                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            holder.Document2 = newFilename;
                        }
                        else
                        {
                            holder.Document2 = null;
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
                    holder.AmountOfLand = 0;
                    //status = "জমির পরিমাণ পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.TotalFloor != null && hvm.TotalFloor > 0)
                {
                    holder.TotalFloor = hvm.TotalFloor;
                }
                else
                {
                    holder.TotalFloor = 0;
                    //status = "মোট তলার সংখ্যা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.EachFloorArea != null && hvm.EachFloorArea > 0)
                {
                    holder.EachFloorArea = hvm.EachFloorArea;
                }
                else
                {
                    holder.EachFloorArea = 0;
                    //status = "প্রতিতলার আয়তন পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.TotalFlat != null && hvm.TotalFlat > 0)
                {
                    holder.TotalFlat = hvm.TotalFlat;
                }
                else
                {
                    holder.TotalFlat = 0;
                    //status = "মোট ফ্ল্যাট সংখ্যা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.HoldersFlatNumber != null && hvm.HoldersFlatNumber > 0)
                {
                    holder.HoldersFlatNumber = hvm.HoldersFlatNumber;
                }
                else
                {
                    holder.HoldersFlatNumber = 0;
                    //status = "নিজ মালিকানাধীন ফ্ল্যাট সংখ্যা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.AllocationLetterNo = !string.IsNullOrWhiteSpace(hvm.AllocationLetterNo) ? hvm.AllocationLetterNo : null;
                holder.AllocationDate = !string.IsNullOrWhiteSpace(hvm.StringAllocationDate)
                    ? (DateTime?)DateTime.ParseExact(hvm.StringAllocationDate, "dd/MM/yyyy", null)
                    : null;
                holder.NamjariLetterNo = !string.IsNullOrWhiteSpace(hvm.NamjariLetterNo) ? hvm.NamjariLetterNo : null;
                holder.NamjariDate = !string.IsNullOrWhiteSpace(hvm.StringNamjariDate)
                    ? (DateTime?)DateTime.ParseExact(hvm.StringNamjariDate, "dd/MM/yyyy", null)
                    : null;
                holder.RecordCorrectionDate = !string.IsNullOrWhiteSpace(hvm.StringRecordCorrectionDate)
                   ? (DateTime?)DateTime.ParseExact(hvm.StringRecordCorrectionDate, "dd/MM/yyyy", null)
                   : null;

                holder.CreatedBy = null;
                holder.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                holder.CreateDate = null;
                holder.LastUpdated = DateTime.Now;
                holder.IsActive = true;
                holder.IsDeleted = null;

                bool isValid = true;
                bool isNullValid = true;//hvm.HolderFlatList.Count == 0 || hvm.HolderFlatList == null || !hvm.HolderFlatList.Any() ? false : true;
                if (isNullValid)
                {
                    if (hvm.HolderFlatList.Count() == 1)
                    {
                        foreach (HolderFlat item in hvm.HolderFlatList)
                        {
                            isValid = item.HolderFlatId == 0 && item.FlorNo == 0 && string.IsNullOrWhiteSpace(item.FlatNo) && item.OwnOrRent == 1 && item.MonthlyRent == 0
                                    && item.SelfOwn == 1 && string.IsNullOrWhiteSpace(item.OwnerName) && item.FlatArea == 0
                                ? false
                                : true;
                        }
                    }
                }


                if (!isValid)
                {
                    status = "কমপক্ষে একটি ফ্ল্যাট সঠিক  বিবরণসহ  সাবমিট করুন ";
                    return new JsonResult { Data = new { status } };
                }


                int holderId = _holdingManager.UpdateHolder(holder);

                if (holderId > 0)
                {
                    if (isNullValid)
                    {
                        if (isValid)
                        {
                            int count = 1;//_holdingManager.DeleteHoldersFlatDataByHolderId(holderId);
                            if (count > 0)
                            {
                                var newListFroUi = hvm.HolderFlatList.OrderBy(f => f.FlorNo);
                                bool canInert = true;
                                foreach (HolderFlat ui_item in newListFroUi)
                                {
                                    canInert = ui_item.HolderFlatId == 0 && ui_item.FlorNo == 0 && string.IsNullOrWhiteSpace(ui_item.FlatNo) && ui_item.OwnOrRent == 1 && ui_item.MonthlyRent == 0
                                    && ui_item.SelfOwn == 1 && string.IsNullOrWhiteSpace(ui_item.OwnerName) && ui_item.FlatArea == 0
                                        ? false
                                        : true;

                                    if (canInert)
                                    {

                                        if (hvm.IsHolderAnOwner == true)
                                        {
                                            HolderFlat details = new HolderFlat()
                                            {
                                                CreateDate = null,
                                                CreatedBy = null,
                                                FlatArea = ui_item.FlatArea != null && ui_item.FlatArea > 0 ? ui_item.FlatArea : null,
                                                FlatNo = !string.IsNullOrWhiteSpace(ui_item.FlatNo) ? ui_item.FlatNo : null,
                                                FlorNo = ui_item.FlorNo,
                                                HolderFlatId = ui_item.HolderFlatId,
                                                HolderId = holderId,
                                                IsActive = null,
                                                IsDeleted = null,
                                                LastUpdated = DateTime.Now,
                                                LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                                IsSelfOwned = ui_item.SelfOwn == 1 ? true : false,
                                                MonthlyRent = ui_item.MonthlyRent != null && ui_item.MonthlyRent > 0 ? ui_item.MonthlyRent : null,
                                                OwnerName = ui_item.SelfOwn == 1 ? holder.HolderName : null,
                                                OwnOrRent = ui_item.OwnOrRent,
                                                SelfOwn = ui_item.SelfOwn,
                                                IsCheckedByHolder = null,
                                                MainHolderId = ui_item.SelfOwn == 1 ? (int?)holderId : null,
                                            };

                                            string returnString = CommonConstantHelper.Success;
                                            if (details.HolderFlatId > 0)
                                            {
                                                returnString = _holdingManager.HoldersFlatUpdate(details);
                                            }
                                            else
                                            {
                                                details.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                                                details.CreateDate = DateTime.Now;
                                                details.IsDeleted = false;
                                                details.IsActive = true;
                                                details.IsCheckedByHolder = false;
                                                returnString = _holdingManager.HoldersFlatInsert(details);
                                            }

                                            if (returnString != CommonConstantHelper.Success)
                                            {
                                                status = "Operation failed in child table";
                                                return new JsonResult { Data = new { status } };
                                            }
                                            else
                                            {
                                                status = "success";
                                            }
                                        }
                                        else
                                        {
                                            if (ui_item.IsCheckedByHolder == true)
                                            {
                                                HolderFlat details = new HolderFlat()
                                                {
                                                    CreateDate = null,
                                                    CreatedBy = null,
                                                    FlatArea = null,
                                                    FlatNo = null,
                                                    FlorNo = null,
                                                    HolderFlatId = ui_item.HolderFlatId,
                                                    HolderId = null,
                                                    IsActive = null,
                                                    IsDeleted = null,
                                                    LastUpdated = DateTime.Now,
                                                    LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                                    IsSelfOwned = null,
                                                    MonthlyRent = ui_item.MonthlyRent != null && ui_item.MonthlyRent > 0 ? ui_item.MonthlyRent : null,
                                                    OwnerName = ui_item.SelfOwn == 1 ? holder.HolderName : null,
                                                    OwnOrRent = ui_item.OwnOrRent,
                                                    SelfOwn = null,
                                                    IsCheckedByHolder = true,
                                                    MainHolderId = ui_item.SelfOwn == 1 ? (int?)holderId : null
                                                };

                                                string returnString = _holdingManager.HoldersFlatUpdateForMainHolder(details);
                                                if (returnString != CommonConstantHelper.Success)
                                                {
                                                    status = "Operation failed in child table";
                                                    return new JsonResult { Data = new { status } };
                                                }
                                                else
                                                {
                                                    status = "success";
                                                }
                                            }
                                            else
                                            {
                                                HolderFlat details = new HolderFlat()
                                                {
                                                    CreateDate = null,
                                                    CreatedBy = null,
                                                    FlatArea = null,
                                                    FlatNo = null,
                                                    FlorNo = null,
                                                    HolderFlatId = ui_item.HolderFlatId,
                                                    HolderId = null,
                                                    IsActive = null,
                                                    IsDeleted = null,
                                                    LastUpdated = null,
                                                    LastUpdatedBy = null,
                                                    IsSelfOwned = false,
                                                    MonthlyRent = 0,
                                                    OwnerName = null,
                                                    OwnOrRent = 1,
                                                    SelfOwn = 2,
                                                    IsCheckedByHolder = false,
                                                    MainHolderId = null
                                                };

                                                string returnString = _holdingManager.HoldersFlatUpdateForMainHolder(details);
                                                if (returnString != CommonConstantHelper.Success)
                                                {
                                                    status = "Operation failed in child table";
                                                    return new JsonResult { Data = new { status } };
                                                }
                                                else
                                                {
                                                    status = "success";
                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        status = "success";
                                    }
                                }
                            }
                            else
                            {
                                status = "Deletion failed from child table";
                                return new JsonResult { Data = new { status } };
                            }
                        }
                        else
                        {
                            status = "success";
                        }
                    }
                    else
                    {
                        status = "success";
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
        public ActionResult FlatTransfer()
        {
            ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName");
            ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo");
            ViewBag.OwnershipSourceId = new SelectList(_ownershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName");
            ViewBag.BuildingTypeId = new SelectList(_buildingTypeManager.GetAllBuildingType(), "BuildingTypeId", "BuildingTypeName");
            ViewBag.Gender = new SelectList(_commonListManager.GetAllGender(), "TypeId", "TypeName");
            ViewBag.MaritialStatus = new SelectList(_commonListManager.GetAllMaritalStatus(), "TypeId", "TypeName");
            ViewBag.OwnerType = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName");
            ViewBag.FlorNo = _commonListManager.GetAllFloor();
            ViewBag.OwnOrRent = _commonListManager.GetAllOwnOrRent();
            ViewBag.SelfOwned = _commonListManager.GetAllOwnType();
            return View();
        }

        [HttpPost]
        public JsonResult FlatTransferAddData(FlatTransferVM hvm)
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

                if (hvm.HolderFlatList.Count == 0 || hvm.HolderFlatList == null || !hvm.HolderFlatList.Any())
                {
                    status = "কমপক্ষে একটি ফ্ল্যাটের বিবরণ সাবমিট করুন";
                    return new JsonResult { Data = new { status } };
                }


                ViewBag.AreaId = new SelectList(_dOHSAreaManager.GetAllDOHSArea(), "AreaId", "AreaName", hvm.AreaId);
                ViewBag.PlotId = new SelectList(_plotManager.GetAllPlot(), "PlotId", "PlotNo", hvm.PlotId);
                ViewBag.OwnershipSourceId = new SelectList(_ownershipSourceManager.GetAllOwnershipSource(), "OwnershipSourceId", "SourceName", hvm.OwnershipSourceId);
                ViewBag.BuildingTypeId = new SelectList(_buildingTypeManager.GetAllBuildingType(), "BuildingTypeId", "BuildingTypeName", hvm.BuildingTypeId);
                ViewBag.Gender = new SelectList(_commonListManager.GetAllGender(), "TypeId", "TypeName", hvm.Gender);
                ViewBag.MaritialStatus = new SelectList(_commonListManager.GetAllMaritalStatus(), "TypeId", "TypeName", hvm.MaritialStatus);
                ViewBag.OwnerType = new SelectList(_commonListManager.GetAllOwnerShipType(), "TypeId", "TypeName", hvm.OwnerType);

                ViewBag.FlorNo = _commonListManager.GetAllFloor();
                ViewBag.OwnOrRent = _commonListManager.GetAllOwnOrRent();
                ViewBag.SelfOwned = _commonListManager.GetAllOwnType();


                // global declaration
                Holder holder = new Holder();
                var maxId = DateTime.Now.ToString("yyyyMMddHHmmssfff");//_holdingManager.GetMAXId();

                // validations

                if (hvm.AreaId > 0)
                {
                    holder.AreaId = hvm.AreaId;
                }
                else
                {
                    status = "এলাকার নাম পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.PlotId > 0)
                {
                    holder.PlotId = hvm.PlotId;
                }
                else
                {
                    status = "প্লট নম্বর পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (string.IsNullOrWhiteSpace(hvm.TransferRefNo))
                {
                    status = "ফ্ল্যাট হস্তান্তরের রেফারেন্স পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (string.IsNullOrWhiteSpace(hvm.StrTransferRefDate))
                {
                    status = "ফ্ল্যাট হস্তান্তরের তারিখ পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }
                else
                {
                    hvm.TransferRefDate = (DateTime?)DateTime.ParseExact(hvm.StrTransferRefDate, "dd/MM/yyyy", null);
                }


                if (!string.IsNullOrWhiteSpace(hvm.HolderName))
                {
                    holder.HolderName = hvm.HolderName;
                }
                else
                {
                    status = "ফ্ল্যাট/বাড়ী মালিকের নাম পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.NID))
                {
                    holder.NID = hvm.NID;
                }
                else
                {
                    status = "জাতীয় পরিচয়পত্র পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.Gender > 0)
                {
                    holder.Gender = hvm.Gender;
                }
                else
                {
                    status = "লিঙ্গ পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (hvm.MaritialStatus > 0)
                {
                    holder.MaritialStatus = hvm.MaritialStatus;
                }
                else
                {
                    status = "বৈবাহিক অবস্থা পাওয়া যাই নি";
                    return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.Father))
                {
                    holder.Father = hvm.Father;
                }
                else
                {
                    holder.Father = null;
                    //status = "পিতার নাম পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.Mother))
                {
                    holder.Mother = hvm.Mother;
                }
                else
                {
                    holder.Mother = null;
                    //status = "মাতার নাম পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.Spouse = !string.IsNullOrWhiteSpace(hvm.Spouse) ? hvm.Spouse : null;
                holder.Contact1 = !string.IsNullOrWhiteSpace(hvm.Contact1) ? hvm.Contact1 : null;

                if (!string.IsNullOrWhiteSpace(hvm.Contact2))
                {
                    holder.Contact2 = hvm.Contact2;
                }
                else
                {
                    holder.Contact2 = null;
                    //status = "মোবাইল নম্বর পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.Email = !string.IsNullOrWhiteSpace(hvm.Email) ? hvm.Email : null;

                if (!string.IsNullOrWhiteSpace(hvm.PresentAdd))
                {
                    holder.PresentAdd = hvm.PresentAdd;
                }
                else
                {
                    holder.PresentAdd = null;
                    //status = "বর্তমান ঠিকানা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.PermanentAdd))
                {
                    holder.PermanentAdd = hvm.PermanentAdd;
                }
                else
                {
                    holder.PermanentAdd = null;
                    //status = "স্থায়ী ঠিকানা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (!string.IsNullOrWhiteSpace(hvm.ContactAdd))
                {
                    holder.ContactAdd = hvm.ContactAdd;
                }
                else
                {
                    holder.ContactAdd = null;
                    //status = "পত্র যোগাযোগের ঠিকানা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.PreviousDueTax = hvm.PreviousDueTax != null && hvm.PreviousDueTax > 0 ? hvm.PreviousDueTax : 0;

                holder.IsHolderAnOwner = false;

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
                        if (file.ContentLength > 2 * 1024 * 1024)
                        {
                            status = "আপলোড করা ছবি এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ছবি আপলোড করুন";
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
                        holder.ImageLocation = null;
                        //status = "পাসপোর্ট সাইজের ছবি পাওয়া যাই নি";
                        //return new JsonResult { Data = new { status } };
                    }
                }
                else
                {
                    holder.ImageLocation = null;
                    //status = "পাসপোর্ট সাইজের ছবি পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
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
                            if (file.ContentLength > 2 * 1024 * 1024)
                            {
                                status = "আপলোড করা ডকুমেন্ট এর সাইজ ২ এমবি এর বেশি। সাইজ ২ এমবি এর সমান বা ২ এমবি চেয়ে ছোট ডকুমেন্ট আপলোড করুন";
                                return new JsonResult { Data = new { status } };
                            }

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
                            holder.Document1 = null;
                        }
                    }

                    if (holder.OwnerType == 3 && Session["DocFile2"] != null)
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
                            newFilename = "/Documents/Holders/FFDocuments/" + newFilename;

                            if (System.IO.File.Exists(newFilename))
                                System.IO.File.Delete(newFilename);
                            file.SaveAs(Path.Combine(Server.MapPath(newFilename)));

                            holder.Document2 = newFilename;
                        }
                        else
                        {
                            holder.Document2 = null;
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
                    holder.AmountOfLand = 0;
                    //status = "জমির পরিমাণ পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.TotalFloor != null && hvm.TotalFloor > 0)
                {
                    holder.TotalFloor = hvm.TotalFloor;
                }
                else
                {
                    holder.TotalFloor = 0;
                    //status = "মোট তলার সংখ্যা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.EachFloorArea != null && hvm.EachFloorArea > 0)
                {
                    holder.EachFloorArea = hvm.EachFloorArea;
                }
                else
                {
                    holder.EachFloorArea = 0;
                    //status = "প্রতিতলার আয়তন পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.TotalFlat != null && hvm.TotalFlat > 0)
                {
                    holder.TotalFlat = hvm.TotalFlat;
                }
                else
                {
                    holder.TotalFlat = 0;
                    //status = "মোট ফ্ল্যাট সংখ্যা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                if (hvm.HoldersFlatNumber != null && hvm.HoldersFlatNumber > 0)
                {
                    holder.HoldersFlatNumber = hvm.HoldersFlatNumber;
                }
                else
                {
                    holder.HoldersFlatNumber = 0;
                    //status = "নিজ মালিকানাধীন ফ্ল্যাট সংখ্যা পাওয়া যাই নি";
                    //return new JsonResult { Data = new { status } };
                }

                holder.AllocationLetterNo = !string.IsNullOrWhiteSpace(hvm.AllocationLetterNo) ? hvm.AllocationLetterNo : null;
                holder.AllocationDate = !string.IsNullOrWhiteSpace(hvm.StringAllocationDate)
                    ? (DateTime?)DateTime.ParseExact(hvm.StringAllocationDate, "dd/MM/yyyy", null)
                    : null;
                holder.NamjariLetterNo = !string.IsNullOrWhiteSpace(hvm.NamjariLetterNo) ? hvm.NamjariLetterNo : null;
                holder.NamjariDate = !string.IsNullOrWhiteSpace(hvm.StringNamjariDate)
                    ? (DateTime?)DateTime.ParseExact(hvm.StringNamjariDate, "dd/MM/yyyy", null)
                    : null;
                holder.RecordCorrectionDate = !string.IsNullOrWhiteSpace(hvm.StringRecordCorrectionDate)
                   ? (DateTime?)DateTime.ParseExact(hvm.StringRecordCorrectionDate, "dd/MM/yyyy", null)
                   : null;

                holder.CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                holder.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                holder.CreateDate = DateTime.Now;
                holder.LastUpdated = DateTime.Now;
                holder.IsActive = true;
                holder.IsDeleted = false;

                bool isValid = true;
                bool isNullValid = true; //hvm.HolderFlatList.Count == 0 || hvm.HolderFlatList == null || !hvm.HolderFlatList.Any() ? false : true;
                if (isNullValid)
                {
                    if (hvm.HolderFlatList.Count() == 1)
                    {
                        foreach (HolderFlat item in hvm.HolderFlatList)
                        {
                            isValid = item.HolderFlatId == 0 && item.FlorNo == 0 && string.IsNullOrWhiteSpace(item.FlatNo) && item.OwnOrRent == 1 && item.MonthlyRent == 0
                                    && item.SelfOwn == 1 && string.IsNullOrWhiteSpace(item.OwnerName) && item.FlatArea == 0
                                ? false
                                : true;
                        }
                    }
                }

                if (!isValid)
                {
                    status = "কমপক্ষে একটি ফ্ল্যাট সঠিক  বিবরণসহ  সাবমিট করুন ";
                    return new JsonResult { Data = new { status } };
                }


                int holderId = _holdingManager.InsertHolder(holder);

                if (holderId > 0)
                {

                    if (isNullValid)
                    {
                        if (isValid)
                        {
                            var newListFroUi = hvm.HolderFlatList.OrderBy(f => f.FlorNo);
                            bool canInert = true;
                            foreach (HolderFlat ui_item in newListFroUi)
                            {
                                canInert = ui_item.HolderFlatId == 0 && ui_item.FlorNo == 0 && string.IsNullOrWhiteSpace(ui_item.FlatNo) && ui_item.OwnOrRent == 1 && ui_item.MonthlyRent == 0
                                && ui_item.SelfOwn == 1 && string.IsNullOrWhiteSpace(ui_item.OwnerName) && ui_item.FlatArea == 0
                                    ? false
                                    : true;
                                if (canInert)
                                {
                                    if (ui_item.IsCheckedByHolder == true)
                                    {
                                        // step 1 insert in falt table 
                                        HolderFlat details = new HolderFlat()
                                        {
                                            CreateDate = DateTime.Now,
                                            CreatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                            FlatArea = ui_item.FlatArea != null && ui_item.FlatArea > 0 ? ui_item.FlatArea : null,
                                            FlatNo = !string.IsNullOrWhiteSpace(ui_item.FlatNo) ? ui_item.FlatNo.Trim() : null,
                                            FlorNo = ui_item.FlorNo,
                                            HolderFlatId = 0,
                                            HolderId = holderId,
                                            IsActive = true,
                                            IsDeleted = false,
                                            LastUpdated = DateTime.Now,
                                            LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]),
                                            IsSelfOwned = ui_item.SelfOwn == 1 ? true : false,
                                            MonthlyRent = ui_item.MonthlyRent != null && ui_item.MonthlyRent > 0 ? ui_item.MonthlyRent : null,
                                            OwnerName = ui_item.SelfOwn == 1 ? holder.HolderName : null,
                                            OwnOrRent = ui_item.OwnOrRent,
                                            SelfOwn = ui_item.SelfOwn,
                                            IsCheckedByHolder = true,
                                            MainHolderId = ui_item.SelfOwn == 1 ? (int?)holderId : null,
                                        };
                                        int transferCountId = _holdingManager.HoldersFlatTransfer(details);

                                        // step 2 incavtive old falt data
                                        var incativeDetails = new HolderFlat();
                                        if (transferCountId > 0)
                                        {
                                            incativeDetails.HolderFlatId = ui_item.HolderFlatId;
                                            incativeDetails.LastUpdated = DateTime.Now;
                                            incativeDetails.LastUpdatedBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId]);
                                            incativeDetails.IsActive = false;
                                            incativeDetails.IsDeleted = true;
                                        }
                                        else
                                        {
                                            status = "Operation failed in child table";
                                            return new JsonResult { Data = new { status } };
                                        }
                                        string incativeReturn = _holdingManager.HoldersFlatInActive(incativeDetails);
                                        if (incativeReturn != CommonConstantHelper.Success)
                                        {
                                            status = "Operation failed in child table";
                                            return new JsonResult { Data = new { status } };
                                        }

                                        // step 3 inserting history table 
                                        HolderFlatHistory flatHistory = new HolderFlatHistory()
                                        {
                                            AreaId = hvm.AreaId,
                                            PlotId = hvm.PlotId,
                                            OldHolderFlatId = ui_item.HolderFlatId,
                                            NewHolderFlatId = transferCountId,
                                            OldHolderId = hvm.HolderId, //_holdingManager.GetHoldersFlatByHolderFlatId(ui_item.HolderFlatId).MainHolderId,
                                            NewHolderId = holderId,
                                            ReferenceNo = hvm.TransferRefNo,
                                            ReferenceDate = hvm.TransferRefDate,
                                            TransactionDate = DateTime.Now,
                                            TransactionBy = Convert.ToInt32(Session[CommonConstantHelper.LogInCredentialId])
                                        };

                                        string insertResult = _flatHistoryManager.InsertHolderFlatHistory(flatHistory);

                                        if (insertResult != CommonConstantHelper.Success)
                                        {
                                            status = "Operation failed in child table";
                                            return new JsonResult { Data = new { status } };
                                        }

                                        status = "success";
                                    }
                                }
                            }
                        }
                        else
                        {
                            status = "success";
                        }
                    }
                    else
                    {
                        status = "success";
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


        public JsonResult GetHolderByAreaIdAndPlotId(int AreaId, int PlotId)
        {
            var data = _holdingManager.GetHolderByAreaIdAndPlotId(AreaId, PlotId);
            return new JsonResult { Data = data ?? null };
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

        public JsonResult GetAllFlatByPlotId(int PlotId)
        {
            var data = _holdingManager.GetAllFlatByPlotId(PlotId);
            return new JsonResult { Data = data ?? null };
        }

        public JsonResult GetAllPlotByAreaId(int AreaId)
        {
            var data = _plotManager.GetPlotByAreaId(AreaId);

            return new JsonResult { Data = data ?? null };
        }

        public JsonResult GetAllotmentNamjariDesignByPlotId(int PlotId)
        {
            return new JsonResult { Data = _holdingManager.GetAllotmentNamjariDesignByPlotId(PlotId) };
        }

        public JsonResult GetRatePerSquareFeet(int AreaId, int BuildingTypeId)
        {
            return new JsonResult { Data = _holdingManager.GetPerSqrFeetPrice(AreaId, BuildingTypeId) };
        }

        public JsonResult GetPlotDetails(int PlotId)
        {
            return new JsonResult { Data = _plotManager.GetPlotById(PlotId) };
        }

        public JsonResult GetAllFlatByAreaAndPlotId(int AreaId, int PlotId)
        {
            var data = _holdingManager.GetAllFlatByAreaAndPlotId(AreaId, PlotId);
            return new JsonResult { Data = data ?? null };
        }

        public JsonResult GetAllFlatByHolderId(int HolderId)
        {
            var data = _holdingManager.GetAllFlatByHolderId(HolderId);
            return new JsonResult { Data = data ?? null };
        }

        public JsonResult GetHolderDetailsByHolderId(int HolderId)
        {
            var data = _holdingManager.GetHolderById(HolderId);
            return new JsonResult { Data = data ?? null };
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


        #region HolderReport
        public ActionResult HolderReport()
        {
            ViewBag.HolderId = new SelectList(_holdingManager.GetAllHolder(), "HolderId", "HolderName");
            return View();
        }
        #endregion

        #region rptHolderDetails
        public ActionResult rptHolderDetails(int id)
        {
            Session["HolderID"] = id;
            Session["AreaId"] = null;
            Session["PlotId"] = null;

            return View();
        }

        public ActionResult rptHolderDetails2(int id)
        {
            Session["HolderID"] = id;
            Session["AreaId"] = null;
            Session["PlotId"] = null;

            return View();
        }
        #endregion

    }
}