using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public  class BulkUpdateMbItemsRemarkReqModel
    {
        public int RequisitionId { get; set; }     // p_requisition_id
        public int UserId { get; set; }            // p_user_id
        public int ConsultiveEngId { get; set; }   // p_consultive_eng_id

        // List of items to update dynamically
        public List<ItemRemarkUpdate> Items { get; set; } = new List<ItemRemarkUpdate>();
    }

    public class ItemRemarkUpdate
    {
        public int ItemId { get; set; }            // p_item_id
        public string Remark { get; set; }         // p_remark
    }
}
