using System;
using System.Net.Mail;
using System.Net;

namespace Topcourse.Utility
{
    public class StringUtil
    {

        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"

        };



        public static string RemoveSign4VietnameseString(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str.Trim().Replace(' ', '-');

        }

        public static string DbFormatString(string originString)
        {
            if (string.IsNullOrEmpty(originString))
                return string.Empty;
            return originString.Replace("'", "\"").Replace("#", "").Replace("|", "");
        }

    }

    public class VTCSendMail
    {
        public bool Send(string toAddress, string subject, string body)
        {
            try
            {
                SmtpClient smtpClient;
                MailMessage message = new MailMessage();

                message = new MailMessage();
                smtpClient = new SmtpClient("mail.vtc.vn", 25);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("info.paygate", @"khongcopass");
                message.From = new MailAddress("info.paygate@vtc.vn", "VTC Paygate");
                //message.ReplyTo = new MailAddress("info.paygate@vtc.vn", "VTC eBank");
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;
                message.To.Add(toAddress);
                message.CC.Add("manhhao.vu@vtc.vn");
                //message.CC.Add("huan.khuc@ vtc.vn");
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return false;
            }
        }



        // public string SendMailPaymnetWebsite
    }
}
