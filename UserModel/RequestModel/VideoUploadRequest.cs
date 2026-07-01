using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class VideoUploadRequest
    {
        public string FileName { get; set; }   // e.g. "video.mp4"
        public string Base64Video { get; set; } // base64 encoded video string
    }
}
