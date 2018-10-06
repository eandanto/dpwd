using System.Collections.Generic;

namespace DPWD_SITE.Models
{
    public class DashboardModel
    {
        public List<WithdrawalModel> WithdrawalModelList { get; set; }

        public List<DepositModel> DepositModelList { get; set; }
    }
}
