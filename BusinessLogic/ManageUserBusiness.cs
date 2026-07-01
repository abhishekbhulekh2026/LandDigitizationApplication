using Microsoft.Extensions.Options;
using Org.BouncyCastle.Utilities;
using Repository;
using Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModel;
using UserModel.ResponseModel;
using Utility;

namespace BusinessLogic
{
    public interface IManageUserBusiness
    {
        Task<CreateUpdateDeleteResponse> GetPendingUserList(int UserId);
        Task<CreateUpdateDeleteResponse> ApproveUser(int UserId, int ApprovedBy_UserId);
        Task<CreateUpdateDeleteResponse> RejectUser(int UserId, int ApprovedBy_UserId, string Remark);
    }

    public class ManageUserBusiness : IManageUserBusiness
    {
        private static string sqlDataSource = CommonVariables.ConnectionString;
        BaseDAL _baseDAL = new BaseDAL();
        private readonly string _domainUrl;
        public ManageUserBusiness(IOptions<DomainSettings> appSettings)
        {
            _domainUrl = appSettings.Value.DomainUrl;
        }

        public async Task<CreateUpdateDeleteResponse> ApproveUser(int UserId, int ApprovedBy_UserId)
        {
            DataTable dt = new DataTable();
            int result = 0;
            string message = string.Empty;
            string messageBody = "";
            try
            {
                AdminUserListResponseModel userprofileresmdl = new AdminUserListResponseModel();
                List<CustomDataPair> stringDataPairsget = new List<CustomDataPair>
            {
                new CustomDataPair() { Key = "@UserId", Obj = UserId }
            };
                dt = _baseDAL.GetData(sqlDataSource, "GetUserProfileDetails", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairsget));

                if (dt != null)
                {

                    userprofileresmdl.UserId = Convert.ToInt32(dt.Rows[0]["user_id"].ToString());
                    userprofileresmdl.RoleId = Convert.ToInt32(dt.Rows[0]["role_id"].ToString());
                    userprofileresmdl.RoleName = dt.Rows[0]["role_name"].ToString();
                    userprofileresmdl.ProfileImage = _domainUrl + dt.Rows[0]["profile_pic"].ToString();
                    userprofileresmdl.ApprovalStatus = dt.Rows[0]["status_approval"].ToString();
                    userprofileresmdl.FullName = dt.Rows[0]["full_name"].ToString();
                    userprofileresmdl.ContactNo = dt.Rows[0]["mobile_number"].ToString();
                    userprofileresmdl.Email = dt.Rows[0]["email"].ToString();
                    userprofileresmdl.DistrictId = Convert.ToInt32(dt.Rows[0]["district_id"].ToString());
                    userprofileresmdl.DistrictName = dt.Rows[0]["districtname"].ToString();
                    userprofileresmdl.BlockId = Convert.ToInt32(dt.Rows[0]["block_id"].ToString());
                    userprofileresmdl.BlockName = dt.Rows[0]["blockname"].ToString();
                    
                    userprofileresmdl.GramPanchayatId = string.IsNullOrWhiteSpace(dt.Rows[0]["gp_id"].ToString()) ? (int?)null : Convert.ToInt32(dt.Rows[0]["gp_id"]);

                    userprofileresmdl.GramPanchayatName = dt.Rows[0]["grampanchayatname"].ToString();
                    userprofileresmdl.CreatedOn = Convert.ToDateTime(dt.Rows[0]["created_date"].ToString());
                    userprofileresmdl.CreatedBy = Convert.ToInt32(dt.Rows[0]["created_byId"].ToString());
                    userprofileresmdl.CreatedByName = dt.Rows[0]["createdby"].ToString();
                    }
            
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                new CustomDataPair() { Key = "@UserId", Obj = UserId },
                  new CustomDataPair() { Key = "@ApprovedBy_UserId", Obj = ApprovedBy_UserId },
                    new CustomDataPair(){ Key="@QueryType", Obj="ApproveUser"},
                     new CustomDataPair(){ Key="Remark", Obj= null}
            };
                result = _baseDAL.InsertData(out message, sqlDataSource, "ApproveRejectUser", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (message == "Approved Successfully..")
                {

                    messageBody = $@"
                        <p>Dear <b>{userprofileresmdl.FullName}</b>,</p>
                        
                        <p>🎉 <b>Congratulations!</b></p>
                        
                        <p>Your account has been <b>successfully approved</b>.</p>
                        
                        <p>✅ You can now log in and start using the <b>Handpump Management System</b> mobile app.</p>
                        
                        <p>Best regards,<br/>
                        <b>Handpump Management System Team</b></p>
                        ";

                    EmailSendHelper.SendEmail("kdsdeveloper25@gmail.com", "fddxnjbzdbrpfzff", "" + userprofileresmdl.Email + "", "Handpump Management System Signup Successful", messageBody);

                    //string otpResponseMsg = await  _otpHelper.SendOtpSms(logins.MobileNo, "signup");

                }

              
                return new CreateUpdateDeleteResponse
                {
                    Message = message,
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> RejectUser(int UserId, int ApprovedBy_UserId, string Remark)
        {
            DataTable dt = new DataTable();
            int result = 0;
            string message = string.Empty;
            try
            {
                List<UserListResponseModel> userprofileresmdl = new List<UserListResponseModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                new CustomDataPair() { Key = "@UserId", Obj = UserId },
                  new CustomDataPair() { Key = "@ApprovedBy_UserId", Obj = ApprovedBy_UserId },
                    new CustomDataPair(){ Key="@QueryType", Obj="RejectUser"},
                     new CustomDataPair(){ Key="Remark", Obj= Remark}
            };
                result = _baseDAL.InsertData(out message, sqlDataSource, "ApproveRejectUser", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));
                return new CreateUpdateDeleteResponse
                {
                    Message = message,
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }
        public async Task<CreateUpdateDeleteResponse> GetPendingUserList(int UserId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<UserListResponseModel> userprofileresmdl = new List<UserListResponseModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                new CustomDataPair() { Key = "@UserId", Obj = UserId },
                 // new CustomDataPair(){ Key="@QueryType", Obj="GetPendingSachivList"}
            };
                dt = _baseDAL.GetData(sqlDataSource, "GetUserPendingApprovalList", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        userprofileresmdl.Add(new UserListResponseModel
                        {
                            UserId = Convert.ToInt32(row["user_id"].ToString()),
                            RoleId = Convert.ToInt32(row["role_id"].ToString()),
                            RoleName = row["role_name"].ToString(),
                            ProfileImage = _domainUrl + row["profile_pic"].ToString(),
                            ApprovalStatus = row["status_approval"].ToString(),
                            FullName = row["full_name"].ToString(),
                            ContactNo = row["mobile_number"].ToString(),
                            Email = row["email"].ToString(),
                            DistrictId = Convert.ToInt32(row["district_id"].ToString()),
                            DistrictName = row["districtname"].ToString(),
                            BlockId = Convert.ToInt32(row["block_id"].ToString()),
                            BlockName = row["blockname"].ToString(),
                            GramPanchayatId = string.IsNullOrWhiteSpace(row["gp_id"].ToString()) ? (int?)null : Convert.ToInt32(row["gp_id"]),

                            GramPanchayatName = row["grampanchayatname"].ToString(),
                            CreatedOn = Convert.ToDateTime(row["created_date"].ToString()),
                            CreatedBy = Convert.ToInt32(row["created_byId"].ToString()),
                            CreatedByName = row["createdby"].ToString(),
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


    }
}
