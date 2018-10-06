using DPWD.Models;
using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DPWD.Controllers
{
    public class UserManagementController : Controller
    {
        DAL.DAL dal = new DAL.DAL();

        public ActionResult Index()
        {
            if (Session["userName"] != null)
            {
                UserViewModel uvm = new UserViewModel();
                uvm.UserModelList = new List<UserModel>();
                var list = dal.GetUsers();

                foreach (var item in list.ToList())
                {
                    if (item.UserName != "IT")
                    {
                        uvm.UserModelList.Add(item);
                    }
                }

                uvm.Message = Session["message"] != null ? Session["message"].ToString() : null;
                uvm.RequestStatus = Session["requestStatus"] != null ? Session["requestStatus"].ToString() : null;
                Session.Remove("message");
                Session.Remove("requestStatus");

                return View(uvm);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult AddUser()
        {
            UserModel model = new UserModel();
            return PartialView("UserManagementModal", model);
        }

        [HttpPost]
        public ActionResult AddUser(UserModel userModel)
        {
            try
            {
                if (userModel.UserName == null || userModel.UserName == "" || userModel.Email == null || userModel.Email == "" || userModel.Password == null || userModel.Password == "")
                {
                    Session["message"] = "Please fill in all the required fields.";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index");
                }

                UnicodeEncoding uEncode = new UnicodeEncoding();
                byte[] data = uEncode.GetBytes(userModel.Password);
                data = new System.Security.Cryptography.SHA512Managed().ComputeHash(data);
                userModel.Password = Convert.ToBase64String(data);

                dal.InsertUser(userModel);
                Session["message"] = "User is successfully added";
                Session["requestStatus"] = "Success";

                return RedirectToAction("Index", "UserManagement");
            }
            catch (Exception ex)
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";

                return RedirectToAction("Index", "UserManagement");
            }
        }

        public ActionResult EditUser(int id)
        {
            UserModel model = dal.GetUserById(id);
            return PartialView("UserManagementModal", model);

        }

        [HttpPost]
        public ActionResult EditUser(UserModel userModel)
        {
            try
            {
                if (userModel.UserName == null || userModel.UserName == "" || userModel.Email == null || userModel.Email == "" || userModel.Password == null || userModel.Password == "")
                {
                    Session["message"] = "Please fill in all the required fields.";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index");
                }
                if (userModel.Password != null && userModel.Password != "")
                {
                    UnicodeEncoding uEncode = new UnicodeEncoding();
                    byte[] data = uEncode.GetBytes(userModel.Password);
                    data = new System.Security.Cryptography.SHA512Managed().ComputeHash(data);
                    userModel.Password = Convert.ToBase64String(data);
                }

                dal.EditUser(userModel);
                Session["message"] = "User is successfully edited";
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


        public ActionResult ResetPassword(int id)
        {
            try
            {
                UserModel um = dal.GetUserById(id);
                if (um.Email == null || um.Email == "")
                {
                    Session["message"] = "Invalid email address.";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index", "UserManagement");
                }

                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                string recover = new string(Enumerable.Repeat(chars, 8)
                  .Select(s => s[random.Next(s.Length)]).ToArray());

                var fromAddress = new MailAddress("macan303.noreply@gmail.com", "ADMIN 303 NO-REPLY");
                var toAddress = new MailAddress(um.Email, um.UserName);
                const string fromPassword = "wtfRU19?";
                const string subject = "Reset Password";
                string body = ("Your new password is: " + recover);

                um.Password = recover;
                UnicodeEncoding uEncode = new UnicodeEncoding();
                byte[] data = uEncode.GetBytes(um.Password);
                data = new System.Security.Cryptography.SHA512Managed().ComputeHash(data);
                um.Password = Convert.ToBase64String(data);
                dal.EditUser(um);

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                //dal.InsertUser(userModel);
                Session["message"] = "Reset password email is successfully sent";
                Session["requestStatus"] = "Success";

                return RedirectToAction("Index", "UserManagement");
            }
            catch (Exception ex)
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";

                return RedirectToAction("Index", "UserManagement");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                DAL.DAL dal = new DAL.DAL();
                dal.DeleteUser(id);
                Session["message"] = "User is successfully deleted";
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
