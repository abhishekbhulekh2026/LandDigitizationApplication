using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class RequisitionListResponseModel
    {
        public int? RequisitionId { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public int? HPId { get; set; }
        public string? HandpumpId { get; set; }
        public int? VillageId { get; set; }

        public string? DistrictName { get; set; }
        public string? BlockName { get; set; }

        public string? VillageName { get; set; }
        public string? GrampanchayatName { get; set; }
        public int? RequisitionTypeId { get; set; }
        public string? RequisitionType { get; set; }
        public int? RequisitionRepairTypeId { get; set; }
        public string? RequisitionRepairType { get; set; }
        public DateTime? RequisitionDate { get; set; }
        public string? RequisitionDesc { get; set; }
        public int? RequisitionStatus { get; set; }
        public int? DPROId { get; set; }
        public string? DPRORemark { get; set; }
        public int? DPROStatus { get; set; }
        public string? DPROUpdatedDateStr { get; set; }

        public int? GPSachivId { get; set; }
        public string? GPSachivRemark { get; set; }
        public int? GPSachivStatus { get; set; }
        public string? GPSachivUpdatedDateStr { get; set; }


        public int? CEId { get; set; }
        public string? CERemark { get; set; }
        public int? CEStatus { get; set; }
        public string? CEUpdatedDateStr { get; set; }

        public DateTime? CreatedDate { get; set; }
        public int? OrderId { get; set; }
        public string? SanctionDateStr { get; set; }
        public decimal? SanctionAmount { get; set; }

        public decimal? TotalMBAmount { get; set; }
        public string? CompletionDateStr { get; set; }
        public string? Description { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? GrandTotal { get; set; }

        public int? VisitMonitoringId { get; set; }
        public string? HandpumpImage { get; set; }
    }

    public class RequisitionCompletionListResponseModel
    {
        public int? RequisitionId { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public int? HPId { get; set; }
        public string? HandpumpId { get; set; }
        public int? VillageId { get; set; }
        public string? VillageName { get; set; }
        public string? GrampanchayatName { get; set; }
        public int? RequisitionTypeId { get; set; }
        public string? RequisitionType { get; set; }
        public int? RequisitionRepairTypeId { get; set; }
        public string? RequisitionRepairType { get; set; }
        public DateTime? RequisitionDate { get; set; }
        public string? RequisitionDesc { get; set; }
        public int? RequisitionStatus { get; set; }
        public int? DPROId { get; set; }
        public string? DPRORemark { get; set; }
        public int? DPROStatus { get; set; }
        public string? DPROUpdatedDateStr { get; set; }

        public int? GPSachivId { get; set; }
        public string? GPSachivRemark { get; set; }
        public int? GPSachivStatus { get; set; }
        public string? GPSachivUpdatedDateStr { get; set; }


        public int? CEId { get; set; }
        public string? CERemark { get; set; }
        public int? CEStatus { get; set; }
        public string? CEUpdatedDateStr { get; set; }

        public DateTime? CreatedDate { get; set; }
        public int? OrderId { get; set; }
        public string? SanctionDateStr { get; set; }
        public decimal? SanctionAmount { get; set; }

        public decimal? TotalMBAmount { get; set; }
        public string? CompletionDateStr { get; set; }
        public string? Description { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? GrandTotal { get; set; }


        public string? HandpumpImage { get; set; }
    }
}
