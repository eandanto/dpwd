using DPWD.DAL;
using DPWD.Models;
using DPWD.Models.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DPWD.Controllers
{
    public class LoginController : Controller
    {
        // GET: Withdrawal
        public ActionResult Index()
        {
            LoginViewModel lvm = new LoginViewModel();
            lvm.UserModel = new UserModel();
            lvm.Message = Session["message"] != null ? Session["message"].ToString() : null;
            lvm.RequestStatus = Session["requestStatus"] != null ? Session["requestStatus"].ToString() : null;
            Session.Remove("message");
            Session.Remove("requestStatus");
            return View(lvm);
        }

        [HttpPost]
        public ActionResult Index(UserModel userModel)
        {
            try
            {
                if (userModel.ValidationCode != Session["Captcha"].ToString())
                {
                    Session["message"] = "Incorrect captcha!";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index", "Login");
                }

                UnicodeEncoding uEncode = new UnicodeEncoding();
                byte[] data = uEncode.GetBytes(userModel.Password);
                data = new System.Security.Cryptography.SHA512Managed().ComputeHash(data);
                userModel.Password = Convert.ToBase64String(data);

                DAL.DAL dal = new DAL.DAL();
                string userName = dal.Login(userModel);
                if (userName != null)
                {
                    Session["userName"] = userName;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    Session["message"] = "Incorrect username or password";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception)
            {
                Session["message"] = "Incorrect username or password";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Logout()
        {
            Session["userName"] = null;
            return RedirectToAction("Index", "Login");
        }

        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int num1 = random.Next(1000, 9999);
            string s = string.Format("{0}", (object)num1);
            this.Session["Captcha"] = (object)num1;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (Bitmap bitmap = new Bitmap(55, 30))
                {
                    using (Graphics graphics = Graphics.FromImage((Image)bitmap))
                    {
                        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                        if (noisy)
                        {
                            Pen pen = new Pen(Color.Yellow);
                            for (int index = 1; index < 10; ++index)
                            {
                                pen.Color = Color.FromArgb(random.Next(0, (int)byte.MaxValue), random.Next(0, (int)byte.MaxValue), random.Next(0, (int)byte.MaxValue));
                                int num2 = random.Next(0, 43);
                                int num3 = random.Next(0, 130);
                                int num4 = random.Next(0, 30);
                                graphics.DrawEllipse(pen, num3 - num2, num4 - num2, num2, num2);
                            }
                        }
                        graphics.DrawString(s, new Font("Tahoma", 15f), Brushes.Gray, 2f, 3f);
                        bitmap.Save((Stream)memoryStream, ImageFormat.Jpeg);
                        return (ActionResult)this.File(memoryStream.GetBuffer(), "image/Jpeg");
                    }
                }
            }
        }

        public ActionResult ResetPassword()
        {
            LoginViewModel model = new LoginViewModel();
            model.UserModel = new UserModel();
            model.Message = Session["message"] != null ? Session["message"].ToString() : null;
            model.RequestStatus = Session["requestStatus"] != null ? Session["requestStatus"].ToString() : null;
            Session.Remove("message");
            Session.Remove("requestStatus");

            return View(model);
        }
        [HttpPost]
        public ActionResult ResetPassword(string email)
        {
            try
            {
                DAL.DAL dal = new DAL.DAL();
                UserModel um = dal.GetUserByEmail(email);
                if (um == null)
                {
                    Session["message"] = "Email is not associated to any registered accounts. Please enter a registered email address.";
                    Session["requestStatus"] = "Error";

                    return RedirectToAction("ResetPassword", "Login");
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

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                Session["message"] = "Unable to perform this request";
                Session["requestStatus"] = "Error";

                return RedirectToAction("ResetPassword", "Login");
            }
        }
    }
}
