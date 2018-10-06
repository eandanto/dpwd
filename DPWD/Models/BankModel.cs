using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class BankModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public string BankName { get; set; }
    }
}