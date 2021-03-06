﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD_SITE.Models.ViewModels
{
    public class GamesViewModel
    {
        public List<WithdrawalModel> WithdrawalModelList { get; set; }

        public List<DepositModel> DepositModelList { get; set; }

        public List<GameModel> GameModelList { get; set; }
    }
}