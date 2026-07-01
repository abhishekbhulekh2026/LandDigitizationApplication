using Microsoft.Extensions.Options;
using Org.BouncyCastle.Utilities;
using Repository;
using Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserModel;
using UserModel.RequestModel;
using UserModel.ResponseModel;
using Utility;

namespace BusinessLogic
{
    public interface IUserBusiness
    {
        Task<CreateUpdateDeleteResponse> UserUpdateProfilePhoto(UserUpdateProfileRequestModel profilePic);
        Task<CreateUpdateDeleteResponse> UserChangePassword(UserChangePasswordRequestModel changePass);
        Task<CreateUpdateDeleteResponse> UserForgetPassword(UserForgetPasswordRequestModel forgerPass);
        Task<CreateUpdateDeleteResponse> UserDeleteAccount(UserDeleteAccountRequestModel deleteAcccount);
    }

    public class UserBusiness : IUserBusiness
    {
        private static string sqlDataSource = CommonVariables.ConnectionString;
        BaseDAL _baseDAL = new BaseDAL();
        private readonly string _domainUrl;
        private readonly EncryptDecryptHelper _aes;
    

        public UserBusiness(IOptions<DomainSettings> appSettings, EncryptDecryptHelper aes)
        {
            _domainUrl = appSettings.Value.DomainUrl;
            _aes = aes;
          
        }
        public async Task<CreateUpdateDeleteResponse> UserUpdateProfilePhoto(UserUpdateProfileRequestModel profilePic)
        {
            UserUpdateProfileRequestModel userprofilePic = new UserUpdateProfileRequestModel();
           
            string message = string.Empty;
            string name = "";
            try
            {
                if (profilePic.UserId >0 && !String.IsNullOrEmpty(profilePic.FileBase64String))
                {
                    int result = 0;
                    DataTable dt = new DataTable();

                    //getting user details from fb

                    List<CustomDataPair> stringDataPairsNew = new List<CustomDataPair>
                       {
                         new CustomDataPair() { Key = "@UserId", Obj = profilePic.UserId }
                        };


                    dt = _baseDAL.GetData(sqlDataSource, "GetUserProfileDetails", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairsNew));

                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            name = row["full_name"].ToString();
                        }
                    }

                    // end 

                    //Insert/Update User Profile Pic
                    if (!String.IsNullOrEmpty(profilePic.FileBase64String))
                    {
                        profilePic.ProfilePhotoPath = GetFileUploadPath(profilePic.FileBase64String, name);
                    }
                  
                    List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                      {
           
                         new CustomDataPair() { Key = "@UserId", Obj = profilePic.UserId },
                         new CustomDataPair() { Key = "@ProfilePic", Obj = profilePic.ProfilePhotoPath },

                     };

