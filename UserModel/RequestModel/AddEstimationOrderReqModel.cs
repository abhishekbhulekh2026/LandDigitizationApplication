using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{

    using System.ComponentModel.DataAnnotations;

    public class AddEstimationOrderReqModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Valid UserId is required.")]
        public int UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Valid RequisitionId is required.")]
        public int RequisitionId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Valid GstId is required.")]
        public int GstId { get; set; }

        [Required(ErrorMessage = "Order description is required.")]
        [MaxLength(500)]
        public string OrderDesc { get; set; }

        [Range(0, 999999999, ErrorMessage = "SubTotal cannot be negative.")]
        public decimal SubTotal { get; set; }

        [Range(0, 999999999, ErrorMessage = "Estimation Consulting Fee cannot be negative.")]
        public decimal EstimationConsultingFee { get; set; }

        [Range(0, 999999999, ErrorMessage = "MB Consulting Fee cannot be negative.")]
        public decimal MbConsultingFee { get; set; }

        [Range(0, 999999999, ErrorMessage = "GST Fee cannot be negative.")]
        public decimal GstFee { get; set; }

        [Range(0, 999999999, ErrorMessage = "Grand Total cannot be negative.")]
        public decimal GrandTotal { get; set; }

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Range(1, int.MaxValue, ErrorMessage = "Valid UpdatedBy is required.")]
        public int UpdatedBy { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one item is required.")]
        public List<ItemDetailModel> Items { get; set; } = new();
    }
    public class ItemDetailModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Valid ItemId is required.")]
        public int ItemId { get; set; }

        [Range(0.01, 999999999, ErrorMessage = "Quantity must be greater than zero.")]
        public decimal Quantity { get; set; }

        [Range(0, 999999999, ErrorMessage = "Rate cannot be negative.")]
        public decimal Rate { get; set; }

        [Range(0, 999999999, ErrorMessage = "Amount cannot be negative.")]
        public decimal Amount { get; set; }
    }
}
