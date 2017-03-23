using FCM.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasNotifications
{
    public class NasFcmSender : Sender , IDisposable
    {
        public NasFcmSender(string serverKey) : base(serverKey)
        {
        }

        public void Dispose()
        {
            
        }
    }
}
