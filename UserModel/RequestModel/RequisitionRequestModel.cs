using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class RequisitionRequestModel
    {
        public int? RequisitionId { get; set; }
        public int? UserId { get; set; }
        public int? HandpumpId { get; set; }
        public int? VillageId { get; set; }
        public int? RequisitionType { get; set; }
        public int? RequisitionRepairType { get; set; }
        public DateTime? RequisitionDate { get; set; }
        public string? RequisitionDescription { get; set; }
        public bool? RequisitionStatus { get; set; }
        public int? UpdatedBy { get; set; }
      
        public string? ImageBase64String { get; set; }
        public string? HandpumpPhotoPath { get; set; }
    }

    //public class OrderDetails
    //{
    //    public int? Id { get; set; }
    //    public string? OrderDescription { get; set; }
    //    public decimal? SubTotal { get; set; }
    //    public decimal? GrandTotal { get; set; }

    //}

    //public class ItemDetails
    //{
    //    public int? Id { get; set; }
    //    public int? ItemId { get; set; }
    //    public int? Quantity { get; set; }
    //    public decimal? Amount { get; set; }

    //}

}
