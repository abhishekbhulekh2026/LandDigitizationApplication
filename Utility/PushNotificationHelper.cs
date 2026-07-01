using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class PushNotificationHelper
    {
        public static void SendNotification(string push_message, string device_token)
        {
            var message = new
            {
                to = "<device_token>",
                notification = new
                {
                    title = "New Message",
                    body = "Hello from .NET Core!"
                }
            };

            var json = JsonConvert.SerializeObject(message);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", "YOUR_FCM_SERVER_KEY");

            var response = client.PostAsync("https://fcm.googleapis.com/fcm/send", httpContent);
        }
    }
}
