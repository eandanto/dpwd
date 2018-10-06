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
    public class WithdrawalController : Controller
    {
        DAL.DAL dal = new DAL.DAL();

        [HttpGet]
        public ActionResult Index()
        {
            ContentManagementController cmc = new ContentManagementController();
            ViewBag.Contents = cmc.GetContentsByType(ContentType.WITHDRAWALTEXT).Contents;

            WithdrawalViewModel wvm = new WithdrawalViewModel();
            wvm.WithdrawalModel = new WithdrawalModel();
            wvm.DepositModelList = dal.GetLatestDeposits();
            wvm.WithdrawalModelList = dal.GetLatestWithdrawals();

            ViewBag.TogelText = dal.GetContentsByType(ContentType.TOGELTEXT).Contents;
            ViewBag.TogelDate = dal.GetContentsByType(ContentType.TOGELDATE).Contents;
            ViewBag.TogelTime = dal.GetContentsByType(ContentType.TOGELTIME).Contents;
            ViewBag.TogelTitle = dal.GetContentsByType(ContentType.TOGELTITLE).Contents;

            var bankAccount = dal.GetBanks();
            ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

            var gameType = dal.GetGameTypes();
            ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

            return View(wvm);
        }

        [HttpPost]
        public ActionResult Index(WithdrawalModel withdrawalModel)
        {
            try
            {
                if (withdrawalModel.ValidationCode != Session["Captcha"].ToString())
                {
                    Session["message"] = "Incorrect captcha!";
                    this.Session["requestStatus"] = (object)"Error";
                    return RedirectToAction("Index");
                }
                else if (withdrawalModel.BankAccount == 1 || withdrawalModel.GameType == 1 || withdrawalModel.WithdrawalAmount == 0 || withdrawalModel.UserName == null || withdrawalModel.UserName == "" || withdrawalModel.BankAccountNameSubmit == null || withdrawalModel.BankAccountNameSubmit == "" || withdrawalModel.BankAccountNumberSubmit == null || withdrawalModel.BankAccountNumberSubmit == "")
                {
                    Session["message"] = "Data tidak sesuai!";
                    this.Session["requestStatus"] = (object)"Error";
                    return RedirectToAction("Index");
                }

                int withdrawalId = dal.InsertWithdrawal(withdrawalModel);
                dal.InsertNotification(withdrawalId, "withdrawal");
                Session["message"] = "Permintaan withdraw berhasil diajukan. Petugas kami akan meninjau permintaan Anda dan Anda akan diinformasikan lebih lanjut kedepannya. Terima kasih.";
                Session["requestStatus"] = "Success";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Session["message"] = "Terjadi kesalahan pada proses pengajuan withdraw.";
                this.Session["requestStatus"] = (object)"Error";
                return RedirectToAction("Index");
            }
        }

        public JsonResult CheckUserName(string userName)
        {
            return Json(dal.GetMemberByUserName(userName), JsonRequestBehavior.AllowGet);
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
    }
}