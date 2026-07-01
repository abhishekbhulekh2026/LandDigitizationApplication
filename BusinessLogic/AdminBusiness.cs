using Microsoft.Extensions.Options;
using Repository;
using Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModel;
using UserModel.RequestModel;
using UserModel.ResponseModel;

namespace BusinessLogic
{
    public interface IAdminBusiness
    {
        Task<CreateUpdateDeleteResponse> CheckApprovalStatus(int requestId);
        Task<CreateUpdateDeleteResponse> ProcessApproval(AdminUserApprovalRequestModel request);
        Task<CreateUpdateDeleteResponse> GetUserPendingApprovalList();

        Task<CreateUpdateDeleteResponse> GetUserList();
        Task<CreateUpdateDeleteResponse> AdminActiveDeactiveUser(AdminActiveDeactiveUser request);
    }

    public class AdminBusiness : IAdminBusiness
    {
        private static string sqlDataSource = CommonVariables.ConnectionString;
        BaseDAL _baseDAL = new BaseDAL();
        private readonly string _domainUrl;
        public AdminBusiness(IOptions<DomainSettings> appSettings)
        {
            _domainUrl = appSettings.Value.DomainUrl;
        }
        public async Task<CreateUpdateDeleteResponse> CheckApprovalStatus(int requestId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<AdminUserStatusRequestModel> checkappvovalresmdl = new List<AdminUserStatusRequestModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
             {
                 new CustomDataPair() { Key = "@UserId", Obj = requestId }
             };
                dt = _baseDAL.GetData(sqlDataSource, "CheckApprovalStatus", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        checkappvovalresmdl.Add(new AdminUserStatusRequestModel
                        {
                            UserId = string.IsNullOrEmpty(row["user_id"]?.ToString()) ? 0 : Convert.ToInt32(row["user_id"]),

                            Status = row["status_approval"]?.ToString() ?? string.Empty,

                            ApprovedBy = string.IsNullOrEmpty(row["status_approved_by"]?.ToString()) ? 0 : Convert.ToInt32(row["status_approved_by"]),

                            ApprovedOn = string.IsNullOrEmpty(row["updated_date"]?.ToString()) ? (DateTime?)null : Convert.ToDateTime(row["updated_date"])
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = checkappvovalresmdl, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> ProcessApproval(AdminUserApprovalRequestModel request)
        {
            string message = string.Empty;
            int result = 0;
            DataTable dt = new DataTable();
            try
            {
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
             {
                 new CustomDataPair() { Key = "@UserId", Obj = request.UserId },
                 new CustomDataPair() { Key = "@Action", Obj = request.Action},
                 new CustomDataPair() { Key = "@ApproverId", Obj = request.ApproverId}
             };
                result = _baseDAL.InsertData(out message, sqlDataSource, "ProcessApproval", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (result > 0)
                {
                    return new CreateUpdateDeleteResponse
                    {
                        Message = message,
                        Status = true
                    };
                }

                else
                {
                    return new CreateUpdateDeleteResponse
                    {

                        Message = message,
                        Status = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse
                {

                    Message = message,
                    Status = false
                };
            }
        }

        public async Task<CreateUpdateDeleteResponse> GetUserPendingApprovalList()
        {
            DataTable dt = new DataTable();
            try
            {
                List<AdminUserListResponseModel> pendingapprovalresmdl = new List<AdminUserListResponseModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                {

                };
                dt = _baseDAL.GetData(sqlDataSource, "GetPendingApprovals", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        pendingapprovalresmdl.Add(new AdminUserListResponseModel
                        {
                            UserId = Convert.ToInt32(row["user_id"].ToString()),
                            RoleId = Convert.ToInt32(row["role_id"].ToString()),
                            RoleName = row["role_name"].ToString(),
                            Status = row["status_approval"].ToString(),
                            FullName = row["full_name"].ToString(),
                            ContactNo = row["mobile_number"].ToString(),
                            Email = row["email"].ToString(),
                            DistrictId = Convert.ToInt32(row["district_id"].ToString()),
                            DistrictName = row["districtname"].ToString(),
                            BlockId = Convert.ToInt32(row["block_id"].ToString()),
                            BlockName = row["blockname"].ToString(),
                            GramPanchayatId = Convert.ToInt32(row["gp_id"].ToString()),
                            GramPanchayatName = row["grampanchayatname"].ToString(),
                            CreatedOn = Convert.ToDateTime(row["created_date"].ToString()),
                            CreatedBy = Convert.ToInt32(row["created_by"].ToString()),
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = pendingapprovalresmdl, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> GetUserList()
        {
            DataTable dt = new DataTable();
            try
            {
                List<UserListResponseModel> userprofileresmdl = new List<UserListResponseModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                //new CustomDataPair() { Key = "@UserId", Obj = UserId },
                 // new CustomDataPair(){ Key="@QueryType", Obj="GetPendingSachivList"}
            };
                dt = _baseDAL.GetData(sqlDataSource, "GetUserListAdmin", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        userprofileresmdl.Add(new UserListResponseModel
                        {
                            UserId = string.IsNullOrWhiteSpace(row["user_id"]?.ToString())
            ? 0
            : Convert.ToInt32(row["user_id"]),

                            RoleId = string.IsNullOrWhiteSpace(row["role_id"]?.ToString())
            ? (int?)null
            : Convert.ToInt32(row["role_id"]),

                            RoleName = string.IsNullOrWhiteSpace(row["role_name"]?.ToString())
            ? "NA"
            : row["role_name"].ToString(),

                            ProfileImage = string.IsNullOrWhiteSpace(row["profile_pic"]?.ToString())
            ? "NA"
            : _domainUrl + row["profile_pic"].ToString(),

                            ApprovalStatus = string.IsNullOrWhiteSpace(row["status_approval"]?.ToString())
            ? "NA"
            : row["status_approval"].ToString(),

                            FullName = string.IsNullOrWhiteSpace(row["full_name"]?.ToString())
            ? "NA"
            : row["full_name"].ToString(),

                            ContactNo = string.IsNullOrWhiteSpace(row["mobile_number"]?.ToString())
            ? "NA"
            : row["mobile_number"].ToString(),

                            Email = string.IsNullOrWhiteSpace(row["email"]?.ToString())
            ? "NA"
            : row["email"].ToString(),

                            DistrictId = string.IsNullOrWhiteSpace(row["district_id"]?.ToString())
            ? 0
            : Convert.ToInt32(row["district_id"]),

                            DistrictName = string.IsNullOrWhiteSpace(row["districtname"]?.ToString())
            ? "NA"
            : row["districtname"].ToString(),

                            BlockId = string.IsNullOrWhiteSpace(row["block_id"]?.ToString())
            ? 0
            : Convert.ToInt32(row["block_id"]),

                            BlockName = string.IsNullOrWhiteSpace(row["blockname"]?.ToString())
            ? "NA"
            : row["blockname"].ToString(),

                            GramPanchayatId = string.IsNullOrWhiteSpace(row["gp_id"]?.ToString())
            ? 0
            : Convert.ToInt32(row["gp_id"]),

                            GramPanchayatName = string.IsNullOrWhiteSpace(row["grampanchayatname"]?.ToString())
            ? "NA"
            : row["grampanchayatname"].ToString(),

                            CreatedOn = string.IsNullOrWhiteSpace(row["created_date"]?.ToString())
            ? DateTime.MinValue
            : Convert.ToDateTime(row["created_date"]),

                            CreatedBy = string.IsNullOrWhiteSpace(row["created_byId"]?.ToString())
            ? 0
            : Convert.ToInt32(row["created_byId"]),

                            CreatedByName = string.IsNullOrWhiteSpace(row["createdby"]?.ToString())
            ? "NA"
            : row["createdby"].ToString(),

                            Status = row["isActive"] != DBNull.Value && Convert.ToBoolean(row["isActive"])
            ? "Active"
            : "InActive"


                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = userprofileresmdl, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> AdminActiveDeactiveUser(AdminActiveDeactiveUser request)
        {
            string message = string.Empty;
            int result = 0;
            DataTable dt = new DataTable();
            string action = "";

            try
            {
                if(request.Action==true)
                    action = "activate";
                else
                    action = "deactive";

                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
             {
                 new CustomDataPair() { Key = "@UserId", Obj = request.UserId },
                 new CustomDataPair() { Key = "@Action", Obj = action},
                 new CustomDataPair() { Key = "@ApproverId", Obj = request.ApproverId}
             };
                result = _baseDAL.InsertData(out message, sqlDataSource, "AdminApprovalUser", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (result > 0)
                {
                    return new CreateUpdateDeleteResponse
                    {
                        Message = message,
                        Status = true
                    };
                }

                else
                {
                    return new CreateUpdateDeleteResponse
                    {

                        Message = message,
                        Status = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse
                {

                    Message = message,
                    Status = false
                };
            }
        }
    }
}
