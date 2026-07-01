using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class GramPanchayatListResModel
    {
        public int Id { get; set; }
        public int BlockId { get; set; }
        public string? GramPanchayatName { get; set; }
        public string? Code { get; set; }
    }
}
