using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Topcourse.Utility
{
    public class SendFromGmail
    {
        public void SendTo(string subject, string messageBody, string email)
        {
            try
            {
                var mail = new MailMessage();
                var smtpServer = new SmtpClient("smtp.gmail.com");
				mail.From = new MailAddress("cuongtau1000@gmail.com");
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.To.Add(email);
				mail.CC.Add("cuongtau1000@gmail.com");
                //mail.Attachments

                /*strFileName has a attachment file name for 
                  attachment process. */
                //string strFileName = null;

                /* We use the following variables to keep track of
                   attachments and after we can delete them */
                //string attach1 = null;
                //string attach2 = null;
                //string attach3 = null;

                /* Bigining of Attachment1 process   & 
                   Check the first open file dialog for a attachment */
                //if (inpAttachment1.PostedFile != null)
                //{
                //    /* Get a reference to PostedFile object */
                //    HttpPostedFile attFile = inpAttachment1.PostedFile;
                //    /* Get size of the file */
                //    int attachFileLength = attFile.ContentLength;
                //    /* Make sure the size of the file is > 0  */
                //    if (attachFileLength > 0)
                //    {
                //        /* Get the file name */
                //        strFileName = Path.GetFileName(inpAttachment1.PostedFile.FileName);
                //        /* Save the file on the server */
                //        inpAttachment1.PostedFile.SaveAs(Server.MapPath(strFileName));
                //        /* Create the email attachment with the uploaded file */
                //        Attachment attach = new Attachment(Server.MapPath(strFileName));
                //        /* Attach the newly created email attachment */
                //        mail.Attachments.Add(attach);
                //        /* Store the attach filename so we can delete it later */
                //        attach1 = strFileName;
                //    }
                //}

                smtpServer.Port = 587;
				smtpServer.Credentials = new System.Net.NetworkCredential("haovmvtc@gmail.com", "abcxyz!@#");
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                //MessageBox.Show("mail Send");

                /* Delete the attachements if any */
                //if (attach1 != null)
                //    File.Delete(Server.MapPath(attach1));
                //if (attach2 != null)
                //    File.Delete(Server.MapPath(attach2));
                //if (attach3 != null)
                //    File.Delete(Server.MapPath(attach3));
			}
			catch (Exception ex)
			{
				Log4Net.LogInfo(ex.ToString());
			}
        }
    }
}
