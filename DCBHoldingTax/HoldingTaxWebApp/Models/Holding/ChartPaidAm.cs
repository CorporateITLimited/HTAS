using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models.Holding
{
    public class ChartPaidAm
    {
        public int MonthDate { get; set; }
        public int YearDate { get; set; }
        public string AreaName { get; set; }
        public int MonthlyPaidAmount { get; set; }

    }
}