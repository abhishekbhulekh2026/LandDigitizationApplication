using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class UserUpdateProfileRequestModel
    {
        public int UserId { get; set; }
        public string? FileBase64String { get; set; }
        public string? ProfilePhotoPath { get; set; }
    }
}
