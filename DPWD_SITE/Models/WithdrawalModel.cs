using DPWD_SITE.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD_SITE.Models
{
    public class WithdrawalModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Withdrawal amount is required.")]
        public decimal WithdrawalAmount { get; set; }

        [Required(ErrorMessage = "Game type is required.")]
        public int GameType { get; set; }

        [Required(ErrorMessage = "Bank account is required.")]
        public int BankAccount { get; set; }

        [Required(ErrorMessage = "Bank account name is required.")]
        public string BankAccountName { get; set; }

        [Required(ErrorMessage = "Bank account number is required.")]
        public string BankAccountNumber { get; set; }

        public string BankAccountNameSubmit { get; set; }

        public string BankAccountNumberSubmit { get; set; }

        public Status Status { get; set; }

        public DateTime WithdrawalDate { get; set; }

        public string ValidationCode { get; set; }
    }
}