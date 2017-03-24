using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NasNotifications
{
    public class EmailNotification : CloudNotification
    {

        private const string HOST = "Smtp.Host";
        private const string PORT = "Smtp.Port";
        private const string USE_SSL = "Smtp.EnableSsl";
        private const string EMAIL_PASSWORD = "fromPassword";
        private const string EMAIL_USER = "fromAddress";
        private const string EMAIL_NAME = "fromName";

        private  int _port;
        private  string _host;
        private  bool _ssl;
        private  string _pwd;
        private string _email;
        private string _name;

        public EmailNotification()
        {
            _port = int.Parse(System.Configuration.ConfigurationManager.AppSettings[PORT]);
            _host = System.Configuration.ConfigurationManager.AppSettings[HOST];
            _ssl = bool.Parse( System.Configuration.ConfigurationManager.AppSettings[USE_SSL]);
            _pwd = System.Configuration.ConfigurationManager.AppSettings[EMAIL_PASSWORD];
            _email = System.Configuration.ConfigurationManager.AppSettings[EMAIL_USER];
            _name = System.Configuration.ConfigurationManager.AppSettings[EMAIL_NAME];
        }


        public async Task sendNotification(NotificationMessage notMessage)
        {
            var fromAddress = new MailAddress(_email, _name);
            var toAddress = new MailAddress(notMessage.To, notMessage.Name);

            var smtp = new SmtpClient
            {
                Host = _host,
                Port = _port,
                EnableSsl = _ssl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _pwd)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = notMessage.Title,
                Body = notMessage.Message
            })
            {
               await smtp.SendMailAsync(message);
            }
        }
    }
}
