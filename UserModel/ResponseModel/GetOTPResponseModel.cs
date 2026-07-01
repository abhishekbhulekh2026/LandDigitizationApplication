using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public  class GetOTPResponseModel
    {
        public string OTP { get; set; }
        public string MobileNo { get; set; }
        public string responseMessage { get; set; }
    }
}
