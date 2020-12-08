using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Topcourse.DataAccess.DAO;
using Topcourse.DataAccess.DTO;
using Topcourse.Utility;
using DBHelpers;

namespace Topcourse.DataAccess.DAOImpl
{
    public class UsersLogDAOImpl : IUsersLogDAO
    {
        private string AdminDBConnectionString = "Server=103.130.212.9;uid=cuongbx;password=Aa@123456;database=Topcourse.AdminDB;";
        /// <summary>
        /// Ghi log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public int InsertUsersLog(UsersLog log)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@_UserID", log.UserID);
                pars[1] = new SqlParameter("@_FunctionID", log.FunctionID);
                pars[2] = new SqlParameter("@_FunctionName", log.FunctionName);
                pars[3] = new SqlParameter("@_Description", log.Description);
                pars[4] = new SqlParameter("@_LogType", log.LogType);
                pars[5] = new SqlParameter("@_ClientIP", log.ClientIP);
                pars[6] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_UserLogs_Insert", pars);
                return Convert.ToInt32(pars[6].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -99;
            }
        }


        /// <summary>
        /// Lấy danh sách UserLog
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userId"></param>
        /// <param name="functionId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<UsersLog> GetListUsersLog(string fromDate, string toDate, int userId, int functionId)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = !string.IsNullOrEmpty(fromDate)
                               ? new SqlParameter("@_Fromdate", fromDate)
                               : new SqlParameter("@_Fromdate", DBNull.Value);
                pars[1] = !string.IsNullOrEmpty(toDate)
                              ? new SqlParameter("@_Todate", toDate)
                              : new SqlParameter("@_Todate", DBNull.Value);
                pars[2] = new SqlParameter("@_UserID", userId);
                pars[3] = new SqlParameter("@_FunctionID", functionId);

                var list_result = new DBHelper(AdminDBConnectionString).GetListSP<UsersLog>("SP_UserLogs_GetPages", pars);
                if (list_result == null || list_result.Count <= 0)
                    return new List<UsersLog>();
                return list_result;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return new List<UsersLog>();
            }
        }



        /// <summary>
        /// Xóa UserLog
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public int DeleteUsersLog(string fromDate, string toDate, int userId, int functionId)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_Fromdate", fromDate);
                pars[1] = new SqlParameter("@_Todate", toDate);
                pars[2] = new SqlParameter("@_UserID", userId);
                pars[3] = new SqlParameter("@_FunctionID", functionId);
                pars[4] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_UserLogs_Delete", pars);
                return Convert.ToInt32(pars[4].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -99;
            }
        }
    }
}
