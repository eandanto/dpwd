using DPWD.Models;
using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace DPWD.Controllers
{
    public class MemberController : Controller
    {
        DAL.DAL dal = new DAL.DAL();
        
        public ActionResult Index()
        {
            if (Session["userName"] != null)
            {
                dal.UpdateNotificationsByType("member");

                MemberViewModel mvm = new MemberViewModel();
                mvm.MemberModelList = dal.GetMembers(null);

                var bankAccount = dal.GetBanks();
                ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

                var gameType = dal.GetGameTypes();
                ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

                var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
                ViewBag.Status = new SelectList(status, "ID", "Name");

                mvm.Message = Session["message"] != null ? Session["message"].ToString() : null;
                mvm.RequestStatus = Session["requestStatus"] != null ? Session["requestStatus"].ToString() : null;
                Session.Remove("message");
                Session.Remove("requestStatus");

                return View(mvm);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Index(MemberModel memberModel)
        {
            MemberViewModel mvm = new MemberViewModel();
            mvm.MemberModelList = dal.GetMembers(memberModel);

            var bankAccount = dal.GetBanks();
            ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

            var gameType = dal.GetGameTypes();
            ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

            var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
            ViewBag.Status = new SelectList(status, "ID", "Name");

            return View(mvm);
        }

        public ActionResult AddMember()
        {
            MemberModel model = new MemberModel();

            var bankAccount = dal.GetBanks();
            var bankAccountList = new List<SelectListItem>();
            foreach (var item in bankAccount)
            {
                bankAccountList.Add(new SelectListItem()
                {
                    Text = item.BankName,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.BankAccount = bankAccountList;

            var gameType = dal.GetGameTypes();
            var gameTypeList = new List<SelectListItem>();
            foreach (var item in gameType)
            {
                gameTypeList.Add(new SelectListItem()
                {
                    Text = item.GameType,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.GameType = gameTypeList;

            var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
            var statusList = new List<SelectListItem>();
            foreach (var item in status)
            {
                statusList.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.ID.ToString()
                });
            }
            ViewBag.Status = statusList;

            return PartialView("MemberModal", model);
        }

        [HttpPost]
        public ActionResult AddMember(MemberModel memberModel)
        {
            try
            {
                if (memberModel.GameType == 1 || memberModel.BankAccount == 1 || memberModel.BankAccountName == null || memberModel.BankAccountName == "" || memberModel.BankAccountNumber == null || memberModel.BankAccountNumber == "" || memberModel.EmailAddress == null || memberModel.EmailAddress == "" || memberModel.Status == Status.Select)
                {
                    Session["message"] = "Please fill in all the required fields.";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index");
                }

                if (dal.CheckDuplicateBankAccountNumberPerGame(memberModel.BankAccountNumber, memberModel.GameType))
                {
                    Session["message"] = "Bank account is already exists";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index");
                }

                dal.InsertMember(memberModel);
                Session["message"] = "Member is successfully added";
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

        public ActionResult ReAssign(int id)
        {
            MemberModel model = dal.GetMemberById(id);
            
            var gameType = dal.GetGameTypes();
            var gameTypeList = new List<SelectListItem>();
            foreach (var item in gameType)
            {
                gameTypeList.Add(new SelectListItem()
                {
                    Text = item.GameType,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.GameType = gameTypeList;

            var userName = dal.GetUserNamesByGameType(model.GameType);
            var userNameList = new List<SelectListItem>();
            foreach (var item in userName)
            {
                userNameList.Add(new SelectListItem()
                {
                    Text = item.UserName,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.UserName = userNameList;

            return PartialView("MemberReAssignModal", model);
        }

        
        public async Task<ActionResult> ReAssignMember(int Id, string UserName, int UserNameId, string Password, string Status)
        {
            try
            {
                MemberModel memberData = dal.GetMemberById(Id);
                memberData.UserName = UserName;

                //generatedPassword
                //var genPassword = System.Web.Security.Membership.GeneratePassword(8, 0);
                //UnicodeEncoding uEncode = new UnicodeEncoding();
                //byte[] data = uEncode.GetBytes(genPassword);
                //data = new System.Security.Cryptography.SHA512Managed().ComputeHash(data);

                string Pass = Password;
                UnicodeEncoding uEncode = new UnicodeEncoding();
                byte[] data = uEncode.GetBytes(Password);
                data = new System.Security.Cryptography.SHA512Managed().ComputeHash(data);
                memberData.Password = Convert.ToBase64String(data);
                dal.ReAssignMember(memberData);

                var message = "Selamat anda berhasil registrasi di macan303.com. Jenis game yang anda mainkan adalah " + memberData.GameTypeName.ToString() + ". Silahkan login dengan Username: " + memberData.UserName + ". Password: " + Pass + ".";
                var url = "https://reguler.zsms.us/apps/smsapi.php?userkey=1ilo23&passkey=pass.123&nohp=" + memberData.PhoneNumber + "&pesan=" + message;
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                if (Status == "list")
                {
                    dal.UpdateUserName(UserNameId);
                }
                else if (Status == "manual")
                {
                    UserNameModel um = new UserNameModel();
                    um.UserName = UserName;
                    dal.InsertUserNameAssigned(um);
                }

                dal.UpdateNotifications(memberData.Id, "member");

                Session["message"] = "Member is successfully assigned";
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

        public ActionResult EditMember(int id)
        {
            try
            {
                MemberModel model = dal.GetMemberById(id);

                var bankAccount = dal.GetBanks();
                var bankAccountList = new List<SelectListItem>();
                foreach (var item in bankAccount)
                {
                    bankAccountList.Add(new SelectListItem()
                    {
                        Text = item.BankName,
                        Value = item.Id.ToString(),
                        Selected = item.Id == (int)model.BankAccount ? true : false
                    });
                }
                ViewBag.BankAccount = bankAccountList;
                
                var gameType = dal.GetGameTypes();
                var gameTypeList = new List<SelectListItem>();
                foreach (var item in gameType)
                {
                    gameTypeList.Add(new SelectListItem()
                    {
                        Text = item.GameType,
                        Value = item.Id.ToString(),
                        Selected = item.Id == (int)model.GameType ? true : false
                    });
                }
                ViewBag.GameType = gameTypeList;

                var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
                var statusList = new List<SelectListItem>();
                foreach (var item in status)
                {
                    statusList.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ID.ToString(),
                        Selected = item.ID == (int)model.Status ? true : false
                    });
                }
                ViewBag.Status = statusList;

                return PartialView("MemberModal", model);
            }
            catch (Exception ex)
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
            
        }
        
        [HttpPost]
        public ActionResult EditMember(MemberModel memberModel)
        {
            try
            {
                if (memberModel.GameType == 1 || memberModel.BankAccount == 1 || memberModel.BankAccountName == null || memberModel.BankAccountName == "" || memberModel.BankAccountNumber == null || memberModel.BankAccountNumber == "" || memberModel.EmailAddress == null || memberModel.EmailAddress == "" || memberModel.Status == Status.Select)
                {
                    Session["message"] = "Please fill in all the required fields.";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index");
                }

                dal.EditMember(memberModel);
                Session["message"] = "Member is successfully edited";
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
                MemberModel model = dal.GetMemberById(id);

                return PartialView("MemberNotesModal", model);
            }
            catch (Exception ex)
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditNotes(MemberModel memberModel)
        {
            try
            {
                dal.EditMemberNotes(memberModel);
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

        public ActionResult Delete(int id)
        {
            try
            {
                dal.DeleteMember(id);
                Session["message"] = "Member is successfully deleted";
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
