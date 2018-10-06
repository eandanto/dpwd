using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPWD.Models
{
    public class TogelModel
    {
        public string TogelText { get; set; }
        
        public string Date { get; set; }

        public string Time { get; set; }

        public string Title { get; set; }
    }
}