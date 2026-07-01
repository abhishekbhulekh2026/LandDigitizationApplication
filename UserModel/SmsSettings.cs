using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel
{
    public class SmsSettings
    {
        public string ApiKey { get; set; }
        public string SenderId { get; set; }
        public string SignupTemplateId { get; set; }
        public string LoginTemplateId { get; set; }
    }
}
