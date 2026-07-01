using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    using System.ComponentModel.DataAnnotations;

    public class HandpumpVisitMonitoringReqModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "RequisitionId is required.")]
        public int RequisitionId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "HandpumpId is required.")]
        public long HandpumpId { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z\s.]+$", ErrorMessage = "RecordingPerson contains invalid characters.")]
        public string? RecordingPerson { get; set; }

        public DateTime? VisitDate { get; set; }

        [RegularExpression("^(good|fair|bad)$", ErrorMessage = "WorkAbility must be good, fair or bad.")]
        public string? WorkAbility { get; set; }

        [StringLength(500)]
        public string? WorkAbilityRemarks { get; set; }

        [RegularExpression("^(good|fair|bad)$")]
        public string? PlatformCondition { get; set; }

        [StringLength(500)]
        public string? PlatformRemarks { get; set; }

        [RegularExpression("^(Firm|Loose|No Pedestal)$")]
        public string? PedestalGrouting { get; set; }

        [StringLength(500)]
        public string? PedestalRemarks { get; set; }

        [Range(0, 100)]
        public int? Strokes12LBucket { get; set; }

        [StringLength(500)]
        public string? StrokesRemarks { get; set; }

        [Range(0, 100)]
        public int? NoOfBreakdowns { get; set; }

        [StringLength(500)]
        public string? BreakdownsRemarks { get; set; }

        [RegularExpression("^(none|slight|high)$")]
        public string? RustingHandle { get; set; }

        [StringLength(500)]
        public string? RustingHandleRemarks { get; set; }

        public List<string>? PoorPerformanceReason { get; set; }

        [StringLength(500)]
        public string? PoorPerformanceRemarks { get; set; }

        [RegularExpression("^(none|slight|high)$")]
        public string? RustingPumpStand { get; set; }

        [StringLength(500)]
        public string? RustingPumpStandRemarks { get; set; }

        [RegularExpression("^(none|slight|high)$")]
        public string? RustingPlunger { get; set; }

        [StringLength(500)]
        public string? RustingPlungerRemarks { get; set; }

        [RegularExpression("^(good|fair|bad)$")]
        public string? CheckValveCondition { get; set; }

        [StringLength(500)]
        public string? CheckValveRemarks { get; set; }

        [RegularExpression("^(none|slight|bad)$")]
        public string? CylinderLinerDamage { get; set; }

        [StringLength(500)]
        public string? CylinderLinerRemarks { get; set; }

        [RegularExpression("^(none|slight|bad)$")]
        public string? BearingDamage { get; set; }

        [StringLength(500)]
        public string? BearingDamageRemarks { get; set; }

        [RegularExpression("^(none|slight|bad)$")]
        public string? RisingMainPumprodsDamage { get; set; }

        [StringLength(500)]
        public string? RisingMainPumprodsRemarks { get; set; }

        [RegularExpression("^(none|slight|bad)$")]
        public string? RisingMainCentralisersDamage { get; set; }

        [StringLength(500)]
        public string? RisingMainCentralisersRemarks { get; set; }

        public List<string>? SealingPartsDamage { get; set; }

        [StringLength(500)]
        public string? SealingPartsRemarks { get; set; }

        [RegularExpression("^(yes|no)$")]
        public string? PreventiveMaintenanceDone { get; set; }

        [StringLength(500)]
        public string? PreventiveMaintenanceRemarks { get; set; }

        [RegularExpression("^(yes|no)$")]
        public string? TechAssistanceAvailable { get; set; }

        [StringLength(500)]
        public string? TechAssistanceRemarks { get; set; }

        [RegularExpression("^(yes|no)$")]
        public string? MaintenanceSatisfying { get; set; }

        [StringLength(500)]
        public string? MaintenanceRemarks { get; set; }

        [StringLength(1000)]
        public string? AdditionalComments { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Required]
        [RegularExpression("^(Pending|Completed|Rejected)$", ErrorMessage = "Invalid Overall Status.")]
        public string Overall_Status { get; set; }
    }

    public class HandpumpVisitMonitoringResModel
    {
        public int Id { get; set; }
        public int RequisitionId { get; set; }
        public int UserId { get; set; }
        public long HandpumpId { get; set; }
        public string RecordingPerson { get; set; }
        public DateTime? VisitDate { get; set; }

        public string WorkAbility { get; set; }
        public string WorkAbilityRemarks { get; set; }

        public string PlatformCondition { get; set; }
        public string PlatformRemarks { get; set; }

        public string PedestalGrouting { get; set; }
        public string PedestalRemarks { get; set; }

        public int? Strokes12lBucket { get; set; }
        public string StrokesRemarks { get; set; }

        public int? NoOfBreakdowns { get; set; }
        public string BreakdownsRemarks { get; set; }

        public string RustingHandle { get; set; }
        public string RustingHandleRemarks { get; set; }

        public string PoorPerformanceReason { get; set; }  // Stored as CSV in DB (convert to List<string> if needed)
        public string PoorPerformanceRemarks { get; set; }

        public string RustingPumpStand { get; set; }
        public string RustingPumpStandRemarks { get; set; }

        public string RustingPlunger { get; set; }
        public string RustingPlungerRemarks { get; set; }

        public string CheckValveCondition { get; set; }
        public string CheckValveRemarks { get; set; }

        public string CylinderLinerDamage { get; set; }
        public string CylinderLinerRemarks { get; set; }

        public string BearingDamage { get; set; }
        public string BearingDamageRemarks { get; set; }

        public string RisingMainPumprodsDamage { get; set; }
        public string RisingMainPumprodsRemarks { get; set; }

        public string RisingMainCentralisersDamage { get; set; }
        public string RisingMainCentralisersRemarks { get; set; }

        public string SealingPartsDamage { get; set; }  // CSV string
        public string SealingPartsRemarks { get; set; }

        public string PreventiveMaintenanceDone { get; set; }
        public string PreventiveMaintenanceRemarks { get; set; }

        public string TechAssistanceAvailable { get; set; }
        public string TechAssistanceRemarks { get; set; }

        public string MaintenanceSatisfying { get; set; }
        public string MaintenanceRemarks { get; set; }

        public string AdditionalComments { get; set; }
        public string Overall_Status { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}
