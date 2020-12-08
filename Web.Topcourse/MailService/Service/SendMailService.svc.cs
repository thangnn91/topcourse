using Common.Utilities.Log;
using Common.Utilities.Security;
using DataAccess.MailAPI.Model;
using MailService.Model;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MailService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SendMail" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SendMail.svc or SendMail.svc.cs at the Solution Explorer and start debugging.
    public class SendMailService : ISendMailService
    {
        private int _Port;
        private string _Host;
        private string _CredentialEmail;
        private string _CredentialPassword;
        public SendMailService()
        {
            _Port = Convert.ToInt32(ConfigurationManager.AppSettings["PORT"] ?? "587");
            _Host = ConfigurationManager.AppSettings["HOST"] ?? "smtp.gmail.com";
            _CredentialEmail = ConfigurationManager.AppSettings["CREDENTIAL_NAME"];
            _CredentialPassword = Security.AESDecryptString(ConfigurationManager.AppSettings["CREDENTIAL_PASS_ENCR"], ConfigurationManager.AppSettings["SECURE_KEY"]);
        }

        public ResponseData SendEmailManual(EmailModel requestSendMail)
        {
            if (string.IsNullOrEmpty(requestSendMail.Sign) || string.IsNullOrEmpty(requestSendMail.ToMail) || string.IsNullOrEmpty(requestSendMail.Content))
            {
                NLogManager.LogMessage($"request send mail invalid. request: {JsonConvert.SerializeObject(requestSendMail)}");
                return new ResponseData { ResponseCode = -1001, Description = "Du lieu dau vao ko hop le" };
            }

            //Check sign
            var secretKey = ConfigurationManager.AppSettings["SECURE_KEY"];
            var plainText = $"{requestSendMail.ToMail}{requestSendMail.Content[0]}{secretKey}";
            if (!requestSendMail.Sign.Equals(Security.MD5Encrypt(plainText)))
            {
                NLogManager.LogMessage($"signature invalid. plainText: {plainText} | request sign: {requestSendMail.Sign}");
                return new ResponseData { ResponseCode = -1002, Description = "Chu ky ko hop le" };
            }
            var requestInsertMail = new Email
            {
                ToEmail = requestSendMail.ToMail,
                FromEmail = _CredentialEmail,
                FromName = "SUPPORT.EDUNET",
                Messages = requestSendMail.Content,
                Subjects = requestSendMail.Title,
                ServiceID = requestSendMail.ServiceID,
                IsResend = requestSendMail.IsResend
            };

            if (Send(requestInsertMail))
            {
                return new ResponseData { ResponseCode = 0, Description = "Gui email thanh cong" };
            }
            return new ResponseData { ResponseCode = -1, Description = "Gui email that bai" };
        }

        private bool Send(Email requestSendMail)
        {
            try
            {              
                MailAddress fromAddr = new MailAddress(_CredentialEmail, requestSendMail.FromName, Encoding.UTF8);
                MailAddress toAddr = new MailAddress(requestSendMail.ToEmail, string.Empty, Encoding.UTF8);
                var smtp = new SmtpClient
                {
                    Host = _Host,
                    Port = _Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_CredentialEmail, _CredentialPassword)
                };

                using (MailMessage message = new MailMessage(fromAddr, toAddr)
                {
                    Subject = requestSendMail.Subjects,
                    Body = requestSendMail.Messages,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,

                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return false;
            }
        }       
    }
}
