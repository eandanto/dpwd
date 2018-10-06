using System;
using System.ComponentModel.DataAnnotations;

namespace DPWD.Models
{
    public class DepositSearchModel
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

        public DPWD.Models.Enum.Status? Status { get; set; }

        public string StartDateString { get; set; }

        public string EndDateString { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
