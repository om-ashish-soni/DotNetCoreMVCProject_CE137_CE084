using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVideoStreaming.Models
{
  
    public class SQLVideoRepository : IVideoEntityRepository
    {
        private readonly AppDbContext context;
        public SQLVideoRepository(AppDbContext context)
        {
            this.context = context;
        }
        Video IVideoEntityRepository.Add(Video Video)
        {
            context.Videos.Add(Video);
            context.SaveChanges();
            return Video;
        }
        Video IVideoEntityRepository.Delete(int Id)
        {
            Video Video = context.Videos.Find(Id);
            if (Video != null)
            {
                context.Videos.Remove(Video);
                context.SaveChanges();
            }
            return Video;
        }

        IEnumerable<Video> IVideoEntityRepository.GetAllVideos()
        {
            return context.Videos;
        }

        IEnumerable<Video> IVideoEntityRepository.GetAllVideosByPattern(string Pattern)
        {
            return context.Videos.Where(m=>m.VideoName.ToLower().Contains(Pattern.ToLower()) || Pattern.ToLower().Contains(m.VideoName));
        }

        Video IVideoEntityRepository.GetVideo(int id)
        {
            return context.Videos.FirstOrDefault(m => m.Id == id);
        }

        Video IVideoEntityRepository.Update(Video VideoChanges)
        {
            var Video = context.Videos.Attach(VideoChanges);
            Video.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return VideoChanges;
        }
    }
}
