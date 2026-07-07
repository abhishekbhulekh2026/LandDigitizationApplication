using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class RecordPageListResModel
    {
        public long PageId { get; set; }

        public long VolumeId { get; set; }

        public int PageNumber { get; set; }

        public string ImageFileName { get; set; }

        public bool IsMissing { get; set; }

        public bool IsDamaged { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FileHash { get; set; }
    }
}
