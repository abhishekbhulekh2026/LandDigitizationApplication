using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class HandpumpRegistrationRequestModel
    {
        public int? Id { get; set; }
        public string? HandpumpId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? VillegeId { get; set; }
        public string? NearbyPersonName { get; set; }
        public string? NearbyPersonContact { get; set; }
        public bool? SoakpitConnected { get; set; }
        public bool? DrainageConnected { get; set; }
        public bool? PlatformBuilt { get; set; }
        public string? HandpumpStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string? FileBase64String { get; set; }
        public string? HandpumpPhotoPath { get; set; }

        public string? VideoFileBase64String { get; set; }
        public string? HandpumpVideoPath { get; set; }

        public DateTime? LastRepairDate { get; set; }
        public DateTime? LastReboreDate { get; set; }
        public string? WaterQuality { get; set; }
        public string? WaterQualityRemarks { get; set; }
        
    }
}
