using ImageSharingLikes.Data;
using ImageSharingLikes.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace ImageSharingLikes.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;
        private IWebHostEnvironment _webHostEnvironment;

        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var repo = new ImageSharingRepo(_connectionString);
            var ivm = new IndexViewModel
            {
                images = repo.GetAll()
            };
            return View(ivm);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile image, string title)
        {
            var fileName = $"{Guid.NewGuid()}-{image.FileName}";
            var fullImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

            using FileStream fs = new FileStream(fullImagePath, FileMode.Create);
            image.CopyTo(fs);

            var repo = new ImageSharingRepo(_connectionString);
            var Image = new Image
            {
                Name = title,
                FilePath = fileName,
                Date = DateTime.Now,
            };
            repo.Add(Image);

            return Redirect("/");
        }

        public IActionResult ViewImage(int id)
        {
            var repo = new ImageSharingRepo(_connectionString);
            var image = repo.GetImageById(id);
            if (image == null)
            {
                return Redirect("/");
            }
            return View(image);
        }

        [HttpPost]
        public void Like(int id)
        {
            var repo = new ImageSharingRepo(_connectionString);
            var image = repo.GetImageById(id);
            if (image == null)
            {
                Redirect("/");
            }
            repo.LikeImage(image);
        }

        public int GetLikes(int id)
        {
            var repo = new ImageSharingRepo(_connectionString);
            var image = repo.GetImageById(id);
            return image.Likes;
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}
