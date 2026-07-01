using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class RequisitionItemListResponseModel
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public int? RequisitionId { get; set; }
        public int? OrderId { get; set; }
        public int? ItemId { get; set; }
        public string? ItemName { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? Consulting_Egnr_Id { get; set; }
        public string? Consulting_Egnr_Remark { get; set; }


    }
}
