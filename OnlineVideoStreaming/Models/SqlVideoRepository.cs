using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVideoStreaming.Models
{
    public class SqlVideoRepository
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
}
