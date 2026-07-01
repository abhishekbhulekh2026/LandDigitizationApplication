using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class FinanceDashboardStatsResponse
    {
        public decimal TotalExpenditure { get; set; }

        public decimal RepairExpenditure { get; set; }

        public decimal ReboreExpenditure { get; set; }

        public int DistrictsCovered { get; set; }

        public int DistrictsNotStarted { get; set; }
    }

    public class FinanceYearlyExpenditureGraphResponse
    {
        public string FinancialYear { get; set; }

        public decimal RepairExpenditure { get; set; }

        public decimal ReboreExpenditure { get; set; }

        public decimal TotalExpenditure { get; set; }
    }

    public class FinanceMonthlyExpenditureGraphResponse
    {
        public string MonthName { get; set; }

        public int MonthNo { get; set; }

        public decimal RepairExpenditure { get; set; }

        public decimal ReboreExpenditure { get; set; }

        public decimal TotalExpenditure { get; set; }
    }
    public class DistrictWiseExpenditureResponseModel
    {
        public int DistrictId { get; set; }

        public string DistrictName { get; set; }

        public decimal RepairExpenditure { get; set; }

        public decimal ReboreExpenditure { get; set; }

        public decimal TotalExpenditure { get; set; }
    }
}
