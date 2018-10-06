using DPWD.Models;
using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPWD.Controllers
{
    public class DepositController : Controller
    {
        DAL.DAL dal = new DAL.DAL();

        public ActionResult Index()
        {
            if (Session["userName"] != null)
            {
                dal.UpdateNotificationsByType("deposit");

                DepositViewModel dvm = new DepositViewModel();
                DepositSearchModel dsm = new DepositSearchModel();
                dsm.Status = Status.PENDING;
                dvm.DepositModelList = dal.GetDeposits(dsm);

                var bankAccount = dal.GetBanks();
                ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

                var gameType = dal.GetGameTypes();
                ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

                var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
                ViewBag.Status = new SelectList(status, "ID", "Name");

                dvm.Message = Session["message"] != null ? Session["message"].ToString() : null;
                dvm.RequestStatus = Session["requestStatus"] != null ? Session["requestStatus"].ToString() : null;
                Session.Remove("message");
                Session.Remove("requestStatus");

                return View(dvm);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Index(DepositSearchModel depositSearchModel)
        {
            try
            {
                DepositViewModel dvm = new DepositViewModel();
                if (depositSearchModel.StartDateString != null && depositSearchModel.StartDateString != "")
                {
                    depositSearchModel.StartDate = DateTime.ParseExact(depositSearchModel.StartDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //depositSearchModel.StartDate = Convert.ToDateTime(depositSearchModel.EndDate).AddDays(1).AddMilliseconds(-1);
                }
                if (depositSearchModel.EndDateString != null && depositSearchModel.EndDateString != "")
                {
                    depositSearchModel.EndDate = DateTime.ParseExact(depositSearchModel.EndDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture).AddDays(1).AddMilliseconds(-1);
                    //depositSearchModel.EndDate = Convert.ToDateTime(depositSearchModel.EndDate).AddDays(1).AddMilliseconds(-1);
                }
                dvm.DepositModelList = dal.GetDeposits(depositSearchModel);

                var bankAccount = dal.GetBanks();
                ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

                var gameType = dal.GetGameTypes();
                ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

                var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
                ViewBag.Status = new SelectList(status, "ID", "Name");

                return View(dvm);
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index", "Deposit");
            }
            
        }

        public JsonResult CheckUserName(string userName)
        {
            return Json(dal.GetMemberByUserName(userName), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reports()
        {
            if (Session["userName"] != null)
            {
                DepositViewModel dvm = new DepositViewModel();
                dvm.DepositModelList = dal.GetTodayDeposits();

                var bankAccount = dal.GetBanks();
                ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

                var gameType = dal.GetGameTypes();
                ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

                var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
                ViewBag.Status = new SelectList(status, "ID", "Name");

                dvm.Message = Session["message"] != null ? Session["message"].ToString() : null;
                dvm.RequestStatus = Session["requestStatus"] != null ? Session["requestStatus"].ToString() : null;
                Session.Remove("message");
                Session.Remove("requestStatus");

                return View(dvm);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Reports(DepositSearchModel depositSearchModel)
        {
            try
            {
                DepositViewModel dvm = new DepositViewModel();
                if (depositSearchModel.StartDateString != null && depositSearchModel.StartDateString != "")
                {
                    depositSearchModel.StartDate = DateTime.ParseExact(depositSearchModel.StartDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //depositSearchModel.StartDate = Convert.ToDateTime(depositSearchModel.EndDate).AddDays(1).AddMilliseconds(-1);
                }
                if (depositSearchModel.EndDateString != null && depositSearchModel.EndDateString != "")
                {
                    depositSearchModel.EndDate = DateTime.ParseExact(depositSearchModel.EndDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture).AddDays(1).AddMilliseconds(-1);
                    //depositSearchModel.EndDate = Convert.ToDateTime(depositSearchModel.EndDate).AddDays(1).AddMilliseconds(-1);
                }
                dvm.DepositModelList = dal.GetDeposits(depositSearchModel);

                var bankAccount = dal.GetBanks();
                ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

                var gameType = dal.GetGameTypes();
                ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

                var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
                ViewBag.Status = new SelectList(status, "ID", "Name");

                return View(dvm);
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index", "Deposit");
            }

        }

        public ActionResult Insert()
        {
            DepositModel model = new DepositModel();

            var bankAccount = dal.GetBanks();
            ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

            var gameType = dal.GetGameTypes();
            ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

            return PartialView("DepositModal", model);
        }
        
        [HttpPost]
        public ActionResult Insert(DepositModel depositModel)
        {
            try
            {
                if (depositModel.UserName == null || depositModel.UserName == "" || depositModel.BankAccount == 1 || depositModel.GameType == 1 || depositModel.BankAccountName == null || depositModel.BankAccountName == "" || depositModel.BankAccountNumber == null || depositModel.BankAccountNumber == "" || depositModel.DepositDate == null || depositModel.DepositAmount == 0)
                {
                    Session["message"] = "Please fill in all the required fields.";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index");
                }

                dal.InsertDeposit(depositModel);
                Session["message"] = "Deposit is successfully added";
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
                
        public ActionResult Approve(int id)
        {
            try
            {
                dal.UpdateDepositStatus(id, "Approve");
                dal.UpdateNotifications(id, "deposit");
                Session["message"] = "Transaction is successfuly approved";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }
        
        public ActionResult Pending(int id)
        {
            try
            {
                dal.UpdateDepositStatus(id, "Pending");
                Session["message"] = "Transaction is successfuly being pending";
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
        
        public ActionResult Reject(int id)
        {
            try
            {
                dal.UpdateDepositStatus(id, "Reject");
                dal.UpdateNotifications(id, "deposit");
                Session["message"] = "Transaction is successfuly rejected";
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

        public ActionResult EditNotes(int id)
        {
            try
            {
                DepositModel model = dal.GetDepositById(id);

                return PartialView("DepositNotesModal", model);
            }
            catch (Exception ex)
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditNotes(DepositModel depositModel)
        {
            try
            {
                dal.EditDepositNotes(depositModel);
                Session["message"] = "Notes is successfully edited";
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
