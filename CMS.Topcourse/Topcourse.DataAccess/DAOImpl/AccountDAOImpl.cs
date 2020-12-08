using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBHelpers;
using Topcourse.DataAccess.DAO;
using Topcourse.DataAccess.DTO;
using Topcourse.Utility;

namespace Topcourse.DataAccess.DAOImpl
{
    public class AccountDAOImpl : IAccountDAO
    {
        private string CoreDBConnectionString = "Server=103.130.212.9;uid=cuongbx;password=Aa@123456;database=Topcourse.CoreDB;";
        // 
        public Account User_GetSingle(int userId, string username)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_UserName", username);
                var user = new DBHelper(CoreDBConnectionString).GetInstanceSP<Account>("SP_User_GetSingle", pars);
                return user;
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new Account();
            }
        }

        // full
        public Account User_GetFull(int userId, string username)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_UserName", username);
                var user = new DBHelper(CoreDBConnectionString).GetInstanceSP<Account>("SP_User_GetFull", pars);
                return user;
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new Account();
            }
        }

        // list
        public List<Account> GetListUsers(int userId, string username, byte userType, byte status)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_Username", username);
                pars[2] = new SqlParameter("@_UserType", userType == 99 ? DBNull.Value : (object)userType); // --1:hoc vien, 2: trung tâm
                pars[3] = new SqlParameter("@_Status", status == 99 ? DBNull.Value : (object)status); // --0:chờ kích hoạt, 1:đã kích hoạt, 2:inactive, 3:block
                var list = new DBHelper(CoreDBConnectionString).GetListSP<Account>("SP_User_GetList", pars);
                if (list == null || list.Count <= 0)
                    return new List<Account>();
                return list;
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Account>();
            }
        }

        // Tao tk
        public int CMS_RegisterUser(Account account)
        {
            try
            {
                var pars = new SqlParameter[10];
                pars[0] = new SqlParameter("@_Username", account.Username);
                pars[1] = new SqlParameter("@_Password", account.Password);
                pars[2] = new SqlParameter("@_Mobile", account.Mobile);
                pars[3] = new SqlParameter("@_Fullname", account.Fullname);
                pars[4] = new SqlParameter("@_CreatedUser", account.CreatedUser);
                pars[5] = new SqlParameter("@_UserProfileInfo", account.UserProfileInfo);
                pars[6] = new SqlParameter("@_DeviceType", (byte)CheckUserAgent.CheckAgent());
                pars[7] = new SqlParameter("@_UserType", account.UserType); // 1 tk hoc vien , 2 tk trung tam
                pars[8] = new SqlParameter("@_ClientIP", Config.GetIP());
                pars[9] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_CMS_RegisterUser", pars);
                return Convert.ToInt32(pars[9].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }

        // update info
        public int User_UpdateProfile(Account account)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@_UserID", account.UserID);
                pars[1] = new SqlParameter("@_Mobile", account.Mobile);
                pars[2] = new SqlParameter("@_Fullname", account.Fullname);
                pars[3] = new SqlParameter("@_CreatedUser", account.CreatedUser);
                pars[4] = new SqlParameter("@_UserProfileInfo", account.UserProfileInfo);
                pars[5] = new SqlParameter("@_ClientIP", Config.GetIP());
                pars[6] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_User_UpdateProfile", pars);
                return Convert.ToInt32(pars[6].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }

        // block acc
        public int User_Block(Account account)
        {
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_UserID", account.UserID);
                pars[1] = new SqlParameter("@_Username", account.Username);
                pars[2] = new SqlParameter("@_CreatedUser", account.CreatedUser);
                pars[3] = new SqlParameter("@_ClientIP", (byte)CheckUserAgent.CheckAgent());
                pars[4] = new SqlParameter("@_Reason", account.Reason);
                pars[5] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_User_Block", pars);
                return Convert.ToInt32(pars[5].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -99;
            }
        }

        // unblock
        public int User_UnBlock(Account account)
        {
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_UserID", account.UserID);
                pars[1] = new SqlParameter("@_Username", account.Username);
                pars[2] = new SqlParameter("@_CreatedUser", account.CreatedUser);
                pars[3] = new SqlParameter("@_ClientIP", (byte)CheckUserAgent.CheckAgent());
                pars[4] = new SqlParameter("@_Reason", account.Reason);
                pars[5] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_User_UnBlock", pars);
                return Convert.ToInt32(pars[5].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -99;
            }
        }

        // Đổi mk
        public int User_ChangePassword(int userId, string newpassword, byte logType, string createdUser, string description)
        {
            try
            {
                var pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_Password", DBNull.Value);
                pars[2] = new SqlParameter("@_PasswordNew", newpassword);
                pars[3] = new SqlParameter("@_LogType", logType);
                pars[4] = new SqlParameter("@_ClientIP", (byte)CheckUserAgent.CheckAgent());
                pars[5] = new SqlParameter("@_CreatedUser", createdUser);
                pars[6] = new SqlParameter("@_Description", description);
                pars[7] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_User_ChangePassword", pars);
                return Convert.ToInt32(pars[7].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }

        // xóa tk
        public int CMS_DeleteUser(int userId, string username, string createdUser)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_Username", username);
                pars[2] = new SqlParameter("@_ClientIP", (byte)CheckUserAgent.CheckAgent());
                pars[3] = new SqlParameter("@_CreatedUser", createdUser);
                pars[4] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_CMS_DeleteUser", pars);
                return Convert.ToInt32(pars[4].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }

        // Location
        public List<Location> GetLocation(int locationType, int? parentID, int locationID)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_LocationLevel", locationType <= 0 ? DBNull.Value : (object)locationType);
                pars[1] = new SqlParameter("@_ParentID", parentID <= 0 ? DBNull.Value : (object)parentID);
                pars[2] = new SqlParameter("@_LocationID", locationID <= 0 ? DBNull.Value : (object)locationID);
                return new DBHelper(CoreDBConnectionString).GetListSP<Location>("SP_Common_GetLocations", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Location>();
            }
        }


        // Education - Trường học (Cơ sở đào tạo)
        public List<Education> Education_GetList(Request_Education requestData)
        {
            try
            {
                var pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@_EduName", string.IsNullOrEmpty(requestData.EduName) ? DBNull.Value : (object)requestData.EduName);
                pars[1] = new SqlParameter("@_UserID", requestData.UserID <= 0 ? DBNull.Value : (object)requestData.UserID);
                pars[2] = new SqlParameter("@_Type", requestData.Type <= 0 ? DBNull.Value : (object)requestData.Type);  //  --1:trong nc, 2 :quoc te
                pars[3] = new SqlParameter("@_IsPartner", DBNull.Value);
                pars[4] = new SqlParameter("@_TraningType", requestData.TraningType <= 0 ? DBNull.Value : (object)requestData.TraningType);  // --1:trong nc, 2:lien ket, 3:du hoc
                pars[5] = new SqlParameter("@_NationalID", requestData.NationalID <= 0 ? DBNull.Value : (object)requestData.NationalID);
                pars[6] = new SqlParameter("@_CurrentPage", 1);
                pars[7] = new SqlParameter("@_PageSize", 10000);
                return new DBHelper(CoreDBConnectionString).GetListSP<Education>("SP_Education_Get", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Education>();
            }
        }

        public Education Education_Detail(int eduId, string alias)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_EduId", eduId);
                pars[1] = new SqlParameter("@_Alias", string.IsNullOrEmpty(alias) ? DBNull.Value : (object)alias);
                var user = new DBHelper(CoreDBConnectionString).GetInstanceSP<Education>("SP_Education_Detail", pars);
                return user;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return new Education();
            }
        }

        //Insert
        public int SP_Education_Insert(Education requestData)
        {
            try
            {
                var pars = new SqlParameter[24];
                pars[0] = new SqlParameter("@_EduName", string.IsNullOrEmpty(requestData.EduName) ? DBNull.Value : (object)requestData.EduName);
                pars[1] = new SqlParameter("@_Alias", string.IsNullOrEmpty(requestData.Alias) ? DBNull.Value : (object)requestData.Alias);
                pars[2] = new SqlParameter("@_EduNameVI", string.IsNullOrEmpty(requestData.EduNameVI) ? DBNull.Value : (object)requestData.EduNameVI);
                pars[3] = new SqlParameter("@_EduNameEN", string.IsNullOrEmpty(requestData.EduNameEN) ? DBNull.Value : (object)requestData.EduNameEN);
                pars[4] = new SqlParameter("@_ShortName", string.IsNullOrEmpty(requestData.ShortName) ? DBNull.Value : (object)requestData.ShortName);
                pars[5] = new SqlParameter("@_SlideImage", string.IsNullOrEmpty(requestData.SlideImage) ? DBNull.Value : (object)requestData.SlideImage);
                pars[6] = new SqlParameter("@_EduAddress", string.IsNullOrEmpty(requestData.EduAddress) ? DBNull.Value : (object)requestData.EduAddress);
                pars[7] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description);
                pars[8] = new SqlParameter("@_Content", string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object)requestData.Content);
                pars[9] = new SqlParameter("@_UserID", requestData.UserID);
                pars[10] = new SqlParameter("@_Type", requestData.Type);
                pars[11] = new SqlParameter("@_IsPartner", requestData.IsPartner);
                pars[12] = new SqlParameter("@_TraningType", requestData.TraningType);
                pars[13] = new SqlParameter("@_Avatar", string.IsNullOrEmpty(requestData.Avatar) ? DBNull.Value : (object)requestData.Avatar);
                pars[14] = new SqlParameter("@_Logo", string.IsNullOrEmpty(requestData.Logo) ? DBNull.Value : (object)requestData.Logo);
                pars[15] = new SqlParameter("@_EduEstablishedYear", requestData.EduEstablishedYear);
                pars[16] = new SqlParameter("@_NationalID", requestData.NationalID);
                pars[17] = new SqlParameter("@_EduMap", string.IsNullOrEmpty(requestData.EduMap) ? DBNull.Value : (object)requestData.EduMap);
                pars[18] = new SqlParameter("@_ContactInfo", string.IsNullOrEmpty(requestData.ContactInfo) ? DBNull.Value : (object)requestData.ContactInfo);
                pars[19] = new SqlParameter("@_BranchInfo", string.IsNullOrEmpty(requestData.BranchInfo) ? DBNull.Value : (object)requestData.BranchInfo);
                pars[20] = new SqlParameter("@_Rate", requestData.Rate);
                pars[21] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[22] = new SqlParameter("@_SEODescription", string.IsNullOrEmpty(requestData.SEODescription) ? DBNull.Value : (object)requestData.SEODescription);
                pars[23] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Education_Insert", pars);
                return Convert.ToInt32(pars[23].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }


        // Update
        public int SP_Education_Update(Education requestData)
        {
            try
            {
                var pars = new SqlParameter[25];
                pars[0] = new SqlParameter("@_EduId", requestData.EduId);
                pars[1] = new SqlParameter("@_EduName", string.IsNullOrEmpty(requestData.EduName) ? DBNull.Value : (object)requestData.EduName);
                pars[2] = new SqlParameter("@_Alias", string.IsNullOrEmpty(requestData.Alias) ? DBNull.Value : (object)requestData.Alias);
                pars[3] = new SqlParameter("@_EduNameVI", string.IsNullOrEmpty(requestData.EduNameVI) ? DBNull.Value : (object)requestData.EduNameVI);
                pars[4] = new SqlParameter("@_EduNameEN", string.IsNullOrEmpty(requestData.EduNameEN) ? DBNull.Value : (object)requestData.EduNameEN);
                pars[5] = new SqlParameter("@_ShortName", string.IsNullOrEmpty(requestData.ShortName) ? DBNull.Value : (object)requestData.ShortName);
                pars[6] = new SqlParameter("@_SlideImage", string.IsNullOrEmpty(requestData.SlideImage) ? DBNull.Value : (object)requestData.SlideImage);
                pars[7] = new SqlParameter("@_EduAddress", string.IsNullOrEmpty(requestData.EduAddress) ? DBNull.Value : (object)requestData.EduAddress);
                pars[8] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description);
                pars[9] = new SqlParameter("@_Content", string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object)requestData.Content);
                pars[10] = new SqlParameter("@_UserID", requestData.UserID);
                pars[11] = new SqlParameter("@_Type", requestData.Type);
                pars[12] = new SqlParameter("@_IsPartner", requestData.IsPartner);
                pars[13] = new SqlParameter("@_TraningType", requestData.TraningType);
                pars[14] = new SqlParameter("@_Avatar", string.IsNullOrEmpty(requestData.Avatar) ? DBNull.Value : (object)requestData.Avatar);
                pars[15] = new SqlParameter("@_Logo", string.IsNullOrEmpty(requestData.Logo) ? DBNull.Value : (object)requestData.Logo);
                pars[16] = new SqlParameter("@_EduEstablishedYear", requestData.EduEstablishedYear);
                pars[17] = new SqlParameter("@_NationalID", requestData.NationalID);
                pars[18] = new SqlParameter("@_EduMap", string.IsNullOrEmpty(requestData.EduMap) ? DBNull.Value : (object)requestData.EduMap);
                pars[19] = new SqlParameter("@_ContactInfo", string.IsNullOrEmpty(requestData.ContactInfo) ? DBNull.Value : (object)requestData.ContactInfo);
                pars[20] = new SqlParameter("@_BranchInfo", string.IsNullOrEmpty(requestData.BranchInfo) ? DBNull.Value : (object)requestData.BranchInfo);
                pars[21] = new SqlParameter("@_Rate", requestData.Rate);
                pars[22] = new SqlParameter("@_SEODescription", string.IsNullOrEmpty(requestData.SEODescription) ? DBNull.Value : (object)requestData.SEODescription);
                pars[23] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[24] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Education_Update", pars);
                return Convert.ToInt32(pars[24].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public int SP_Education_Delete(Request_Education requestData)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_EduId", requestData.EduId);
                pars[1] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Education_Delete", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }


        // Comment
        public int User_Comment_Insert(Comment_Request requestData)
        {
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_TargetID", requestData.TargetID);
                pars[1] = new SqlParameter("@_UserID", requestData.UserID);
                pars[2] = new SqlParameter("@_Comment", string.IsNullOrEmpty(requestData.Comment) ? DBNull.Value : (object)requestData.Comment);
                pars[3] = new SqlParameter("@_ParentID", requestData.ParentID);
                pars[4] = new SqlParameter("@_Type", requestData.Type);
                pars[5] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_User_Comment_Insert", pars);
                return Convert.ToInt32(pars[5].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public int User_Comment_Update(Comment_Request requestData)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_Comment", string.IsNullOrEmpty(requestData.Comment) ? DBNull.Value : (object)requestData.Comment);
                pars[2] = new SqlParameter("@_UpdateUser", string.IsNullOrEmpty(requestData.UpdateUser) ? DBNull.Value : (object)requestData.UpdateUser);
                pars[3] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_User_Comment_Update", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }


        public int User_Comment_Delete(Comment_Request requestData)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_UpdateUser", string.IsNullOrEmpty(requestData.UpdateUser) ? DBNull.Value : (object)requestData.UpdateUser);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_User_Comment_Delete", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public List<CommentModel> Comment_GetList(Comment_Request requestData)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_Id", requestData.Id <= 0 ? DBNull.Value : (object)requestData.Id);
                pars[1] = new SqlParameter("@_TargetID", requestData.TargetID <= 0 ? DBNull.Value : (object)requestData.TargetID);
                pars[2] = new SqlParameter("@_Type", requestData.Type <= 0 ? DBNull.Value : (object)requestData.Type);  // 1 trường, 2 khóa , 3 học bổng
                pars[3] = new SqlParameter("@_UserID", requestData.UserID <= 0 ? DBNull.Value : (object)requestData.UserID);
                pars[4] = new SqlParameter("@_ParentID", requestData.ParentID <= 0 ? DBNull.Value : (object)requestData.ParentID);
                return new DBHelper(CoreDBConnectionString).GetListSP<CommentModel>("SP_User_Comment_GetList", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<CommentModel>();
            }
        }


        // Order
        public List<Order_Payment> OrderPayment_Get(long orderId, byte status, byte courseType, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var pars = new SqlParameter[11];
                pars[0] = new SqlParameter("@_OrderID", orderId <= 0 ? DBNull.Value : (object)orderId);
                pars[1] = new SqlParameter("@_Stautus", status == 0 ? DBNull.Value : (object)status);
                pars[2] = new SqlParameter("@_CourseType", courseType <= 0 ? DBNull.Value : (object)status);
                pars[3] = new SqlParameter("@_TransRefID", DBNull.Value);
                pars[4] = new SqlParameter("@_BankCode", DBNull.Value);
                pars[5] = new SqlParameter("@_BankTransNo", DBNull.Value);
                pars[6] = new SqlParameter("@_PayResponseCode", DBNull.Value);
                pars[7] = new SqlParameter("@_CreatedFromDate", fromDate == new DateTime() ? DBNull.Value : (object)fromDate);
                pars[8] = new SqlParameter("@_CreatedToDate", toDate == new DateTime() ? DBNull.Value : (object)toDate);
                pars[9] = new SqlParameter("@_PayFromDate", DBNull.Value);
                pars[10] = new SqlParameter("@_PayToDate", DBNull.Value);
                return new DBHelper(CoreDBConnectionString).GetListSP<Order_Payment>("SP_OrderPayment_Get", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Order_Payment>();
            }
        }


        // chi tiết đơn 
        public List<Order_Payment> OrderPayment_GetProduct(long orderId)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_OrderID", orderId <= 0 ? DBNull.Value : (object)orderId);
                return new DBHelper(CoreDBConnectionString).GetListSP<Order_Payment>("SP_OrderPayment_GetProduct", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Order_Payment>();
            }
        }


        // duyệt giao dịch
        public int SP_OrderPayment_ConfirmStatus(OrderPayment_ConfirmStatus requestData)
        {
            try
            {
                var pars = new SqlParameter[11];
                pars[0] = new SqlParameter("@_OrderID", requestData.OrderID);
                pars[1] = new SqlParameter("@_Stautus", requestData.Status);
                pars[2] = new SqlParameter("@_TransRefID", string.IsNullOrEmpty(requestData.TransRefID) ? DBNull.Value : (object)requestData.TransRefID);
                pars[3] = new SqlParameter("@_BankCode", string.IsNullOrEmpty(requestData.BankCode) ? DBNull.Value : (object)requestData.BankCode);
                pars[4] = new SqlParameter("@_BankTransNo", string.IsNullOrEmpty(requestData.BankTransNo) ? DBNull.Value : (object)requestData.BankTransNo);
                pars[5] = new SqlParameter("@_CardType", string.IsNullOrEmpty(requestData.CardType) ? DBNull.Value : (object)requestData.CardType);
                pars[6] = new SqlParameter("@_PayDate", requestData.PayDate);
                pars[7] = new SqlParameter("@_PayResponseCode", string.IsNullOrEmpty(requestData.PayResponseCode) ? DBNull.Value : (object)requestData.PayResponseCode);
                pars[8] = new SqlParameter("@_TmnCode", string.IsNullOrEmpty(requestData.TmnCode) ? DBNull.Value : (object)requestData.TmnCode);
                pars[9] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[10] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_OrderPayment_ConfirmStatus", pars);
                return Convert.ToInt32(pars[10].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }
    }
}
