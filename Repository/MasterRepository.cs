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
    public interface IMasterRepository
    {
        Task<DataTable> GetLocationMaster(string actionType);
        Task<DataTable> GetVillageMaster(string actionType, int? tehsilId, int? parganaId);
        Task<DataTable> GetRoleMaster(string actionType);
        Task<DataTable> GetRecordTypeMaster(int villageId);
    }


    public class MasterRepository : IMasterRepository
    {
        private readonly string _connectionString;
        public MasterRepository()
        {
            _connectionString = CommonVariables.ConnectionString;
        }

        public async Task<DataTable> GetLocationMaster(string actionType)
        {
            return await SqlHelper.ExecuteDataTableAsync(
                _connectionString,
                "admin.Sp_LocationMaster",
                30,
                CommandType.StoredProcedure,
                new SqlParameter("@ActionType", actionType)
            );
        }

        public async Task<DataTable> GetVillageMaster(string actionType, int? tehsilId, int? parganaId)
        {
            return await SqlHelper.ExecuteDataTableAsync(
                _connectionString,
                "admin.Sp_VillageMaster",
                30,
                CommandType.StoredProcedure,
                new SqlParameter("@ActionType", actionType),
                 new SqlParameter("@TehsilId", tehsilId),
                  new SqlParameter("@ParganaId", parganaId)
            );
        }

        public async Task<DataTable> GetRoleMaster(string actionType)
        {
            return await SqlHelper.ExecuteDataTableAsync(
                _connectionString,
                "admin.Sp_RoleMaster",
                30,
                CommandType.StoredProcedure,
                new SqlParameter("@ActionType", actionType)
            );
        }

        public async Task<DataTable> GetRecordTypeMaster(int villageId)
        {
            return await SqlHelper.ExecuteDataTableAsync(
                _connectionString,
                "admin.Sp_RecordTypeMaster",
                30,
                CommandType.StoredProcedure,
                new SqlParameter("@VillageId", villageId)
            );
        }

    }
}
