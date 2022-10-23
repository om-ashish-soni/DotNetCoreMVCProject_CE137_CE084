using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVideoStreaming.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string VideoName { get; set; }

        public string Description { get; set; }

        public int Likes { get; set; }

        public DateTime Date { get; set; }

        public int Views { get; set; }

        /*public string ChannelName { get; set; }*/

        [Required]
        public string Source { get; set; }

        public int UserId { get; set; }
        public User Channel { get; set; }
    }
}
