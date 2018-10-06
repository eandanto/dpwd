using DPWD.Models;
using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DPWD.Controllers
{
    public class NotificationController : Controller
    {
        [HttpGet]
        public JsonResult GetNotifications()
        {
            DAL.DAL dal = new DAL.DAL();
            List<NotificationModel> ln = dal.GetNotifications();
            //dal.UpdateNotificationsPulledStatus(ln);
            return Json(ln, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateNotifications()
        {
            DAL.DAL dal = new DAL.DAL();
            //List<NotificationModel> ln = dal.GetPulledNotifications();
            //dal.UpdateNotifications(ln);
            dal.UpdateNotifications();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTopFiveNotifications()
        {
            DAL.DAL dal = new DAL.DAL();
            List<NotificationModel> ln = dal.GetTopFiveNotifications();
            return Json(ln, JsonRequestBehavior.AllowGet);
        }
    }
}
