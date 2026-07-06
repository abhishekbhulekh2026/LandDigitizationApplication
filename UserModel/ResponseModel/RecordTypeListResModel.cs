using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class RecordTypeListResModel
    {
        public int RecordTypeId { get; set; }

        public string? RecordNameEn { get; set; }

        public int? VolumeNumber { get; set; }

        public int? YearFrom { get; set; }

        public string? Language { get; set; }

        public string? YearTypeEn { get; set; }

        public string? DisplayName { get; set; }
    }
}
