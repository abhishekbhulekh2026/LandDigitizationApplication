using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class VillegeListResModel
    {
        public int Id { get; set; }
        public int GramPanchayatId { get; set; }
        public string? VillageName { get; set; }
        public string? Code { get; set; }
    }
}
