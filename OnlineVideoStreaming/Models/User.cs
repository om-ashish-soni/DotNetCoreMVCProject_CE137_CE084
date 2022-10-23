using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVideoStreaming.Models
{
    public class User



    {  
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string ChannelName { get; set; }
        /// <summary>
        /// /
        /// </summary>

        public int Subscriber { get; set; }

        public int Views { get; set; }

        public DateTime JoinDate { get; set; }


    }
}
