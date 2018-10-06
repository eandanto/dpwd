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
    public class WithdrawalController : Controller
    {
        DAL.DAL dal = new DAL.DAL();

        public ActionResult Index()
        {
            if (Session["userName"] != null)
            {
                dal.UpdateNotificationsByType("withdrawal");

                WithdrawalViewModel wvm = new WithdrawalViewModel();
                WithdrawalSearchModel wsm = new WithdrawalSearchModel();
                wsm.Status = Status.PENDING;
                wvm.WithdrawalModelList = dal.GetWithdrawals(wsm);

                var bankAccount = dal.GetBanks();
                ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

                var gameType = dal.GetGameTypes();
                ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

                var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
                ViewBag.Status = new SelectList(status, "ID", "Name");

                wvm.Message = Session["message"] != null ? Session["message"].ToString() : null;
                wvm.RequestStatus = Session["requestStatus"] != null ? Session["requestStatus"].ToString() : null;
                Session.Remove("message");
                Session.Remove("requestStatus");

                return View(wvm);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Index(WithdrawalSearchModel withdrawalSearchModel)
        {
            try
            {
                WithdrawalViewModel wvm = new WithdrawalViewModel();
                if (withdrawalSearchModel.StartDateString != null && withdrawalSearchModel.StartDateString != "")
                {
                    withdrawalSearchModel.StartDate = DateTime.ParseExact(withdrawalSearchModel.StartDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                if (withdrawalSearchModel.EndDateString != null && withdrawalSearchModel.EndDateString != "")
                {
                    withdrawalSearchModel.EndDate = DateTime.ParseExact(withdrawalSearchModel.EndDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture).AddDays(1).AddMilliseconds(-1);
                }
                wvm.WithdrawalModelList = dal.GetWithdrawals(withdrawalSearchModel);

                var bankAccount = dal.GetBanks();
                ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

                var gameType = dal.GetGameTypes();
                ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

                var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
                ViewBag.Status = new SelectList(status, "ID", "Name");

                return View(wvm);
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index", "Withdrawal");
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
                WithdrawalViewModel wvm = new WithdrawalViewModel();
                wvm.WithdrawalModelList = dal.GetTodayWithdrawals();

                var bankAccount = dal.GetBanks();
                ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

                var gameType = dal.GetGameTypes();
                ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

                var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
                ViewBag.Status = new SelectList(status, "ID", "Name");

                wvm.Message = Session["message"] != null ? Session["message"].ToString() : null;
                wvm.RequestStatus = Session["requestStatus"] != null ? Session["requestStatus"].ToString() : null;
                Session.Remove("message");
                Session.Remove("requestStatus");

                return View(wvm);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Reports(WithdrawalSearchModel withdrawalSearchModel)
        {
            try
            {
                WithdrawalViewModel wvm = new WithdrawalViewModel();
                if (withdrawalSearchModel.StartDateString != null && withdrawalSearchModel.StartDateString != "")
                {
                    withdrawalSearchModel.StartDate = DateTime.ParseExact(withdrawalSearchModel.StartDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                if (withdrawalSearchModel.EndDateString != null && withdrawalSearchModel.EndDateString != "")
                {
                    withdrawalSearchModel.EndDate = DateTime.ParseExact(withdrawalSearchModel.EndDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture).AddDays(1).AddMilliseconds(-1);
                }
                wvm.WithdrawalModelList = dal.GetWithdrawals(withdrawalSearchModel);

                var bankAccount = dal.GetBanks();
                ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

                var gameType = dal.GetGameTypes();
                ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

                var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
                ViewBag.Status = new SelectList(status, "ID", "Name");

                return View(wvm);
            }
            catch
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index", "Withdrawal");
            }

        }

        public ActionResult Insert()
        {
            WithdrawalModel model = new WithdrawalModel();

            var bankAccount = dal.GetBanks();
            ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

            var gameType = dal.GetGameTypes();
            ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

            return PartialView("WithdrawalModal", model);
        }
        
        [HttpPost]
        public ActionResult Insert(WithdrawalModel withdrawalModel)
        {
            try
            {
                if (withdrawalModel.UserName == null || withdrawalModel.UserName == "" || withdrawalModel.BankAccount == 1 || withdrawalModel.GameType == 1 || withdrawalModel.BankAccountName == null || withdrawalModel.BankAccountName == "" || withdrawalModel.BankAccountNumber == null || withdrawalModel.BankAccountNumber == "" || withdrawalModel.WithdrawalAmount == 0)
                {
                    Session["message"] = "Please fill in all the required fields.";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index");
                }

                dal.InsertWithdrawal(withdrawalModel);
                Session["message"] = "Withdrawal is successfully added";
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
                
        public ActionResult Approve(int id)
        {
            try
            {
                dal.UpdateWithdrawalStatus(id, "Approve");
                dal.UpdateNotifications(id, "withdrawal");
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
                dal.UpdateWithdrawalStatus(id, "Pending");
                Session["message"] = "Transaction is successfuly being pending";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
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
                dal.UpdateWithdrawalStatus(id, "Reject");
                dal.UpdateNotifications(id, "withdrawal");
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
                WithdrawalModel model = dal.GetWithdrawalById(id);

                return PartialView("WithdrawalNotesModal", model);
            }
            catch (Exception ex)
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditNotes(WithdrawalModel withdrawalModel)
        {
            try
            {
                dal.EditWithdrawalNotes(withdrawalModel);
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
