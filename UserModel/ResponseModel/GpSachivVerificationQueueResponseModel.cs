using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class GpSachivVerificationQueueResponseModel
    {
        public long HId { get; set; }
        public string HandpumpId { get; set; }
        public string VillageName { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public bool IsFunctional { get; set; }
        public string WaterQuality { get; set; }
        public string VerificationStatus { get; set; }
        public string PhotoUrl { get; set; }
        public string VideoUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string NearbyPersonName { get; set; }
        public string NearbyPersonContact { get; set; }
        public bool IsPlatformBuilt { get; set; }
        public bool IsDrainageConnected { get; set; }
        public bool IsSoakpitConnected { get; set; }
        public string WaterQualityRemark { get; set; }
        public DateTime? LastRepairDate { get; set; }

        public DateTime? LastReboreDate { get; set; }

        public int? CreatedBy { get; set; }

        public string SachivStatus { get; set; }

        public DateTime? SachivActionDate { get; set; }

        public string SachivRemark { get; set; }

        public int? SachivApprovedBy { get; set; }

        public string SachivName { get; set; }

        public bool IsAutoVerified { get; set; }
    }
}
