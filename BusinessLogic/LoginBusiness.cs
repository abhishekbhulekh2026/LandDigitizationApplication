using Microsoft.Data.SqlClient;
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
using Utility;

namespace BusinessLogic
{
    public interface ILoginBusiness
    {
       Task<LoginResponseModel> UserAuthentication(LoginRequestModel logins);
       Task<CreateUpdateDeleteResponse> Logout(LogoutRequestModel model);
       Task<bool> IsTokenRevoked(string jti);
    }
    public class LoginBusiness : ILoginBusiness
    {

        private readonly ILoginRepository _repository;
        private readonly EncryptDecryptHelper _aes;

        public LoginBusiness(
            ILoginRepository repository,
            EncryptDecryptHelper aes)
        {
            _repository = repository;
            _aes = aes;
        }

        public async Task<LoginResponseModel>UserAuthentication(LoginRequestModel logins)
        {
            LoginResponseModel userLogin = new();

            var encrypted =
                _aes.EncryptStringToBytes_Aes(
                    logins.Password);

            DataTable dt =
                     await _repository.UserAuthenticationAsync(
                           logins.UserName,
                           encrypted);

            if (dt.Rows.Count == 0)
            {
                userLogin.LoginMessage =
                    "Invalid username or password";

                return userLogin;
            }

            DataRow row = dt.Rows[0];

            userLogin.Id =
                Convert.ToInt32(row["Id"]);

            userLogin.UserName =
                row["UserName"]?.ToString();

            userLogin.UserRole =
                row["UserRole"]?.ToString();

            userLogin.Status =
                row["ApprovalStatus"]?.ToString();

            userLogin.LoginMessage =
                row["LoginMessage"]?.ToString();

            userLogin.ResponseCode =
               row["ResponseCode"]?.ToString();

            return userLogin;
        }

        public async Task<bool>IsTokenRevoked(string jti)
        {
            DataTable dt =
                await _repository.IsTokenRevoked(jti);

            return dt.Rows.Count > 0 &&
                   Convert.ToInt32(
                       dt.Rows[0]["IsRevoked"]) > 0;
        }

        public async Task<CreateUpdateDeleteResponse>Logout(LogoutRequestModel model)
        {
            DataTable dt =
              await  _repository.Logout(
                    model.UserId,
                    model.TokenJti,
                    model.TokenExpiry,
                    string.IsNullOrWhiteSpace(model.Reason)
                        ? "Logout"
                        : model.Reason);

            return new CreateUpdateDeleteResponse
            {
                Status =
                    dt.Rows.Count > 0 &&
                    Convert.ToBoolean(
                        dt.Rows[0]["Status"]),

                Message =
                    dt.Rows.Count > 0
                    ? dt.Rows[0]["Message"]
                        ?.ToString()
                    : "Logout completed"
            };
        }
    }
}
