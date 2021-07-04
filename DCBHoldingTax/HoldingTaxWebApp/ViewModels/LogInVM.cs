using HoldingTaxWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoldingTaxWebApp.ViewModels
{
    public class LogInVM
    {
        [Required, Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, Display(Name = "Password")]
        public string Password { get; set; }
        public DateTime? LogInTime { get; set; }
        public DateTime? LogOutTime { get; set; }
        public string UserLogDetails { get; set; }



        public CommonEntityHelper CommonEntity { get; set; }
        public LogInVM()
        {
            CommonEntity = new CommonEntityHelper();
        }

        //public LogInVM() => CommonEntity = new CommonEntityHelper();
    }
}