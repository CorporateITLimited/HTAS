using HoldingTaxWebApp.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels.DBO
{
    public class IssueCombineVM
    {
        public int IssueId { get; set; }
        public int HolderId { get; set; }
        public int StatusTypeId { get; set; }
        public string Subject { get; set; }
        //public string Details { get; set; }
        //public string Doc1 { get; set; }
        //public string Doc2 { get; set; }
        public DateTime? SolvedDate { get; set; }
        public string StringSolvedDate { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public string CreatedByUserName { get; set; }
        public string UpdatedByUserName { get; set; }

        ////out of table
        public string HolderName { get; set; }
        public string StatusName { get; set; }

        public List<IssueDetails> IssueDetails { get; set; }

        public IssueCombineVM()
        {
            IssueDetails = new List<IssueDetails>();
        }


    }
}