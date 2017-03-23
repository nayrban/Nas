using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasNotifications
{
    public class NotificationMessage
    {
        public string Message { get; set; }

        public string Title { get; set; }

        public List<string> NotificationKeyList { get; set; }
    }
}
