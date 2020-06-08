using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PharmixCommon
{
   public class Email
    {
        public bool SendEmail(string email, string subject, string message,string file="")
        {
            try
            {
                MailMessage msz = new MailMessage
                {
                    From = new MailAddress("contact.web@pharmix.co.uk", subject),//Email which you are getting from contact us page 
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,

                };
                msz.To.Add(email);//Where mail will be sent  
                SmtpClient smtp = new SmtpClient
                {
                    Host = "send.one.com",
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("contact.web@pharmix.co.uk", "Pharmix123#"),
                    EnableSsl = true
                };


                smtp.Send(msz);
                return true;
            }
            catch (Exception ex)
            {
                TraceService("Exception- " + ex.Message + "at " + DateTime.Now);
                return false;
            }
        }

        private void TraceService(string content)
        {

            //set up a filestream
            FileStream fs = new FileStream(ConfigurationManager.AppSettings["LogFilePath"].ToString(), FileMode.OpenOrCreate, FileAccess.Write);
            //FileStream fs = new FileStream("F:My App/Upwork/Files/Log.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //set up a streamwriter for adding text
            StreamWriter sw = new StreamWriter(fs);

            //find the end of the underlying filestream
            sw.BaseStream.Seek(0, SeekOrigin.End);

            //add the text
            sw.WriteLine(content);
            //add the text to the underlying filestream

            sw.Flush();
            //close the writer
            sw.Close();


        }
    }
}
