using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repository.DbContext
{
    public static class SqlHelper
    {

        public static async Task<DataTable> ExecuteDataTableAsync(
    string connectionString,
    string commandText,
    int timeout = 30,
    CommandType commandType = CommandType.StoredProcedure,
    params SqlParameter[] parameters)
        {
            DataTable dt = new();

            await using SqlConnection con =
                new(connectionString);

            await using SqlCommand cmd =
                new(commandText, con);

            cmd.CommandType = commandType;
            cmd.CommandTimeout = timeout;

            if (parameters?.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await con.OpenAsync();

            using SqlDataReader reader =
                await cmd.ExecuteReaderAsync();

            dt.Load(reader);

            return dt;
        }


        public static async Task<int> ExecuteNonQueryAsync(
    string connectionString,
    string commandText,
    int timeout = 30,
    CommandType commandType = CommandType.StoredProcedure,
    params SqlParameter[] parameters)
        {
            await using SqlConnection con = new(connectionString);
            await using SqlCommand cmd = new(commandText, con);

            cmd.CommandType = commandType;
            cmd.CommandTimeout = timeout;

            if (parameters?.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await con.OpenAsync();

            return await cmd.ExecuteNonQueryAsync();
        }



        //USAGE
        // var result = await SqlHelper.ExecuteNonQueryWithOutputAsync(...);

        // int rows = result.RowsAffected;
        // string message = result.Message;

        public static async Task<(int RowsAffected, string Message)> ExecuteNonQueryWithOutputAsync(
    string connectionString,
    string commandText,
    string outputParameterName = "@Message",
    int timeout = 30,
    params SqlParameter[] parameters)
        {
            await using SqlConnection con = new(connectionString);
            await using SqlCommand cmd = new(commandText, con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeout;

            if (parameters?.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await con.OpenAsync();

            int result = await cmd.ExecuteNonQueryAsync();

            string message = string.Empty;

            if (cmd.Parameters.Contains(outputParameterName))
            {
                message = Convert.ToString(cmd.Parameters[outputParameterName].Value)
                          ?? string.Empty;
            }

            return (result, message);
        }


        public static async Task<T?> ExecuteScalarAsync<T>(
    string connectionString,
    string commandText,
    int timeout = 30,
    CommandType commandType = CommandType.StoredProcedure,
    params SqlParameter[] parameters)
        {
            await using SqlConnection con = new(connectionString);
            await using SqlCommand cmd = new(commandText, con);

            cmd.CommandType = commandType;
            cmd.CommandTimeout = timeout;

            if (parameters?.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await con.OpenAsync();

            object? result = await cmd.ExecuteScalarAsync();

            if (result == null || result == DBNull.Value)
                return default;

            return (T)Convert.ChangeType(result, typeof(T));
        }


        public static async Task<DataSet> ExecuteDataSetAsync(
    string connectionString,
    string commandText,
    int timeout = 30,
    CommandType commandType = CommandType.StoredProcedure,
    params SqlParameter[] parameters)
        {
            DataSet ds = new();

            await using SqlConnection con = new(connectionString);
            await using SqlCommand cmd = new(commandText, con);

            cmd.CommandType = commandType;
            cmd.CommandTimeout = timeout;

            if (parameters?.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await con.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            do
            {
                DataTable table = new();
                table.Load(reader);
                ds.Tables.Add(table);
            }
            while (await reader.NextResultAsync());

            return ds;
        }

        public static async Task<SqlDataReader> ExecuteReaderAsync(
    string connectionString,
    string commandText,
    int timeout = 30,
    CommandType commandType = CommandType.StoredProcedure,
    params SqlParameter[] parameters)
        {
            SqlConnection con = new(connectionString);

            try
            {
                await con.OpenAsync();

                SqlCommand cmd = new(commandText, con);

                cmd.CommandType = commandType;
                cmd.CommandTimeout = timeout;

                if (parameters?.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            catch
            {
                await con.DisposeAsync();
                throw;
            }
        }


        public static async Task<SqlConnection> GetOpenConnectionAsync(
    string connectionString)
        {
            SqlConnection con = new(connectionString);

            await con.OpenAsync();

            return con;
        }


        public static SqlParameter CreateParameter(
    string parameterName,
    object? value,
    SqlDbType sqlDbType,
    ParameterDirection direction = ParameterDirection.Input)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                Value = value ?? DBNull.Value,
                SqlDbType = sqlDbType,
                Direction = direction
            };
        }
    }
}