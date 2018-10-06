using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class UserNameModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public string UserName { get; set; }

        public int? GameType { get; set; }

        public string GameTypeName { get; set; }

        public int Availability { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateAssigned { get; set; }
    }
}