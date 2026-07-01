using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class HandpumpMatrixResponse
    {
        public int TotalHandpumps { get; set; }
        public int ActiveHandpumps { get; set; }
        public int InactiveHandpumps { get; set; }
        public int UnderMaintenance { get; set; }
    }

    public class ComplaintMatrixResponse
    {
        public int TotalComplaintsRaised { get; set; }
        public int TotalResolved { get; set; }
        public int TotalPending { get; set; }
    }

    public class TopInactiveHandpumpDistrictResponse
    {
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int InactiveHandpumps { get; set; }
    }
    public class TopUnderMaintenanceHandpumpDistrictResponse
    {
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int UnderMaintenanceHandpumps { get; set; }
    }

    public class TopPendingComplaintDistrictResponse
    {
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int PendingComplaints { get; set; }
    }

    // Master response for dashboard
    public class DashboardResponse
    {
        public HandpumpMatrixResponse? HandpumpMatrix { get; set; }
        public ComplaintMatrixResponse? ComplaintMatrix { get; set; }

        public List<TopDistrictResponse>? Top10InactiveHandpumps { get; set; }
        public List<TopDistrictResponse>? Top10UnderMaintenance { get; set; }
        public List<TopDistrictResponse>? Top10PendingComplaints { get; set; }
    }

    // Handpump Matrix
   

    // Top District data for all top 10 queries
    public class TopDistrictResponse
    {
        public long DistrictId { get; set; }
        public string DistrictName { get; set; } = string.Empty;
        public int Count { get; set; }   // Can be inactive, under maintenance or pending complaints
    }

    public class ExpenditureDashboardResponse
    {
        public decimal RepairExpenditureLac { get; set; }
        public decimal ReboreExpenditureLac { get; set; }
        public decimal TotalExpenditureLac { get; set; }

        public decimal AvgRepairCostPerHp { get; set; }
        public decimal AvgReboreCostPerHp { get; set; }
    }
    public class ExpenditureDashboardFilterResponseModel
    {
        /* ---------- Last Month ---------- */
        public decimal LmRepair { get; set; }
        public decimal LmRebore { get; set; }
        public decimal LmTotal { get; set; }

        /* ---------- Last Financial Year ---------- */
        public decimal LfRepair { get; set; }
        public decimal LfRebore { get; set; }
        public decimal LfTotal { get; set; }

        /* ---------- Current Financial Year ---------- */
        public decimal CfRepair { get; set; }
        public decimal CfRebore { get; set; }
        public decimal CfTotal { get; set; }

        /* ---------- Average Cost per Handpump ---------- */
        public decimal AvgRepairHp { get; set; }
        public decimal AvgReboreHp { get; set; }
    }

}
