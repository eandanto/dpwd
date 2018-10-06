using DPWD.Models;
using DPWD.Models.Enum;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace DPWD.Controllers
{
    public class ContentManagementController : Controller
    {
        private DPWD.DAL.DAL dal = new DPWD.DAL.DAL();

        public ActionResult Index()
        {
            ContentManagementViewModel managementViewModel = new ContentManagementViewModel();
            List<ContentManagementModel> contents = this.dal.GetContents();
            managementViewModel.RunningTextModel = contents[0];
            managementViewModel.DisclaimerModel = contents[1];
            managementViewModel.RegisterModel = contents[2];
            managementViewModel.DepositModel = contents[3];
            managementViewModel.WithdrawalModel = contents[4];
            managementViewModel.PromotionModel = contents[5];
            managementViewModel.ContactModel = contents[6];
            managementViewModel.ContactLiveChatModel = contents[7];
            managementViewModel.ContactYahooMailModel = contents[8];
            managementViewModel.ContactBbmModel = contents[9];
            managementViewModel.ContactLineModel = contents[10];
            managementViewModel.ContactWeChatModel = contents[11];
            managementViewModel.ContactWhatsAppModel = contents[12];
            managementViewModel.BankBcaModel = contents[13];
            managementViewModel.BankMandiriModel = contents[14];
            managementViewModel.BankBniModel = contents[15];
            managementViewModel.TogelModel = new TogelModel()
            {
                TogelText = contents[16].Contents,
                Date = contents[17].Contents,
                Title = contents[18].Contents,
                Time = contents[22].Contents
            };
            managementViewModel.BankOfflineModel = contents[25];
            managementViewModel.DashboardImage1 = contents[27];
            managementViewModel.DashboardImage2 = contents[28];
            managementViewModel.DashboardImage3 = contents[29];
            managementViewModel.ScriptHead = contents[30];
            managementViewModel.ScriptBody = contents[31];
            managementViewModel.PopUpStatus = contents[32];
            managementViewModel.PopUpContent = contents[33];
            managementViewModel.BankDepositList = this.dal.GetBankDepositList();
            managementViewModel.PromotionList = this.dal.GetPromotionList();
            managementViewModel.GamesList = this.dal.GetGamesList();
            managementViewModel.Message = this.Session["message"] != null ? this.Session["message"].ToString() : (string)null;
            managementViewModel.RequestStatus = this.Session["requestStatus"] != null ? this.Session["requestStatus"].ToString() : (string)null;
            this.Session.Remove("message");
            this.Session.Remove("requestStatus");
            return (ActionResult)this.View((object)managementViewModel);
        }

        [HttpPost]
        public ActionResult UpdateDashboardImage(int index, HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "slider" + index.ToString() + ".jpg");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\slider" + index.ToString() + ".jpg";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                switch (index)
                {
                    case 1:
                        this.dal.UpdateContent(ContentType.DASHBOARDIMAGE1, "slider" + index.ToString() + ".jpg");
                        break;
                    case 2:
                        this.dal.UpdateContent(ContentType.DASHBOARDIMAGE2, "slider" + index.ToString() + ".jpg");
                        break;
                    default:
                        this.dal.UpdateContent(ContentType.DASHBOARDIMAGE3, "slider" + index.ToString() + ".jpg");
                        break;
                }
                this.Session["message"] = (object)"Image slider is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        public ActionResult DeleteDashboardImage(int id)
        {
            try
            {
                switch (id)
                {
                    case 1:
                        this.dal.UpdateContent(ContentType.DASHBOARDIMAGE1, "(EMPTY)");
                        break;
                    case 2:
                        this.dal.UpdateContent(ContentType.DASHBOARDIMAGE2, "(EMPTY)");
                        break;
                    default:
                        this.dal.UpdateContent(ContentType.DASHBOARDIMAGE3, "(EMPTY)");
                        break;
                }
                this.Session["message"] = (object)"Image slider is successfully deleted";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateRunningText(ContentManagementModel runningTextModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.RUNNINGTEXT, runningTextModel.Contents);
                this.dal.UpdateContent(ContentType.RUNNINGTEXTDATE, DateTime.Now.ToString("dddd, dd/MM/yy"));
                this.Session["message"] = (object)"Running text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateDisclaimerText(ContentManagementModel disclaimerModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.DISCLAIMERTEXT, disclaimerModel.Contents);
                this.Session["message"] = (object)"Disclaimer text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateBankOfflineText(ContentManagementModel bankOfflineModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.BANKOFFLINE, bankOfflineModel.Contents);
                this.Session["message"] = (object)"Bank Offline text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateMainLogoImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "new-logo.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\new-logo.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"Main Logo image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateShortLogoImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "new-logo-short.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\new-logo-short.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"Short Logo image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateWaysToPlayImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "ways-to-play.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\ways-to-play.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"Ways to Play image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateRegisterText(ContentManagementModel registerModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.REGISTERTEXT, registerModel.Contents);
                this.Session["message"] = (object)"Registration text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateDepositText(ContentManagementModel depositModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.DEPOSITTEXT, depositModel.Contents);
                this.Session["message"] = (object)"Deposit text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateWithdrawalText(ContentManagementModel withdrawalModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.WITHDRAWALTEXT, withdrawalModel.Contents);
                this.Session["message"] = (object)"Withdrawal text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdatePromotionText(ContentManagementModel promotionModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.PROMOTIONTEXT, promotionModel.Contents);
                this.Session["message"] = (object)"Promotion text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactText(ContentManagementModel contactModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.CONTACTUSTEXT, contactModel.Contents);
                this.Session["message"] = (object)"Contact text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactLiveChatImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "contactLiveChat.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\contactLiveChat.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"Live Chat image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactYahooImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "contactYahoo.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\contactYahoo.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"Yahoo Mail image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactBbmImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "contactBbm.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\contactBbm.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"BBM image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactLineImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "contactLine.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\contactLine.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"Line image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactWeChatImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "contactWeChat.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\contactWeChat.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"WeChat image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactWhatsAppImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "contactWhatsApp.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\contactWhatsApp.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"WhatsApp image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactLiveChatText(ContentManagementModel contactLiveChatModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.CONTACTUSLIVECHATTEXT, contactLiveChatModel.Contents);
                this.Session["message"] = (object)"Live Chat text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactYahooMailText(ContentManagementModel contactYahooMailModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.CONTACTUSYAHOOMAILTEXT, contactYahooMailModel.Contents);
                this.Session["message"] = (object)"Yahoo Mail text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactBbmText(ContentManagementModel contactBbmModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.CONTACTUSBBMTEXT, contactBbmModel.Contents);
                this.Session["message"] = (object)"BBM text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactLineText(ContentManagementModel contactLineModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.CONTACTUSLINETEXT, contactLineModel.Contents);
                this.Session["message"] = (object)"Line text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactWeChatText(ContentManagementModel contactWeChatModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.CONTACTUSWECHATTEXT, contactWeChatModel.Contents);
                this.Session["message"] = (object)"WeChat text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateContactWhatsAppText(ContentManagementModel ContactWhatsAppModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.CONTACTUSWHATSAPPTEXT, ContactWhatsAppModel.Contents);
                this.Session["message"] = (object)"WhatsApp text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateBankLogosImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "bank-logos.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\bank-logos.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"Bank logos image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateBankBcaImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "bankBca.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\bankBca.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"Bank BCA image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateBankMandiriImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "bankMandiri.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\bankMandiri.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"Bank Mandiri image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateBankBniImage(HttpPostedFileBase file)
        {
            try
            {
                string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "bankBni.png");
                string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\bankBni.png";
                if (System.IO.File.Exists(str1))
                {
                    System.IO.File.Delete(str1);
                }
                if (System.IO.File.Exists(str2))
                {
                    System.IO.File.Delete(str2);
                }
                file.SaveAs(str1);
                file.SaveAs(str2);
                this.Session["message"] = (object)"Bank BNI image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateBankBcaText(ContentManagementModel bankBcaModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.BANKBCATEXT, bankBcaModel.Contents);
                this.Session["message"] = (object)"Bank BCA text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateBankMandiriText(ContentManagementModel bankMandiriModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.BANKMANDIRITEXT, bankMandiriModel.Contents);
                this.Session["message"] = (object)"Bank Mandiri text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateBankBniText(ContentManagementModel bankBniModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.BANKBNITEXT, bankBniModel.Contents);
                this.Session["message"] = (object)"Bank BNI text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateTogelText(TogelModel togelModel)
        {
            try
            {
                this.dal.UpdateContent(ContentType.TOGELTEXT, togelModel.TogelText);
                this.dal.UpdateContent(ContentType.TOGELDATE, togelModel.Date);
                this.dal.UpdateContent(ContentType.TOGELTIME, togelModel.Time);
                this.dal.UpdateContent(ContentType.TOGELTITLE, togelModel.Title);
                this.Session["message"] = (object)"Togel text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        //[HttpPost]
        //public ActionResult UpdatePopUpModal(ContentManagementModel popUpStatus, ContentManagementModel popUpContent)
        //{
        //    try
        //    {
        //        this.dal.UpdateContent(ContentType.POPUPSTATUS, popUpStatus.Contents);
        //        this.dal.UpdateContent(ContentType.POPUPCONTENT, popUpContent.Contents);
        //        this.Session["message"] = (object)"Pop Up Modal is successfully updated";
        //        this.Session["requestStatus"] = (object)"Success";
        //        return (ActionResult)this.RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        this.Session["message"] = (object)"Unable to perform this request";
        //        this.Session["requestStatus"] = (object)"Error";
        //        return (ActionResult)this.RedirectToAction("Index");
        //    }
        //}

        [HttpPost]
        public ActionResult UpdateScriptHeadText(ContentManagementModel scriptHead)
        {
            try
            {
                this.dal.UpdateContent(ContentType.SCRIPTHEAD, scriptHead.Contents);
                this.Session["message"] = (object)"Script head text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateScriptBodyText(ContentManagementModel scriptBody)
        {
            try
            {
                this.dal.UpdateContent(ContentType.SCRIPTBODY, scriptBody.Contents);
                this.Session["message"] = (object)"Script body text is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        public ActionResult InsertBankDeposit()
        {
            BankDepositModel bankDepositModel = new BankDepositModel();
            IList<SelectListItem> source = (IList<SelectListItem>)new List<SelectListItem>()
        {
          new SelectListItem() { Text = "Active", Value = "1" },
          new SelectListItem() { Text = "Inactive", Value = "0" }
        };
            ViewBag.Status = source;
            return PartialView("BankDepositModal", bankDepositModel);
        }

        [HttpPost]
        public ActionResult InsertBankDeposit(BankDepositModel bankDepositModel, HttpPostedFileBase file)
        {
            try
            {
                this.dal.InsertBankDeposit(bankDepositModel);
                if (file != null)
                {
                    string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "bank" + bankDepositModel.BankName + ".png");
                    string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\bank" + bankDepositModel.BankName + ".png";
                    if (System.IO.File.Exists(str1))
                    {
                        System.IO.File.Delete(str1);
                    }
                    if (System.IO.File.Exists(str2))
                    {
                        System.IO.File.Delete(str2);
                    }
                    file.SaveAs(str1);
                    file.SaveAs(str2);
                }
                this.Session["message"] = (object)"Bank Deposit is successfully added";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.Session["message"] = (object)ex.Message.ToString();
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        public ActionResult EditBankDeposit(int id)
        {
            BankDepositModel bankDepositListById = this.dal.GetBankDepositListById(id);
            IList<SelectListItem> source = new List<SelectListItem>()
      {
        new SelectListItem() { Text = "Active", Value = "1" },
        new SelectListItem() { Text = "Inactive", Value = "0" }
      };
            ViewBag.Status = source;
            return PartialView("BankDepositModal", bankDepositListById);
        }

        [HttpPost]
        public ActionResult EditBankDeposit(BankDepositModel bankDepositModel, HttpPostedFileBase file)
        {
            try
            {
                this.dal.UpdateBankDeposit(bankDepositModel);
                if (file != null)
                {
                    string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "bank" + bankDepositModel.BankName + ".png");
                    string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\bank" + bankDepositModel.BankName + ".png";
                    if (System.IO.File.Exists(str1))
                    {
                        System.IO.File.Delete(str1);
                    }
                    if (System.IO.File.Exists(str2))
                    {
                        System.IO.File.Delete(str2);
                    }
                    file.SaveAs(str1);
                    file.SaveAs(str2);
                }
                this.Session["message"] = (object)"Bank Deposit is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        public ActionResult DeleteBankDeposit(int id)
        {
            try
            {
                this.dal.DeleteBankDeposit(id);
                this.Session["message"] = (object)"Bank Deposit is successfully deleted";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdatePopUpModal(ContentManagementModel popUpStatus, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), "modal.jpg");
                    string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\modal.jpg";
                    if (System.IO.File.Exists(str1))
                    {
                        System.IO.File.Delete(str1);
                    }
                    if (System.IO.File.Exists(str2))
                    {
                        System.IO.File.Delete(str2);
                    }
                    file.SaveAs(str1);
                    file.SaveAs(str2);
                }

                this.dal.UpdateContent(ContentType.POPUPSTATUS, popUpStatus.Contents);

                this.Session["message"] = (object)"Pop up image is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        public ActionResult InsertPromotion()
        {
            PromotionModel promotionModel = new PromotionModel();
            return PartialView("PromotionModal", promotionModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InsertPromotion(PromotionModel promotionModel, HttpPostedFileBase file)
        {
            try
            {
                this.dal.InsertPromotion(promotionModel);
                if (file != null)
                {
                    string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), promotionModel.PromotionName + ".png");
                    string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\" + promotionModel.PromotionName + ".png";
                    if (System.IO.File.Exists(str1))
                    {
                        System.IO.File.Delete(str1);
                    }
                    if (System.IO.File.Exists(str2))
                    {
                        System.IO.File.Delete(str2);
                    }
                    file.SaveAs(str1);
                    file.SaveAs(str2);
                }
                this.Session["message"] = (object)"Promotion is successfully added";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.Session["message"] = (object)ex.Message.ToString();
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        public ActionResult EditPromotion(int id)
        {
            PromotionModel promotionById = this.dal.GetPromotionById(id);

            return PartialView("PromotionModal", promotionById);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditPromotion(PromotionModel promotionModel, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    this.dal.UpdatePromotion(promotionModel);
                    string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), promotionModel.PromotionName + ".png");
                    string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\" + promotionModel.PromotionName + ".png";
                    if (System.IO.File.Exists(str1))
                    {
                        System.IO.File.Delete(str1);
                    }
                    if (System.IO.File.Exists(str2))
                    {
                        System.IO.File.Delete(str2);
                    }
                    file.SaveAs(str1);
                    file.SaveAs(str2);
                }
                else
                {
                    PromotionModel pm = dal.GetPromotionById(promotionModel.Id);
                    if (pm.PromotionName != promotionModel.PromotionName)
                    {
                        string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), pm.PromotionName + ".png");
                        string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\" + pm.PromotionName + ".png";

                        string str1New = Path.Combine(this.Server.MapPath("~/Resources/images"), promotionModel.PromotionName + ".png");
                        string str2New = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\" + promotionModel.PromotionName + ".png";

                        if (System.IO.File.Exists(str1))
                        {
                            System.IO.File.Move(str1, str1New);
                        }
                        if (System.IO.File.Exists(str2))
                        {
                            System.IO.File.Move(str2, str2New);
                        }
                    }
                    this.dal.UpdatePromotion(promotionModel);
                }
                this.Session["message"] = (object)"Promotion is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        public ActionResult DeletePromotion(int id)
        {
            try
            {
                this.dal.DeletePromotion(id);
                this.Session["message"] = (object)"Promotion is successfully deleted";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        public ActionResult InsertGame()
        {
            GameModel gameModel = new GameModel();
            return PartialView("GameModal", gameModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InsertGame(GameModel gameModel, HttpPostedFileBase file)
        {
            try
            {
                this.dal.InsertGame(gameModel);
                if (file != null)
                {
                    string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), gameModel.GameName + "GAME.png");
                    string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\" + gameModel.GameName + "GAME.png";
                    if (System.IO.File.Exists(str1))
                    {
                        System.IO.File.Delete(str1);
                    }
                    if (System.IO.File.Exists(str2))
                    {
                        System.IO.File.Delete(str2);
                    }
                    file.SaveAs(str1);
                    file.SaveAs(str2);
                }
                this.Session["message"] = (object)"Game is successfully added";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.Session["message"] = (object)ex.Message.ToString();
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult EditGame(int id)
        {
            GameModel gameById = this.dal.GetGameById(id);

            return PartialView("GameModal", gameById);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditGame(GameModel gameModel, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    this.dal.UpdateGame(gameModel);
                    string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), gameModel.GameName + "GAME.png");
                    string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\" + gameModel.GameName + "GAME.png";
                    if (System.IO.File.Exists(str1))
                    {
                        System.IO.File.Delete(str1);
                    }
                    if (System.IO.File.Exists(str2))
                    {
                        System.IO.File.Delete(str2);
                    }
                    file.SaveAs(str1);
                    file.SaveAs(str2);
                }
                else
                {
                    GameModel pm = dal.GetGameById(gameModel.Id);
                    if (pm.GameName != gameModel.GameName)
                    {
                        string str1 = Path.Combine(this.Server.MapPath("~/Resources/images"), pm.GameName + "GAME.png");
                        string str2 = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\" + pm.GameName + "GAME.png";

                        string str1New = Path.Combine(this.Server.MapPath("~/Resources/images"), gameModel.GameName + "GAME.png");
                        string str2New = "C:\\ClientSites\\macan303.com\\httpdocs\\Resources\\images\\" + gameModel.GameName + "GAME.png";

                        if (System.IO.File.Exists(str1))
                        {
                            System.IO.File.Move(str1, str1New);
                        }
                        if (System.IO.File.Exists(str2))
                        {
                            System.IO.File.Move(str2, str2New);
                        }
                    }
                    this.dal.UpdateGame(gameModel);
                }
                this.Session["message"] = (object)"Game is successfully updated";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }

        public ActionResult DeleteGame(int id)
        {
            try
            {
                this.dal.DeleteGame(id);
                this.Session["message"] = (object)"Game is successfully deleted";
                this.Session["requestStatus"] = (object)"Success";
                return (ActionResult)this.RedirectToAction("Index");
            }
            catch
            {
                this.Session["message"] = (object)"Unable to perform this request";
                this.Session["requestStatus"] = (object)"Error";
                return (ActionResult)this.RedirectToAction("Index");
            }
        }
    }
}
