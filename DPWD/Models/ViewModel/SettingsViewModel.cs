using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class SettingsViewModel
    {
        public List<BankModel> BankModelList { get; set; }
        public List<GameTypeModel> GameTypeModelList { get; set; }

        public List<UserNameModel> UserNameModelList { get; set; }
        public string UserName { get; set; }
        public int? GameType { get; set; }
        public Availability Availability { get; set; }
        public string Message { get; set; }
        public string RequestStatus { get; set; }
    }
}