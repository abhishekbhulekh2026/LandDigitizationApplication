using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class AdminActiveDeactiveUser
    {
        public int? UserId { get; set; }
        public bool? Action { get; set; }
        public int? ApproverId { get; set; }
    }
}
