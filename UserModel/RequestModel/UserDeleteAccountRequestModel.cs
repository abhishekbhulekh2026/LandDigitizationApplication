using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class UserDeleteAccountRequestModel
    {
        public int UserId { get; set; }
        public string? Reason { get; set; }
    }
}
