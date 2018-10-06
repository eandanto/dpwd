using DPWD_SITE.Models.Enum;
using DPWD_SITE.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPWD_SITE.Controllers
{
    public class GamesController : Controller
    {
        // GET: Games
        public ActionResult Index()
        {
            DAL.DAL dal = new DAL.DAL();
            GamesViewModel gvm = new GamesViewModel();
            gvm.DepositModelList = dal.GetLatestDeposits();
            gvm.WithdrawalModelList = dal.GetLatestWithdrawals();
            gvm.GameModelList = dal.GetGamesList();

            ViewBag.TogelText = dal.GetContentsByType(ContentType.TOGELTEXT).Contents;
            ViewBag.TogelDate = dal.GetContentsByType(ContentType.TOGELDATE).Contents;
            ViewBag.TogelTime = dal.GetContentsByType(ContentType.TOGELTIME).Contents;
            ViewBag.TogelTitle = dal.GetContentsByType(ContentType.TOGELTITLE).Contents;


            return View(gvm);
        }
    }
}