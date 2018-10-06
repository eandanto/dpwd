// Decompiled with JetBrains decompiler
// Type: DPWD_SITE.Controllers.DepositController
// Assembly: DPWD_SITE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 599367B4-4296-4B1F-BB4A-17B610A8E9AD
// Assembly location: C:\Users\Erlangga\Documents\Projects\DPWD\New folder\DPWD_SITE.dll

using DPWD_SITE.Models;
using DPWD_SITE.Models.Enum;
using DPWD_SITE.Models.ViewModels;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace DPWD_SITE.Controllers
{
    public class DepositController : Controller
    {
        private DPWD_SITE.DAL.DAL dal = new DPWD_SITE.DAL.DAL();

        public ActionResult Index()
        {
            ContentManagementController cmc = new ContentManagementController();
            ViewBag.Contents = cmc.GetContentsByType(ContentType.DEPOSITTEXT).Contents;

            DepositViewModel dvm = new DepositViewModel();
            dvm.DepositModel = new DepositModel();
            dvm.DepositModelList = dal.GetLatestDeposits();
            dvm.WithdrawalModelList = dal.GetLatestWithdrawals();
            dvm.BankDepositModelList = dal.GetBankDepositList();

            ViewBag.TogelText = dal.GetContentsByType(ContentType.TOGELTEXT).Contents;
            ViewBag.TogelDate = dal.GetContentsByType(ContentType.TOGELDATE).Contents;
            ViewBag.TogelTime = dal.GetContentsByType(ContentType.TOGELTIME).Contents;
            ViewBag.TogelTitle = dal.GetContentsByType(ContentType.TOGELTITLE).Contents;

            var bankAccount = dal.GetBanks();
            ViewBag.BankAccount = new SelectList(bankAccount, "Id", "BankName");

            var gameType = dal.GetGameTypes();
            ViewBag.GameType = new SelectList(gameType, "Id", "GameType");

            return View(dvm);
        }

        [HttpPost]
        public ActionResult Index(DepositModel depositModel)
        {
            try
            {
                if (depositModel.ValidationCode != this.Session["Captcha"].ToString())
                {
                    this.Session["message"] = (object)"Incorrect captcha!";
                    this.Session["requestStatus"] = (object)"Error";
                    return (ActionResult)this.RedirectToAction(nameof(Index));
                }
                else if (depositModel.BankAccount == 1 || depositModel.GameType == 1 || depositModel.DepositAmount == 0 || depositModel.UserName == null || depositModel.UserName == "" || depositModel.BankAccountNameSubmit == "" || depositModel.BankAccountNameSubmit == null || depositModel.BankAccountNumberSubmit == null || depositModel.BankAccountNumberSubmit == "")
                {
                    Session["message"] = "Data tidak sesuai!";
                    this.Session["requestStatus"] = (object)"Error";
                    return RedirectToAction("Index");
                }

                int depositId = dal.InsertDeposit(depositModel);
                dal.InsertNotification(depositId, "deposit");
                Session["message"] = (object)"Permintaan deposit berhasil diajukan. Petugas kami akan meninjau permintaan Anda dan Anda akan diinformasikan lebih lanjut kedepannya. Terima kasih.";
                Session["requestStatus"] = "Success";
                return (ActionResult)this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                this.Session["message"] = (object)"Terjadi kesalahan pada proses pengajuan deposit.";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction(nameof(Index));
            }
        }

        public JsonResult CheckUserName(string userName)
        {
            return Json(dal.GetMemberByUserName(userName), JsonRequestBehavior.AllowGet);
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
    }
}
