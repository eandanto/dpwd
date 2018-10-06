using DPWD_SITE.Models.Enum;
using DPWD_SITE.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPWD_SITE.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            DAL.DAL dal = new DAL.DAL();
            ContactUsViewModel cvm = new ContactUsViewModel();
            cvm.DepositModelList = dal.GetLatestDeposits();
            cvm.WithdrawalModelList = dal.GetLatestWithdrawals();

            ViewBag.TogelText = dal.GetContentsByType(ContentType.TOGELTEXT).Contents;
            ViewBag.TogelDate = dal.GetContentsByType(ContentType.TOGELDATE).Contents;
            ViewBag.TogelTime = dal.GetContentsByType(ContentType.TOGELTIME).Contents;
            ViewBag.TogelTitle = dal.GetContentsByType(ContentType.TOGELTITLE).Contents;

            ContentManagementController cmc = new ContentManagementController();
            ViewBag.Contents = cmc.GetContentsByType(ContentType.CONTACTUSTEXT).Contents;
            ViewBag.LiveChat = cmc.GetContentsByType(ContentType.CONTACTUSLIVECHATTEXT).Contents;
            ViewBag.YahooMail= cmc.GetContentsByType(ContentType.CONTACTUSYAHOOMAILTEXT).Contents;
            ViewBag.Bbm = cmc.GetContentsByType(ContentType.CONTACTUSBBMTEXT).Contents;
            ViewBag.Line = cmc.GetContentsByType(ContentType.CONTACTUSLINETEXT).Contents;
            ViewBag.WeChat = cmc.GetContentsByType(ContentType.CONTACTUSWECHATTEXT).Contents;
            ViewBag.WhatsApp = cmc.GetContentsByType(ContentType.CONTACTUSWHATSAPPTEXT).Contents;

            return View(cvm);
        }
    }
}