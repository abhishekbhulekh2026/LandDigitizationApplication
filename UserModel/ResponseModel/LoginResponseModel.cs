using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.ResponseModel
{
    public  class LoginResponseModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }   
        public string? UserRole { get; set; }
        public string? Status { get; set; }
        public string? LoginMessage { get; set; }

        public string? ResponseCode { get; set; }
    }
}
