using DPWD_SITE.Models;
using DPWD_SITE.Models.Enum;
using DPWD_SITE.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPWD_SITE.Controllers
{
    public class RegistrationController : Controller
    {
        DAL.DAL dal = new DAL.DAL();

        public ActionResult Index()
        {
            ContentManagementController cmc = new ContentManagementController();
            ViewBag.Contents = cmc.GetContentsByType(ContentType.REGISTERTEXT).Contents;

            RegistrationViewModel rvm = new RegistrationViewModel();
            rvm.RegistrationModel = new RegistrationModel();
            rvm.DepositModelList = dal.GetLatestDeposits();
            rvm.WithdrawalModelList = dal.GetLatestWithdrawals();

            ViewBag.TogelText = dal.GetContentsByType(ContentType.TOGELTEXT).Contents;
            ViewBag.TogelDate = dal.GetContentsByType(ContentType.TOGELDATE).Contents;
            ViewBag.TogelTime = dal.GetContentsByType(ContentType.TOGELTIME).Contents;
            ViewBag.TogelTitle = dal.GetContentsByType(ContentType.TOGELTITLE).Contents;

            var bankAccount = dal.GetBanks();
            ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

            var gameType = dal.GetGameTypes();
            ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

            var status = from Status s in Enum.GetValues(typeof(Status)) select new { ID = (int)s, Name = s.ToString() };
            ViewBag.Status = new SelectList(status, "ID", "Name");

            return View(rvm);
        }

        [HttpPost]
        public ActionResult Index(RegistrationModel registrationModel)
        {
            try
            {
                if (registrationModel.ValidationCode != this.Session["Captcha"].ToString())
                {
                    Session["message"] = "Captcha tidak sesuai!";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index");
                }
                else if (registrationModel.BankAccount == 1 || registrationModel.GameType == 1 || registrationModel.BankAccountName == null || registrationModel.BankAccountName == "" || registrationModel.BankAccountNumber == null || registrationModel.BankAccountNumber == "" || registrationModel.PhoneNumber == null || registrationModel.PhoneNumber == "" || registrationModel.EmailAddress == null || registrationModel.EmailAddress == "")
                {
                    Session["message"] = "Harap isi semua kolom dengan benar.";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index");
                }


                if (dal.CheckDuplicateBankAccountNumberPerGame(registrationModel.BankAccountNumber, Convert.ToInt32(registrationModel.GameType)))
                {
                    Session["message"] = "Rekening anda telah terdaftar pada permainan yang anda pilih.";
                    Session["requestStatus"] = "Error";
                    return RedirectToAction("Index");
                }

                int memberId = dal.InsertMember(registrationModel);
                dal.InsertNotification(memberId, "member");
                Session["message"] = "Selamat! Pelanggan terhormat, Anda telah berhasil terdaftar sebagai anggota kami. Pihak customer service kami akan segera menghubungi Anda.";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Session["message"] = "Terjadi kesalahan pada proses registrasi";
                Session["requestStatus"] = "Error";
                return RedirectToAction("Index");
            }
        }

        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(1000, 9999);
            var captcha = string.Format("{0}", a);

            //store answer 
            Session["Captcha"] = a;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(55, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }

        //public string GenerateUserName()
        //{
        //    Random random = new Random();
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //    string userName = new string(Enumerable.Repeat(chars, 8)
        //      .Select(s => s[random.Next(s.Length)]).ToArray());

        //    if (dal.GetMemberByUserName(userName) != null)
        //    {
        //        GenerateUserName();
        //    }

        //    return userName;
        //}
    }
}
