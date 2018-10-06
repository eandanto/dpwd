using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class NotificationModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        
        public string Type { get; set; }
        
        public NotificationStatus Status { get; set; }

        public int Pulled { get; set; }

        public DateTime Time { get; set; }
    }
}