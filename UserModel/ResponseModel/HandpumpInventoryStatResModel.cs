using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class HandpumpInventoryStatResModel
    {
        public int TotalFunctionalHandpumps { get; set; }
        public int TotalNonFunctionalHandpumps { get; set; }
        public int TotalRepairCivilMechanicalCounts { get; set; }
        public int TotalReboreMechanicalCounts { get; set; }
    }

    public class ItemMasterResponseModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public string ItemStatus { get; set; }
        public int ExpenditureHead { get; set; }
    }

    public class DistrictWiseHandpumpStatusRequestModel
    {
        public string Search { get; set; }
        public string SortBy { get; set; }        // district_name, total_handpumps, functional, non_functional, functional_percentage
        public string SortOrder { get; set; }     // ASC / DESC
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
    public class DistrictWiseHandpumpStatusResponseModel
    {
        public int SerialNo { get; set; }
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int TotalHandpumps { get; set; }
        public int Functional { get; set; }
        public int NonFunctional { get; set; }
        public decimal FunctionalPercentage { get; set; }
        public string StatusIndicator { get; set; }
    }

    public class ItemMasterInvetoryResponseModel
    {
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string Status { get; set; }
    }

}
