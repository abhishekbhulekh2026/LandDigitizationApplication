using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class DistrictRankingResponseModel
    {
        public List<TopDistrictModel> TopDistricts { get; set; }
        public List<BottomDistrictModel> BottomDistricts { get; set; }
        public List<NotStartedDistrictModel> NotStartedDistricts { get; set; }
    }

    public class TopDistrictModel
    {
        public int RankNo { get; set; }
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public decimal TotalExpenditure { get; set; }
    }

    public class BottomDistrictModel
    {
        public int RankNo { get; set; }
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public decimal TotalExpenditure { get; set; }
    }

    public class NotStartedDistrictModel
    {
        public int Sno { get; set; }
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string Status { get; set; }
    }

    public class FinanceMonthWiseData
    {
        public string Month { get; set; }

        public int MonthNumber { get; set; }

        public decimal Repair { get; set; }

        public decimal Rebore { get; set; }

        public decimal Total { get; set; }
    }

    public class FinanceYearlyExpenditureResponse
    {
        public string FinancialYear { get; set; }

        public decimal Repair { get; set; }

        public decimal Rebore { get; set; }

        public decimal Total { get; set; }
    }

    public class DistrictMonthWiseExpenditureResponse
    {
        public string Month { get; set; }
        public int MonthNumber { get; set; }
        public decimal Repair { get; set; }
        public decimal Rebore { get; set; }
        public decimal Total { get; set; }
    }

    public class DistrictMonthWiseExpenditureApiResponse
    {
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string FinancialYear { get; set; }
        public List<DistrictMonthWiseExpenditureResponse> ResData { get; set; }
    }
}
