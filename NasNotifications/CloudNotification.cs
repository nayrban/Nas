using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasNotifications
{
    public interface CloudNotification
    {
        void sendNotification(NotificationMessage message);
    }
}
