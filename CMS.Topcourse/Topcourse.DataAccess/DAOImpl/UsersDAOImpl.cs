using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Topcourse.DataAccess.DAO;
using Topcourse.DataAccess.DTO;
using Topcourse.Utility;
using DBHelpers;

namespace Topcourse.DataAccess.DAOImpl
{
    public class UsersDAOImpl : IUsersDAO
    {
        private string AdminDBConnectionString = "Server=103.130.212.9;uid=cuongbx;password=Aa@123456;database=Topcourse.AdminDB;";
        /// <summary>
        /// Xác thực người dùng
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="systemId"></param>
        /// <param name="responseCode"></param>
        /// @_SystemID INT = 0, --bổ sung
        /// <returns></returns>
        public Users Authentication(string username, string email, string password, int systemId, ref int responseCode)
        {
            try
            {

                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_Username", username);
                pars[1] = new SqlParameter("@_Email", email);
                pars[2] = new SqlParameter("@_Password", password);
                pars[3] = new SqlParameter("@_ClientIP", System.Web.HttpContext.Current.Request.UserHostAddress);
                pars[4] = new SqlParameter("@_SystemID", systemId);
                pars[5] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var user = new DBHelper(AdminDBConnectionString).GetInstanceSP<Users>("sp_User_Authenticate", pars);
                responseCode = Convert.ToInt32(pars[5].Value);
                return user;
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return new Users();
            }
        }

        /// <summary>
        /// Get User theo UserID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users SelectByUserID(int userId)
        {
            try
            {
                return new DBHelper(AdminDBConnectionString).GetInstanceSP<Users>("SP_User_GetByCondition", new SqlParameter("@_UserID", userId));
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return new Users();
            }
        }



        /// <summary>
        /// Get User theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Users GetByEmail(string email)
        {
            try
            {
                return new DBHelper(AdminDBConnectionString).GetInstanceSP<Users>("SP_User_GetByCondition", new SqlParameter("@_Email", email));
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return new Users();
            }
        }
        /// <summary>
        /// Get User theo Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Users GetByUsername(string username)
        {
            try
            {
                return new DBHelper(AdminDBConnectionString).GetInstanceSP<Users>("SP_User_GetByCondition", new SqlParameter("@_Username", username));
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return new Users();
            }
        }



