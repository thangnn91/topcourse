using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Topcourse.DataAccess.DAO;
using Topcourse.DataAccess.DTO;
using Topcourse.Utility;
using DBHelpers;
using Topcourse.Utility;


namespace Topcourse.DataAccess.DAOImpl
{
    public class FunctionsDAOImpl : IFucntionsDAO
    {
        private string AdminDBConnectionString = "Server=103.130.212.9;uid=cuongbx;password=Aa@123456;database=Topcourse.AdminDB;";
        /// <summary>
        /// Lấy FunctionID theo tên funtion
        /// </summary>
        /// <param name="functionname"></param>
        /// <returns></returns>
        public int GetFunctionID(string functionname)
        {
            try
            {
                var functionlist = (List<Functions>)System.Web.HttpContext.Current.Session[SessionsManager.SESSION_FUNCTIONS];
                var fun = functionlist.Find(f => f.Url.ToLower().LastIndexOf("/" + functionname.ToLower()) > 0);
                return fun != null ? fun.FunctionID : 0;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -99;
            }
        }


        /// <summary>
        /// Lấy danh sách funtion theo UserID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="grant"></param>
        /// <param name="systemId"></param>
        /// ,@_SystemID VARCHAR(100) = '' -- 1,2,3,4 ... bổ sung
        /// <returns></returns>
        public List<Functions> GetListFunctionByUserId(int userId, int grant, string systemId)
        {
            try 
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_GetGrant", grant < 0 ? DBNull.Value : (object)grant);
                pars[2] = new SqlParameter("@_SystemID", string.IsNullOrEmpty(systemId) ? string.Empty : (object)systemId);
                return new DBHelper(AdminDBConnectionString).GetListSP<Functions>("SP_Function_GetByUserID", pars);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return new List<Functions>();
            }
        }

        /// Lây danh sách Fucntion theo các tham số truyền vào, Có phân trang
        // @_Keyword NVARCHAR(150) = ''
        //,@_ParentID int = -1 -- bổ sung
        //, @_SystemID        int = -1 -- bổ sung
        // , @_Status INT         
        //,@_CurrPage INT
        //    , @_RecordPerPage INT
        //,@_TotalRecord INT OUT"
        /// <returns></returns>
        public List<Functions> GetListFunctions(string keyword, int parentId, int systemId, int status, int pageNumber, int pageSize, ref int totalRecord)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@_Keyword", string.IsNullOrEmpty(keyword) ? string.Empty : keyword);
                pars[1] = new SqlParameter("@_ParentID", parentId < 0 ? DBNull.Value : (object)parentId);
                pars[2] = new SqlParameter("@_SystemID", systemId < 0 ? DBNull.Value : (object)systemId);
                pars[3] = new SqlParameter("@_Status", status);
                pars[4] = new SqlParameter("@_CurrPage", pageNumber);
                pars[5] = new SqlParameter("@_RecordPerPage", pageSize);
                pars[6] = new SqlParameter("@_TotalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = new DBHelper(AdminDBConnectionString).GetListSP<Functions>("SP_Functions_GetPage", pars);
                totalRecord = Convert.ToInt32(pars[6].Value);
                return list;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                totalRecord = 0;
                return new List<Functions>();
            }
        }

        /// <summary>
        /// Insert Fucntion
        /// </summary>
        /// <param name="functions"></param>
        /// <returns> >0 : thanh cong
        ///			-1: da ton tai
        ///			-99: loi he thong
        /// </returns>
        public int InsertUpdateFunction(Functions functions)
        {
            try
            {
                var pars = new SqlParameter[11];
                pars[0] = new SqlParameter("@_FunctionID", functions.FunctionID);
                pars[1] = new SqlParameter("@_FunctionName", functions.FunctionName);
                pars[2] = new SqlParameter("@_Url", functions.Url);
                pars[3] = new SqlParameter("@_IsDisplay", functions.IsDisplay);
                pars[4] = new SqlParameter("@_IsActive", functions.IsActive);
                pars[5] = new SqlParameter("@_ParentID", functions.ParentID);
                pars[6] = new SqlParameter("@_Order", functions.Order);
                pars[7] = new SqlParameter("@_CssIcon", functions.CssIcon);
                pars[8] = new SqlParameter("@_ActionName", functions.ActionName);
                pars[9] = new SqlParameter("@_SystemID", functions.SystemID);
                pars[10] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_Functions_Update", pars);
                return Convert.ToInt32(pars[10].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -99;
            }
        }
        public int UpdateOrder(int functionId, int parentId, int order)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_FunctionID", functionId);
                pars[1] = new SqlParameter("@_ParentID", parentId);
                pars[2] = new SqlParameter("@_Order", order);
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_Functions_UpdateOrder", pars);
                return 1;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -99;
            }
        }

        /// <summary>
        /// Xóa Functions
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public int DelleteFunction(int functionId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_FunctionID", functionId);
                pars[1] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(AdminDBConnectionString).ExecuteNonQuerySP("SP_Functions_Delete", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -99;
            }
        }


        //Lây danh sách theo điều kiện(các tham só truyền theo mặc định sẽ bỏ qua điều kiện lọc)
        //@_ParentID int = -1
        //,@_FunctionID INT = 0
        //, @_FunctionName nvarchar(50) = ''
        //,@_ActionName varchar(100) = ''
        //,@_IsActive int = -1
        //,@_IsDisplay int = -1
        //,@_SystemID VARCHAR(100) = '' -- 1,2,3,4 ...bổ sung
        public List<Functions> GetFunction_ByCondition(int fatherId, int functionId, string functionName, string actionName, int isactive, int isdisplay, string systemId)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@_ParentID", fatherId <= 0 ? DBNull.Value : (object)fatherId);
                pars[1] = new SqlParameter("@_FunctionID", functionId <= 0 ? DBNull.Value : (object)functionId);
                pars[2] = new SqlParameter("@_FunctionName", String.IsNullOrEmpty(functionName) ? DBNull.Value : (object)functionName);
                pars[3] = new SqlParameter("@_ActionName", string.IsNullOrEmpty(actionName) ? DBNull.Value : (object)actionName);
                pars[4] = new SqlParameter("@_IsActive", isactive <= 0 ? DBNull.Value : (object)isactive);
                pars[5] = new SqlParameter("@_IsDisplay", isdisplay <= 0 ? DBNull.Value : (object)isdisplay);
                pars[6] = new SqlParameter("@_SystemID", string.IsNullOrEmpty(systemId) ? DBNull.Value : (object)systemId);
                var result = new DBHelper(AdminDBConnectionString).GetListSP<Functions>("SP_Functions_SelectByCondition", pars);
                return result;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return new List<Functions>();
            }

        }
    }
}

