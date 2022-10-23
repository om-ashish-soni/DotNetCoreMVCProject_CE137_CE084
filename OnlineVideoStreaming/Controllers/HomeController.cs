using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineVideoStreaming.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVideoStreaming.Controllers
{
    public class HomeController : Controller
    {
        private IHttpContextAccessor _accessor;
        /* private readonly ILogger<HomeController> _logger;*/
        private readonly IUserRepository _userRepo;
        private readonly IVideoEntityRepository _videoRepo;
        public HomeController(IUserRepository userRepo,IHttpContextAccessor accessor,IVideoEntityRepository videoRepo)
        {
            _userRepo = userRepo;
            _accessor = accessor;
            _videoRepo = videoRepo;
        }

        public IActionResult Index()
        {
            //string filename = "Ed Sheeran - Perfect (Official Music Video).mp4";
            
            var ChannelId = HttpContext.Request.Cookies["ChannelId"];
            var Email = HttpContext.Request.Cookies["Email"];
            if(ChannelId != null && Email != null)
            {
                return RenderHome();
            }
            
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult RenderHome()
        {
            
            List<Video> videoList= _videoRepo.GetAllVideos().ToList();
            ViewData["VideoList"] = videoList;
            return View("Home");
        }
        public IActionResult SubmitLogin(User user)
        {
            User existingUser = null;
            if (ModelState.IsValid)
            {
                existingUser = _userRepo.GetUser(user.Email, user.Password);
                if (existingUser == null)
                {
                    ViewData["Error"] = "email or password is incorrect, please enter wisely";
                    return View("Login");
                }
                
                
            }
            //ViewData["User"] = existingUser;
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10);

            //Create a Cookie with a suitable Key and add the Cookie to Browser.
            Response.Cookies.Append("ChannelId", existingUser.Id.ToString(), option);
            Response.Cookies.Append("Email", existingUser.Email.ToString(), option);


            /*
            CookieOptions cookieOptions=new CookieOptions { MaxAge = TimeSpan.FromDays(10) , SameSite = SameSiteMode.Lax };
            HttpContext.Response.Cookies.Append("ChannelId", existingUser.Id.ToString(),cookieOptions);
            HttpContext.Response.Cookies.Append("Email", existingUser.Email,cookieOptions);
            */
            return RenderHome();
        }

        public IActionResult Signin()
        {
            //ViewData["Error"] = "is this visible?";
            return View();
        }

        [HttpPost]
        public IActionResult SubmitSignin(User user)
        {
            if (ModelState.IsValid)
            {
                User existingUser = _userRepo.GetExistingUser(user.Email);
                if(existingUser != null)
                {
                    ViewData["Error"] = "User with given email already registered";
                    return View("Signin");
                }
                else
                {
                    User newUser = _userRepo.Add(user);
                    //ViewData["User"] = newUser;
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(10);

                    //Create a Cookie with a suitable Key and add the Cookie to Browser.
                    Response.Cookies.Append("ChannelId", newUser.Id.ToString(), option);
                    Response.Cookies.Append("Email", newUser.Email.ToString(), option);

                }
            }
            return RenderHome();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult CreateVideo()
        {
            var ChannelId = HttpContext.Request.Cookies["ChannelId"];
            ViewData["ChannelId"] = ChannelId;
            return View();
        }
        [HttpPost]
        async public Task<IActionResult> SubmitVideo(VideoMeta videoMeta) 
        {

            var ChannelIdStr = HttpContext.Request.Cookies["ChannelId"];
            if(ChannelIdStr == null)
            {
                return View("Login");
            }

            int ChannelId = Convert.ToInt32(ChannelIdStr);
            Video video = new Video();
            video.UserId = ChannelId;
            video.VideoName = videoMeta.VideoName;
            video.Description = videoMeta.Description;

            
            string format = "Mddyyyyhhmmsstt";
            string filename = ChannelId.ToString() + "_" + video.VideoName + "_" + DateTime.Now.ToString(format) + ".mp4";
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            filepath = Path.Combine(filepath, filename);

            ViewData["filepath"] = filepath;
            ViewData["filename"] = filename;
            // code to upload
            List<Microsoft.AspNetCore.Http.IFormFile> fileList = videoMeta.files;
            foreach(var file in fileList)
            {
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                break;
            }

            video.Source = filename;//add here
                                    // done upload

            Video newVideo = _videoRepo.Add(video);
            ViewData["Video"] = newVideo;
            return View("ShowVideo");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
