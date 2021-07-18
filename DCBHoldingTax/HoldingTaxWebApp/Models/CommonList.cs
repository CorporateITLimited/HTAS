using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.Models
{
    public class CommonList
    {
        [Display(Name = "তৈরিকারী")]
        public int TypeId { get; set; }
        [Display(Name = "তৈরিকারী")]
        public string TypeName { get; set; }
    }
}