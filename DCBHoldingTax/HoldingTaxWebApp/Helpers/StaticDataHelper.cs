using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Helpers
{
    public class StaticDataHelper
    {
        public static IEnumerable<SelectListItem> GetActiveStatusForDropdown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>() {
                new SelectListItem(){ Text="সক্রিয়", Value="true" },
                new SelectListItem(){ Text="নিষ্ক্রিয়", Value="false" }
            };
            return selectListItems;
        }
    }
}