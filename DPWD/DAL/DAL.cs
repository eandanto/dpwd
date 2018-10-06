using DPWD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using DPWD.Models.Enum;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DPWD.DAL
{
    public class DAL
    {
        string constr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        //Notifications
        #region 
        public List<NotificationModel> GetNotifications()
        {
            NotificationModel n;
            List<NotificationModel> ln = new List<NotificationModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Notifications WHERE Status != 0 ORDER BY Time DESC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            n = new NotificationModel();
                            n.Id = Convert.ToInt32(reader["id"]);
                            n.Type = reader["type"].ToString();
                            n.Status = (NotificationStatus)Convert.ToInt32(reader["status"]);
                            n.Time = DateTime.Parse(reader["time"].ToString());
                            n.Pulled = Convert.ToInt32(reader["pulled"]);

                            ln.Add(n);
                        }
                    }

                    conn.Close();
                }
            }

            return ln;
        }

        public List<NotificationModel> GetPulledNotifications()
        {
            NotificationModel n;
            List<NotificationModel> ln = new List<NotificationModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Notifications WHERE Pulled != 0 ORDER BY Time DESC LIMIT 5";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            n = new NotificationModel();
                            n.Id = Convert.ToInt32(reader["id"]);
                            n.Type = reader["type"].ToString();
                            n.Status = (NotificationStatus)Convert.ToInt32(reader["status"]);
                            n.Time = DateTime.Parse(reader["time"].ToString());
                            n.Pulled = Convert.ToInt32(reader["pulled"]);

                            ln.Add(n);
                        }
                    }

                    conn.Close();
                }
            }

            return ln;
        }

        public List<NotificationModel> GetTopFiveNotifications()
        {
            NotificationModel n;
            List<NotificationModel> ln = new List<NotificationModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Notifications ORDER BY Time DESC LIMIT 5";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            n = new NotificationModel();
                            n.Id = Convert.ToInt32(reader["id"]);
                            n.Type = reader["type"].ToString();
                            n.Status = (NotificationStatus)Convert.ToInt32(reader["status"]);
                            n.Time = DateTime.Parse(reader["time"].ToString());

                            ln.Add(n);
                        }
                    }

                    conn.Close();
                }
            }

            return ln;
        }

        public List<NotificationModel> GetAllNotifications()
        {
            DeleteNotifications();

            NotificationModel n;
            List<NotificationModel> ln = new List<NotificationModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Notifications WHERE ORDER BY Time DESC ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            n = new NotificationModel();
                            n.Id = Convert.ToInt32(reader["id"]);
                            n.Type = reader["type"].ToString();
                            n.Status = (NotificationStatus)Convert.ToInt32(reader["status"]);
                            n.Time = DateTime.Parse(reader["time"].ToString());

                            ln.Add(n);
                        }
                    }

                    conn.Close();
                }
            }

            return ln;
        }

        //public void UpdateNotifications(List<NotificationModel> list)
        //{
        //    using (MySqlConnection conn = new MySqlConnection(constr))
        //    {
        //        conn.Open();

        //        using (MySqlCommand cmd = new MySqlCommand())
        //        {
        //            cmd.Connection = conn;

        //            foreach (var item in list)
        //            {
        //                cmd.CommandText = "UPDATE Notifications SET Status = 0 WHERE Id = " + item.Id + ";";
        //                cmd.ExecuteNonQuery();
        //            }

        //            conn.Close();
        //        }
        //    }
        //}

        public void UpdateNotifications()
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "UPDATE Notifications SET Status = 0;";
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public void UpdateNotifications(int id, string type)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "UPDATE Notifications SET Status = 0 WHERE FKId = " + id.ToString() + " AND Type = '" + type + "';";
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public void UpdateNotificationsByType(string type)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "UPDATE Notifications SET Status = 0 WHERE Type ='" + type + "'";
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }


        public void UpdateNotificationsPulledStatus(List<NotificationModel> list)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    foreach (var item in list)
                    {
                        cmd.CommandText = "UPDATE Notifications SET Pulled = 1 WHERE Id = " + item.Id + ";";
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
        }

        public void DeleteNotifications()
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE Notifications WHERE Time < @date;";
                    cmd.Parameters.AddWithValue("date", Convert.ToDateTime(DateTime.Now.AddDays(-30)));
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
        #endregion
        //End Notifications

        //Content
        #region
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

                    cmd.CommandText = "SELECT * FROM bankdeposit ORDER BY Id ASC";

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

        public BankDepositModel GetBankDepositListById(int id)
        {
            BankDepositModel bdm = new BankDepositModel();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM bankdeposit WHERE Id = @Id ORDER BY Id ASC";
                    cmd.Parameters.AddWithValue("Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bdm = new BankDepositModel();
                            bdm.Id = Convert.ToInt32(reader["id"]);
                            bdm.BankName = reader["bankname"].ToString();
                            bdm.BankDetails = reader["bankdetails"].ToString();
                            bdm.Status = Convert.ToInt32(reader["status"]);
                        }
                    }

                    conn.Close();
                }
            }

            return bdm;
        }

        public void InsertBankDeposit(BankDepositModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO bankdeposit " +
                        "(BankName, BankDetails, Status) " +
                        "VALUES(@BankName, @BankDetails, @Status);";
                    cmd.Parameters.AddWithValue("BankName", model.BankName);
                    cmd.Parameters.AddWithValue("BankDetails", model.BankDetails);
                    cmd.Parameters.AddWithValue("Status", Convert.ToInt32(model.Status));

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void UpdateBankDeposit(BankDepositModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE bankdeposit SET " +
                    "BankName = @BankName, BankDetails = @BankDetails, Status = @Status WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(model.Id));
                    cmd.Parameters.AddWithValue("Status", Convert.ToInt32(model.Status));
                    cmd.Parameters.AddWithValue("BankName", model.BankName);
                    cmd.Parameters.AddWithValue("BankDetails", model.BankDetails);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public void DeleteBankDeposit(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM bankdeposit WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
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

        public PromotionModel GetPromotionById(int id)
        {
            PromotionModel bdm = new PromotionModel();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM promotion WHERE Id = @Id ORDER BY Id ASC";
                    cmd.Parameters.AddWithValue("Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bdm = new PromotionModel();
                            bdm.Id = Convert.ToInt32(reader["id"]);
                            bdm.PromotionName = reader["promotionname"].ToString();
                            bdm.PromotionDetails = reader["promotiondetails"].ToString();
                        }
                    }

                    conn.Close();
                }
            }

            return bdm;
        }

        public void InsertPromotion(PromotionModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO promotion " +
                        "(PromotionName, PromotionDetails) " +
                        "VALUES(@PromotionName, @PromotionDetails);";
                    cmd.Parameters.AddWithValue("PromotionName", model.PromotionName);
                    cmd.Parameters.AddWithValue("PromotionDetails", model.PromotionDetails);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void UpdatePromotion(PromotionModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE promotion SET " +
                    "PromotionName = @PromotionName, PromotionDetails = @PromotionDetails WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(model.Id));
                    cmd.Parameters.AddWithValue("PromotionName", model.PromotionName);
                    cmd.Parameters.AddWithValue("PromotionDetails", model.PromotionDetails);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public void DeletePromotion(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM promotion WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
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

        public GameModel GetGameById(int id)
        {
            GameModel bdm = new GameModel();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM gameslist WHERE Id = @Id ORDER BY Id ASC";
                    cmd.Parameters.AddWithValue("Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bdm = new GameModel();
                            bdm.Id = Convert.ToInt32(reader["id"]);
                            bdm.GameName = reader["gamename"].ToString();
                            bdm.GameLandingUrl = reader["gamelandingurl"].ToString();
                        }
                    }

                    conn.Close();
                }
            }

            return bdm;
        }

        public void InsertGame(GameModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO gameslist " +
                        "(GameName, GameLandingUrl) " +
                        "VALUES(@GameName, @GameLandingUrl);";
                    cmd.Parameters.AddWithValue("GameName", model.GameName);
                    cmd.Parameters.AddWithValue("GameLandingUrl", model.GameLandingUrl);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void UpdateGame(GameModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE gameslist SET " +
                    "GameName = @GameName, GameLandingUrl = @GameLandingUrl WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(model.Id));
                    cmd.Parameters.AddWithValue("GameName", model.GameName);
                    cmd.Parameters.AddWithValue("GameLandingUrl", model.GameLandingUrl);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public void DeleteGame(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM gameslist WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void UpdateContent(ContentType type, string contents)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE contentmanagement SET " +
                    "Contents = @Contents WHERE Type = @Type;";

                    cmd.Parameters.AddWithValue("Type", Convert.ToInt32(type));
                    cmd.Parameters.AddWithValue("Contents", contents);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public List<ContentManagementModel> GetContents()
        {
            ContentManagementModel cm;
            List<ContentManagementModel> lcm = new List<ContentManagementModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM contentmanagement ORDER BY Id ASC";

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
        #endregion
        //End Content

        //Settings
        #region
        public SettingsViewModel GetSettings()
        {
            SettingsViewModel svm = new SettingsViewModel();
            BankModel bm;
            List<BankModel> lbm = new List<BankModel>();
            GameTypeModel gtm;
            List<GameTypeModel> lgtm = new List<GameTypeModel>();
            UserNameModel um;
            List<UserNameModel> lum = new List<UserNameModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM banks WHERE Id > 1 ORDER BY Id ASC";

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

                    svm.BankModelList = lbm;
                    cmd.CommandText = "SELECT * FROM gametype WHERE Id > 1 ORDER BY Id ASC";

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

                    svm.GameTypeModelList = lgtm;
                    cmd.CommandText = "SELECT u.*, g.GameType as gametypename FROM username u LEFT JOIN gametype g ON u.GameType = g.Id ORDER BY Id DESC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            um = new UserNameModel();
                            um.Id = Convert.ToInt32(reader["id"]);
                            um.UserName = reader["username"].ToString();
                            um.GameTypeName = reader["gametypename"].ToString();
                            um.Availability = Convert.ToInt32(reader["availability"].ToString());
                            um.DateCreated = Convert.ToDateTime(reader["datecreated"].ToString());
                            if (reader["dateassigned"] != null && reader["dateassigned"].ToString() != "")
                            {
                                um.DateAssigned = Convert.ToDateTime(reader["dateassigned"].ToString());
                            }
                            else
                            {
                                um.DateAssigned = null;
                            }

                            lum.Add(um);
                        }
                    }

                    svm.UserNameModelList = lum;

                    conn.Close();
                }
            }

            return svm;
        }

        public SettingsViewModel GetSettings(string username, Availability availability, int? gameType)
        {
            SettingsViewModel svm = new SettingsViewModel();
            BankModel bm;
            List<BankModel> lbm = new List<BankModel>();
            GameTypeModel gtm;
            List<GameTypeModel> lgtm = new List<GameTypeModel>();
            UserNameModel um;
            List<UserNameModel> lum = new List<UserNameModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM banks WHERE Id > 1 ORDER BY Id ASC";

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

                    svm.BankModelList = lbm;
                    cmd.CommandText = "SELECT * FROM gametype WHERE Id > 1 ORDER BY Id ASC";

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

                    svm.GameTypeModelList = lgtm;

                    if (username == null)
                    {
                        username = "";
                    }
                    string sql = "SELECT u.*, g.GameType as gametypename FROM username u LEFT JOIN gametype g ON u.GameType = g.Id WHERE username LIKE '%" + username + "%'";
                    if (availability != Availability.Select)
                    {
                        sql += " AND availability LIKE '%" + (Convert.ToInt32(availability) - 1) + "%'";
                    }
                    if (gameType != null && gameType != 0)
                    {
                        sql += " AND gametype LIKE '%" + (Convert.ToInt32(gameType)) + "%'";
                    }
                    cmd.CommandText = sql;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            um = new UserNameModel();
                            um.Id = Convert.ToInt32(reader["id"]);
                            um.UserName = reader["username"].ToString();
                            um.GameTypeName = reader["gametypename"].ToString();
                            um.Availability = Convert.ToInt32(reader["availability"].ToString());
                            um.DateCreated = Convert.ToDateTime(reader["datecreated"].ToString());
                            if (reader["dateassigned"] != null && reader["dateassigned"].ToString() != "")
                            {
                                um.DateAssigned = Convert.ToDateTime(reader["dateassigned"].ToString());
                            }
                            else
                            {
                                um.DateAssigned = null;
                            }

                            lum.Add(um);
                        }
                    }

                    svm.UserNameModelList = lum;

                    conn.Close();
                }
            }

            return svm;
        }

        public UserNameModel GetUserNameById(int id)
        {
            UserNameModel um = null;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT u.*, g.GameType as gametypename FROM username u LEFT JOIN gametype g ON u.GameType = g.Id WHERE u.Id = @Id";
                    cmd.Parameters.AddWithValue("Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            um = new UserNameModel();
                            um.Id = Convert.ToInt32(reader["Id"]);
                            um.UserName = reader["UserName"].ToString();
                            um.GameType = Convert.ToInt32(reader["GameType"]);
                        }
                    }

                    conn.Close();
                }
            }

            return um;
        }

        public void InsertBank(BankModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO banks" +
                        "(BankName)" +
                        "VALUES(@BankName);";

                    cmd.Parameters.AddWithValue("BankName", model.BankName);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void InsertUserName(UserNameModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO username" +
                        "(Username, GameType, Availability, DateCreated)" +
                        "VALUES(@Username, @GameType, 0, DATE_ADD(now(), INTERVAL 12 HOUR));";

                    cmd.Parameters.AddWithValue("Username", model.UserName);
                    cmd.Parameters.AddWithValue("GameType", model.GameType);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void InsertUserNameAssigned(UserNameModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO username" +
                        "(Username, Availability, DateCreated, DateAssigned)" +
                        "VALUES(@Username, @GameType, 1, DATE_ADD(now(), INTERVAL 12 HOUR), DATE_ADD(now(), INTERVAL 12 HOUR));";

                    cmd.Parameters.AddWithValue("Username", model.UserName);
                    cmd.Parameters.AddWithValue("GameType", model.GameType);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void UpdateUserName(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE username SET " +
                    "Availability = 1, DateAssigned = DATE_ADD(now(), INTERVAL 12 HOUR) WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(id));
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public void EditUserName(UserNameModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE username SET " +
                    "Username = @Username, GameType = @GameType WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(model.Id));
                    cmd.Parameters.AddWithValue("Username", model.UserName);
                    cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public void InsertGameType(GameTypeModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO gametype" +
                        "(GameType)" +
                        "VALUES(@GameType);";

                    cmd.Parameters.AddWithValue("GameType", model.GameType);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void DeleteBank(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM banks WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void DeleteUserName(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM username WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void DeleteGameType(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM gametype WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

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

        public List<UserNameModel> GetUserNames()
        {
            UserNameModel um;
            List<UserNameModel> lum = new List<UserNameModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM username WHERE Availability != 1 ORDER BY UserName ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            um = new UserNameModel();
                            um.Id = Convert.ToInt32(reader["id"]);
                            um.UserName = reader["username"].ToString();
                            um.Availability = Convert.ToInt32(reader["availability"].ToString());
                            um.DateCreated = Convert.ToDateTime(reader["datecreated"].ToString());
                            if (reader["dateassigned"] != null && reader["dateassigned"].ToString() != "")
                            {
                                um.DateAssigned = Convert.ToDateTime(reader["dateassigned"].ToString());
                            }
                            else
                            {
                                um.DateAssigned = null;
                            }

                            lum.Add(um);
                        }
                    }

                    conn.Close();
                }
            }

            return lum;
        }

        public List<UserNameModel> GetUserNamesByGameType(int? gameType)
        {
            UserNameModel um;
            List<UserNameModel> lum = new List<UserNameModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    string sql = "SELECT * FROM username WHERE ";
                    if (gameType != null)
                    {
                        sql += "GameType = @GameType AND ";
                        cmd.Parameters.AddWithValue("GameType", gameType);
                    }
                    sql += "Availability != 1 ORDER BY UserName ASC";
                    cmd.CommandText = sql;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            um = new UserNameModel();
                            um.Id = Convert.ToInt32(reader["id"]);
                            um.UserName = reader["username"].ToString();
                            um.Availability = Convert.ToInt32(reader["availability"].ToString());
                            um.DateCreated = Convert.ToDateTime(reader["datecreated"].ToString());
                            if (reader["dateassigned"] != null && reader["dateassigned"].ToString() != "")
                            {
                                um.DateAssigned = Convert.ToDateTime(reader["dateassigned"].ToString());
                            }
                            else
                            {
                                um.DateAssigned = null;
                            }

                            lum.Add(um);
                        }
                    }

                    conn.Close();
                }
            }

            return lum;
        }
        #endregion
        //End Settings

        //Withdrawal
        #region
        public void InsertWithdrawal(WithdrawalModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Withdrawal " +
                        "(BankAccount, BankAccountName, BankAccountNumber, WithdrawalAmount, WithdrawalDate, GameType, Status, Username, ActionDate, Notes) " +
                       "VALUES(@BankAccount, @BankAccountName, @BankAccountNumber, @WithdrawalAmount, DATE_ADD(now(), INTERVAL 12 HOUR), @GameType, 1, @UserName, null, @Notes);";
                    cmd.Parameters.AddWithValue("BankAccount", Convert.ToInt32(model.BankAccount));
                    cmd.Parameters.AddWithValue("BankAccountName", model.BankAccountName);
                    cmd.Parameters.AddWithValue("BankAccountNumber", model.BankAccountNumber);
                    cmd.Parameters.AddWithValue("WithdrawalAmount", model.WithdrawalAmount);
                    cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                    cmd.Parameters.AddWithValue("UserName", model.UserName);
                    cmd.Parameters.AddWithValue("Notes", model.Notes);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public List<WithdrawalModel> GetWithdrawals(WithdrawalSearchModel model)
        {
            WithdrawalModel w;
            List<WithdrawalModel> lw = new List<WithdrawalModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (model != null)
                    {
                        cmd.CommandText = "SELECT w.*, b.BankName, g.GameType AS GameTypeName FROM withdrawal AS w INNER JOIN banks AS b ON w.BankAccount  = b.Id INNER JOIN gametype AS g ON w.GameType = g.id WHERE Username LIKE @Username " +
                        "AND BankAccountNumber LIKE @BankAccountNumber AND BankAccountName LIKE @BankAccountName";
                        if (model.Status != null && model.Status != Status.Select)
                        {
                            cmd.CommandText += " AND Status = @Status";
                            cmd.Parameters.AddWithValue("Status", Convert.ToInt32(model.Status));
                        }
                        if (model.GameType != null && model.GameType != 1)
                        {
                            cmd.CommandText += " AND w.GameType = @GameType";
                            cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                        }
                        if (model.BankAccount != null && model.BankAccount != 1)
                        {
                            cmd.CommandText += " AND BankAccount = @BankAccount";
                            cmd.Parameters.AddWithValue("BankAccount", Convert.ToInt32(model.BankAccount));
                        }
                        if (model.StartDate != null)
                        {
                            cmd.CommandText += " AND WithdrawalDate >= @StartDate";
                            cmd.Parameters.AddWithValue("StartDate", model.StartDate?.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        if (model.EndDate != null)
                        {
                            cmd.CommandText += " AND WithdrawalDate <= @EndDate";
                            cmd.Parameters.AddWithValue("EndDate", model.EndDate?.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        cmd.CommandText += " ORDER BY WithdrawalDate DESC";
                        cmd.Parameters.AddWithValue("UserName", model.UserName != null ? "%" + model.UserName + "%" : "%%");
                        cmd.Parameters.AddWithValue("BankAccountNumber", model.BankAccountNumber != null ? "%" + model.BankAccountNumber + "%" : "%%");
                        cmd.Parameters.AddWithValue("BankAccountName", model.BankAccountName != null ? "%" + model.BankAccountName + "%" : "%%");
                    }
                    else
                    {
                        cmd.CommandText = "SELECT * FROM Withdrawal ORDER BY WithdrawalDate DES";
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            w = new WithdrawalModel();
                            w.Id = Convert.ToInt32(reader["id"]);
                            w.UserName = reader["username"].ToString();
                            w.Status = (Status)Convert.ToInt32(reader["status"]);
                            w.WithdrawalDate = DateTime.Parse(reader["withdrawaldate"].ToString());
                            w.GameType = Convert.ToInt32(reader["gametype"]);
                            w.GameTypeName = reader["gametypename"].ToString();
                            w.WithdrawalAmount = Convert.ToDecimal(reader["WithdrawalAmount"].ToString());
                            w.BankAccountNumber = reader["bankaccountnumber"].ToString();
                            w.BankAccountName = reader["bankaccountname"].ToString();
                            w.BankAccount = Convert.ToInt32(reader["bankaccount"]);
                            w.BankName = reader["bankname"].ToString();
                            if (reader["actiondate"] != null && reader["actiondate"].ToString() != "")
                            {
                                w.ActionDate = DateTime.Parse(reader["actiondate"].ToString());
                            }
                            w.Notes = reader["notes"].ToString();

                            lw.Add(w);
                        }
                    }

                    conn.Close();
                }
            }

            return lw;
        }

        public List<WithdrawalModel> GetTodayWithdrawals()
        {
            WithdrawalModel w;
            List<WithdrawalModel> lw = new List<WithdrawalModel>();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT w.*, b.BankName, g.GameType AS GameTypeName FROM withdrawal AS w INNER JOIN banks AS b ON w.BankAccount  = b.Id INNER JOIN gametype AS g ON w.GameType = g.id WHERE WithdrawalDate >= '" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            w = new WithdrawalModel();
                            w.Id = Convert.ToInt32(reader["id"]);
                            w.UserName = reader["username"].ToString();
                            w.Status = (Status)Convert.ToInt32(reader["status"]);
                            w.WithdrawalDate = DateTime.Parse(reader["withdrawaldate"].ToString());
                            w.GameType = Convert.ToInt32(reader["gametype"]);
                            w.GameTypeName = reader["gametypename"].ToString();
                            w.WithdrawalAmount = Convert.ToDecimal(reader["WithdrawalAmount"].ToString());
                            w.BankAccountNumber = reader["bankaccountnumber"].ToString();
                            w.BankAccountName = reader["bankaccountname"].ToString();
                            w.BankAccount = Convert.ToInt32(reader["bankaccount"]);
                            w.BankName = reader["bankname"].ToString();
                            if (reader["actiondate"] != null && reader["actiondate"].ToString() != "")
                            {
                                w.ActionDate = DateTime.Parse(reader["actiondate"].ToString());
                            }
                            w.Notes = reader["notes"].ToString();

                            lw.Add(w);
                        }
                    }

                    conn.Close();
                }
            }

            return lw;
        }

        public WithdrawalModel GetWithdrawalById(int id)
        {
            WithdrawalModel w = null;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT w.*, b.BankName, g.GameType AS GameTypeName FROM withdrawal AS w INNER JOIN banks AS b ON w.BankAccount  = b.Id INNER JOIN gametype AS g ON w.GameType = g.id WHERE w.Id = @Id";
                    cmd.Parameters.AddWithValue("Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            w = new WithdrawalModel();
                            w.Id = Convert.ToInt32(reader["id"]);
                            w.UserName = reader["username"].ToString();
                            w.Status = (Status)Convert.ToInt32(reader["status"]);
                            w.WithdrawalDate = DateTime.Parse(reader["withdrawaldate"].ToString());
                            w.GameType = Convert.ToInt32(reader["gametype"]);
                            w.GameTypeName = reader["gametypename"].ToString();
                            w.WithdrawalAmount = Convert.ToDecimal(reader["WithdrawalAmount"].ToString());
                            w.BankAccountNumber = reader["bankaccountnumber"].ToString();
                            w.BankAccountName = reader["bankaccountname"].ToString();
                            w.BankAccount = Convert.ToInt32(reader["bankaccount"]);
                            w.BankName = reader["bankname"].ToString();
                            if (reader["actiondate"] != null && reader["actiondate"].ToString() != "")
                            {
                                w.ActionDate = DateTime.Parse(reader["actiondate"].ToString());
                            }
                            w.Notes = reader["notes"].ToString();
                        }
                    }

                    conn.Close();
                }
            }

            return w;
        }

        public void EditWithdrawalNotes(WithdrawalModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Withdrawal SET " +
                        "Notes = @Notes WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(model.Id));
                    cmd.Parameters.AddWithValue("Notes", model.Notes);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void UpdateWithdrawalStatus(int id, string action)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Withdrawal SET " +
                    "Status = @Status, ActionDate = DATE_ADD(now(), INTERVAL 12 HOUR) WHERE Id = @Id;";
                    if (action == "Approve")
                    {
                        cmd.Parameters.AddWithValue("Status", Convert.ToInt32(Status.APPROVED));
                    }
                    else if (action == "Pending")
                    {
                        cmd.Parameters.AddWithValue("Status", Convert.ToInt32(Status.PENDING));
                    }
                    else if (action == "Reject")
                    {
                        cmd.Parameters.AddWithValue("Status", Convert.ToInt32(Status.REJECTED));
                    }

                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(id));
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
        #endregion
        //End Withdrawal


        //Deposit
        #region
        public void InsertDeposit(DepositModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Deposit " +
                        "(BankAccount, BankAccountName, BankAccountNumber, DepositAmount, DepositDate, GameType, Status, Username, ActionDate, Notes) " +
                        "VALUES(@BankAccount, @BankAccountName, @BankAccountNumber, @DepositAmount, DATE_ADD(now(), INTERVAL 12 HOUR), @GameType, 1, @UserName, null, @Notes);";
                    cmd.Parameters.AddWithValue("BankAccount", Convert.ToInt32(model.BankAccount));
                    cmd.Parameters.AddWithValue("BankAccountName", model.BankAccountName);
                    cmd.Parameters.AddWithValue("BankAccountNumber", model.BankAccountNumber);
                    cmd.Parameters.AddWithValue("DepositAmount", model.DepositAmount);
                    cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                    cmd.Parameters.AddWithValue("UserName", model.UserName);
                    cmd.Parameters.AddWithValue("Notes", model.Notes);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public List<DepositModel> GetDeposits(DepositSearchModel model)
        {
            DepositModel d;
            List<DepositModel> ld = new List<DepositModel>();
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (model != null)
                    {
                        cmd.CommandText = "SELECT d.*, b.BankName, g.GameType AS GameTypeName FROM deposit AS d INNER JOIN banks AS b ON d.BankAccount  = b.Id INNER JOIN gametype AS g ON d.GameType = g.id WHERE UserName LIKE @UserName " +
                        "AND BankAccountNumber LIKE @BankAccountNumber AND BankAccountName LIKE @BankAccountName";
                        if (model.Status != null && model.Status != Status.Select)
                        {
                            cmd.CommandText += " AND Status = @Status";
                            cmd.Parameters.AddWithValue("Status", Convert.ToInt32(model.Status));
                        }
                        if (model.GameType != null && model.GameType != 1)
                        {
                            cmd.CommandText += " AND d.GameType = @GameType";
                            cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                        }
                        if (model.BankAccount != null && model.BankAccount != 1)
                        {
                            cmd.CommandText += " AND BankAccount = @BankAccount";
                            cmd.Parameters.AddWithValue("BankAccount", Convert.ToInt32(model.BankAccount));
                        }
                        if (model.StartDate != null)
                        {
                            cmd.CommandText += " AND DepositDate >= '" + model.StartDate?.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        }
                        if (model.EndDate != null)
                        {
                            cmd.CommandText += " AND DepositDate <= '" + model.EndDate?.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        }

                        cmd.CommandText += " ORDER BY DepositDate DESC";
                        cmd.Parameters.AddWithValue("UserName", model.UserName != null ? "%" + model.UserName + "%" : "%%");
                        cmd.Parameters.AddWithValue("BankAccountNumber", model.BankAccountNumber != null ? "%" + model.BankAccountNumber + "%" : "%%");
                        cmd.Parameters.AddWithValue("BankAccountName", model.BankAccountName != null ? "%" + model.BankAccountName + "%" : "%%");
                    }
                    else
                    {
                        cmd.CommandText = "SELECT * FROM Deposit ORDER BY DepositDate DESC";
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            d = new DepositModel();
                            d.Id = Convert.ToInt32(reader["id"]);
                            d.UserName = reader["username"].ToString();
                            d.Status = (Status)Convert.ToInt32(reader["status"]);
                            d.DepositDate = DateTime.Parse(reader["depositdate"].ToString());
                            d.DepositAmount = Convert.ToDecimal(reader["depositamount"].ToString());
                            d.GameType = Convert.ToInt32(reader["gametype"]);
                            d.BankAccountNumber = reader["bankaccountnumber"].ToString();
                            d.BankAccountName = reader["bankaccountname"].ToString();
                            d.BankAccount = Convert.ToInt32(reader["bankaccount"]);
                            d.BankName = reader["bankname"].ToString();
                            d.GameTypeName = reader["gametypename"].ToString();
                            if (reader["actiondate"] != null && reader["actiondate"].ToString() != "")
                            {
                                d.ActionDate = DateTime.Parse(reader["actiondate"].ToString());
                            }
                            d.Notes = reader["notes"].ToString();

                            ld.Add(d);
                        }
                    }

                    conn.Close();
                }
            }

            return ld;
        }

        public List<DepositModel> GetTodayDeposits()
        {
            DepositModel d;
            List<DepositModel> ld = new List<DepositModel>();
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT d.*, b.BankName, g.GameType AS GameTypeName FROM deposit AS d INNER JOIN banks AS b ON d.BankAccount  = b.Id INNER JOIN gametype AS g ON d.GameType = g.id WHERE DepositDate >= '" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            d = new DepositModel();
                            d.Id = Convert.ToInt32(reader["id"]);
                            d.UserName = reader["username"].ToString();
                            d.Status = (Status)Convert.ToInt32(reader["status"]);
                            d.DepositDate = DateTime.Parse(reader["depositdate"].ToString());
                            d.DepositAmount = Convert.ToDecimal(reader["depositamount"].ToString());
                            d.GameType = Convert.ToInt32(reader["gametype"]);
                            d.BankAccountNumber = reader["bankaccountnumber"].ToString();
                            d.BankAccountName = reader["bankaccountname"].ToString();
                            d.BankAccount = Convert.ToInt32(reader["bankaccount"]);
                            d.BankName = reader["bankname"].ToString();
                            d.GameTypeName = reader["gametypename"].ToString();
                            if (reader["actiondate"] != null && reader["actiondate"].ToString() != "")
                            {
                                d.ActionDate = DateTime.Parse(reader["actiondate"].ToString());
                            }
                            d.Notes = reader["notes"].ToString();

                            ld.Add(d);
                        }
                    }

                    conn.Close();
                }
            }

            return ld;
        }

        public DepositModel GetDepositById(int id)
        {
            DepositModel d = null;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT d.*, b.BankName, g.GameType AS GameTypeName FROM deposit AS d INNER JOIN banks AS b ON d.BankAccount  = b.Id INNER JOIN gametype AS g ON d.GameType = g.id WHERE d.Id = @Id";
                    cmd.Parameters.AddWithValue("Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            d = new DepositModel();
                            d.Id = Convert.ToInt32(reader["id"]);
                            d.UserName = reader["username"].ToString();
                            d.Status = (Status)Convert.ToInt32(reader["status"]);
                            d.DepositDate = DateTime.Parse(reader["depositdate"].ToString());
                            d.DepositAmount = Convert.ToDecimal(reader["depositamount"].ToString());
                            d.GameType = Convert.ToInt32(reader["gametype"]);
                            d.BankAccountNumber = reader["bankaccountnumber"].ToString();
                            d.BankAccountName = reader["bankaccountname"].ToString();
                            d.BankAccount = Convert.ToInt32(reader["bankaccount"]);
                            d.BankName = reader["bankname"].ToString();
                            d.GameTypeName = reader["gametypename"].ToString();
                            if (reader["actiondate"] != null && reader["actiondate"].ToString() != "")
                            {
                                d.ActionDate = DateTime.Parse(reader["actiondate"].ToString());
                            }
                            d.Notes = reader["notes"].ToString();
                        }
                    }

                    conn.Close();
                }
            }

            return d;
        }

        public void UpdateDepositStatus(int id, string action)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Deposit SET " +
                        "Status = @Status, ActionDate = DATE_ADD(now(), INTERVAL 12 HOUR) WHERE Id = @Id;";
                    if (action == "Approve")
                    {
                        cmd.Parameters.AddWithValue("Status", Convert.ToInt32(Status.APPROVED));
                    }
                    else if (action == "Pending")
                    {
                        cmd.Parameters.AddWithValue("Status", Convert.ToInt32(Status.PENDING));
                    }
                    else if (action == "Reject")
                    {
                        cmd.Parameters.AddWithValue("Status", Convert.ToInt32(Status.REJECTED));
                    }

                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(id));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void EditDepositNotes(DepositModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Deposit SET " +
                        "Notes = @Notes WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(model.Id));
                    cmd.Parameters.AddWithValue("Notes", model.Notes);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
        #endregion
        //End Deposit


        //Members
        #region
        public void InsertMember(MemberModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Members" +
                        "(UserName, GameType, BankAccount, BankAccountName, BankAccountNumber, Status, PhoneNumber, EmailAddress, RegistrationDate, ActionDate, Notes)" +
                        "VALUES(@UserName, @GameType, @BankAccount, @BankAccountName, @BankAccountNumber, 1, @PhoneNumber, @EmailAddress, DATE_ADD(now(), INTERVAL 12 HOUR), null, @Notes);";

                    cmd.Parameters.AddWithValue("UserName", model.UserName);
                    cmd.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber);
                    cmd.Parameters.AddWithValue("EmailAddress", model.EmailAddress);
                    cmd.Parameters.AddWithValue("BankAccountNumber", model.BankAccountNumber);
                    cmd.Parameters.AddWithValue("BankAccountName", model.BankAccountName);
                    cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                    cmd.Parameters.AddWithValue("BankAccount", Convert.ToInt32(model.BankAccount));
                    cmd.Parameters.AddWithValue("Notes", model.Notes);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void EditMember(MemberModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Members SET " +
                        "GameType = @GameType, BankAccount = @BankAccount, " +
                        "BankAccountName = @BankAccountName, BankAccountNumber = @BankAccountNumber, Status = @Status, " +
                        "PhoneNumber = @PhoneNumber, EmailAddress = @EmailAddress, Notes = @Notes";
                    if (model.UserName != null)
                    {
                        cmd.CommandText += ", UserName = @UserName";
                        cmd.Parameters.AddWithValue("UserName", model.UserName);
                    }
                    if (model.Status != Status.PENDING)
                    {
                        cmd.CommandText += ", ActionDate = DATE_ADD(now(), INTERVAL 12 HOUR)";
                    }

                    cmd.CommandText += " WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber);
                    cmd.Parameters.AddWithValue("EmailAddress", model.EmailAddress);
                    cmd.Parameters.AddWithValue("BankAccountNumber", model.BankAccountNumber);
                    cmd.Parameters.AddWithValue("BankAccountName", model.BankAccountName);
                    cmd.Parameters.AddWithValue("Status", Convert.ToInt32(model.Status));
                    cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                    cmd.Parameters.AddWithValue("BankAccount", Convert.ToInt32(model.BankAccount));
                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(model.Id));
                    cmd.Parameters.AddWithValue("Notes", model.Notes);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void EditMemberNotes(MemberModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Members SET " +
                        "Notes = @Notes WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(model.Id));
                    cmd.Parameters.AddWithValue("Notes", model.Notes);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void ReAssignMember(MemberModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Members SET " +
                        "UserName = @UserName, GameType = @GameType, Password = @Password, Status = 2 WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("UserName", model.UserName);
                    cmd.Parameters.AddWithValue("Password", model.Password);
                    cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(model.Id));
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public List<MemberModel> GetMembers(MemberModel model)
        {
            MemberModel m;
            List<MemberModel> lm = new List<MemberModel>();
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (model != null)
                    {
                        cmd.CommandText = "SELECT m.*, b.BankName AS BankName, g.GameType AS GameTypeName FROM members AS m INNER JOIN banks AS b ON m.BankAccount  = b.Id INNER JOIN gametype AS g ON m.GameType = g.id WHERE PhoneNumber Like @PhoneNumber " +
                        "AND EmailAddress LIKE @EmailAddress AND BankAccountNumber LIKE @BankAccountNumber AND BankAccountName LIKE @BankAccountName";
                        if (model.UserName != null && model.UserName != "")
                        {
                            cmd.CommandText += " AND UserName LIKE @UserName";
                            cmd.Parameters.AddWithValue("UserName", model.UserName);
                        }
                        if (model.Status != null && model.Status != Status.Select)
                        {
                            cmd.CommandText += " AND Status = @Status";
                            cmd.Parameters.AddWithValue("Status", Convert.ToInt32(model.Status));
                        }
                        if (model.GameType != null && model.GameType != 1)
                        {
                            cmd.CommandText += " AND m.GameType = @GameType";
                            cmd.Parameters.AddWithValue("GameType", Convert.ToInt32(model.GameType));
                        }
                        if (model.BankAccount != null && model.BankAccount != 1)
                        {
                            cmd.CommandText += " AND BankAccount = @BankAccount";
                            cmd.Parameters.AddWithValue("BankAccount", Convert.ToInt32(model.BankAccount));
                        }

                        cmd.CommandText += " ORDER BY RegistrationDate DESC";
                        cmd.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber != null ? "%" + model.PhoneNumber + "%" : "%%");
                        cmd.Parameters.AddWithValue("EmailAddress", model.EmailAddress != null ? "%" + model.EmailAddress + "%" : "%%");
                        cmd.Parameters.AddWithValue("BankAccountNumber", model.BankAccountNumber != null ? "%" + model.BankAccountNumber + "%" : "%%");
                        cmd.Parameters.AddWithValue("BankAccountName", model.BankAccountName != null ? "%" + model.BankAccountName + "%" : "%%");
                    }
                    else
                    {
                        cmd.CommandText = "SELECT m.*, b.BankName AS BankName, g.GameType AS GameTypeName FROM members AS m INNER JOIN banks AS b ON m.BankAccount  = b.Id INNER JOIN gametype AS g ON m.GameType = g.id ORDER BY RegistrationDate DESC";
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            m = new MemberModel();
                            m.Id = Convert.ToInt32(reader["id"]);
                            m.UserName = reader["username"].ToString();
                            m.Status = (Status)Convert.ToInt32(reader["status"]);
                            m.RegistrationDate = DateTime.Parse(reader["registrationdate"].ToString());
                            m.PhoneNumber = reader["phonenumber"].ToString();
                            m.GameType = Convert.ToInt32(reader["gametype"]);
                            m.GameTypeName = reader["gametypename"].ToString();
                            m.EmailAddress = reader["emailaddress"].ToString();
                            m.BankAccountNumber = reader["bankaccountnumber"].ToString();
                            m.BankAccountName = reader["bankaccountname"].ToString();
                            m.BankAccount = Convert.ToInt32(reader["bankaccount"]);
                            m.BankName = reader["bankname"].ToString();
                            if (reader["actiondate"] != null && reader["actiondate"].ToString() != "")
                            {
                                m.ActionDate = DateTime.Parse(reader["actiondate"].ToString());
                            }
                            m.Notes = reader["notes"].ToString();

                            lm.Add(m);
                        }
                    }

                    conn.Close();
                }
            }

            return lm;
        }

        public MemberModel GetMemberById(int id)
        {
            MemberModel m = null;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT m.*, b.BankName, g.GameType AS GameTypeName FROM members AS m INNER JOIN banks AS b ON m.BankAccount  = b.Id INNER JOIN gametype AS g ON m.GameType = g.id WHERE m.Id = @Id";
                    cmd.Parameters.AddWithValue("Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            m = new MemberModel();
                            m.Id = Convert.ToInt32(reader["id"]);
                            m.UserName = reader["username"].ToString();
                            m.Status = (Status)Convert.ToInt32(reader["status"]);
                            m.RegistrationDate = DateTime.Parse(reader["registrationdate"].ToString());
                            m.PhoneNumber = reader["phonenumber"].ToString();
                            m.GameType = Convert.ToInt32(reader["gametype"]);
                            m.GameTypeName = reader["gametypename"].ToString();
                            m.EmailAddress = reader["emailaddress"].ToString();
                            m.BankAccountNumber = reader["bankaccountnumber"].ToString();
                            m.BankAccountName = reader["bankaccountname"].ToString();
                            m.BankAccount = Convert.ToInt32(reader["bankaccount"]);
                            m.BankName = reader["bankname"].ToString();
                            if (reader["actiondate"] != null && reader["actiondate"].ToString() != "")
                            {
                                m.ActionDate = DateTime.Parse(reader["actiondate"].ToString());
                            }
                            m.Notes = reader["notes"].ToString();
                        }
                    }

                    conn.Close();
                }
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

        public void DeleteMember(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM Members WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public MemberModel GetMemberByUserName(string username)
        {
            MemberModel m = null;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT m.*, b.BankName, g.GameType AS GameTypeName FROM members AS m INNER JOIN banks AS b ON m.BankAccount  = b.Id INNER JOIN gametype AS g ON m.GameType = g.id WHERE m.Username LIKE '%" + username + "%'";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            m = new MemberModel();
                            m.Id = Convert.ToInt32(reader["id"]);
                            m.UserName = reader["username"].ToString();
                            m.Status = (Status)Convert.ToInt32(reader["status"]);
                            m.RegistrationDate = DateTime.Parse(reader["registrationdate"].ToString());
                            m.PhoneNumber = reader["phonenumber"].ToString();
                            m.GameType = Convert.ToInt32(reader["gametype"]);
                            m.GameTypeName = reader["gametypename"].ToString();
                            m.EmailAddress = reader["emailaddress"].ToString();
                            m.BankAccountNumber = reader["bankaccountnumber"].ToString();
                            m.BankAccountName = reader["bankaccountname"].ToString();
                            m.BankAccount = Convert.ToInt32(reader["bankaccount"]);
                            m.BankName = reader["bankname"].ToString();
                            if (reader["actiondate"] != null && reader["actiondate"].ToString() != "")
                            {
                                m.ActionDate = DateTime.Parse(reader["actiondate"].ToString());
                            }
                            m.Notes = reader["notes"].ToString();
                        }
                    }

                    conn.Close();
                }
            }

            return m;
        }
        #endregion
        //End Members


        //Dashboard
        #region
        public DashboardModel GetDashboardData()
        {
            DashboardModel d = new DashboardModel();
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT COUNT(*) AS TodaysMember FROM Members WHERE RegistrationDate  >= DATE(DATE_ADD(NOW(), INTERVAL 12 HOUR)) AND Status = 2";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            d.TodaysMembers = reader["TodaysMember"].ToString() != "" ? Convert.ToInt32(reader["TodaysMember"]) : 0;
                        }
                    }

                    cmd.CommandText = "SELECT COUNT(*) AS TodaysDeposit FROM Deposit WHERE DepositDate >= DATE(DATE_ADD(NOW(), INTERVAL 12 HOUR)) AND Status = 2";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            d.TodaysDeposits = reader["TodaysDeposit"].ToString() != "" ? Convert.ToInt32(reader["TodaysDeposit"]) : 0;
                        }
                    }

                    cmd.CommandText = "SELECT SUM(DepositAmount) AS TodaysDepositAmount FROM Deposit WHERE DepositDate >= DATE(DATE_ADD(NOW(), INTERVAL 12 HOUR)) AND Status = 2";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            d.TodaysDepositsAmount = reader["TodaysDepositAmount"].ToString() != "" ? Convert.ToDecimal(reader["TodaysDepositAmount"]) : 0;
                        }
                    }

                    cmd.CommandText = "SELECT COUNT(*) AS TodaysWithdrawal FROM Withdrawal WHERE WithdrawalDate >= DATE(DATE_ADD(NOW(), INTERVAL 12 HOUR)) AND Status = 2";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            d.TodaysWithdrawal = reader["TodaysWithdrawal"].ToString() != "" ? Convert.ToInt32(reader["TodaysWithdrawal"]) : 0;
                        }
                    }

                    cmd.CommandText = "SELECT SUM(WithdrawalAmount) AS TodaysWithdrawalAmount FROM Withdrawal WHERE WithdrawalDate >= DATE(DATE_ADD(NOW(), INTERVAL 12 HOUR)) AND Status = 2";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            d.TodaysWithdrawalAmount = reader["TodaysWithdrawalAmount"].ToString() != "" ? Convert.ToDecimal(reader["TodaysWithdrawalAmount"]) : 0;
                        }
                    }

                    d.TodaysTotal = d.TodaysDeposits + d.TodaysWithdrawal;
                    d.TodaysTotalAmount = d.TodaysDepositsAmount - d.TodaysWithdrawalAmount;

                    conn.Close();
                }
            }

            return d;
        }
        #endregion
        //End Dashboard


        //User
        #region 
        public string Login(UserModel model)
        {
            string userName = null;
            List<WithdrawalModel> lw = new List<WithdrawalModel>();
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Users WHERE UserName LIKE @UserName " +
                    "AND Password LIKE @Password";

                    cmd.Parameters.AddWithValue("UserName", model.UserName);
                    cmd.Parameters.AddWithValue("Password", model.Password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userName = reader["UserName"].ToString();
                        }
                    }

                    conn.Close();
                }
            }

            return userName;
        }

        public List<UserModel> GetUsers()
        {
            List<UserModel> lu = new List<UserModel>();
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Users ORDER BY Id DESC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel um = new UserModel();
                            um.Id = Convert.ToInt32(reader["Id"]);
                            um.UserName = reader["UserName"].ToString();
                            um.Email = reader["Email"].ToString();

                            lu.Add(um);
                        }
                    }

                    conn.Close();
                }
            }

            return lu;
        }

        public UserModel GetUserById(int id)
        {
            UserModel um = null;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Users WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            um = new UserModel();
                            um.Id = Convert.ToInt32(reader["Id"]);
                            um.UserName = reader["UserName"].ToString();
                            um.Email = reader["Email"].ToString();

                        }
                    }

                    conn.Close();
                }
            }

            return um;
        }

        public UserModel GetUserByEmail(string email)
        {
            UserModel um = null;
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Users WHERE Email = @Email";
                    cmd.Parameters.AddWithValue("Email", email);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            um = new UserModel();
                            um.Id = Convert.ToInt32(reader["Id"]);
                            um.UserName = reader["UserName"].ToString();
                            um.Email = reader["Email"].ToString();
                        }
                    }

                    conn.Close();
                }
            }

            return um;
        }

        public void InsertUser(UserModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Users" +
                        "(UserName, Email, Password)" +
                        "VALUES(@UserName, @Email, @Password);";

                    cmd.Parameters.AddWithValue("UserName", model.UserName);
                    cmd.Parameters.AddWithValue("Email", model.Email);
                    cmd.Parameters.AddWithValue("Password", model.Password);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void EditUser(UserModel model)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Users SET " +
                        "UserName = @UserName, Email = @Email";

                    if (model.Password != null && model.Password != "")
                    {
                        cmd.CommandText += ", Password = @Password";
                        cmd.Parameters.AddWithValue("Password", model.Password);
                    }
                    cmd.CommandText += " WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("UserName", model.UserName);
                    cmd.Parameters.AddWithValue("Email", model.Email);
                    cmd.Parameters.AddWithValue("Id", model.Id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public void DeleteUser(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM Users WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        #endregion
        //End User
    }
}