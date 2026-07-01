using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class RepairEstimationRequestModel
    {
        
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public int? Quantity { get; set; }
        [Range(0, 999999999, ErrorMessage = "Rate cannot be negative.")]
        public decimal? Rate { get; set; }

        [Range(0, 999999999, ErrorMessage = "Amount cannot be negative.")]
        public decimal? Amount { get; set; }
        public int? UpdatedBy { get; set; }
        public string Source { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
    }

    public class ReboreEstimationRequestModel
    {

        public string ItemName { get; set; }
        public string Unit { get; set; }
        public int? Quantity { get; set; }
        [Range(0, 999999999, ErrorMessage = "Rate cannot be negative.")]
        public decimal? Rate { get; set; }

        [Range(0, 999999999, ErrorMessage = "Amount cannot be negative.")]
        public decimal? Amount { get; set; }
        public int? UpdatedBy { get; set; }
        public string Source { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
    }
}
