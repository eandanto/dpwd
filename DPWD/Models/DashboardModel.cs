using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class DashboardModel
    {
        public int TodaysMembers { get; set; }

        public int TodaysDeposits { get; set; }

        public decimal TodaysDepositsAmount { get; set; }

        public int TodaysWithdrawal { get; set; }

        public decimal TodaysWithdrawalAmount { get; set; }

        public int TodaysTotal { get; set; }

        public decimal TodaysTotalAmount { get; set; }
    }
}