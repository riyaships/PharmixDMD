using System;
using System.Net.Mail;

namespace Pharmix.Web.Extensions
{
    public class Email
    {
        public bool SendEmail(string email, string subject, string message)
        {
            try
            {
                MailMessage msz = new MailMessage
                {
                    From = new MailAddress("ds@dataweb24x7.co.uk", "Pharmix Web Enquiry"),//Email which you are getting from contact us page 
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };
                msz.To.Add(email);//Where mail will be sent  
                SmtpClient smtp = new SmtpClient
                {
                    Host = "send.one.com",
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("ds@dataweb24x7.co.uk", "Pharmix123#"),
                    EnableSsl = true
                };

                smtp.Send(msz);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    public static class PharmixStaticHelper
    {
        public static string SuperAdminUsername { get; set; } = "pharmix@pharmix.co.uk";
        public static string SuperAdminPassword { get; set; } = "Pharmix123#";
        public static string SuperAdminRoleName { get; set; } = "SuperAdmin";
    }
}
