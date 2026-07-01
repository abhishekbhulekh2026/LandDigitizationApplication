using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Helper
    {
        //private static readonly ErrorLogDAL _errorLogDAL = new ErrorLogDAL();
        /// <summary>
        /// Convert data table to class object list 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToObjectList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        /// <summary>
        /// convert data table to class object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T DataTableToObject<T>(DataTable dt)
        {
            T data = Activator.CreateInstance<T>();
            if (dt.Rows.Count > 0)
            {
                T item = GetItem<T>(dt.Rows[0]);
                data = item;
            }
            return data;
        }

        /// <summary>
        /// Iterate datatble column name and value of cell
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        //if (dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        /// <summary>
        /// Generate Sql parameter by paramter and value
        /// </summary>
        /// <param name="stringDataPairs"></param>
        /// <returns></returns>
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
               // new ErrorLogDAL().Save("GenerateDataParameters", ex.ToString());
                //_errorLogDAL.SaveErrorLog(new ErrorLog()
                //{
                //    CreatedOnDttm = DateTime.Now,
                //    ErrorMessage = ex.Message,
                //    Url = "Database Call Error::" + new StackTrace().GetFrame(1).GetMethod().Name,
                //});
            }
            return parameters;
        }
    }
}
