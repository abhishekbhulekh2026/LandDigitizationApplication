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
        Task<CreateUpdateDeleteResponse> GetBlockListByDistrict(MasterReqModel mreqmdl);
        Task<CreateUpdateDeleteResponse> GetDistrictAbbreviation();
        Task<CreateUpdateDeleteResponse> GetGramPanchayatByBlock(MasterReqModel mreqmdl);
        Task<CreateUpdateDeleteResponse> GetVillegeByGramPanchayat(MasterReqModel mreqmdl);
        Task<CreateUpdateDeleteResponse> GetRoleList();
        Task<CreateUpdateDeleteResponse> GetReboreEstimation();
        Task<CreateUpdateDeleteResponse> GetRepairEstimation();

        Task<CreateUpdateDeleteResponse> GetRepairRequisitionTypes(int Id);
        Task<CreateUpdateDeleteResponse> GetRequisitionTypes();
        Task<CreateUpdateDeleteResponse> GstActiveDetails();
        Task<CreateUpdateDeleteResponse> GetVillagesByUserId(int UserId);

        Task<CreateUpdateDeleteResponse> GetHandpumpListByVillage(int villageId);

        Task<CreateUpdateDeleteResponse> AddRepairEstimateItems(RepairEstimationRequestModel model);
        Task<CreateUpdateDeleteResponse> AddReboreEstimateItems(ReboreEstimationRequestModel model);
        Task<CreateUpdateDeleteResponse> GetDistrictByUserId(int UserId);
    }

    public class MasterBusiness : IMasterBusiness
    {
        private static string sqlDataSource = CommonVariables.ConnectionString;
        BaseDAL _baseDAL = new BaseDAL();
        public async Task<CreateUpdateDeleteResponse> DistrictList()
        {
            DataTable dt = new DataTable();
            try
            {
                List<DistrictListResModel> districtListReqModel = new List<DistrictListResModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                {
                    new CustomDataPair() { Key = "@QueryType", Obj = "GetDistrictList" },
                      new CustomDataPair() { Key = "@Id", Obj = 42 },
                };
                dt = _baseDAL.GetData(sqlDataSource, "GetMasterData", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        districtListReqModel.Add(new DistrictListResModel
                        {
                            Id = Convert.ToInt32(row["id"].ToString()),
                            DistrictName = row["name_en"].ToString(),
                            Code = row["code"].ToString(),
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = districtListReqModel, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> GetDistrictByUserId(int UserId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<DistrictListResModel> districtListReqModel = new List<DistrictListResModel>();

                string objname = "";

                if (UserId == 1)
                {
                    objname = "GetDistrictList";
                }
                else
                {
                    objname = "GetDistrictListByUserId"; 
                }


                    List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                {
                    new CustomDataPair() { Key = "@QueryType", Obj = objname },
                      new CustomDataPair() { Key = "@Id", Obj = UserId },
                };
                dt = _baseDAL.GetData(sqlDataSource, "GetMasterData", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        districtListReqModel.Add(new DistrictListResModel
                        {
                            Id = Convert.ToInt32(row["id"].ToString()),
                            DistrictName = row["name_en"].ToString(),
                            Code = row["code"].ToString(),
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = districtListReqModel, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }
        public async Task<CreateUpdateDeleteResponse> GetBlockListByDistrict(MasterReqModel mreqmdl)
        {
            DataTable dt = new DataTable();
            try
            {
                List<BlockListResModel> blockListReqModel = new List<BlockListResModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                new CustomDataPair() { Key = "@Id", Obj = mreqmdl.Id },
                new CustomDataPair() { Key = "@QueryType", Obj = "GetBlockList" }
            };
                dt = _baseDAL.GetData(sqlDataSource, "GetMasterData", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        blockListReqModel.Add(new BlockListResModel
                        {
                            Id = Convert.ToInt32(row["id"].ToString()),
                            BlockName = row["name_en"].ToString(),
                            Code = row["code"].ToString(),
                            DistrictId = Convert.ToInt32(row["district_id"].ToString()),
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = blockListReqModel, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> GetDistrictAbbreviation()
        {
            DataTable dt = new DataTable();
            try
            {
                List<DistrictAbbreviationListModel> districtAbbrReqModel = new List<DistrictAbbreviationListModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                 new CustomDataPair() { Key = "@Id", Obj = null },
                new CustomDataPair() { Key = "@QueryType", Obj = "GetDistrictAbbrList" }
            };
                dt = _baseDAL.GetData(sqlDataSource, "GetMasterData", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        districtAbbrReqModel.Add(new DistrictAbbreviationListModel
                        {
                            Id = Convert.ToInt32(row["id"].ToString()),
                            DistrictName = row["district_name"].ToString(),
                            Abbreviation = row["abbreviation"].ToString(),
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = districtAbbrReqModel, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> GetGramPanchayatByBlock(MasterReqModel mreqmdl)
        {
            DataTable dt = new DataTable();
            try
            {
                List<GramPanchayatListResModel> gpListReqModel = new List<GramPanchayatListResModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                new CustomDataPair() { Key = "@Id", Obj = mreqmdl.Id },
                new CustomDataPair() { Key = "@QueryType", Obj = "GetGramPanchayatList" }
            };
                dt = _baseDAL.GetData(sqlDataSource, "GetMasterData", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        gpListReqModel.Add(new GramPanchayatListResModel
                        {
                            Id = Convert.ToInt32(row["id"].ToString()),
                            GramPanchayatName = row["name_en"].ToString(),
                            Code = row["code"].ToString(),
                            BlockId= Convert.ToInt32(row["block_id"].ToString()),
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = gpListReqModel, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> GetVillegeByGramPanchayat(MasterReqModel mreqmdl)
        {
            DataTable dt = new DataTable();
            try
            {
                List<VillegeListResModel> villegeListReqModel = new List<VillegeListResModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                new CustomDataPair() { Key = "@Id", Obj = mreqmdl.Id },
                new CustomDataPair() { Key = "@QueryType", Obj = "GetVillageList" }
            };
                dt = _baseDAL.GetData(sqlDataSource, "GetMasterData", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        villegeListReqModel.Add(new VillegeListResModel
                        {
                            Id = Convert.ToInt32(row["id"].ToString()),
                            VillageName = row["name_en"].ToString(),
                            Code = row["code"].ToString(),
                            GramPanchayatId= Convert.ToInt32(row["gram_panchayat_id"].ToString()),
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = villegeListReqModel, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> GetRoleList()
        {
            DataTable dt = new DataTable();
            try
            {
                List<RoleListResModel> roleListReqModel = new List<RoleListResModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                new CustomDataPair() { Key = "@Id", Obj = null },
                new CustomDataPair() { Key = "@QueryType", Obj = "GetRollList" }
            };
                dt = _baseDAL.GetData(sqlDataSource, "GetMasterData", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        roleListReqModel.Add(new RoleListResModel
                        {
                            Id = Convert.ToInt32(row["role_id"].ToString()),
                            RoleName = row["role_name"].ToString(),
                            Status = Convert.ToBoolean(row["status"]),
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = roleListReqModel, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }
        }

        public async Task<CreateUpdateDeleteResponse> GetReboreEstimation()
        {
            DataTable dt = new DataTable();
            try
            {
                List<ReboreEstimationResponseModel> reboreresmdl = new List<ReboreEstimationResponseModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                {

                };
                dt = _baseDAL.GetData(sqlDataSource, "GetReboreEstimation_A", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        reboreresmdl.Add(new ReboreEstimationResponseModel
                        {
                            Id = Convert.ToInt32(row["id"]),
                            ItemName = row["item_name"].ToString(),
                            Unit = row["unit"].ToString(),
                            Quantity = Convert.ToInt32(row["quantity"]),
                            Rate = Convert.ToDecimal(row["rate"]),
                            Amount = Convert.ToDecimal(row["amount"]),
                            Source = row["source"].ToString(),
                            Length = row["length"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(row["length"]),
                            Width = row["width"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(row["width"]),
                            Height = row["height"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(row["height"])
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = reboreresmdl, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> GetRepairEstimation()
        {
            DataTable dt = new DataTable();
            try
            {
                List<RepairEstimationResponseModel> repairresmdl = new List<RepairEstimationResponseModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                {
                    

                };
                dt = _baseDAL.GetData(sqlDataSource, "GetRepairEstimation_A", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        repairresmdl.Add(new RepairEstimationResponseModel
                        {
                            Id = Convert.ToInt32(row["id"]),
                            ItemName = row["item_name"].ToString(),
                            Unit = row["unit"].ToString(),
                            Quantity = Convert.ToInt32(row["quantity"]),
                            Rate = Convert.ToDecimal(row["rate"]),
                            Amount = Convert.ToDecimal(row["amount"]),
                            Source = row["source"].ToString(),
                            Length = row["length"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(row["length"]),
                            Width = row["width"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(row["width"]),
                            Height = row["height"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(row["height"])
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = repairresmdl, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> GetRepairRequisitionTypes(int Id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<RequisitionRepairTypeResModel> repairresmdl = new List<RequisitionRepairTypeResModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
        {
            new CustomDataPair() { Key = "@Id", Obj = Id }
        };
                dt = _baseDAL.GetData(sqlDataSource, "GetRequisitionRepairTypes", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        repairresmdl.Add(new RequisitionRepairTypeResModel
                        {
                            Id = Convert.ToInt32(row["Id"].ToString()),
                            Name = row["type"].ToString(),
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = repairresmdl, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }
        public async Task<CreateUpdateDeleteResponse> GetRequisitionTypes()
        {
            DataTable dt = new DataTable();
            try
            {
                List<RequisitionTypeResModel> repairresmdl = new List<RequisitionTypeResModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                {

                };
                dt = _baseDAL.GetData(sqlDataSource, "GetRequisitionTypes", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        repairresmdl.Add(new RequisitionTypeResModel
                        {
                            Id = Convert.ToInt32(row["Id"].ToString()),
                            Name = row["Name"].ToString(),
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = repairresmdl, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> GstActiveDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                List<GSTMasterResponseModel> gstMasterList = new List<GSTMasterResponseModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
        {
           
        };

                dt = _baseDAL.GetData(
                        sqlDataSource,
                        "GetActiveGstMaster",   // <-- Your stored procedure name
                        CommonVariables.SqlCommandTimeout,
                        CommandType.StoredProcedure,
                        Helper.GenerateDataParameters(stringDataPairs)
                    );

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        gstMasterList.Add(new GSTMasterResponseModel
                        {
                            Id = Convert.ToInt32(row["id"]),
                            GstCode = row["gst_code"].ToString(),
                            Description = row["description"].ToString(),
                            Cgst = Convert.ToDecimal(row["cgst"]),
                            Sgst = Convert.ToDecimal(row["sgst"]),
                            Igst = Convert.ToDecimal(row["igst"]),
                            EffectiveFrom = Convert.ToDateTime(row["effective_from"]),
                            EffectiveTo = row["effective_to"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["effective_to"]),
                            IsActive = Convert.ToBoolean(row["is_active"]),
                            CreatedDate = Convert.ToDateTime(row["created_date"]),
                            UpdatedDate = Convert.ToDateTime(row["updated_date"])
                        });
                    }
                }

                return new CreateUpdateDeleteResponse
                {
                    Data = gstMasterList,
                    Message = "success",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }


        public async Task<CreateUpdateDeleteResponse> GetVillagesByUserId(int UserId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<VillegeListResModel> villegeListReqModel = new List<VillegeListResModel>();
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
            {
                new CustomDataPair() { Key = "@User_Id", Obj = UserId },
                
            };
                dt = _baseDAL.GetData(sqlDataSource, "GetVillages_ByUser", CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, Helper.GenerateDataParameters(stringDataPairs));

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        villegeListReqModel.Add(new VillegeListResModel
                        {
                            Id = Convert.ToInt32(row["id"].ToString()),
                            VillageName = row["name_en"].ToString(),
                            
                        });
                    }
                }
                return new CreateUpdateDeleteResponse { Data = villegeListReqModel, Message = "success", Status = true };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse { Message = ex.Message, Status = false };
            }

        }

        public async Task<CreateUpdateDeleteResponse> GetHandpumpListByVillage(int villageId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<HandpumpListByVillageResModel> handpumpList = new List<HandpumpListByVillageResModel>();

                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
        {
            new CustomDataPair() { Key = "@VillegeId", Obj = villageId }
        };

                dt = _baseDAL.GetData(
                        sqlDataSource,
                        "GetHandpumpListByVillege",  // Stored Procedure name
                        CommonVariables.SqlCommandTimeout,
                        CommandType.StoredProcedure,
                        Helper.GenerateDataParameters(stringDataPairs)
                    );

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        handpumpList.Add(new HandpumpListByVillageResModel
                        {
                            H_Id = Convert.ToInt32(row["id"]),
                            HandpumpId = row["handpump_id"].ToString()
                        });
                    }
                }

                return new CreateUpdateDeleteResponse
                {
                    Data = handpumpList,
                    Message = "success",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new CreateUpdateDeleteResponse
                {
                    Message = ex.Message,
                    Status = false
                };
            }
        }


        public async Task<CreateUpdateDeleteResponse> AddRepairEstimateItems(RepairEstimationRequestModel model)
        {
            try
            {
                using (var conn = new MySqlConnection(sqlDataSource))
                {
                    await conn.OpenAsync();
                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        try
                        {
                            int timeout = 90;

                            // 🔹 Step 1: Prepare Params
                            var repairParams = new List<CustomDataPair>
                    {
                        new CustomDataPair { Key = "@p_action", Obj = "INSERT" },
                        new CustomDataPair { Key = "@p_id", Obj = 0 }, // Not needed for insert
                        new CustomDataPair { Key = "@p_item_name", Obj = model.ItemName },
                        new CustomDataPair { Key = "@p_unit", Obj = model.Unit },
                        new CustomDataPair { Key = "@p_quantity", Obj = model.Quantity },
                        new CustomDataPair { Key = "@p_rate", Obj = model.Rate },
                        new CustomDataPair { Key = "@p_amount", Obj = model.Amount },
                        new CustomDataPair { Key = "@p_updated_by", Obj = model.UpdatedBy },
                        new CustomDataPair { Key = "@p_source", Obj = model.Source },
                        new CustomDataPair { Key = "@p_length", Obj = model.Length },
                        new CustomDataPair { Key = "@p_width", Obj = model.Width },
                        new CustomDataPair { Key = "@p_height", Obj = model.Height }
                    };

                            // 🔹 Step 2: Call Stored Procedure
                            int result = _baseDAL.ExecuteStoredProcedure(
                                conn, transaction, "repair_estimate_master", repairParams, null, timeout);

                            // 🔹 Commit
                            await transaction.CommitAsync();

                            if (result > 0)
                            {
                                return new CreateUpdateDeleteResponse
                                {
                                    Status = true,
                                    Message = "Repair estimation item has been inserted successfully!",
                                    Data = result
                                };
                            }
                            else
                            {
                                return new CreateUpdateDeleteResponse
                                {
                                    Status = false,
                                    Message = "Insert failed, no rows affected!",
                                    Data = null
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            return new CreateUpdateDeleteResponse
                            {
                                Status = false,
                                Message = "Transaction failed: " + ex.Message
                            };
                        }
                    }
                }
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


        public async Task<CreateUpdateDeleteResponse> AddReboreEstimateItems(ReboreEstimationRequestModel model)
        {
            try
            {
                using (var conn = new MySqlConnection(sqlDataSource))
                {
                    await conn.OpenAsync();
                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        try
                        {
                            int timeout = 90;

                            // 🔹 Step 1: Prepare Params
                            var repairParams = new List<CustomDataPair>
                    {
                        new CustomDataPair { Key = "@p_action", Obj = "INSERT" },
                        new CustomDataPair { Key = "@p_id", Obj = 0 }, // Not needed for insert
                        new CustomDataPair { Key = "@p_item_name", Obj = model.ItemName },
                        new CustomDataPair { Key = "@p_unit", Obj = model.Unit },
                        new CustomDataPair { Key = "@p_quantity", Obj = model.Quantity },
                        new CustomDataPair { Key = "@p_rate", Obj = model.Rate },
                        new CustomDataPair { Key = "@p_amount", Obj = model.Amount },
                        new CustomDataPair { Key = "@p_updated_by", Obj = model.UpdatedBy },
                        new CustomDataPair { Key = "@p_source", Obj = model.Source },
                        new CustomDataPair { Key = "@p_length", Obj = model.Length },
                        new CustomDataPair { Key = "@p_width", Obj = model.Width },
                        new CustomDataPair { Key = "@p_height", Obj = model.Height }
                    };

                            // 🔹 Step 2: Call Stored Procedure
                            int result = _baseDAL.ExecuteStoredProcedure(
                                conn, transaction, "rebore_estimate_master", repairParams, null, timeout);

                            // 🔹 Commit
                            await transaction.CommitAsync();

                            if (result > 0)
                            {
                                return new CreateUpdateDeleteResponse
                                {
                                    Status = true,
                                    Message = "Rebore estimation item has been inserted successfully!",
                                    Data = result
                                };
                            }
                            else
                            {
                                return new CreateUpdateDeleteResponse
                                {
                                    Status = false,
                                    Message = "Insert failed, no rows affected!",
                                    Data = null
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            return new CreateUpdateDeleteResponse
                            {
                                Status = false,
                                Message = "Transaction failed: " + ex.Message
                            };
                        }
                    }
                }
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
