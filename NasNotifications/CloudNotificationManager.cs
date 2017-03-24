using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasNotifications
{
    public class CloudNotificationManager
    {
        public static async Task SendNotification(NotificationMessage notificationMessage)
        {
            CloudNotification cloudNotification = null;

            switch (notificationMessage.NotificationType)
            {
                case NotificationType.Email:
                    cloudNotification = new EmailNotification();
                    break;
                case NotificationType.Fcm:
                    cloudNotification = new FcmNotification();
                    break;
            }
            if(cloudNotification != null)
                await cloudNotification.sendNotification(notificationMessage);
        }
    }
}
