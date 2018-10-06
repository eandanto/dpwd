using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class GameModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public string GameName { get; set; }

        public string GameLandingUrl { get; set; }

        public int Status { get; set; }
        //0 inactive, 1 active
    }
}