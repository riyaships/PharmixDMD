using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmix.Web.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private IConfiguration _configuration;
        string _ForgotPasswordTemplatePath = string.Empty;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _ForgotPasswordTemplatePath = @"C:/Files/PharmixForgotPasswordTemplate.htm";
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            string messageBody = string.Empty;
            using (StreamReader reader = new StreamReader(_ForgotPasswordTemplatePath))
            {
                messageBody = reader.ReadToEnd();
            }

            StringBuilder str = new StringBuilder();
            //messageBody = string.Concat(_XMLFilePath, filePath);
            messageBody = messageBody.Replace("{EMAIL_NAME}", email);
            messageBody = messageBody.Replace("{resetPasswordLink}", message);


            new PharmixCommon.Email().SendEmail(email, subject, messageBody);
            return Task.CompletedTask;
        }
    }
}
