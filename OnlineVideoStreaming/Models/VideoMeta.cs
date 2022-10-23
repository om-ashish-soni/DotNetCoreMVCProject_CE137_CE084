using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVideoStreaming.Models
{
    public class VideoMeta
    {

        public List<Microsoft.AspNetCore.Http.IFormFile> files { get; set; }


        public string VideoName{get;set;}
        public string Description{get;set;}

        public int ChannelId { get; set; }

    }
}
