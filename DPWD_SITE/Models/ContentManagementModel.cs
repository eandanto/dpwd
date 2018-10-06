using DPWD_SITE.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD_SITE.Models
{
    public class ContentManagementModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        
        public ContentType Type { get; set; }
        
        public string Contents { get; set; }
    }
}