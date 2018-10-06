using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class MemberViewModel
    {
        public List<MemberModel> MemberModelList { get; set; }
        public MemberModel MemberModel { get; set; }
        public string Message { get; set; }
        public string RequestStatus { get; set; }
    }
}