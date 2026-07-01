using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel
{
    public class JWTTokenResponse
    {
        public string? UserID { get; set; }
        public string? Token { get; set; }
        public string? ResponseMessage { get; set; }
    }
}
