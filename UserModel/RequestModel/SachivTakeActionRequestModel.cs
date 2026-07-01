using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class SachivTakeActionRequestModel
    {
        public long HandpumpId { get; set; }
        public string Action { get; set; }   // Verified / Rejected
        public int SachivUserId { get; set; }
        public string Remark { get; set; }
    }

    public class SachivTakeActionResponseModel
    {
        public long HandpumpId { get; set; }
        public string SachivStatus { get; set; }
        public DateTime? SachivActionDate { get; set; }
        public string SachivRemark { get; set; }
        public int? SachivApprovedBy { get; set; }
        public string Message { get; set; }
    }
}
