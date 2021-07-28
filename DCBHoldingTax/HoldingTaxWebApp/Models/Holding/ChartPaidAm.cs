using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Holding
{
    public class ChartPaidAm
    {
        public string MonthDate { get; set; }
        public string AreaName { get; set; }
        public decimal? MonthlyPaidAmount { get; set; }

    }
}