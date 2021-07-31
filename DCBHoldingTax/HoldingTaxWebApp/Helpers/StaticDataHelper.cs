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

        public static IEnumerable<SelectListItem> GetOwnerForDropdown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>() {
                new SelectListItem(){ Text="হ্যা মালিক", Value="true" },
                new SelectListItem(){ Text="না মালিক নয়", Value="false" }
            };
            return selectListItems;
        }

        public static IEnumerable<SelectListItem> GetNoticeTypeNameStatusForDropdown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>() {
                new SelectListItem(){ Text="গৃহকরের প্রাথমিক বিজ্ঞপ্তি  (জুলাই ১ হতে জুলাই ৩১)", Value="1" },
                new SelectListItem(){ Text="রিবেটসহ গৃহকর প্রাপ্তির বিজ্ঞপ্তি (নভেম্বর ১ হতে নভেম্বর ৩০)", Value="2" },
                new SelectListItem(){ Text="গৃহকরের চূড়ান্ত বিজ্ঞপ্তি  (মে ১ হতে মে ৩০)", Value="3" }
            };
            return selectListItems;
        }
    }
}