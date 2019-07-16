using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TriggerWebhook
{
    [Serializable]
    public class RequestBody
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string MailNickname { get; set; }
        public string[] Owner { get; set; }
    }
}
