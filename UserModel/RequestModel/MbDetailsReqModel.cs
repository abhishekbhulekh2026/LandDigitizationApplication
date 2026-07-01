using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class MbDetailsReqModel
    {
        public int RequisitionId { get; set; }
        public int Handpump_Id { get; set; }
        public decimal? SanctionAmount { get; set; }
        public decimal? TotalMaterialCost { get; set; }
        public decimal? TotalLabourCost { get; set; }
        public decimal? DailyWageRate { get; set; }
        public int UpdatedBy { get; set; }
        public string MaterialBillfileBase64String { get; set; }
        public string MaterialBillfilePath { get; set; }
    }

    public class MbDetailsResModel
    {
        public int MbId { get; set; }
        public string RequisitionId { get; set; }
        public string HandpumpId { get; set; }
        public string Mode { get; set; }   // requisition_type
        public int VillageId { get; set; }
        public decimal? SanctionAmount { get; set; }
        public string MaterialImgFile { get; set; }
        public decimal? TotalMaterialCost { get; set; }
        public decimal? TotalLabourCost { get; set; }
        public decimal? DailyWageRate { get; set; }
        public int? NoOfMandays { get; set; }
        public decimal TotalProjectCost { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string Status { get; set; }
    }

}
