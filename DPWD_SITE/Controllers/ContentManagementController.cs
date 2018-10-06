using DPWD_SITE.Models;
using DPWD_SITE.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPWD_SITE.Controllers
{
    public class ContentManagementController : Controller
    {
        DAL.DAL dal = new DAL.DAL();

        [HttpGet]
        public JsonResult GetMainContents()
        {
            List<ContentManagementModel> lcm = dal.GetMainContents();

            return Json(lcm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPopUp()
        {
            List<ContentManagementModel> lcm = dal.GetPopUp();

            return Json(lcm, JsonRequestBehavior.AllowGet);
        }

        public ContentManagementModel GetContentsByType(ContentType type)
        {
            return dal.GetContentsByType(type);
        }
    }
}
