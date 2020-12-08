using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.DataAccess.DTO
{
    public class Function_SMSGateway
    {
        public int FunctionID { set; get; }
        public string FunctionName { set; get; }
        public string Description { set; get; }
        public DateTime CreatedTime { set; get; }
        public bool IsActive { set; get; }
        public string CreatedUser { set; get; }
    }

    public class Function_Param
    {
        public int FunctionID { set; get; }
        public string FunctionName { set; get; }
        public string Description { set; get; }
        public int IsActive { set; get; }
        public string UserAction { set; get; }
    }

    public class SMS_MO
    {
        public Int64 MO_ID { set; get; }
        public string UserID { set; get; }
        public string ServiceID { set; get; }
        public string CommandCode { set; get; }
        public string Message { set; get; }
        public Int64 RequestID { set; get; }
        public DateTime RequestTime { set; get; }
        public int RequestUnixTime { set; get; }
        public DateTime ResponseTime { set; get; }
        public int ResponseUnixTime { set; get; }
        public int Month { set; get; }
        public Int16 StatusProcess { set; get; }
        public string TelcoCode { set; get; }
        public string Message_GOC { set; get; }
        public string PartnerCode { set; get; }
        public string PartnerSecretKey { set; get; }
        public string LinkApi { set; get; }
    }

    public class SMS_MT
    {
        public Int64 MT_ID { set; get; }
        public Int64 MO_ID { set; get; }
        public string UserID { set; get; }
        public string ServiceID { set; get; }
        public string CommandCode { set; get; }
        public string Message { set; get; }
        public Int64 RequestID { set; get; }
        public byte MsgType { set; get; }
        public int ContentType { set; get; }
        public DateTime RequestTime { set; get; }
        public int RequestUnixTime { set; get; }
        public DateTime ResponseTime { set; get; }
        public int ResponseUnixTime { set; get; }
        public int Month { set; get; }
        public Int16 ResponseStatus { set; get; }
        public byte IsCDR { set; get; }
        public byte RetryCount { set; get; }
        public byte MsgCount { set; get; }
        public byte Priority { set; get; }
        public string TelcoCode { set; get; }
        public string PartnerCode { set; get; }
        public string PartnerSecretKey { set; get; }
        public string Signature { set; get; }
    }


    public class Partner
    {
        public int Partner_id { set; get; }
        public string PartnerCode { set; get; }
        public string PartnerName { set; get; }
        public string PartnerSecretKey { set; get; }
        public bool Status { set; get; }
        public string LinkApi { set; get; }
    }

    public class PartnerFunction
    {
        public int id { set; get; }
        public int PartnerID { set; get; }
        public int FunctionID { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedUser { set; get; }
    }

}
