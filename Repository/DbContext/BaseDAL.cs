using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
namespace Repository.DbContext
{
    public class BaseDAL
    {
       // private static string sqlDataSource = "server=127.0.0.1;port=3306;database=hms_handpump;user=root;password=1234;";
        //private static string sqlDataSource = 

        //public static string sqlDataSource = CommonVariables.ConnectionString;
        //private readonly ErrorLogDAL _errorLogDAL = new ErrorLogDAL();

        /// <summary>
        /// Execute sql query and return datatable
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="timeOut"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public DataTable GetData(string sqlDataSource, string sqlText, int timeOut = 90, CommandType commandType = CommandType.Text, params IDataParameter[] sqlParams)
        {
            DataTable result = new DataTable();
            try
            {
              
                // MySqlDataReader reader;
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sqlText, conn) { CommandType = commandType })
                    {
                        if (sqlParams.Length > 0)
                        {
                            foreach (IDataParameter para in sqlParams)
                            {
                                cmd.Parameters.Add(para);
                            }
                        }
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = timeOut;
                        using (var reader = cmd.ExecuteReader()) {
                                result.Load(reader);
                        }

                            //reader =  cmd.ExecuteReader();
                       
                      //  reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptionAsync(ex);
                throw ex;
              
            }
            return result;
        }

        /// <summary>
        /// Execute sql query and return jsonObject string
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="jsonOutputParam"></param>
        /// <param name="timeOut"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public string GetJsonData(string sqlDataSource, string sqlText, string jsonOutputParam, int timeOut = 30, CommandType commandType = CommandType.Text, params IDataParameter[] sqlParams)
        {
            try
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);

