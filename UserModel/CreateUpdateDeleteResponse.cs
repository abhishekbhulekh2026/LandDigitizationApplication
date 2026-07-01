using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel
{
    public class CreateUpdateDeleteResponse
    {
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public string Errror { get; set; }
    }
}
