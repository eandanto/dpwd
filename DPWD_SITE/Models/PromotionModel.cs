using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD_SITE.Models
{
    public class PromotionModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public string PromotionName { get; set; }

        public string PromotionDetails { get; set; }
        
    }
}