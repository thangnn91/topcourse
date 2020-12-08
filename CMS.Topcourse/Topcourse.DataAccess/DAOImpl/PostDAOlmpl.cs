using DBHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topcourse.DataAccess.DAO;
using Topcourse.DataAccess.DTO;
using Topcourse.Utility;

namespace Topcourse.DataAccess.DAOImpl
{   
    public class PostDAOlmpl : IPostDAO
    {
        private string CoreDBConnectionString = "Server=103.130.212.9;uid=cuongbx;password=Aa@123456;database=Topcourse.CoreDB;";
        public int Delete_Post(int postId, string createdUser)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_PostId", postId);
                pars[1] = new SqlParameter("@_CreatedUser", createdUser);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Post_Delete", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }
        public int UpdateStatus_Post(int postId, int status, string actionUser)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_PostID", postId);
                pars[1] = new SqlParameter("@_Status", status);
                pars[2] = new SqlParameter("@_ActionUser", actionUser);
                pars[3] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Post_UpdateStatus", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }
        public Post GetInfo_Post(int postId)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_PostID", postId);
                return new DBHelper(CoreDBConnectionString).GetInstanceSP<Post>("SP_Post_GetDetail", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new Post();
            }
        }
        //public  PageResult<Post> GetListPost(string keyword,int pageIndex,int pageSize)
        //{
        //    try
        //    {
        //        using (var conn = new SqlConnection(CoreDBConnectionString))
        //        {
        //            if (conn.State == ConnectionState.Closed)
        //                conn.Open();
        //            var pars = new SqlParameter[4];
        //            pars[0] = new SqlParameter("@keyword", keyword);
        //            pars[1] = new SqlParameter("@pageIndex", pageIndex);
        //            pars[2] = new SqlParameter("@pageSize", pageSize);
        //            pars[3] = new SqlParameter("@totalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = "SP_Get_Post_AllPaging";
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddRange(pars);
        //            var result = cmd.ExecuteScalar();
        //            return new PageResult<Post>()
        //            {
        //                Items = (List<Post>)result,
        //                PageIndex = pageIndex,
        //                PageSize = pageSize,
        //                TotalRows = Convert.ToInt32(pars[3].Value)
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log4Net.PublishException(ex);
        //        return new PageResult<Post>();
        //    }
        //}
        public List<Post> GetAllPost(PostRequest requestData)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Status", requestData.Status <= 0 ? DBNull.Value : (object)requestData.Status);
                //pars[1] = new SqlParameter("@_FromDate", requestData.FromDate == new DateTime() ? DBNull.Value : (object)requestData.FromDate);
                //pars[2] = new SqlParameter("@_ToDate", requestData.ToDate == new DateTime() ? DBNull.Value : (object)requestData.ToDate);
                return new DBHelper(CoreDBConnectionString).GetListSP<Post>("SP_CMS_Post_GetPage", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Post>();
            }
        }
        public int Insert_Post(Post requestData)
        {
            try
            {
                var pars = new SqlParameter[15];
                pars[0] = new SqlParameter("@_Title", string.IsNullOrEmpty(requestData.Title) ? DBNull.Value : (object)requestData.Title);
                pars[1] = new SqlParameter("@_TitleDisplay", string.IsNullOrEmpty(requestData.TitleDisplay) ? DBNull.Value : (object)requestData.TitleDisplay);
                pars[2] = new SqlParameter("@_Alias", string.IsNullOrEmpty(requestData.Alias) ? DBNull.Value : (object)requestData.Alias);
                pars[3] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description); 
                pars[4] = new SqlParameter("@_Content", string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object)requestData.Content);
                pars[5] = new SqlParameter("@_ThumbailImage", string.IsNullOrEmpty(requestData.ThumbailImage) ? DBNull.Value : (object)requestData.ThumbailImage);
                pars[6] = new SqlParameter("@_HomeFlag", requestData.HomeFlag);
                pars[7] = new SqlParameter("@_HotFlag", requestData.HotFlag); 
                //pars[8] = new SqlParameter("@_Status", requestData.Status); 
                pars[8] = new SqlParameter("@_ViewCount", requestData.ViewCount); 
                pars[9] = new SqlParameter("@_MetaKeyword", string.IsNullOrEmpty(requestData.MetaKeyword) ? DBNull.Value : (object)requestData.MetaKeyword);
                pars[10] = new SqlParameter("@_MetaDescription", string.IsNullOrEmpty(requestData.MetaDescription) ? DBNull.Value : (object)requestData.MetaDescription);
                pars[11] = new SqlParameter("@_Author", string.IsNullOrEmpty(requestData.Author) ? DBNull.Value : (object)requestData.Author);
                pars[12] = new SqlParameter("@_UpdateBy", string.IsNullOrEmpty(requestData.UpdateBy) ? DBNull.Value : (object)requestData.UpdateBy);        
                pars[13] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[14] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Post_Insert", pars);
                return Convert.ToInt32(pars[14].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }
        public int Update_Post(Post requestData)
        {
            try
            {
                var pars = new SqlParameter[15];
                pars[0] = new SqlParameter("@_Id", requestData.PostID);
                pars[1] = new SqlParameter("@_Title", string.IsNullOrEmpty(requestData.Title) ? DBNull.Value : (object)requestData.Title);
                pars[2] = new SqlParameter("@_TitleDisplay", string.IsNullOrEmpty(requestData.TitleDisplay) ? DBNull.Value : (object)requestData.TitleDisplay);
                pars[3] = new SqlParameter("@_Alias", string.IsNullOrEmpty(requestData.Alias) ? DBNull.Value : (object)requestData.Alias);
                pars[4] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description);
                pars[5] = new SqlParameter("@_Content", string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object)requestData.Content);
                pars[6] = new SqlParameter("@_ThumbailImage", string.IsNullOrEmpty(requestData.ThumbailImage) ? DBNull.Value : (object)requestData.ThumbailImage);
                pars[7] = new SqlParameter("@_HomeFlag", requestData.HomeFlag);
                pars[8] = new SqlParameter("@_HotFlag", requestData.HotFlag);
                pars[9] = new SqlParameter("@_ViewCount", requestData.ViewCount);
                pars[10] = new SqlParameter("@_MetaKeyword", string.IsNullOrEmpty(requestData.MetaKeyword) ? DBNull.Value : (object)requestData.MetaKeyword);
                pars[11] = new SqlParameter("@_MetaDescription", string.IsNullOrEmpty(requestData.MetaDescription) ? DBNull.Value : (object)requestData.MetaDescription);
                pars[12] = new SqlParameter("@_Author", string.IsNullOrEmpty(requestData.Author) ? DBNull.Value : (object)requestData.Author);
                pars[13] = new SqlParameter("@_UpdateBy", string.IsNullOrEmpty(requestData.UpdateBy) ? DBNull.Value : (object)requestData.UpdateBy);
                pars[14] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Post_Update", pars);
                return Convert.ToInt32(pars[14].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }
    }
}
