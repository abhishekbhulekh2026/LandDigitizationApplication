using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class HandpumpListFilterReqModel
    {
        public int? DistrictId { get; set; }
        public int? BlockId { get; set; }
        public int? GPId { get; set; }
    }
}
