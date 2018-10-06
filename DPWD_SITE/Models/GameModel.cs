using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD_SITE.Models
{
    public class GameModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public string GameName { get; set; }

        public string GameLandingUrl { get; set; }
        
    }
}