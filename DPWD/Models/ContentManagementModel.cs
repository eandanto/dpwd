using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPWD.Models
{
    public class ContentManagementModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        
        public ContentType Type { get; set; }

        [AllowHtml]
        public string Contents { get; set; }
    }
}