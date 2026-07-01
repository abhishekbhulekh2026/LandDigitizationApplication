using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public class GeoLocationDistanceResponseModel
    {
        public bool Exists { get; set; }
        public double? Distance { get; set; }
    }
}