        /// <summary>
        /// Get list<Users>theo điều kiện, có phân trang
        /// </summary>
        /// <param name="departmentID"></param>
        /// <param name="groupID"></param>
        /// <param name="isAcitve"></param>
        /// <param name="email"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Users> GetListUsers(string Keyword, int isActive, int isGrant, string listgroup)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_Status", isActive);
                pars[1] = new SqlParameter("@_Keyword", Keyword);
                pars[2] = new SqlParameter("@_GroupID", listgroup);
                pars[3] = new SqlParameter("@_IsGrant", isGrant != 1 ? DBNull.Value : (object)isGrant);
                var list = new DBHelper(AdminDBConnectionString).GetListSP<Users>("SP_User_GetPage", pars);
                if (list == null || list.Count <= 0)
                    return new List<Users>();
                return list;
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return new List<Users>();
            }
        }

        /// <summary>
        /// Update thông tin User
        /// </summary>
        /// <param name="users"></param>
        /// <param name="listGroup"></param>
        /// <param name="listSystem"></param>
        /// ,@_GroupID VARCHAR(100) = '' --@_GroupID: 1,2,3,4,5...
        /// ,@_SystemID VARCHAR(100) = '' --SystemID : 1,2,3,4...
        /// <returns></returns>
        public int UpdateUsers(Users users, string listGroup, string listSystem)
        {
            try
            {
                var pars = new SqlParameter[11];
                pars[0] = new SqlParameter("@_UserID", (users.UserID < 0) ? 0 : users.UserID);
                pars[1] = new SqlParameter("@_Username", string.IsNullOrEmpty(users.Username) ? DBNull.Value : (object)users.Username);
                pars[2] = new SqlParameter("@_Email", string.IsNullOrEmpty(users.Email) ? DBNull.Value : (object)users.Email);
                pars[3] = new SqlParameter("@_FullName", string.IsNullOrEmpty(users.FullName) ? DBNull.Value : (object)users.FullName);
                pars[4] = new SqlParameter("@_Password", string.IsNullOrEmpty(users.Password) ? DBNull.Value : (object)users.Password);
                pars[5] = new SqlParameter("@_IsActive", users.Status);
                pars[6] = new SqlParameter("@_IsAdministrator", users.IsAdministrator);
                pars[7] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(users.CreatedUser) ? DBNull.Value : (object)users.CreatedUser);
                pars[8] = new SqlParameter("@_GroupID", string.IsNullOrEmpty(listGroup) ? DBNull.Value : (object)listGroup);
                pars[9] = new SqlParameter("@_SystemID", string.IsNullOrEmpty(listSystem) ? DBNull.Value : (object)listSystem);
                pars[10] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_User_InsertUpdate", pars);
                return Convert.ToInt32(pars[10].Value);
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return -99;
            }
        }



        /// <summary>
        /// Xóa thông tin một user theo UserID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int DelleteUsers(int userId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_User_Delete", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return -99;
            }
        }


        public int UpdateActiveUser(int Id)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", Id);
                pars[1] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_User_UpdateActive", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception e)
            {
                Log4Net.PublishException(e);
                return -99;
            }
        }

        public int ChangePassword(string UserName, string PasswordOld, string PasswordNew)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_UserName", UserName);
                pars[1] = new SqlParameter("@_PasswordOld", PasswordOld);
                pars[2] = new SqlParameter("@_PasswordNew", PasswordNew);
                pars[3] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_User_ChangePassword", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception e)
            {
                Log4Net.PublishException(e);
                return -99;
            }
        }

        /// Cập nhập trạng thái Lock
        /// <param name="Id"></param>
        /// <param name="isLock"></param> 
        /// <returns></returns>
        public int UpdateUserLock(int Id, bool isLock)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", Id);
                pars[1] = new SqlParameter("@_IsLock", isLock); // 0 mở ; 1 đóng
                pars[2] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_User_Update_Lock", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception e)
            {
                Log4Net.PublishException(e);
                return -99;
            }
        }



        #region Quản trị Bộ Phận ( Nhóm Group )

        //@_GroupID int 
        //,@_Name nvarchar(50)
        //    ,@_Alias nvarchar(50) = ''
        // ,@_IsActive bit = NULL
        //, @_SystemID int
        //,@_ResponseStatus int output

        public int Group_InsertUpdate(Group group)
        {
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_GroupID", group.GroupID);
                pars[1] = new SqlParameter("@_Name", group.Name);
                pars[2] = new SqlParameter("@_Alias", group.Alias);
                pars[3] = new SqlParameter("@_IsActive", group.IsActive);
                pars[4] = new SqlParameter("@_SystemID", group.SystemID);
                pars[5] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_Group_InsertUpdate", pars);
                return Convert.ToInt32(pars[5].Value);
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return -99;
            }
        }


        public int Group_Delete(int groupUserId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_GroupID", groupUserId);
                pars[1] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_Group_Delete", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return -99;
            }
        }

        //Lấy danh sách theo điều kiênện
        public List<Group> GetAllGroupUser(int groupId, string name, bool isActive, int systemID)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_GroupID", groupId);
                pars[1] = new SqlParameter("@_Name", name);
                pars[2] = new SqlParameter("@_IsActive", isActive);
                pars[3] = new SqlParameter("@_SystemtID", systemID);
                var list = new DBHelper(AdminDBConnectionString).GetListSP<Group>("SP_Group_GetByCondition", pars);
                return list;
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return new List<Group>();
            }
        }

        #endregion


        #region System 

        public List<SystemUser> System_GetList()
        {
            try
            {
                var list = new DBHelper(AdminDBConnectionString).GetListSP<SystemUser>("SP_System_GetList");
                return list;
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return new List<SystemUser>();
            }
        }

        public List<User_System> UserSystem_GetList(int userId, int systemId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_SystemID", systemId);
                var list = new DBHelper(AdminDBConnectionString).GetListSP<User_System>("SP_UserSystem_GetList", pars);
                return list;
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return new List<User_System>();
            }
        }




        // SP_UserSystem_ActiveOrStandBy Bật tắt userSystem (dành cho mod của từng hệ thống) 
        // "@_ERR_NOT_EXIST_ACCOUNT INT = -50
        // @_ERR_UNKNOWN INT = -99
        // @_ERR_DATA_INVALID INT = -600        "
        public int UserSystem_ActiveOrStandBy(int userId, int system, string createdUser)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_SystemID", system);
                pars[2] = new SqlParameter("@_CreatedUser", createdUser);
                pars[3] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_UserSystem_ActiveOrStandBy", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                    Log4Net.PublishException(ex);
                return -99;
            }
        }

        #endregion

    }
}
