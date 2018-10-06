using DPWD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPWD.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["userName"] != null)
            {
                DAL.DAL dal = new DAL.DAL();
               DashboardModel dc = dal.GetDashboardData();

                return View(dc);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}