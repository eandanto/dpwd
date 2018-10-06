using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD_SITE.Models
{
    public class BankDepositModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public string BankName { get; set; }

        public string BankDetails { get; set; }

        public int Status { get; set; }
        //0 inactive, 1 active
    }
}