using DPWD_SITE.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using DPWD_SITE.Models.Enum;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DPWD_SITE.DAL
{
    public class DAL
    {
        string constr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        //Dashboard
        #region
        public List<DepositModel> GetLatestDeposits()
        {
            DepositModel dm;
            List<DepositModel> ldm = new List<DepositModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM deposit WHERE Status = 2 ORDER BY DepositDate DESC LIMIT 15";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dm = new DepositModel();
                            dm.Id = Convert.ToInt32(reader["id"]);
                            //dm.UserName = reader["username"].ToString().Remove(reader["username"].ToString().Length - 3) + "***";
                            dm.UserName = reader["username"].ToString();
                            for (int i = 1; i <= dm.UserName.Length; i++)
                            {
                                if (i % 2 != 0)
                                {
                                    dm.UserName = dm.UserName.Substring(0, i - 1) + "*" + dm.UserName.Substring(i, dm.UserName.Length - i);
                                }
                            }
                            dm.DepositAmount = Convert.ToDecimal(reader["depositamount"]);

                            ldm.Add(dm);
                        }
                    }

                    conn.Close();
                }
            }

            return ldm;
        }

        public List<WithdrawalModel> GetLatestWithdrawals()
        {
            WithdrawalModel wm;
            List<WithdrawalModel> lwm = new List<WithdrawalModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM withdrawal WHERE Status = 2 ORDER BY WithdrawalDate DESC LIMIT 15";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wm = new WithdrawalModel();
                            wm.Id = Convert.ToInt32(reader["id"]);
                            //wm.UserName = reader["username"].ToString().Remove(reader["username"].ToString().Length - 3) + "***";
                            wm.UserName = reader["username"].ToString();
                            for (int i = 1; i <= wm.UserName.Length; i++)
                            {
                                if (i % 2 != 0)
                                {
                                    wm.UserName = wm.UserName.Substring(0, i - 1) + "*" + wm.UserName.Substring(i, wm.UserName.Length - i);
                                }
                            }

                            wm.WithdrawalAmount = Convert.ToDecimal(reader["withdrawalamount"]);

                            lwm.Add(wm);
                        }
                    }

                    conn.Close();
                }
            }

            return lwm;
        }
        #endregion
        //End Dashboard

        //Notification
        #region 
        public void InsertNotification(int id, string type)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Notifications " +
                        "(Type, Status, Time, FKId) " +
                        "VALUES(@Type, 1, DATE_ADD(now(), INTERVAL 12 HOUR), @FKId)";
                    cmd.Parameters.AddWithValue("Type", type);
                    cmd.Parameters.AddWithValue("FKId", id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        #endregion
        //End Notification

        //Settings
        #region 
        public List<BankModel> GetBanks()
        {
            BankModel bm;
            List<BankModel> lbm = new List<BankModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM banks ORDER BY Id ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bm = new BankModel();
                            bm.Id = Convert.ToInt32(reader["id"]);
                            bm.BankName = reader["bankname"].ToString();

                            lbm.Add(bm);
                        }
                    }

                    conn.Close();
                }
            }

            return lbm;
        }

        public List<GameTypeModel> GetGameTypes()
        {
            GameTypeModel gtm;
            List<GameTypeModel> lgtm = new List<GameTypeModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM gametype ORDER BY Id ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gtm = new GameTypeModel();
                            gtm.Id = Convert.ToInt32(reader["id"]);
                            gtm.GameType = reader["gametype"].ToString();

                            lgtm.Add(gtm);
                        }
                    }

                    conn.Close();
                }
            }

            return lgtm;
        }
        #endregion
        //End Settings

        //Content Management
        #region
        public List<ContentManagementModel> GetMainContents()
        {
            ContentManagementModel cm;
            List<ContentManagementModel> lcm = new List<ContentManagementModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM contentmanagement WHERE Id IN (1, 2, 26, 27, 31, 32) ORDER BY Id ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cm = new ContentManagementModel();
                            cm.Id = Convert.ToInt32(reader["id"]);
                            cm.Type = (ContentType)Convert.ToInt32(reader["type"]);
                            cm.Contents = reader["contents"].ToString();

                            lcm.Add(cm);
                        }
                    }

                    conn.Close();
                }
            }

            return lcm;
        }

        public List<ContentManagementModel> GetPopUp()
        {
            ContentManagementModel cm;
            List<ContentManagementModel> lcm = new List<ContentManagementModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM contentmanagement WHERE Id IN (33, 34) ORDER BY Id ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cm = new ContentManagementModel();
                            cm.Id = Convert.ToInt32(reader["id"]);
                            cm.Type = (ContentType)Convert.ToInt32(reader["type"]);
                            cm.Contents = reader["contents"].ToString();

                            lcm.Add(cm);
                        }
                    }

                    conn.Close();
                }
            }

            return lcm;
        }

        public ContentManagementModel GetContentsByType(ContentType type)
        {
            ContentManagementModel cm = new ContentManagementModel();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM contentmanagement WHERE Type = @Type ORDER BY Id ASC";
                    cmd.Parameters.AddWithValue("Type", Convert.ToInt32(type));
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cm.Id = Convert.ToInt32(reader["id"]);
                            cm.Type = (ContentType)Convert.ToInt32(reader["type"]);
                            cm.Contents = reader["contents"].ToString();
                        }
                    }

                    conn.Close();
                }
            }

            return cm;
        }

        public List<PromotionModel> GetPromotionList()
        {
            PromotionModel bdm;
            List<PromotionModel> lbdm = new List<PromotionModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM promotion ORDER BY Id ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bdm = new PromotionModel();
                            bdm.Id = Convert.ToInt32(reader["id"]);
                            bdm.PromotionName = reader["promotionname"].ToString();
                            bdm.PromotionDetails = reader["promotiondetails"].ToString();

                            lbdm.Add(bdm);
                        }
                    }

                    conn.Close();
                }
            }

            return lbdm;
        }

        public List<GameModel> GetGamesList()
        {
            GameModel bdm;
            List<GameModel> lbdm = new List<GameModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM gameslist ORDER BY Id ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bdm = new GameModel();
                            bdm.Id = Convert.ToInt32(reader["id"]);
                            bdm.GameName = reader["gamename"].ToString();
                            bdm.GameLandingUrl = reader["gamelandingurl"].ToString();

                            lbdm.Add(bdm);
                        }
                    }

                    conn.Close();
                }
            }

            return lbdm;
        }
        #endregion
        //End Content Management

        //Withdrawal
        #region
        public int InsertWithdrawal(WithdrawalModel model)
        {
            int id = 0;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Withdrawal " +
                        "(BankAccount, BankAccountName, BankAccountNumber, WithdrawalDate, WithdrawalAmount, GameType, Status, Username, ActionDate) " +
                        "VALUES(@BankAccount, @BankAccountName, @BankAccountNumber, DATE_ADD(now(), INTERVAL 12 HOUR), @WithdrawalAmount, @GameType, 1, @UserName, null); SELECT LAST_INSERT_ID();";
                    cmd.Parameters.AddWithValue("BankAccount", Convert.ToInt32(model.BankAccount));
                    cmd.Parameters.AddWithValue("BankAccountName", model.BankAccountNameSubmit);
                    cmd.Parameters.AddWithValue("BankAccountNumber", model.BankAccountNumberSubmit);
                    cmd.Parameters.AddWithValue("WithdrawalAmount", model.WithdrawalAmount);
                    cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                    cmd.Parameters.AddWithValue("UserName", model.UserName);
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conn.Close();
            }
            return id;
        }
        #endregion
        //End Withdrawal


        //Deposit
        #region
        public int InsertDeposit(DepositModel model)
        {
            int id = 0;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Deposit " +
                        "(BankAccount, BankAccountName, BankAccountNumber, DepositDate, DepositAmount, GameType, Status, Username, ActionDate) " +
                        "VALUES(@BankAccount, @BankAccountName, @BankAccountNumber, DATE_ADD(now(), INTERVAL 12 HOUR), @DepositAmount, @GameType, 1, @UserName, null); SELECT LAST_INSERT_ID();";
                    cmd.Parameters.AddWithValue("BankAccount", Convert.ToInt32(model.BankAccount));
                    cmd.Parameters.AddWithValue("BankAccountName", model.BankAccountNameSubmit);
                    cmd.Parameters.AddWithValue("BankAccountNumber", model.BankAccountNumberSubmit);
                    cmd.Parameters.AddWithValue("DepositAmount", model.DepositAmount);
                    cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                    cmd.Parameters.AddWithValue("UserName", model.UserName);
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conn.Close();
            }
            return id;
        }
        #endregion
        //End Deposit


        //Members
        #region
        public int InsertMember(RegistrationModel model)
        {
            int id = 0;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Members " +
                        "(GameType, BankAccount, BankAccountName, BankAccountNumber, Status, PhoneNumber, EmailAddress, RegistrationDate, ActionDate)" +
                        "VALUES(@GameType, @BankAccount, @BankAccountName, @BankAccountNumber, 1, @PhoneNumber, @EmailAddress, DATE_ADD(now(), INTERVAL 12 HOUR), null); SELECT LAST_INSERT_ID()";
                    cmd.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber);
                    cmd.Parameters.AddWithValue("EmailAddress", model.EmailAddress);
                    cmd.Parameters.AddWithValue("BankAccountNumber", model.BankAccountNumber);
                    cmd.Parameters.AddWithValue("BankAccountName", model.BankAccountName);
                    cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                    cmd.Parameters.AddWithValue("BankAccount", Convert.ToInt32(model.BankAccount));
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conn.Close();
            }
            return id;
        }
        
        public RegistrationModel GetMemberByUserName(string userName)
        {
            RegistrationModel m = null;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Members WHERE UserName = @UserName";
                    cmd.Parameters.AddWithValue("UserName", userName);
                    cmd.ExecuteNonQuery();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            m = new RegistrationModel();
                            m.Id = Convert.ToInt32(reader["id"]);
                            m.UserName = reader["username"].ToString();
                            m.Status = (Status)Convert.ToInt32(reader["status"]);
                            m.RegistrationDate = DateTime.Parse(reader["registrationdate"].ToString());
                            m.PhoneNumber = reader["phonenumber"].ToString();
                            m.GameType = Convert.ToInt32(reader["gametype"]);
                            m.EmailAddress = reader["emailaddress"].ToString();
                            m.BankAccountNumber = reader["bankaccountnumber"].ToString();
                            m.BankAccountName = reader["bankaccountname"].ToString();
                            m.BankAccount = Convert.ToInt32(reader["bankaccount"]);
                            if (reader["actiondate"] != null && reader["actiondate"].ToString() != "")
                            {
                                m.ActionDate = DateTime.Parse(reader["actiondate"].ToString());
                            }
                        }
                    }
                }

                conn.Close();
            }

            return m;
        }

        public bool CheckDuplicateBankAccountNumber(string bankAccountNumber)
        {
            bool isDuplicate = false;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT COUNT(*) AS count from members WHERE BankAccountNumber = @BankAccountNumber";
                    cmd.Parameters.AddWithValue("BankAccountNumber", bankAccountNumber.Trim());

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["count"]) > 0)
                            {
                                isDuplicate = true;
                            }
                        }
                    }

                    conn.Close();
                }
            }

            return isDuplicate;
        }

        public bool CheckDuplicateBankAccountNumberPerGame(string bankAccountNumber, int? gameType)
        {
            bool isDuplicate = false;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT COUNT(*) AS count from members WHERE BankAccountNumber = @BankAccountNumber AND GameType = @GameType";
                    cmd.Parameters.AddWithValue("BankAccountNumber", bankAccountNumber.Trim());
                    cmd.Parameters.AddWithValue("GameType", gameType);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["count"]) > 0)
                            {
                                isDuplicate = true;
                            }
                        }
                    }

                    conn.Close();
                }
            }

            return isDuplicate;
        }

        public List<BankDepositModel> GetBankDepositList()
        {
            BankDepositModel bdm;
            List<BankDepositModel> lbdm = new List<BankDepositModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM bankdeposit WHERE Status != 0 ORDER BY Id ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bdm = new BankDepositModel();
                            bdm.Id = Convert.ToInt32(reader["id"]);
                            bdm.BankName = reader["bankname"].ToString();
                            bdm.BankDetails = reader["bankdetails"].ToString();
                            bdm.Status = Convert.ToInt32(reader["status"]);

                            lbdm.Add(bdm);
                        }
                    }

                    conn.Close();
                }
            }

            return lbdm;
        }
        #endregion
        //End Members
    }
}