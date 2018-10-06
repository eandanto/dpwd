using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DPWD.Models;
using DPWD.Models.Enum;

namespace DPWD.Controllers
{
    public class SettingsController : Controller
    {
        DAL.DAL dal = new DAL.DAL();
        
        public ActionResult Index()
        {
            SettingsViewModel svm = dal.GetSettings();
            var availability = from Availability s in Enum.GetValues(typeof(Availability)) select new { ID = (int)s, Name = s.ToString() };
            ViewBag.Availability = new SelectList(availability, "ID", "Name");
            var gameType = dal.GetGameTypes();
            ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

            svm.Message = Session["message"] != null ? Session["message"].ToString() : null;
            svm.RequestStatus = Session["requestStatus"] != null ? Session["requestStatus"].ToString() : null;
            Session.Remove("message");
            Session.Remove("requestStatus");

            return View(svm);
        }

        public ActionResult AddBank()
        {
            BankModel model = new BankModel();

            return PartialView("BankModal", model);
        }

        [HttpPost]
        public ActionResult AddBank(BankModel bankModel)
        {
            try
            {
                dal.InsertBank(bankModel);
                Session["message"] = "Bank is successfully added";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }

        public ActionResult AddGameType()
        {
            GameTypeModel model = new GameTypeModel();

            return PartialView("GameTypeModal", model);
        }

        [HttpPost]
        public ActionResult AddGameType(GameTypeModel gameTypeModel)
        {
            try
            {
                dal.InsertGameType(gameTypeModel);
                Session["message"] = "Game Type is successfully added";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }

        public ActionResult AddUserName()
        {
            UserNameModel model = new UserNameModel();
            var gameType = dal.GetGameTypes();
            ViewBag.GameType = new SelectList(gameType, "Id", "GameType");
            return PartialView("UserNameModal", model);
        }

        [HttpPost]
        public ActionResult AddUserName(UserNameModel userNameModel)
        {
            try
            {
                dal.InsertUserName(userNameModel);
                Session["message"] = "User Name is successfully added";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditUserName(int id)
        {
            UserNameModel model = dal.GetUserNameById(id);
            var gameType = dal.GetGameTypes();
            ViewBag.GameType = new SelectList(gameType, "Id", "GameType");
            return PartialView("UserNameModal", model);
        }

        [HttpPost]
        public ActionResult EditUserName(UserNameModel userNameModel)
        {
            try
            {
                dal.EditUserName(userNameModel);
                Session["message"] = "User Name is successfully edited";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Index(SettingsViewModel settingsViewModel)
        {
            try
            {
                SettingsViewModel svm = dal.GetSettings(settingsViewModel.UserName, settingsViewModel.Availability, settingsViewModel.GameType);

                var availability = from Availability s in Enum.GetValues(typeof(Availability)) select new { ID = (int)s, Name = s.ToString() };
                ViewBag.Availability = new SelectList(availability, "ID", "Name");

                return View(svm);
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index", "Settings");
            }

        }

        public ActionResult DeleteBank(int id)
        {
            try
            {
                dal.DeleteBank(id);
                Session["message"] = "Bank is successfully deleted";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteUserName(int id)
        {
            try
            {
                dal.DeleteBank(id);
                Session["message"] = "Bank is successfully deleted";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteGameType(int id)
        {
            try
            {
                dal.DeleteGameType(id);
                Session["message"] = "Game Type is successfully deleted";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }
    }
}