using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class MemberModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int? GameType { get; set; }

        public string GameTypeName { get; set; }

        public int? BankAccount { get; set; }

        public string BankName { get; set; }

        public string BankAccountName { get; set; }
        
        public string BankAccountNumber { get; set; }

        public Status? Status { get; set; }
        
        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? ActionDate { get; set; }

        public string Notes { get; set; }
    }
}