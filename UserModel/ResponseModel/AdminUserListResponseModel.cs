using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class AdminUserListResponseModel
    {
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Status { get; set; }
        public string? FullName { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public int? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public int? BlockId { get; set; }
        public string? BlockName { get; set; }
        public int? GramPanchayatId { get; set; }
        public string? GramPanchayatName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public string? ProfileImage { get; set; }
        public string? ApprovalStatus { get; set; }
       
    }
}
