using System;
using System.Web.Mvc;
using ShortUrl.Models;

namespace ShortUrl.Controllers
{
    public class ShortenController : Controller
    {
        public ShortenController(IRepository<ShortenedUrl> repository)
        {
            
        }

        [HttpPost]
        public ActionResult Index(string url)
        {
            var shortUrl = new Models.ShortenedUrl
            {
                Id = 1,
                Created = DateTime.UtcNow,
                Url = url
            };

            return Content($"Shortening '{shortUrl.Url}' to {shortUrl.Id}");
        }

        [HttpPost]
        public ActionResult Debug(string url)
        {
            var shortUrl = new Models.ShortenedUrl
            {
                Id = 1,
                Created = DateTime.UtcNow,
                Url = url
            };

            return Content($"Shortening '{shortUrl.Url}' to {shortUrl.Id}");
        }
    }
}