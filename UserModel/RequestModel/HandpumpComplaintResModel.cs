using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{

    public class HandpumpComplaintReqModel
    {
        [Required]
        [Range(1, long.MaxValue)]
        public long HandpumpId { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long DistrictId { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long BlockId { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long GpId { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long VillageId { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z\s.]+$", ErrorMessage = "Invalid complainant name.")]
        public string ComplainantName { get; set; }

        [Required]
        [RegularExpression(@"^[6-9][0-9]{9}$", ErrorMessage = "Invalid mobile number.")]
        public string ContactNumber { get; set; }

        [StringLength(250)]
        public string Landmark { get; set; }

        [Required]
        [StringLength(100)]
        public string IssueCategory { get; set; }

        [Required]
        [RegularExpression("^(Low|Medium|High|Critical)$", ErrorMessage = "Invalid urgency level.")]
        public string UrgencyLevel { get; set; }

        [Range(0, 365)]
        public int? ResolutionTimelineDays { get; set; }

        [Required]
        [StringLength(1000)]
        public string IssueDescription { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CreatedBy { get; set; }
    }
    public class HandpumpComplaintResModel
    {
        public int ComplaintId { get; set; }
        public long H_Id { get; set; }
        public string? HandpumpId { get; set; }
        public string? DistrictName { get; set; }
        public string? BlockName { get; set; }
        public string? GpName { get; set; }
        public string? VillageName { get; set; }
        public string ComplainantName { get; set; }
        public string ContactNumber { get; set; }
        public string Landmark { get; set; }
        public string IssueCategory { get; set; }
        public string UrgencyLevel { get; set; }      // e.g. Low, Medium, High, Critical
        public int? ResolutionTimelineDays { get; set; }
        public string IssueDescription { get; set; }
        public string Status { get; set; }            // e.g. Open, In-Progress, Resolved, Closed
        public int? CreatedBy { get; set; }
        public string? CreateddateStr { get; set; }
    }

    public class HandpumpComplaintSummaryModel
    {
        public int ComplaintID { get; set; }
        public string ComplaintStatus { get; set; }
        public string Urgency { get; set; }
        public int? ResolutionTimeline { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string? CreatedDateStr => CreatedDate?.ToString("dd-MM-yyyy");
        public DateTime? UpdatedDate { get; set; }

        // Handpump Info
        public string HandpumpCode { get; set; }
        public string HandpumpLocation { get; set; }
        public string HandpumpStatus { get; set; }
        public string Village { get; set; }
        public string GramPanchayat { get; set; }
        public string Block { get; set; }
        public string District { get; set; }
        public string Landmark { get; set; }

        // Complainant Info
        public string ComplainantName { get; set; }
        public string ContactNumber { get; set; }
        public string ReportedBy { get; set; }

        // Issue Details
        public string IssueCategory { get; set; }
        public string IssueDescription { get; set; }
    }

}
