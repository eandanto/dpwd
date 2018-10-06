using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DPWD.Models
{
    public class ContentManagementViewModel
    {
        public ContentManagementModel RunningTextModel { get; set; }

        public ContentManagementModel DisclaimerModel { get; set; }

        public ContentManagementModel BankOfflineModel { get; set; }

        public ContentManagementModel RegisterModel { get; set; }

        public ContentManagementModel DepositModel { get; set; }

        public ContentManagementModel WithdrawalModel { get; set; }

        public ContentManagementModel PromotionModel { get; set; }

        public ContentManagementModel ContactModel { get; set; }

        public ContentManagementModel ContactLiveChatModel { get; set; }

        public ContentManagementModel ContactYahooMailModel { get; set; }

        public ContentManagementModel ContactBbmModel { get; set; }

        public ContentManagementModel ContactLineModel { get; set; }

        public ContentManagementModel ContactWeChatModel { get; set; }

        public ContentManagementModel ContactWhatsAppModel { get; set; }

        public ContentManagementModel BankBcaModel { get; set; }

        public ContentManagementModel BankMandiriModel { get; set; }

        public ContentManagementModel BankBniModel { get; set; }

        public TogelModel TogelModel { get; set; }

        public ContentManagementModel DashboardImage1 { get; set; }

        public ContentManagementModel DashboardImage2 { get; set; }

        public ContentManagementModel DashboardImage3 { get; set; }

        public List<BankDepositModel> BankDepositList { get; set; }
        public List<PromotionModel> PromotionList { get; set; }

        public List<GameModel> GamesList { get; set; }

        public ContentManagementModel ScriptHead { get; set; }

        public ContentManagementModel ScriptBody { get; set; }

        public ContentManagementModel PopUpStatus { get; set; }

        public ContentManagementModel PopUpContent { get; set; }

        public string Message { get; set; }

        public string RequestStatus { get; set; }

    }
}