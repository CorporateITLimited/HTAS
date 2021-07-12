using System;

namespace HoldingTaxWebApp.Gateway.Dbo
{
    internal class DOHSarea : DOHSArea
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int MedicalAmount { get; set; }
        public decimal? TotalArea { get; set; }
        public int CurrentPlotNumber { get; set; }
        public int CurrentFlatNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}