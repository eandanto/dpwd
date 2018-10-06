using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class WithdrawalSearchModel
    {
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Game type is required.")]
        public int? GameType { get; set; }

        [Required(ErrorMessage = "Bank account is required.")]
        public int? BankAccount { get; set; }

        [Required(ErrorMessage = "Bank account name is required.")]
        public string BankAccountName { get; set; }

        [Required(ErrorMessage = "Bank account number is required.")]
        public string BankAccountNumber { get; set; }

        public Status? Status { get; set; }
        public string StartDateString { get; set; }
               
        public string EndDateString { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}