                var currentMethodName = sf.GetMethod();
                var bfhj = st.GetFrame(1).GetMethod().Name;

                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sqlText, conn) { CommandType = commandType })
                    {
                        if (sqlParams.Length > 0)
                        {
                            foreach (IDataParameter para in sqlParams)
                            {
                                cmd.Parameters.Add(para);
                            }
                        }
                        cmd.Parameters.Add(jsonOutputParam, MySqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.CommandTimeout = timeOut;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        jsonOutputParam = cmd.Parameters[jsonOutputParam].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //new ErrorLogDAL().Save("GetJsonData", ex.ToString());
                //_errorLogDAL.SaveErrorLog(new ErrorLog()
                //{

                //    CreatedOnDttm = DateTime.Now,
                //    ErrorMessage = ex.Message,
                //    Url = "Database Call Error::" + new StackTrace().GetFrame(1).GetMethod().Name + "==>" + sqlText,
                //});
                jsonOutputParam = "";
            }
            return jsonOutputParam; ;
        }
        public int GetIntData(string sqlDataSource, string sqlText, string intOutputParam, int timeOut = 30, CommandType commandType = CommandType.Text, params IDataParameter[] sqlParams)
        {
            int returnData = 0;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sqlText, conn) { CommandType = commandType })
                    {
                        if (sqlParams.Length > 0)
                        {
                            foreach (IDataParameter para in sqlParams)
                            {
                                cmd.Parameters.Add(para);
                            }
                        }
                        cmd.Parameters.Add(intOutputParam, MySqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        cmd.CommandTimeout = timeOut;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        returnData = Convert.ToInt32(cmd.Parameters[intOutputParam].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                //new ErrorLogDAL().Save("GetIntData", ex.ToString());
                //_errorLogDAL.SaveErrorLog(new ErrorLog()
                //{
                //    CreatedOnDttm = DateTime.Now,
                //    ErrorMessage = ex.Message,
                //    Url = "Database Call Error::" + new StackTrace().GetFrame(1).GetMethod().Name + "==>" + sqlText,
                //});
            }
            return returnData;
        }

        /// <summary>
        /// Execute sql query and return result identity
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="timeOut"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public int SetData(string sqlDataSource, string sqlText, int timeOut = 30, CommandType commandType = CommandType.Text, params IDataParameter[] sqlParams)
        {
            int rows = -1;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sqlText, conn) { CommandType = commandType })
                    {
                        if (sqlParams.Length > 0)
                        {
                            foreach (IDataParameter para in sqlParams)
                            {
                                cmd.Parameters.Add(para);
                            }
                        }
                        cmd.CommandTimeout = timeOut;
                        rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                //new ErrorLogDAL().Save("SetData", ex.ToString());
                //_errorLogDAL.SaveErrorLog(new ErrorLog()
                //{
                //    CreatedOnDttm = DateTime.Now,
                //    ErrorMessage = ex.Message,
                //    Url = "Database Call Error::" + new StackTrace().GetFrame(1).GetMethod().Name + "==>" + sqlText,
                //});
            }
            return rows;
        }
        /// <summary>
        /// Execute sql query and return result identity
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="timeOut"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public int InsertData(out string message, string sqlDataSource, string sqlText, int timeOut = 30, CommandType commandType = CommandType.Text, params IDataParameter[] sqlParams)
        {
            int rows = -1;
            string result = "";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sqlText, conn) { CommandType = commandType })
                    {
                        var outParam = new MySqlParameter("@signup_status", MySqlDbType.String)
                        {
                            Direction = ParameterDirection.Output
                        };
                        if (sqlParams.Length > 0)
                        {

                            foreach (IDataParameter para in sqlParams)
                            {
                                cmd.Parameters.Add(para);

                            }
                            cmd.Parameters.Add(outParam);
                        }
                        cmd.CommandTimeout = timeOut;
                        rows = cmd.ExecuteNonQuery();
                        result = outParam.Value.ToString();
                        message = result;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                LogExceptionAsync(ex);
                //new ErrorLogDAL().Save("InsertData", ex.ToString());
                //_errorLogDAL.SaveErrorLog(new ErrorLog()
                //{
                //    CreatedOnDttm = DateTime.Now,
                //    ErrorMessage = ex.Message,
                //    Url = "Database Call Error::" + new StackTrace().GetFrame(1).GetMethod().Name + "==>" + sqlText,
                //});
            }
            
            return rows;
        }
        public int CheckData(string sqlDataSource, string sqlText, int timeOut = 30, CommandType commandType = CommandType.Text, params IDataParameter[] sqlParams)
        {
            int rows = 0;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sqlText, conn) { CommandType = commandType })
                    {
                        if (sqlParams.Length > 0)
                        {
                            foreach (IDataParameter para in sqlParams)
                            {
                                cmd.Parameters.Add(para);
                            }
                        }
                        cmd.CommandTimeout = timeOut;
                        rows = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                //new ErrorLogDAL().Save("CheckData", ex.ToString());
                //_errorLogDAL.SaveErrorLog(new ErrorLog()
                //{
                //    CreatedOnDttm = DateTime.Now,
                //    ErrorMessage = ex.Message,
                //    Url = "Database Call Error::" + new StackTrace().GetFrame(1).GetMethod().Name + "==>" + sqlText,
                //});
            }
            return rows;
        }

        public static IDataParameter[] GenerateDataParameters(List<CustomDataPair> stringDataPairs)
        {
            var parameters = new IDataParameter[stringDataPairs.Count];
            try
            {
                for (int i = 0; i < stringDataPairs.Count; i++)
                {
                    if (stringDataPairs[i].Obj != null)
                        parameters[i] = new MySqlParameter(stringDataPairs[i].Key, stringDataPairs[i].Obj);
                    else
                        parameters[i] = new MySqlParameter(stringDataPairs[i].Key, stringDataPairs[i].Obj);
                }
            }
            catch (Exception ex)
            {
                //_errorLogDAL.SaveErrorLog(new ErrorLog()
                //{
                //    CreatedOnDttm = DateTime.Now,
                //    ErrorMessage = ex.Message,
                //    Url = "Database Call Error::" + new StackTrace().GetFrame(1).GetMethod().Name,
                //});
            }
            return parameters;
        }
        public DataTable GetGeoTagLocation(string connStr, string query)
        {
            var dt = new DataTable();
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

        public int ExecuteStoredProcedure(MySqlConnection conn, MySqlTransaction transaction, string procedureName, List<CustomDataPair> inputParams, MySqlParameter outputParam, int timeout = 90)
        {
            try
            {
                using (var cmd = new MySqlCommand(procedureName, conn, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = timeout;

                    var parameters = Helper.GenerateDataParameters(inputParams);
                    cmd.Parameters.AddRange(parameters);

                    if (outputParam != null)
                        cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    return outputParam != null ? Convert.ToInt32(outputParam.Value) : 1;
                }

            }

            catch (Exception ex)
            {
                LogExceptionAsync(ex);
                return 0;
                //_errorLogDAL.SaveErrorLog(new ErrorLog()
                //{
                //    CreatedOnDttm = DateTime.Now,
                //    ErrorMessage = ex.Message,
                //    Url = "Database Call Error::" + new StackTrace().GetFrame(1).GetMethod().Name,
                //}
            }
        }

        public static void LogExceptionAsync(Exception ex)
        {
            try
            {
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");

                // Ensure folder exists
                if (!Directory.Exists(wwwRootPath))
                    Directory.CreateDirectory(wwwRootPath);

                // Create a unique file name
                var fileName = $"Error_{DateTime.Now:yyyyMMdd_HHmmssfff}.txt";
                var filePath = Path.Combine(wwwRootPath, fileName);

                // Build log text
                var logText = new StringBuilder();
                logText.AppendLine($"Time: {DateTime.Now}");
                logText.AppendLine($"Message: {ex.Message}");
                logText.AppendLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    logText.AppendLine("Inner Exception:");
                    logText.AppendLine(ex.InnerException.ToString());
                }

                // Write log file asynchronously
                 File.WriteAllText(filePath, logText.ToString(), Encoding.UTF8);
            }
            catch
            {
                // Last-resort fallback — do not throw further
            }
        }

      


    }
}
