using DPWD_SITE.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD_SITE.Models
{
    public class RegistrationModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Game type is required.")]
        public int GameType { get; set; }

        [Required(ErrorMessage = "Bank account is required.")]
        public int BankAccount { get; set; }

        [Required(ErrorMessage = "Bank account name is required.")]
        public string BankAccountName { get; set; }

        [Required(ErrorMessage = "Bank account number is required.")]
        public string BankAccountNumber { get; set; }

        public Status? Status { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        public string EmailAddress { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? ActionDate { get; set; }

        public string ValidationCode { get; set; }
    }
}