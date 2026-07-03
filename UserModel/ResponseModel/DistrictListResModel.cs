using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class DistrictListResModel
    {
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        public string? DistrictCode { get; set; }
        public string? NameEn { get; set; }
        public string? NameHi { get; set; }
    }

    public class TehsilListResModel
    {
        public int TehsilId { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string TehsilCode { get; set; }
        public string NameEn { get; set; }
        public string NameHi { get; set; }
    }

    public class ParganaListResModel
    {
        public int ParganaId { get; set; }
        public string ParganaCode { get; set; }
        public string NameEn { get; set; }
        public string NameHi { get; set; }
    }

    public class VillageListResModel
    {
        public int VillageId { get; set; }

        public string VillageCode { get; set; }

        public int TehsilParganaMapId { get; set; }

        public int TehsilId { get; set; }

        public int ParganaId { get; set; }

        public string TehsilName { get; set; }

        public string ParganaName { get; set; }

        public string TehsilParganaEn { get; set; }

        public string TehsilParganaHi { get; set; }

        public string NameEn { get; set; }

        public string NameHi { get; set; }
    }

}
