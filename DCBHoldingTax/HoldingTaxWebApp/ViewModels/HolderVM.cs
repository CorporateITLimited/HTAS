﻿using HoldingTaxWebApp.Models.Holding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels
{
    public class HolderVM
    {
        public int HolderId { get; set; }

        [Display(Name = "Holder Name")]
        public string HolderName { get; set; }

        [Display(Name = "Area Name")]
        public int AreaId { get; set; }

        [Display(Name = "Area Name")]
        public string AreaName { get; set; }

        public int PlotId { get; set; }

        [Display(Name = "Plot Id Number")]
        public string PlotIdNumber { get; set; }

        [Display(Name = "Plot Number")]
        public string PlotNo { get; set; }

        [Display(Name = "Plot Number")]
        public string NID { get; set; }

        [Display(Name = "Gender")]
        public int? Gender { get; set; }

        [Display(Name = "Gender Type")]
        public string GenderType { get; set; }

        [Display(Name = "Maritial Status")]
        public int? MaritialStatus { get; set; }

        [Display(Name = "Maritial Status")]
        public string MaritialStatusType { get; set; }

        [Display(Name = "Father's Name")]
        public string Father { get; set; }

        [Display(Name = "Mother's Name")]
        public string Mother { get; set; }

        [Display(Name = "Spouse")]
        public string Spouse { get; set; }

        [Display(Name = "Mobile No")]
        public string Contact1 { get; set; }

        [Display(Name = "Land No")]
        public string Contact2 { get; set; }

        [Display(Name = "Land No")]
        public string Email { get; set; }

        [Display(Name = "Present Address")]
        public string PresentAdd { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAdd { get; set; }

        [Display(Name = "Contact Address")]
        public string ContactAdd { get; set; }

        [Display(Name = "Source Name")]
        public int OwnershipSourceId { get; set; }

        [Display(Name = "Source Name")]
        public string SourceName { get; set; }

        [Display(Name = "Owner Type")]
        public int? OwnerType { get; set; }

        [Display(Name = "Owner Type")]
        public string OwnerTypeName { get; set; }

        [Display(Name = "Building Type")]
        public int BuildingTypeId { get; set; }

        [Display(Name = "Building Type")]
        public string BuildingTypeName { get; set; }

        [Display(Name = "Amount of land")]
        public decimal? AmountOfLand { get; set; }

        [Display(Name = "Total Floor")]
        public int? TotalFloor { get; set; }

        [Display(Name = "Each Floor Area")]
        public decimal? EachFloorArea { get; set; }

        [Display(Name = "Total Flat")]
        public int? TotalFlat { get; set; }

        [Display(Name = "Holders Flat Number")]
        public int? HoldersFlatNumber { get; set; }

        [Display(Name = "Previous Due Tax")]
        public decimal? PreviousDueTax { get; set; }

        [Display(Name = "Image Location")]
        public string ImageLocation { get; set; }

        [Display(Name = "Document1")]
        public string Document1 { get; set; }

        [Display(Name = "Document2")]
        public string Document2 { get; set; }

        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        [Display(Name = "Created By ")]
        public string CreatedByUsername { get; set; }

        [Display(Name = "Updated Date ")]
        public DateTime? LastUpdated { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedByUsername { get; set; }

        [Display(Name = "Status")]
        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public List<HolderFlat> HolderFlatList { get; set; }

        public HttpPostedFileBase image_file { get; set; }
        public HttpPostedFileBase document_file_1 { get; set; }
        public HttpPostedFileBase document_file_2 { get; set; }


        public HolderVM()
        {
            HolderFlatList = new List<HolderFlat>();
        }

    }
}