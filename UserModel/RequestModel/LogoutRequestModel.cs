using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class LogoutRequestModel
    {
        public long UserId { get; set; }
        public string TokenJti { get; set; }
        public DateTime? TokenExpiry { get; set; }
        public string Reason { get; set; } = "Logout";
    }
}
