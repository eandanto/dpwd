using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class LoginViewModel
    {
        public UserModel UserModel { get; set; }
        public string Message { get; set; }
        public string RequestStatus { get; set; }
    }
}