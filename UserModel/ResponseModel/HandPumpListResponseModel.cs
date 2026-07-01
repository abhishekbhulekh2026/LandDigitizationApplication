using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class HandPumpListResponseModel
    {
        public int H_id { get; set; }
        public string? HandpumpId { get; set; }
        public string? HandpumpImage { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? VillegeId { get; set; }
        public string? NearByPersonName { get; set; }
        public string? NearByPersonContact { get; set; }
        public int? SoakpitConnected { get; set; }
        public int? DrainageConnected { get; set; }
        public int? PlateformBuild { get; set; }
        public string? HandpumpStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByID { get; set; }
        public string? CreatedBy { get; set; }
        public int? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public int? BlockId { get; set; }
        public string? BlockName { get; set; }
        public int? GPId { get; set; }
        public string? GrampanchayatName { get; set; }
        public string? VillegeName { get; set; }


          public string? HandpumpVideoPath { get; set; }
        public DateTime? LastRepairDate { get; set; }
        public DateTime? LastReboreDate { get; set; }
          public string? WaterQuality { get; set; }
          public string? WaterQualityRemarks { get; set; }
    }
}
