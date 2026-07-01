using Microsoft.Data.SqlClient;
using Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModel;

namespace Repository
{

    public interface ILoginRepository
    {
        Task<DataTable> UserAuthenticationAsync(string username,string password);
        Task<DataTable> Logout(long userId,string tokenJti,DateTime? expiry,string reason);
        Task<DataTable> IsTokenRevoked(string tokenJti);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;
        public LoginRepository()
        {
            _connectionString = CommonVariables.ConnectionString;
        }
        public async Task<DataTable>UserAuthenticationAsync(string username,string password)
        {
            return await SqlHelper.ExecuteDataTableAsync(
                _connectionString,
                "Sp_UserLogin",
                30,
                CommandType.StoredProcedure,

                new SqlParameter("@Username", username),
                new SqlParameter("@PasswordHash", password)
            );
        }
        public async Task<DataTable> Logout(long userId,string tokenJti,DateTime? expiry,string reason)
        {
            return await SqlHelper.ExecuteDataTableAsync(
                _connectionString,
                "Sp_LogoutRevokeToken",
                  30,
                    CommandType.StoredProcedure,
                new SqlParameter("@UserId", userId),
                new SqlParameter("@TokenJti", tokenJti),
                new SqlParameter("@TokenExpiry",
                    expiry ?? (object)DBNull.Value),
                new SqlParameter("@Reason", reason)
            );
        }
        public async Task<DataTable> IsTokenRevoked(string tokenJti)
        {
            return await SqlHelper.ExecuteDataTableAsync(
                _connectionString,
                "Sp_CheckRevokedToken",
                30,
                    CommandType.StoredProcedure,
                new SqlParameter("@TokenJti", tokenJti)
            );
        }
    }
}
