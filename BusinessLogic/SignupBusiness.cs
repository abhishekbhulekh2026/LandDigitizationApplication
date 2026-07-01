using Microsoft.Extensions.Options;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Utilities;
using Repository;
using Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserModel;
using UserModel.RequestModel;
using UserModel.ResponseModel;
using Utility;
namespace BusinessLogic
{
    public interface ISignupBusiness
    {
        Task<CreateUpdateDeleteResponse> UserSignup(SignupRequestModel logins);
        Task<CreateUpdateDeleteResponse> GetUserProfileById(int UserId);
        Task<string> SendOtpSms(string phone, string messageType);
        string DecryptPasswordString(string passtring, string passkey);
    }

    public class SignupBusiness : ISignupBusiness
    {
        private static string sqlDataSource = CommonVariables.ConnectionString;
        BaseDAL _baseDAL = new BaseDAL();
        private readonly string _domainUrl;
        private readonly EncryptDecryptHelper _aes;

        public SignupBusiness(IOptions<DomainSettings> appSettings, EncryptDecryptHelper aes)
        {
            _domainUrl = appSettings.Value.DomainUrl;
            _aes = aes;
           
        }

        public async Task<CreateUpdateDeleteResponse> UserSignup(SignupRequestModel logins)
        {
            SignupRequestModel userSignup = new SignupRequestModel();
            string message = string.Empty;
            string filepathstr = "";
            string messageBody = "";
            string unEncrptdpass = logins.Password;
            try
            {
                if(!String.IsNullOrEmpty(logins.Email) && !String.IsNullOrEmpty(logins.Password) && !String.IsNullOrEmpty(logins.FullName))
                {
                    int result = 0;
                    DataTable dt = new DataTable();

                    if (!String.IsNullOrEmpty(logins.FileBase64String))
                    {
                        logins.ProfilePhotoPath = GetFileUploadPath(logins.FileBase64String, logins.FullName);
                    }

                    //  _aes.GenerateKeys();
                    var encrypted = _aes.EncryptStringToBytes_Aes(logins.Password);
                    logins.Password = encrypted.ToString();

                    List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
            new CustomDataPair() { Key = "@FullName", Obj = logins.FullName },
            new CustomDataPair() { Key = "@MobileNo", Obj = logins.MobileNo },
            new CustomDataPair() { Key = "@Email", Obj = logins.Email },
            new CustomDataPair() { Key = "@Password", Obj = logins.Password },
            new CustomDataPair() { Key = "@DistrictId", Obj = logins.DistrictId },
            new CustomDataPair() { Key = "@BlockId", Obj = logins.BlockId },
            new CustomDataPair() { Key = "@GPId",  Obj = (logins.GPId == 0 ? DBNull.Value : logins.GPId) },
            new CustomDataPair() { Key = "@RoleId", Obj = logins.RoleId },
            new CustomDataPair() { Key = "@ProfilePic", Obj = logins.ProfilePhotoPath },
            new CustomDataPair() { Key = "@CreatedDate", Obj = logins.CreatedDate },
            new CustomDataPair() { Key = "@UpdatedDate", Obj = logins.UpdatedDate },
            new CustomDataPair() { Key = "@CreatedBy", Obj = logins.CreatedBy },
            new CustomDataPair() { Key = "@Status", Obj = logins.Status }

        };

                    result = _baseDAL.InsertData(out message, sqlDataSource, "UserSignup", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));
                    if (result > 0)
                    {
                        if (message == "Signup Successful")
                        {

                            messageBody = $@"
                                <p>Dear <b>{logins.FullName}</b>,</p>
                                
                                <p>🎉 <b>Thank you</b> for registering with <b>Handpump Management System</b>!</p>
                                
                                <p>Your login credentials for the mobile app are as follows:</p>
                                
                                <p>
                                &nbsp;&nbsp;🔑 <b>User ID:</b> <i>{logins.Email}</i><br/>
                                &nbsp;&nbsp;🔒 <b>Password:</b> <i>{unEncrptdpass}</i>
                                </p>
                                
                                <p>✅ You can now log in and start using the mobile app.</p>
                                
                                <p><i><b>Tip:</b> For your security, please change your password after your first login.</i></p>
                                
                                <p>Best regards,<br/>
                                <b>Handpump Management System Team</b></p>
                                ";


                            EmailSendHelper.SendEmail("kdsdeveloper25@gmail.com", "fddxnjbzdbrpfzff", "" + logins.Email + "", "Handpump Management System Signup Successful", messageBody);

                            //string otpResponseMsg = await  _otpHelper.SendOtpSms(logins.MobileNo, "signup");
                           
                        }

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
                else
                {
                    return new CreateUpdateDeleteResponse
                    {

                        Message = "Username, Password, Fullname is required..!",
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

        public async Task<CreateUpdateDeleteResponse> GetUserProfileById(int UserId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<AdminUserListResponseModel> userprofileresmdl = new List<AdminUserListResponseModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                new CustomDataPair() { Key = "@UserId", Obj = UserId }
            };
                dt = _baseDAL.GetData(sqlDataSource, "GetUserProfileDetails", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        userprofileresmdl.Add(new AdminUserListResponseModel
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

        public static string GetFileUploadPath(string fileBase64String, string fullName)
        {
            try {
                string filepathstr = "";
                string datetimeUniqueStr = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string uniquename = "" + fullName.Replace(" ", "").ToLower() + "_" + datetimeUniqueStr;
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserProfileImage", "User_" + fullName.Replace(" ", "").ToLower());
                var imageUrl = $"/UserProfileImage/{"User_" + fullName.Replace(" ", "").ToLower()}/";
                filepathstr = FileUploadHelper.UploadBase64StringTofile(fileBase64String, folderPath, uniquename, imageUrl);
                return filepathstr;
            }
            catch (Exception ex) 
            {
                return ex.Message;
            }
        }

        public async Task<string> SendOtpSms(string phone, string messageType)
        {
            var random = new Random();
            string otp = random.Next(1000, 9999).ToString(); // 4-digit OTP

            using var client = new HttpClient();

            string template_id = "";
            string mobile = phone;
            string apikey = "LvLwipJXfbNToUhy";
            string sender_id = "KDSPRD";
            string message = "";

            if (messageType == "signup")
            {
                template_id = "1707175290463687273";
                message = $"OTP for signup in Panchayati Raj Department (PRD) is " + otp + " - KDSPRD";
            }
            else if (messageType == "login")
            {
                template_id = "1707175283838142960";
                message = $"OTP for login in Panchayati Raj Department ( PRD ) is " + otp + " - KDSPRD";
            }
            else
            {
                return "Error: Invalid message type";
            }

            // URL encode the message to avoid breaking the query string
            string encodedMessage = Uri.EscapeDataString(message);
            string url = $"https://manage.txly.in/vb/apikey.php?apikey={apikey}&senderid={sender_id}&templateid={template_id}&number={mobile}&message={encodedMessage}";

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                // Optionally: Log or save OTP in DB
                return $"Success: OTP {otp} sent to {mobile}";
            }
            else
            {
                string errorMsg = $"SMS API failed. StatusCode: {response.StatusCode}, Reason: {response.ReasonPhrase}";
                return $"Error: {errorMsg}";
            }
        }

        public string DecryptPasswordString(string passtring, string passkey) {

            try
            {
                string resultStr = "";
                if (passkey == "HMS5050")
                {
                    DataTable dt = new DataTable();
                    List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                {
                  
                     new CustomDataPair() { Key = "@input_password", Obj = passtring },
                };
                    dt = _baseDAL.GetData(sqlDataSource, "UserDecryptPass", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                    if (dt != null)
                    {
                        if (dt.Rows[0][0].ToString()== "Password found")
                        {
                            var encryptedPass = _aes.DecryptStringFromBytes_Aes(passtring);
                            resultStr = encryptedPass;
                        }

                        else if(dt.Rows[0][0].ToString() == "Password not found")
                        {
                            resultStr = "incorrect password or not found in database..!";
                        }

                        else
                            resultStr = "Invalid passkey...!"; 
                    }
                    else
                    {
                        resultStr = "incorrect password or not found in database..!";
                    }
                }
                else
                {
                    resultStr = "Invalid passkey...!";
                }
               return resultStr;
            }
            catch(Exception ex)
            {
                return "password format not supported by aes decryption...!";
            }
            
        }
    }
}
