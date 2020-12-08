using System;

namespace DataAccess.MailAPI.Model
{
    public class Email
    {
        public int ServiceID { set; get; }
        public string FromName { set; get; }
        public string FromEmail { set; get; }
        public string ToEmail { set; get; }
        public string Subjects { set; get; }
        public string Messages { set; get; }
        public bool IsResend { set; get; }
    }

    public class SMS
    {
        public int ServiceID { set; get; }
        public string FromName { set; get; }
        public string Mobile { set; get; }       
        public string Messages { set; get; }
        public bool IsResend { set; get; }
        public bool IsFee { set; get; }
    }

    public class SMSMO
    {
        public string ServiceNumber { set; get; }
        public string FromName { set; get; }
        public string Mobile { set; get; }
        public string MOMessages { set; get; }
        public string CommandCode { set; get; }
        public string RequestID { set; get; }
        public bool IsStatusErr { set; get; }
    }

    public class SMSMT
    {
        public long LogID { set; get; }
        public string MTMessages { set; get; }
        public string DescriptionError { set; get; }
        public bool IsStatusErr { set; get; }
    }


    public class EmailInformation
    {
        public long Id { set; get; }
        public string Alias { set; get; }
        public int MainCateID { set; get; }
        public DateTime CreatedDate { set; get; }
        public byte LanguageID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string ImageUrl { set; get; }
        public string Tags { set; get; }
        public byte Type { set; get; }
        public byte Status { set; get; }
        public string UpdatedUser { set; get; }
        public DateTime UpdateDate { set; get; }
        public DateTime PublishDate { set; get; }
        public string Content { set; get; }
        public string ContentSMS { set; get; }
        public int ViewCount { set; get; }
    }
}
