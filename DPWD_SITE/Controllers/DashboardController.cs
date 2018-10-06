using DPWD_SITE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPWD_SITE.Controllers
{
    public class DashboardController : Controller
    {
        DAL.DAL dal = new DAL.DAL();

        public ActionResult Index()
        {
            DashboardModel dm = new DashboardModel();
            dm.DepositModelList = dal.GetLatestDeposits();
            dm.WithdrawalModelList = dal.GetLatestWithdrawals();

            ViewBag.TogelText = dal.GetContentsByType(Models.Enum.ContentType.TOGELTEXT).Contents;
            ViewBag.TogelDate = dal.GetContentsByType(Models.Enum.ContentType.TOGELDATE).Contents;
            ViewBag.TogelTime = dal.GetContentsByType(Models.Enum.ContentType.TOGELTIME).Contents;
            ViewBag.TogelTitle = dal.GetContentsByType(Models.Enum.ContentType.TOGELTITLE).Contents;

            ViewBag.DashboardImage1 = dal.GetContentsByType(Models.Enum.ContentType.DASHBOARDIMAGE1).Contents;
            ViewBag.DashboardImage2 = dal.GetContentsByType(Models.Enum.ContentType.DASHBOARDIMAGE2).Contents;
            ViewBag.DashboardImage3 = dal.GetContentsByType(Models.Enum.ContentType.DASHBOARDIMAGE3).Contents;

            return View(dm);
        }
    }
}
