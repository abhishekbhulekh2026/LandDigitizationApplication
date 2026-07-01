using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class UploadEstimatePDFReqModel
    {
        public int? Id { get; set; }
        public string? PDfFileBase64String { get; set; }
        public string? PDfEstimatePath { get; set; }
        public string? HandpumpID { get; set; }

        public int UserId { get; set; }
    }
}
