using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class AdminUserStatusRequestModel
    {
        public int? UserId { get; set; }
        public string? Status { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
    }
}
