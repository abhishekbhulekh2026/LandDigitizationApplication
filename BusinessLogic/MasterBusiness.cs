using MySql.Data.MySqlClient;
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
using UserModel.RequestModel;
using UserModel.ResponseModel;

namespace BusinessLogic
{
    public interface IMasterBusiness
    {
        Task<CreateUpdateDeleteResponse> DistrictList();
        Task<CreateUpdateDeleteResponse> VillageList(int? tehsilId, int? parganaId);
        Task<CreateUpdateDeleteResponse> TehsilList();
        Task<CreateUpdateDeleteResponse> ParganaList();
        Task<CreateUpdateDeleteResponse> RoleList();
        Task<CreateUpdateDeleteResponse> RecordTypeList(int villageId);
    }

    public class MasterBusiness : IMasterBusiness
    {
        private readonly IMasterRepository _repository;
        public MasterBusiness(IMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateUpdateDeleteResponse> DistrictList()
        {
            DataTable dt = await _repository.GetLocationMaster("GetDistrictList");

            List<DistrictListResModel> districtList = new();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    districtList.Add(new DistrictListResModel
                    {
                        DistrictId = Convert.ToInt32(row["DistrictId"]),
                        StateId = Convert.ToInt32(row["StateId"]),
                        DistrictCode = row["DistrictCode"]?.ToString(),
                        NameEn = row["NameEn"]?.ToString(),
                        NameHi = row["NameHi"]?.ToString(),

                    });
                }
                return new CreateUpdateDeleteResponse
                {
                    Status = true,
                    Message = "Success!",
                    Data = districtList
                };
            }

            return new CreateUpdateDeleteResponse
            {
                Status = true,
                Message = "data not found!",
                
            };
        }

        public async Task<CreateUpdateDeleteResponse> TehsilList()
        {
            DataTable dt = await _repository.GetLocationMaster("GetTehsil");

            List<TehsilListResModel> tehsilList = new();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    tehsilList.Add(new TehsilListResModel
                    {
                        TehsilId = Convert.ToInt32(row["TehsilId"]),
                        DistrictId = Convert.ToInt32(row["DistrictId"]),
                        DistrictName = row["DistrictName"]?.ToString(),
                        TehsilCode = row["TehsilCode"]?.ToString(),
                        NameEn = row["NameEn"]?.ToString(),
                        NameHi = row["NameHi"]?.ToString(),
                    });
                }
                return new CreateUpdateDeleteResponse
                {
                    Status = true,
                    Message = "Success!",
                    Data = tehsilList
                };
            }

            return new CreateUpdateDeleteResponse
            {
                Status = true,
                Message = "data not found!",

            };
        }

        public async Task<CreateUpdateDeleteResponse> ParganaList()
        {
            DataTable dt = await _repository.GetLocationMaster("GetPargana");

            List<ParganaListResModel> parganaList = new();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    parganaList.Add(new ParganaListResModel
                    {
                        ParganaId = Convert.ToInt32(row["ParganaId"]),
                        ParganaCode = row["ParganaCode"]?.ToString(),
                        NameEn = row["NameEn"]?.ToString(),
                        NameHi = row["NameHi"]?.ToString(),

                    });
                }
                return new CreateUpdateDeleteResponse
                {
                    Status = true,
                    Message = "Success!",
                    Data = parganaList
                };
            }

            return new CreateUpdateDeleteResponse
            {
                Status = true,
                Message = "data not found!",

            };
        }
        
        public async Task<CreateUpdateDeleteResponse> VillageList(int? tehsilId, int? parganaId)
        {
            try
            {
                DataTable dt = await _repository.GetVillageMaster("GetVillage", tehsilId, parganaId);

                List<VillageListResModel> villages = new();

                foreach (DataRow row in dt.Rows)
                {
                    villages.Add(new VillageListResModel
                    {
                        VillageId = Convert.ToInt32(row["VillageId"]),
                        VillageCode = row["VillageCode"].ToString(),

                        TehsilParganaMapId = Convert.ToInt32(row["TehsilParganaMapId"]),
                        TehsilId = Convert.ToInt32(row["TehsilId"]),
                        ParganaId = Convert.ToInt32(row["ParganaId"]),

                        TehsilName = row["TehsilName"].ToString(),
                        ParganaName = row["ParganaName"].ToString(),

                        TehsilParganaEn = row["TehsilParganaEn"].ToString(),
                        TehsilParganaHi = row["TehsilParganaHi"].ToString(),

                        NameEn = row["NameEn"].ToString(),
                        NameHi = row["NameHi"].ToString()
                    });
                }

                return new CreateUpdateDeleteResponse
                {
                    Status = true,
                    Message = "Success",
                    Data = villages
                };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<CreateUpdateDeleteResponse> RoleList()
        {
            try
            {
                DataTable dt = await _repository.GetRoleMaster("GetUserRole");

                List<RoleListResModel> roleList = new();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        roleList.Add(new RoleListResModel
                        {
                            RoleId = Convert.ToInt32(row["RoleId"]),
                            RoleCode = row["RoleCode"]?.ToString(),
                            RoleName = row["RoleName"]?.ToString(),
                            RoleDescription = row["RoleDescription"]?.ToString()
                        });
                    }
                }

                return new CreateUpdateDeleteResponse
                {
                    Status = true,
                    Message = "Success",
                    Data = roleList
                };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<CreateUpdateDeleteResponse> RecordTypeList(int villageId)
        {
            try
            {
                DataTable dt = await _repository.GetRecordTypeMaster(villageId);

                List<RecordTypeListResModel> recordList = new();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        recordList.Add(new RecordTypeListResModel
                        {
                            RecordTypeId = Convert.ToInt32(row["RecordTypeId"]),
                            RecordNameEn = row["RecordNameEn"]?.ToString(),
                            VolumeNumber = Convert.ToInt32(row["VolumeNumber"]),
                            YearFrom = row["YearFrom"] == DBNull.Value
                                ? null
                                : Convert.ToInt32(row["YearFrom"]),
                            Language = row["Language"]?.ToString(),
                            YearTypeEn = row["YearTypeEn"]?.ToString(),
                            DisplayName = row["DisplayName"]?.ToString()
                        });
                    }
                }

                return new CreateUpdateDeleteResponse
                {
                    Status = true,
                    Message = "Success",
                    Data = recordList
                };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }
    }
}
