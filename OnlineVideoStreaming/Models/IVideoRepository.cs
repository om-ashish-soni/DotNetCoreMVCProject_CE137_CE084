using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVideoStreaming.Models
{
    public class IVideoRepository
    {
        Video GetVideo(int Id);
        IEnumerable<Video> GetAllVideos();
        Video Add(Video Video);
        Video Update(Video VideoChanges);
        Video Delete(int Id);
    }
}
