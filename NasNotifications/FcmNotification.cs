using FCM.Net;
using System;
using System.Threading.Tasks;

namespace NasNotifications
{
    public class FcmNotification : CloudNotification
    {
        private const string FCM_SERVER_KEY = "FcmServerKey";

        private string serverKey;

        public FcmNotification()
        {
            serverKey = System.Configuration.ConfigurationManager.AppSettings[FCM_SERVER_KEY];
        }

        public async Task sendNotification(NotificationMessage notificationMessage)
        {
            using (var sender = new NasFcmSender(serverKey))
            {
                var message = new Message
                {
                    RegistrationIds = notificationMessage.NotificationKeyList,
                    Notification = new Notification
                    {
                        Title = notificationMessage.Title,
                        Body = notificationMessage.Message
                    }
                };
                var result = await sender.SendAsync(message);
                Console.WriteLine($"Success: {result.MessageResponse.Success}");           
            }
        }

     
    }
}
