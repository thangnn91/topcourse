using System;
using System.Net.Mail;
using System.Net.Sockets;
using System.Web.Http;
using Topcourse.Utility;

namespace CMS.Topcourse.Controllers.Common
{
    public class POP3Auth : System.Net.Sockets.TcpClient
    {
        public POP3Auth()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string CheckAuth(string username, string password)
        {
            string message;
            string response;
            Connect("mail.vtc.vn", 110);
            response = Response();
            if (response.Substring(0, 3) != "+OK")
            {
                return response;
            }
            message = "USER " + username + "\r\n";
            Write(message);
            response = Response();

            if (response.Substring(0, 3) != "+OK")
            {
                return response;
            }
            message = "PASS " + password + "\r\n";
            Write(message);
            response = Response();
            if (response.Substring(0, 3) != "+OK")
            {
                return response;
            }
            message = "QUIT\r\n";
            Write(message);
            return "OK";
        }
        private string Response()
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] serverbuff = new Byte[1024];
            NetworkStream stream = GetStream();
            int count = 0;
            while (true)
            {
                byte[] buff = new Byte[2];
                int bytes = stream.Read(buff, 0, 1);
                if (bytes == 1)
                {
                    serverbuff[count] = buff[0];
                    count++;
                    if (buff[0] == '\n')
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            string retval = enc.GetString(serverbuff, 0, count);
            return retval;
        }
        private void Write(string message)
        {

            System.Text.ASCIIEncoding en = new System.Text.ASCIIEncoding();
            byte[] WriteBuffer = new byte[1024];
            WriteBuffer = en.GetBytes(message);
            NetworkStream stream = GetStream();
            stream.Write(WriteBuffer, 0, WriteBuffer.Length);
        }


        [HttpPost]
        public bool SendEmail(string _name, string _email, string _description)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("hotro1.edunet@gmail.com");
                mail.To.Add("cuongbx@1fintech.vn");
                mail.Subject = _name;
                mail.Body = _description;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("hotro1.edunet@gmail.com", "Aa@123456");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return false;
            }
            return true;
        }
    }
}