                    result = _baseDAL.InsertData(out message, sqlDataSource, "sp_UpdateUserProfilePhoto", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));
                    if (result > 0)
                    {
                        if (message == "Profile Photo Added Successful")
                        {
                            userprofilePic.ProfilePhotoPath = _domainUrl + profilePic.ProfilePhotoPath;
                            userprofilePic.UserId = profilePic.UserId;
                            return new CreateUpdateDeleteResponse
                            {
                                Data = userprofilePic,
                                Message = message,
                                Status = true
                            };
                        }
                        else
                        {
                            return new CreateUpdateDeleteResponse
                            {
                                Message = "Error in Updating Profile Photo",
                                Status = false
                            };
                        }
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

                        Message = "Invalid image format..!",
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
        public static string GetFileUploadPath(string fileBase64String, string fullName)
        {
            try
            {
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

        public async Task<CreateUpdateDeleteResponse> UserChangePassword(UserChangePasswordRequestModel changePass)
        {
            string message = string.Empty;
            try
            {
                if (!String.IsNullOrEmpty(changePass.OldPassword) && !String.IsNullOrEmpty(changePass.NewPassword) && changePass.UserId>0)
                {
                    int result = 0;
                    DataTable dt = new DataTable();

                    var encryptedOldpass = _aes.EncryptStringToBytes_Aes(changePass.OldPassword);
                    changePass.OldPassword = encryptedOldpass.ToString();

                    var encryptedNewpass = _aes.EncryptStringToBytes_Aes(changePass.NewPassword);
                    changePass.NewPassword = encryptedNewpass.ToString();

                    List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                    {
            
                 new CustomDataPair() { Key = "@UserId", Obj = changePass.UserId },
                 new CustomDataPair() { Key = "@OldPassword", Obj = changePass.OldPassword },
                 new CustomDataPair() { Key = "@NewPassword", Obj = changePass.NewPassword },

              };
                     //string otpResponseMsg = await _otpHelper.SendOtpSms("7081615521", "login");


                    result = _baseDAL.InsertData(out message, sqlDataSource, "UserChangePassword", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));
                    if (result > 0)
                    {
                        //if (message == "Password Updated Successfully..!")
                        //{

                        //}

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

                        Message = "Old Password and New Password is required..!",
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

       

        public async Task<CreateUpdateDeleteResponse> UserForgetPassword(UserForgetPasswordRequestModel forgerPass)
        {
           
          

            try
            {
                if (!String.IsNullOrEmpty(forgerPass.Email))
                {
                    int result = 0;
                    DataTable dt = new DataTable();
                    string decryptdPass = "";
                    string ecryptdPass = "";
                    string name = "";
                    string messgae = "";
                    List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                    {

                
                 new CustomDataPair() { Key = "@Email", Obj = forgerPass.Email }
               //  new CustomDataPair() { Key = "@MobileNo", Obj = forgerPass.MobileNo },

              };

                    dt = _baseDAL.GetData(sqlDataSource, "UserForgetPasswordNew", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));
                    ecryptdPass = dt.Rows[0][0].ToString();
                    name = dt.Rows[0][1].ToString();
                    messgae = dt.Rows[0][0].ToString();
                    if (dt != null && !String.IsNullOrEmpty(ecryptdPass))
                    {
                        string messageBody = "";
                        if (ecryptdPass != "Incorrect Email")
                        {
                            decryptdPass = _aes.DecryptStringFromBytes_Aes(ecryptdPass);

                            messageBody = $@"
                                Hi {name},

                                As per your request, here are your account login details:
                                
                                Password: {decryptdPass}
                                
                                Please keep this information safe and do not share it with anyone.

                                If you did not request this information, please contact our support team immediately.
                                
                                Regards,
                                Handpump Management System.";

                            EmailSendHelper.SendEmail("kdsdeveloper25@gmail.com", "fddxnjbzdbrpfzff", "" + forgerPass.Email + "", "Forget Password Request", messageBody);
                            return new CreateUpdateDeleteResponse
                            {
                                Message = "Your password has beed sent to your registered email..kindly check..!",
                                Status = true
                            };

                        }

                        else
                        {
                                   return new CreateUpdateDeleteResponse
                                   {
                                       Message = "Your password has beed sent to your registered email..kindly check..!",
                                       Status = true
                                   };
                        }
                         
                    }

                    else
                    {
                        return new CreateUpdateDeleteResponse
                        {

                            Message = "Your password has beed sent to your registered email..kindly check..!",
                            Status = true
                        };
                    }
                }
                else
                {
                    return new CreateUpdateDeleteResponse
                    {

                        Message = "Email is required..!",
                        Status = false
                    };
                }

            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse
                {

                    Message = "Your password has beed sent to your registered email..kindly check..!",
                    Status = true
                };
            }

        }

        public async Task<CreateUpdateDeleteResponse> UserDeleteAccount(UserDeleteAccountRequestModel deleteAcccount)
        {
            string message = string.Empty;
            try
            {
                if (deleteAcccount.UserId > 0)
                {

                    int result = 0;
                    DataTable dt = new DataTable();

                    List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                    {

                 new CustomDataPair() { Key = "@p_user_id", Obj = deleteAcccount.UserId },
                 new CustomDataPair() { Key = "@p_reason", Obj = deleteAcccount.Reason },
                 
                    };

                    // result = _baseDAL.InsertData(out message, sqlDataSource, "UserDeleteAccount", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));
                    dt = _baseDAL.GetData(sqlDataSource, "UserDeleteAccount", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                    if (dt != null && dt.Rows.Count>0)
                    {
                        //if (message == "Password Updated Successfully..!")
                        //{

                        //}

                        return new CreateUpdateDeleteResponse
                        {
                            Message = dt.Rows[0][0].ToString(),
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

                        Message = "Old Password and New Password is required..!",
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
