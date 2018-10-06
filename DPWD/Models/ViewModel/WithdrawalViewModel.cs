using System.Collections.Generic;

namespace DPWD.Models
{
    public class WithdrawalViewModel
    {
        public List<WithdrawalModel> WithdrawalModelList { get; set; }

        public WithdrawalSearchModel WithdrawalSearchModel { get; set; }

        public string Message { get; set; }

        public string RequestStatus { get; set; }
    }
}
