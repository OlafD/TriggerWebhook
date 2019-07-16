using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace TriggerWebhook
{
    class Program
    {
        static string WEBHOOK_URL = "https://s2events.azure-automation.net/webhooks";

        static void Main(string[] args)
        {
            ConsoleHelper cs = new ConsoleHelper("TriggerWebhook", "(c) PlanB. GmbH / OlafD (2019)");
            cs.welcome();

            string webhookId = cs.getNamedArgument(args, "WebhookId", "");
            string description = cs.getNamedArgument(args, "Description", "");
            string displayName = cs.getNamedArgument(args, "DisplayName", "");
            string mailNickname = cs.getNamedArgument(args, "MailNickname", "");
            string owners = cs.getNamedArgument(args, "Owners", "");

            RequestBody requestBody = new RequestBody();
            requestBody.DisplayName = displayName;
            requestBody.Description = description;
            requestBody.MailNickname = mailNickname;

            string[] arr = owners.Split(',');
            requestBody.Owner = arr;

            string jsonBody = JsonConvert.SerializeObject(requestBody);

            WebRequest request = WebRequest.Create(GetWebhook(webhookId));
            request.Method = "Post";
            request.ContentType = "application/json";
            
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(jsonBody);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    string responseString = sr.ReadToEnd();

                    Console.WriteLine(responseString);
                }
            }
        }

        static string GetWebhook(string webhookId)
        {
            return string.Format("{0}?token={1}", WEBHOOK_URL, webhookId);
        }
    }
}
