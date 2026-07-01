using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class AdminUserApprovalRequestModel
    {
        public int? UserId { get; set; }
        public string? Action { get; set; }
        public int? ApproverId { get; set; }
        
    }
}
