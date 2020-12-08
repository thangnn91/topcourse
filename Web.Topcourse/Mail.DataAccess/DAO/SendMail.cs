using Common.Utilities.Database;
using Common.Utilities.Log;
using DataAccess.MailAPI.IDAO;
using DataAccess.MailAPI.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.MailAPI.DAO
{
    public class SendMail : ISendMail
    {
        public long InsertEmail(Email requestData)
        {
            try
            {
                var pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@_ServiceID", requestData.ServiceID);
                pars[1] = new SqlParameter("@_FromName", string.IsNullOrEmpty(requestData.FromName) ? DBNull.Value : (object)requestData.FromName);
                pars[2] = new SqlParameter("@_FromEmail", string.IsNullOrEmpty(requestData.FromEmail) ? DBNull.Value : (object)requestData.FromEmail);
                pars[3] = new SqlParameter("@_ToEmail", string.IsNullOrEmpty(requestData.ToEmail) ? DBNull.Value : (object)requestData.ToEmail);
                pars[4] = new SqlParameter("@_Subjects", string.IsNullOrEmpty(requestData.Subjects) ? DBNull.Value : (object)requestData.Subjects);
                pars[5] = new SqlParameter("@_Messages", string.IsNullOrEmpty(requestData.Messages) ? DBNull.Value : (object)requestData.Messages);
                pars[6] = new SqlParameter("@_IsResend", requestData.IsResend);
                pars[7] = new SqlParameter("@_ResponseStatus", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                new DBHelper(DBConfig.MailServiceConnectionString).ExecuteNonQuerySP("SP_Mail_Insert", pars);
                return Convert.ToInt64(pars[7].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -696;
            }
        }

        public long EmailUpdateStatus(long logID, int status)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_LogID", logID);
                pars[1] = new SqlParameter("@_Status", status);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                new DBHelper(DBConfig.MailServiceConnectionString).ExecuteNonQuerySP("SP_Mail_UpdateStatus", pars);
                return Convert.ToInt64(pars[2].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -696;
            }
        }

    }
}
